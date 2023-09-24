using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace KmnlkCommon.Shareds
{
    public static class LoggerManagement
    {
        public enum ENUM_TYPE_MSG_LOGGER
        {
            INFO,
            ERROR,
            EXCEPTION
        }
        public enum ENUM_TYPE_Block_LOGGER
        {
            START,
            END
        }
        public interface ILog
        {
            void WriteToLog(string scope, string additional, ENUM_TYPE_MSG_LOGGER type, ENUM_TYPE_Block_LOGGER ox=ENUM_TYPE_Block_LOGGER.END, string msg = "SUCCESS");
        }
        public class FileLogger : ILog
        {
            private string logPath = "";
            private string extFile = "log";

            public FileLogger(string path, string ext = "log")
            {
                extFile = ext;
                logPath = path;
            }

            public void WriteToLog(string scope, string additional, ENUM_TYPE_MSG_LOGGER type, ENUM_TYPE_Block_LOGGER ox=ENUM_TYPE_Block_LOGGER.END, string msg = "SUCCESS")
            {
                if (logPath != "")
                {
                    try
                    {
                        StringBuilder st = new StringBuilder();
                        st.Append(Thread.CurrentThread.ManagedThreadId.ToString());
                        st.Append(";");
                        st.Append(scope);
                        st.Append(";");
                        st.Append(DateTime.Now.ToString("yyyyMMdd-HHmmss"));
                        st.Append(";");
                        st.Append(msg);
                        st.Append(";");
                        st.Append(((int)ox).ToString());
                        st.Append(";");
                        if (type == ENUM_TYPE_MSG_LOGGER.ERROR)
                            st.Append("ERROR");
                        else if (type == ENUM_TYPE_MSG_LOGGER.EXCEPTION)
                            st.Append("EXCEPTION");
                        else if (type == ENUM_TYPE_MSG_LOGGER.INFO)
                            st.Append("INFO");

                        st.Append(";");
                        st.Append(additional);
                        st.Append("\n");
                        string pathX = logPath + DateTime.Now.ToString("yyyyMMdd") + "." + extFile;
                        if (!File.Exists(pathX))
                        {
                            StreamWriter rw= File.CreateText(pathX);
                            rw.Write(st.ToString());
                            rw.Close();
                                return;
                        }

                      StreamWriter wr=  File.AppendText(pathX);
                        wr.Write(st.ToString());
                        wr.Close();
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }
        public class DBLogger : ILog
        {
            public string logPath = "";
            public string extFile = "log";

            public DBLogger(string path, string ext = "log")
            {
                extFile = ext;
                logPath = path;
            }

            public void WriteToLog(string scope, string additional, ENUM_TYPE_MSG_LOGGER type, ENUM_TYPE_Block_LOGGER ox=ENUM_TYPE_Block_LOGGER.END, string msg = "SUCCESS")
            {
                if (logPath != "")
                {
                    try
                    {
                        StringBuilder st = new StringBuilder();
                        st.Append(Thread.CurrentThread.ManagedThreadId.ToString());
                        st.Append(";");
                        st.Append(scope);
                        st.Append(";");
                        st.Append(DateTime.Now.ToString("yyyyMMdd-HHmmss"));
                        st.Append(";");
                        st.Append(msg);
                        st.Append(";");
                        st.Append(((int)ox).ToString());
                        st.Append(";");
                        if (type == ENUM_TYPE_MSG_LOGGER.ERROR)
                            st.Append("ERROR");
                        else if (type == ENUM_TYPE_MSG_LOGGER.EXCEPTION)
                            st.Append("EXCEPTION");
                        else if (type == ENUM_TYPE_MSG_LOGGER.INFO)
                            st.Append("INFO");

                        st.Append(";");
                        st.Append(additional);
                        st.Append("\n");
                        string pathX = logPath + DateTime.Now.ToString("yyyyMMdd") + "." + extFile;
                        if (!File.Exists(pathX))
                        {
                            File.Create(pathX);
                        }

                        File.AppendAllText(pathX, st.ToString());
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }
    }
}
