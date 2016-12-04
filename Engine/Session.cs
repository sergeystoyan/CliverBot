//********************************************************************************************
//Author: Sergey Stoyan
//        sergey.stoyan@gmail.com
//        sergey_stoyan@yahoo.com
//        http://www.cliversoft.com
//Copyright: (C) 2006-2013, Sergey Stoyan
//********************************************************************************************

using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Linq;
using System.Reflection;

/*
TBD:
- use LiteSQL or DBLite as a storage
- serialize InputItems into json and thus allow arrays etc
- ? Bot static session methods move to a session subclass singleton within CustomBot
*/

namespace Cliver.Bot
{
    public partial class Session
    {
        public static Session This
        {
            get
            {
                return This_;
            }
        }
        static Session This_;

        Session()
        {
            This_ = this;
            State = SessionState.STARTING;

            Log.Main.Inform("Loading configuration from " + Config.DefaultStorageDir);
            Config.Reload(Config.DefaultStorageDir);

            ConfigurationDir = Dir + "\\" + Config.CONFIG_FOLDER_NAME;

            Log.Writer.Exitig += (string message) =>
            {
                //CustomizationApi.FatalError();
                Close();
            };

            input_item_type_names2input_item_type = (from t in Assembly.GetEntryAssembly().GetTypes() where t.BaseType == typeof(InputItem) select t).ToDictionary(t => t.Name, t => t);
            Cliver.Bot.InputItem.Initialize(input_item_type_names2input_item_type.Values.ToList());
            work_item_type_name2work_item_types = (from t in Assembly.GetEntryAssembly().GetTypes() where (t.BaseType == typeof(WorkItem) && t.Name != typeof(SingleValueWorkItem<>).Name) || (t.BaseType != null && t.BaseType.Name == typeof(SingleValueWorkItem<>).Name) select t).ToDictionary(t => t.Name, t => t);
            Cliver.Bot.WorkItem.Initialize(work_item_type_name2work_item_types.Values.ToList());
            //tag_item_type_name2tag_item_types = (from t in Assembly.GetEntryAssembly().GetTypes() where (t.BaseType == typeof(TagItem) && t.Name != typeof(SingleValueTagItem<>).Name) || (t.BaseType != null && t.BaseType.Name == typeof(SingleValueTagItem<>).Name) select t).ToDictionary(t => t.Name, t => t);
            tag_item_type_name2tag_item_types = (from t in Assembly.GetEntryAssembly().GetTypes() where t.BaseType == typeof(TagItem) select t).ToDictionary(t => t.Name, t => t);
            Cliver.Bot.TagItem.Initialize(tag_item_type_name2tag_item_types.Values.ToList());
            if (input_item_type_names2input_item_type.Count < 1)
                throw new Exception("No InputItem derive was found");

            Restored = false;
            storage = new Storage();
            DateTime old_start_time;
            string old_time_mark;
            SessionState old_state = storage.ReadLastState(out old_start_time, out old_time_mark);
            switch (old_state)
            {
                case SessionState.NULL:
                    break;
                case SessionState.STARTING:
                case SessionState.COMPLETED:
                case SessionState.FATAL_ERROR:
                    break;
                case SessionState.RESTORING:
                case SessionState.RUNNING:
                case SessionState.CLOSING:
                case SessionState.UNCOMPLETED:
                case SessionState.BROKEN:
                    if (Settings.Engine.RestoreBrokenSession && !ProgramRoutines.IsParameterSet(CommandLineParameters.NOT_RESTORE_SESSION))
                    {
                        if (LogMessage.AskYesNo("Previous session " + old_time_mark + " is not completed. Restore it?", true))
                        {
                            StartTime = old_start_time;
                            TimeMark = old_time_mark;
                            storage.WriteState(SessionState.RESTORING, new { restoring_time = RestoreTime, restoring_session_time_mark = get_time_mark(RestoreTime) });
                            Log.Main.Inform("Loading configuration from " + ConfigurationDir);
                            Config.Reload(ConfigurationDir);
                            storage.RestoreSession();
                            Restored = true;
                        }
                    }
                    break;
                default:
                    throw new Exception("Unknown option: " + old_state);
            }

            if (!Restored)
            {
                if (old_state != SessionState.NULL)
                {
                    string old_dir_new_path = Log.WorkDir + "\\Data" + "_" + old_time_mark + "_" + old_state;
                    Log.Main.Write("The old session folder moved to " + old_dir_new_path);
                    storage.Close();
                    Directory.Move(Dir, old_dir_new_path);
                    PathRoutines.CreateDirectory(Dir);
                    storage = new Storage();
                }

                StartTime = Log.MainSession.CreatedTime;// DateTime.Now;
                TimeMark = get_time_mark(StartTime);
                storage.WriteState(SessionState.STARTING, new { session_start_time = StartTime, session_time_mark = TimeMark });
                read_input_file();
                Config.CopyFiles(Dir);
            }

            Creating?.Invoke();

            Bot.SessionCreating();
        }
        Dictionary<string, Type> input_item_type_names2input_item_type;
        Dictionary<string, Type> work_item_type_name2work_item_types;
        Dictionary<string, Type> tag_item_type_name2tag_item_types;

        internal readonly Counter ProcessorErrors = new Counter("processor_errors", Settings.Engine.MaxProcessorErrorNumber, max_error_count);
        static void max_error_count(int count)
        {
            Session.FatalErrorClose("Fatal error: errors in succession: " + count);
        }

        static string get_time_mark(DateTime dt)
        {
            return dt.ToString("yyMMddHHmmss");
        }

        public readonly string Dir = PathRoutines.CreateDirectory(Log.WorkDir + "\\Data");

        /// <summary>
        /// Time when the session was restored if it was.
        /// </summary>
        public readonly DateTime RestoreTime = DateTime.Now;

        /// <summary>
        /// Time when this session or rather restored session was started,.
        /// </summary>
        public readonly DateTime StartTime;//must be equal to RestoreTime if no restart
        public readonly string TimeMark;

        public readonly bool Restored;

        public readonly string ConfigurationDir;

        public static void Start()
        {
            try
            {
                Log.Initialize(Log.Mode.SESSIONS, Settings.Log.PreWorkDir, Settings.Log.WriteLog, Settings.Log.DeleteLogsOlderDays, Program.LogSessionPrefix);
                Log.Main.Inform("Version compiled: " + Program.GetCustomizationCompiledTime().ToString());
                Log.Main.Inform("Command line parameters: " + string.Join("|", Environment.GetCommandLineArgs()));

                if (This != null)
                    throw new Exception("Previous session was not closed.");
                new Session();
                if (This == null)
                    return;
                BotCycle.Start();
                Session.State = SessionState.RUNNING;
                This.storage.WriteState(SessionState.RUNNING, new { });
            }
            catch (ThreadAbortException)
            {
                Close();
                throw;
            }
            catch (Exception e)
            {
                Session.FatalErrorClose(e);
            }
        }

        /// <summary>
        /// Closes current session: closes session logs if all input Items were processed
        /// </summary>
        public static void Close()
        {
            lock (Log.MainThread)
            {
                if (This == null)
                    return;
                if (This.closing_thread != null)
                    return;
                State = SessionState.CLOSING;
                This.storage.WriteState(State, new { });
                This.closing_thread = ThreadRoutines.Start(This.close);
            }
        }
        Thread closing_thread = null;
        void close()
        {
            lock (This_)
            {
                try
                {
                    Log.Main.Write("Closing the bot session: " + Session.State.ToString());
                    BotCycle.Abort();

                    if (This.IsUnprocessedInputItem)
                        State = SessionState.BROKEN;
                    else if (This.IsItem2Restore)
                        State = SessionState.UNCOMPLETED;
                    else
                        State = SessionState.COMPLETED;

                    This.storage.WriteState(State, new { });

                    try
                    {
                        Bot.SessionClosing();
                    }
                    catch (Exception e)
                    {
                        Session.State = SessionState.FATAL_ERROR;
                        This.storage.WriteState(State, new { });
                        LogMessage.Error(e);
                        Bot.FatalError(e.Message);
                    }

                    try
                    {
                        Closing?.Invoke();
                    }
                    catch (Exception e)
                    {
                        Session.State = SessionState.FATAL_ERROR;
                        This.storage.WriteState(State, new { });
                        LogMessage.Error(e);
                        Bot.FatalError(e.Message);
                    }

                    InputItemQueue.Close();
                    FileWriter.ClearSession();
                }
                catch (ThreadAbortException)
                {
                }
                catch (Exception e)
                {
                    Session.State = SessionState.FATAL_ERROR;
                    This.storage.WriteState(State, new { });
                    LogMessage.Error(e);
                    Bot.FatalError(e.Message);
                }
                finally
                {
                    storage.Close();
                    switch (State)
                    {
                        case SessionState.NULL:
                        case SessionState.STARTING:
                        case SessionState.COMPLETED:
                        case SessionState.FATAL_ERROR:
                            Directory.Move(Dir, Dir + "_" + TimeMark + "_" + State);
                            break;
                        case SessionState.RESTORING:
                        case SessionState.RUNNING:
                        case SessionState.CLOSING:
                        case SessionState.UNCOMPLETED:
                        case SessionState.BROKEN:
                            break;
                        default:
                            throw new Exception("Unknown option: " + State);
                    }
                    This_ = null;
                    Cliver.Log.ClearSession();
                }
            }

            try
            {
                Closed?.Invoke();
            }
            catch (Exception e)
            {
                LogMessage.Error(e);
                Bot.FatalError(e.Message);
            }
        }

        void read_input_file()
        {
            Log.Main.Write("Loading InputItems from the input file.");
            Type start_input_item_type = (from t in Assembly.GetEntryAssembly().GetTypes() where t.IsSubclassOf(typeof(InputItem)) && !t.IsGenericType select t).First();
            InputItemQueue start_input_item_queue = GetInputItemQueue(start_input_item_type.Name);
            FillStartInputItemQueue(start_input_item_queue, start_input_item_type);
        }

        public enum SessionState
        {
            NULL,
            STARTING,
            RESTORING,//restoring phase
            RUNNING,
            CLOSING,
            COMPLETED,
            UNCOMPLETED,
            BROKEN,
            FATAL_ERROR
        }

        public static SessionState State
        {
            get
            {
                if (This == null)
                    return SessionState.NULL;
                return This._state;
            }
            private set
            {
                if (This == null)
                    throw new Exception("Trying to set session state while no session exists.");
                if (This._state >= SessionState.COMPLETED)
                    return;
                if (This._state > value)
                    throw new Exception("Session state cannot change from " + This._state + " to " + value);
                This._state = value;
            }
        }
        SessionState _state = SessionState.NULL;
    }
}