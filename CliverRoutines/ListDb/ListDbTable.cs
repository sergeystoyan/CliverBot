﻿/********************************************************************************************
        Author: Sergey Stoyan
        sergey.stoyan@gmail.com
        http://www.cliversoft.com
********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using Newtonsoft.Json;

namespace Cliver
{
    public partial class ListDb
    {
        public class testDocument : ListDb.Document
        {
            public string A = DateTime.Now.ToString() + "\r\n" + DateTime.Now.Ticks.ToString();
            public string B = "test";
            public long C = DateTime.Now.Ticks;
            public DateTime D = DateTime.Now;

            static public void Test()
            {
                ListDb.Table<testDocument> t = ListDb.Table<testDocument>.Get();
                //t.Drop();
                t.Save(new testDocument());
                t.Save(new testDocument());
                testDocument d = t.Last();
                d.A = "changed";
                t.Save(d);
                t.Remove(t.First());
                //t.Flush();
            }
        }

        public class Document
        {

        }

        public class Table<D> : List<D>, IDisposable where D : Document, new()
        {
            public static Table<D> Get(string directory = null)
            {
                directory = get_normalized_directory(directory);

                WeakReference wr;
                string key = directory + "\\" + typeof(D).Name;
                if (!table_keys2table.TryGetValue(key, out wr))
                {
                    if (!wr.IsAlive)
                    {
                        Table<D> t = new Table<D>(directory);
                        wr = new WeakReference(t);
                        table_keys2table[key] = wr;
                    }
                }
                return (Table<D>)wr.Target;
            }
            static Dictionary<string, WeakReference> table_keys2table = new Dictionary<string, WeakReference>();

            static string get_normalized_directory(string directory = null)
            {
                if (directory == null)
                    directory = Cliver.Log.GetAppCommonDataDir();
                return PathRoutines.GetNormalizedPath(directory);
            }

            public readonly string Log = null;
            TextWriter log_writer;
            public readonly string File = null;
            TextWriter file_writer;
            readonly string new_file;
            //public Type DocumentType;
            public Modes Mode = Modes.FLUSH_TABLE_ON_CLOSE;

            public enum Modes
            {
                NULL = 0,
                KEEP_OPEN_TABLE_FOREVER = 1,//requires explicite call Close()
                FLUSH_TABLE_ON_CLOSE = 2,
            }

            Table(string directory = null)
            {
                directory = get_normalized_directory(directory);

                File = directory + "\\" + GetType().Name + ".listdb";
                new_file = File + ".new";
                Log = directory + "\\" + GetType().Name + ".listdb.log";

                if (System.IO.File.Exists(new_file))
                {
                    if (System.IO.File.Exists(File))
                        System.IO.File.Delete(File);
                    System.IO.File.Move(new_file, File);
                    if (System.IO.File.Exists(Log))
                        System.IO.File.Delete(Log);
                }

                if (System.IO.File.Exists(File))
                {
                    using (TextReader fr = new StreamReader(File))
                    {
                        string[] log_ls;
                        if (System.IO.File.Exists(Log))
                            log_ls = System.IO.File.ReadAllLines(Log);
                        else
                            log_ls = new string[0];
                        foreach (string l in log_ls)
                        {
                            Match m = Regex.Match(l, @"deleted:\s+(\d+)");
                            if (m.Success)
                            {
                                this.RemoveAt(int.Parse(m.Groups[1].Value));
                                continue;
                            }
                            m = Regex.Match(l, @"replaced:\s+(\d+)");
                            if (m.Success)
                            {
                                read_next(fr);
                                int p1 = int.Parse(m.Groups[1].Value);
                                this.RemoveAt(p1);
                                D d = this[this.Count - 1];
                                this.RemoveAt(this.Count - 1);
                                this.Insert(p1, d);
                                continue;
                            }
                            m = Regex.Match(l, @"added:\s+(\d+)");
                            if (m.Success)
                            {
                                read_next(fr);
                                int p1 = int.Parse(m.Groups[1].Value);
                                if (p1 != this.Count - 1)
                                    throw new Exception("Log file broken.");
                                continue;
                            }
                        }

                        if (log_ls.Length > 0)
                        {
                            Match m = Regex.Match(log_ls[log_ls.Length - 1], @"replacing:\s+(\d+)\s+with\s+(\d+)");
                            if (m.Success)
                            {//replacing was broken so delete the new document if it was added
                                int i2 = int.Parse(m.Groups[2].Value);
                                if (i2 < this.Count)
                                {
                                    log_writer = new StreamWriter(Log, true);
                                    ((StreamWriter)log_writer).AutoFlush = true;
                                    RemoveAt(i2);
                                    log_writer.Dispose();
                                }
                            }
                        }
                    }
                }

                file_writer = new StreamWriter(File, true);
                ((StreamWriter)file_writer).AutoFlush = true;
                log_writer = new StreamWriter(Log, true);
                ((StreamWriter)log_writer).AutoFlush = true;
            }
            void read_next(TextReader fr)
            {
                string r = fr.ReadLine();
                this.Add(JsonConvert.DeserializeObject<D>(r));
            }

            ~Table()
            {
                Dispose();
            }

            public void Dispose()
            {
                if ((Mode & Modes.FLUSH_TABLE_ON_CLOSE) == Modes.FLUSH_TABLE_ON_CLOSE)
                    Flush();
                if (file_writer != null)
                    file_writer.Dispose();
                if (log_writer != null)
                    log_writer.Dispose();
            }

            public void Flush()
            {
                log_writer.WriteLine("flushing");

                using (TextWriter new_file_writer = new StreamWriter(new_file, false))
                {
                    foreach (D d in this)
                        new_file_writer.WriteLine(JsonConvert.SerializeObject(d, Formatting.None));
                    new_file_writer.Flush();
                }

                if (file_writer != null)
                    file_writer.Dispose();
                if (System.IO.File.Exists(File))
                    System.IO.File.Delete(File);
                System.IO.File.Move(new_file, File);
                file_writer = new StreamWriter(File, true);
                ((StreamWriter)file_writer).AutoFlush = true;

                if (log_writer != null)
                    log_writer.Dispose();
                log_writer = new StreamWriter(Log, false);
                ((StreamWriter)log_writer).AutoFlush = true;
                log_writer.WriteLine("flushed");
            }

            public void Drop()
            {
                base.Clear();

                if (file_writer != null)
                    file_writer.Dispose();
                if (System.IO.File.Exists(File))
                    System.IO.File.Delete(File);

                if (log_writer != null)
                    log_writer.Dispose();
                if (System.IO.File.Exists(Log))
                    System.IO.File.Delete(Log);
            }

            public void Clear()
            {
                base.Clear();

                if (file_writer != null)
                    file_writer.Dispose();
                file_writer = new StreamWriter(File, false);

                if (log_writer != null)
                    log_writer.Dispose();
                log_writer = new StreamWriter(Log, false);
            }

            public SaveResults Save(D document)
            {
                int i = this.IndexOf(document);
                if (i >= 0)
                {
                    log_writer.WriteLine("replacing: " + i);
                    file_writer.WriteLine(JsonConvert.SerializeObject(document, Formatting.None));
                    log_writer.WriteLine("replaced: " + i);
                    return SaveResults.UPDATED;
                }
                else
                {
                    file_writer.WriteLine(JsonConvert.SerializeObject(document, Formatting.None));
                    log_writer.WriteLine("added: " + this.Count);
                    base.Add(document);
                    return SaveResults.ADDED;
                }
            }
            public enum SaveResults
            {
                ADDED,
                UPDATED,
            }

            /// <summary>
            /// !!! Differs from List: Table works as an ordered HashSet !!!
            /// </summary>
            /// <param name="document"></param>
            /// <returns></returns>
            public SaveResults Add(D document)
            {
                return Save(document);
            }

            public void AddRange(IEnumerable<D> documents)
            {
                throw new Exception("TBD");
                base.AddRange(documents);
            }

            public bool Remove(D document)
            {
                int i = this.IndexOf(document);
                if (i >= 0)
                {
                    base.RemoveAt(i);
                    log_writer.WriteLine("deleted: " + i);
                    return true;
                }
                return false;
            }

            public int RemoveAll(Predicate<D> match)
            {
                throw new Exception("TBD");
                return base.RemoveAll(match);
            }

            public void RemoveAt(int index)
            {
                base.RemoveAt(index);
                log_writer.WriteLine("deleted: " + index);
            }

            public void RemoveRange(int index, int count)
            {
                for (int i = index; i < count; i++)
                {
                    base.RemoveAt(i);
                    log_writer.WriteLine("deleted: " + i);
                }
            }

            public D GetPrevious(D document)
            {
                if (document == null)
                    return null;
                int i = IndexOf(document);
                if (i < 1)
                    return null;
                return this[i - 1];
            }

            public D GetNext(D document)
            {
                if (document == null)
                    return null;
                int i = this.IndexOf(document);
                if (i + 1 >= this.Count)
                    return null;
                return this[i + 1];
            }
        }

        public static string GetNormalized(string s)
        {
            if (s == null)
                return null;
            return Regex.Replace(s.ToLower(), @" +", " ").Trim();
        }
    }
}