using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreaslandLib.Unity3D.Core
{
    /// <summary>
    /// 整个应用的初始化入口，先调用App.Init();
    /// </summary>
    public class App
    {
        private static string _companyName = "Treasland";
        private static string _productName = "TreaslandProducts";

        public static string companyName
        {
            get
            {
                return App._companyName;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    App._companyName = value;
                }
            }
        }

        public static string productName
        {
            get
            {
                return App._productName;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    App._productName = value;
                }
            }
        }


        public static void Init(string __companyName, string __productName)
        {
            // 初始化公司和产品名
            App.companyName = __companyName;
            App.productName = __productName;

            // 初始化数据路径
            PathConfig.Init();
        }

    }
}
