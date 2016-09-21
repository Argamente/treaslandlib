using System.Collections.Generic;
using UnityEngine;

namespace TreaslandLib.Unity3D.Managers
{
    public class ConstantHolder
    {
        private static GameObject _instance;
        private static bool _inited = false;

        private static GameObject GetInstance()
        {
            if (!_inited)
            {
                _inited = true;
                _instance = new GameObject("ConstantHolder");
                GameObject.DontDestroyOnLoad(_instance);
            }
            return _instance;
        }


        public static T AddComponent<T>() where T : Component
        {
            return GetInstance().AddComponent<T>();
        }

        public static T GetComponent<T>() where T : Component
        {
            return GetInstance().GetComponent<T>();
        }


        public static void RemoveComponent<T>() where T : Component
        {
            GameObject.Destroy(GetInstance().GetComponent<T>());
        }
    }
}
