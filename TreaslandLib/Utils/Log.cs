using System;
using UnityEngine;

namespace TreaslandLib.Utils
{
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


        public static string GetDebugString(object _class,params object[] values)
        {
            string logStr = "";
            for(int i = 0; i < values.Length; ++i)
            {
                logStr += values[i].ToString() + " ";
            }
            return logStr;
        }
        
    }
}

