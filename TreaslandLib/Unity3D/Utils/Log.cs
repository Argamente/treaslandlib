using System;
using UnityEngine;
using System.IO;
using TreaslandLib.Unity3D.Core;
using TreaslandLib.Utils;

namespace TreaslandLib.Unity3D.Utils
{
    public enum enLogType
    {
        Info,
        Warning,
        Error,
        Fatal,
    }

    public class Log
    {
        private const string File_INFO = "Info";
        private const string File_Error = "Error";
        private const string File_Fatal = "Fatal";
        private const string File_Wraning = "Warning";

        static public void Write (string _logFileName, object _class, params object[] logValues)
        {
            log(_logFileName, _class, enLogType.Info, logValues);
        }

        static public void Info (object _class, params object[] logValues)
        {
            log(File_INFO, _class, enLogType.Info, logValues);
        }

        static public void Error (object _class, params object[] logValues)
        {
            log(File_Error, _class, enLogType.Error, logValues);
        }


        private static void log(string _logFileName, object _class, enLogType logType, object[] values)
        {
            string logStr = GetDebugString(_class, values);

            if (Application.isEditor)
            {
                if(logType == enLogType.Error)
                {
                    Debug.LogError(logStr);
                }
                else if(logType == enLogType.Warning)
                {
                    Debug.LogWarning(logStr);
                }
                else if(logType == enLogType.Fatal)
                {
                    Debug.LogError(logStr);
                }
                else
                {
                    Debug.Log(logStr);
                }
            }

            try
            {
                string fileName = PathConfig.userDataPath + "/Log/Unity/" + _logFileName + ".log";
                IOUtils.AppendTextFile(fileName, logStr + "\n");
            }
            catch(Exception e)
            {
                Debug.LogError(e);
            }

        }



        public static string GetDebugString(object _class,params object[] values)
        {
            string logStr = "[Log] " + DateTime.Now.ToString("T") + " ";
            if(_class == null)
            {
                logStr += "[file:\"\"]";
            }
            else
            {
                logStr += "[file:\"" + _class + "\"] - ";
            }

            for(int i = 0; i < values.Length; ++i)
            {
                try
                {
                    logStr += values[i] == null ? "null" : values[i].ToString() + " ";
                }
                catch(Exception e)
                {
                    logStr += "";
                    Debug.LogError(e);
                }
            }
            
            return logStr;
        }
    }
}

