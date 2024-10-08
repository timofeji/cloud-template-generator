using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TemplateGenerator
{
    /// <summary>
    /// Logs errors, events
    /// </summary>
    public class Log
    {
        public Log()
        {

        }


        #region Errors
     
        public static bool LogErr(string Source, string ErrMsg, string LogPath)
        {
            try
            {
                //Log Error
                FileStream fs = new FileStream(LogPath + "ErrorLog.txt", FileMode.Append, FileAccess.Write);
                StreamWriter wr = new StreamWriter(fs);
                string sMsg = "ERROR IN " + Source + " on " + DateTime.Now.ToShortDateString() + " at " +
                    DateTime.Now.ToShortTimeString() + ":  " + ErrMsg + Environment.NewLine;
                wr.Write(sMsg);
                wr.Close();
                return (true);
            }
            catch (Exception Exc)
            {
                return (false);
            }
        }
        #endregion Errors

        #region Events

        public static bool LogEvent(string Source, string Msg, string LogPath)
        {
            try
            {
                //Log Event
                // write to file and return true				
                FileStream fs = new FileStream(LogPath + "EventLog.txt", FileMode.Append, FileAccess.Write);
                StreamWriter wr = new StreamWriter(fs);
                wr.Write("On " + DateTime.Now.ToShortDateString() + " at " +
                    DateTime.Now.ToShortTimeString() + ":  " + Source + " -- " + Msg + Environment.NewLine);
                wr.Close();
                return (true);
            }
            catch (Exception Exc)
            {
                LogErr("Log.LogEvent", Exc.Message, LogPath);
                return (false);
            }
        }
        #endregion Events

        #region CodeTiming

        public static bool LogDuration(string Source, DateTime StartTime, DateTime EndTime, string LogPath)
        {
            TimeSpan dtDuration = EndTime.Subtract(StartTime);
            try
            {
                //Log Error
                Log.LogEvent(Source, dtDuration.TotalMilliseconds.ToString(), LogPath);
                return (true);
            }
            catch (Exception Exc)
            {
                Log.LogErr("Log.LogDuration", Exc.Message, LogPath);
                return (false);
            }
        }

        #endregion CodeTiming

 
    }
}
