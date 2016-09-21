using System;
using UnityEngine;
using System.IO;

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
        static public void Write (string _logFileName, object _class, params object[] logValues)
        {
            
        }

        static public void Info (object _class, params object[] logValues)
        {
            string str = GetDebugString(_class, logValues);
            Debug.Log(str);
        }

        static public void Error (object _class, params object[] logValues)
        {
            string str = GetDebugString(_class, logValues);
            Debug.LogError(str);    
        }


        private static void log(object _class, enLogType logType, object[] values)
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

