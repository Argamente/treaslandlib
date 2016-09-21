using System;
using System.Collections.Generic;
using UnityEngine;
using TreaslandLib.Unity3D.Utils;

namespace TreaslandLib.Unity3D.Core
{
    public class PathConfig
    {
        /// <summary>
        /// Unity 存放资源的路径,资源加载路径
        /// </summary>
        public static string unityAssetsPath
        {
            get;
            private set;
        }

        /// <summary>
        /// Unity 的 Application.datePath
        /// </summary>
        public static string unityDataPath
        {
            get;
            private set;
        }

        /// <summary>
        /// 用于存放用户数据的路径
        /// </summary>
        public static string userDataPath
        {
            get;
            private set;
        }


        public static void Init()
        {
            if (PlatformUtils.isDesktopPlatform)
            {
                PathConfig.unityAssetsPath = Application.streamingAssetsPath;
            }
            else
            {
                PathConfig.unityAssetsPath = Application.persistentDataPath + "/Assets";
            }


            // 存放用户数据的路径，Win平台下存放于 %appdata% 目录下，即Roaming目录下
            if (PlatformUtils.isWinPlatform)
            {

            }
            else
            {
                PathConfig.userDataPath = Application.persistentDataPath;
            }

            
        }
    }
}
