using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Script.Serialization;

namespace Cliver.Bot
{
    public partial class Settings
    {
        [Cliver.Settings.Obligatory]
        public static readonly LogClass Log;

        public class LogClass : Cliver.Settings
        {
            public int DeleteLogsOlderDays = 3;
            public string PreWorkDir = null;
            public string PrefWorkDirName = "CliverBotSessions";
            public bool WriteLog = true;

            override public void Loaded()
            {
                if (string.IsNullOrEmpty(PreWorkDir))
                {
                    PreWorkDir = Regex.Replace(Cliver.Log.AppDir, @":.*", @":\" + PrefWorkDirName, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    if (System.Threading.Thread.CurrentThread.GetApartmentState() == System.Threading.ApartmentState.STA)
                    {
                        if (!LogMessage.AskYesNo("A folder where the application will store log data is not specified. By default it will be created in the following path:\r\n" + PreWorkDir + "\r\nClick Yes if you agree, click No if you want to specify another location.", true, false))
                        {
                            System.Windows.Forms.FolderBrowserDialog f = new System.Windows.Forms.FolderBrowserDialog();
                            f.Description = "Specify a folder where the application will store log data.";
                            while (f.ShowDialog(/*MainForm.This*/) != System.Windows.Forms.DialogResult.OK) ;
                            PreWorkDir = f.SelectedPath;
                        }
                    }
                    else
                        LogMessage.Inform("A folder where the application will store log data is: " + PreWorkDir + ".\nIt can be changed in the app's settings");
                    Save();
                }
            }
        }
    }
}