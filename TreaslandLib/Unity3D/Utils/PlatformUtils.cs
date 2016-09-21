using System;
using System.Collections.Generic;
using UnityEngine;

namespace TreaslandLib.Unity3D.Utils
{
    public class PlatformUtils
    {

        public static bool isAndroidPlatform
        {
            get
            {
                return Application.platform == RuntimePlatform.Android;
            }
        }

        public static bool isIosPlatform
        {
            get
            {
                return Application.platform == RuntimePlatform.IPhonePlayer;
            }
        }

        public static bool isWPPlatform
        {
            get
            {
                return Application.platform == RuntimePlatform.WP8Player;
            }
        }

        public static bool isWinPlatform
        {
            get
            {
                return Application.platform == RuntimePlatform.WindowsPlayer
                       || Application.platform == RuntimePlatform.WindowsWebPlayer
                       || Application.platform == RuntimePlatform.WindowsEditor;
            }
        }

        public static bool isOSXPlatform
        {
            get
            {
                return Application.platform == RuntimePlatform.OSXPlayer
                       || Application.platform == RuntimePlatform.OSXWebPlayer
                       || Application.platform == RuntimePlatform.OSXEditor;
            }
        }

        public static bool isMobilePlatform
        {
            get
            {
                return isAndroidPlatform || isIosPlatform || isWPPlatform;
            }
        }

        public static bool isDesktopPlatform
        {
            get
            {
                return isWinPlatform || isOSXPlatform;
            }
        }

        public static bool isEditor
        {
            get
            {
                return Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor;
            }
        }
    }
}
