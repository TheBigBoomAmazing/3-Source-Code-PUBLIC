using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Configuration;

namespace eContract.Common
{
    public class ConfigHelper
    {
      

        public static string UIPATH = GetSetting("System.UIPath");

        public static string HttpImageUrl = GetSetting("System.HttpImageUrl");

        public static string OfferShowKey = GetSetting("System_OfferShowKey");

        private static string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        private static object GetConfig(string key)
        {
            return ConfigurationManager.GetSection(key);
        }
        private static ConnectionStringSettings GetConnectionStrings(string key)
        {
            return ConfigurationManager.ConnectionStrings[key];
        }
        public static string Version = GetSetting("System_Version");

        public static string GetSetString(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        public static int CacheTime
        {
            get
            {
                string CacheTime = GetSetting("System.CacheTime");
                if (!string.IsNullOrEmpty(CacheTime))
                {
                    return int.Parse(CacheTime);
                }
                return 30;
            }
        }

        private static string ApplicationName = GetSetting("System.SystemName");
        private static string FrameName
        {
            get
            {
               var value= GetSetting("System.FrameName");
               return !string.IsNullOrWhiteSpace(value) ? value : "bootstrap";
            }
        }
        /// <summary>
        /// 系统默认框架css
        /// </summary>
        public static string FrameCSSPath
        {
            get {
                return string.Format(UIPATH + "/css/frame/{0}", FrameName);
            }
        }
        public static string FrameJSPath
        {
            get
            {
                return string.Format(UIPATH + "/scripts/frame/{0}", FrameName);
            }
        }
        private static string SystemTheme = GetSetting("System.SystemTheme");
        public static string SystemCSSPath
        {
            get
            {
                return string.Format(UIPATH + "/css/{0}/{1}", ApplicationName, !string.IsNullOrWhiteSpace(SystemTheme) ? SystemTheme : "default");
            }
        }
        public static string SystemJSPath
        {
            get
            {
                return string.Format(UIPATH + "/scripts/{0}", ApplicationName);
            }
        }
        public static string B2BUrl
        {
            get {
                return GetSetString("System.B2BUrl");
            }
        }
        public static string B2CUrl
        {
            get
            {
                return GetSetString("System.B2CUrl");
            }
        }
        public static string SystemCode
        {
            get
            {
                return GetSetting("System.SystemCode");
               
            }
        }
        public static string SSOUrl
        {
            get
            {
                return GetSetString("System.SSOUrl");
            }
        }
    }
}
