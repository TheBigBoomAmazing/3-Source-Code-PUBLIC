using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enyim.Caching;
using Enyim.Caching.Memcached;

namespace eContract.Common.WebUtils
{
    public static class CachingService
    {
        #region initialize
        static MemcachedClient client;
        static CachingService()
        {
            client = new MemcachedClient();
        }

        public static void Set(string key, object value)
        {
            bool result = client.Store(StoreMode.Set, key, value);
        }

        public static object Get(string key)
        {
            object result = client.Get(key);
            return result;
        }

        public static T Get<T>(string key)
        {
            return client.Get<T>(key);
        }

        public static bool Remove(string key)
        {
            return client.Remove(key);
        }
        #endregion 

        #region Security caching
        public static int SecurityLevel
        {
            get
            {
                return Get<int>("SecurityLevel");
            }
            set
            {
                Set("SecurityLevel", value);
            }
        }
        /// <summary>
        /// 设置Security缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetSecurity(string key, object value)
        {
            Set("Security-" + key + "-" + SecurityLevel, value);
        }

        /// <summary>
        /// 获取Security缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetSecurity<T>(string key)
        {
            return Get<T>("Security-" + key + "-" + SecurityLevel);
        }

        /// <summary>
        /// 清除Security缓存
        /// </summary>
        public static void ClearSecurity()
        {
            SecurityLevel = SecurityLevel + 1;
        }
        #endregion

        #region Store caching
        static int StoreLevel
        {
            get
            {
                return Get<int>("StoreLevel");
            }
            set
            {
                Set("StoreLevel", value);
            }
        }
        /// <summary>
        /// 设置Store缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetStore(string key, object value)
        {
            Set("Store-" + key + "-" + StoreLevel, value);
        }

        /// <summary>
        /// 获取Store缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetStore(string key)
        {
            return Get("Store-" + key + "-" + StoreLevel);
        }
        /// <summary>
        /// 获取Store缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetStore<T>(string key)
        {
            return Get<T>("Store-" + key + "-" + StoreLevel);
        }

        /// <summary>
        /// 清除Store缓存
        /// </summary>
        public static void ClearStore()
        {
            StoreLevel = StoreLevel + 1;
        }
        #endregion

        #region Orgnization caching
        static int OrgnizationLevel
        {
            get
            {
                return Get<int>("OrgnizationLevel");
            }
            set
            {
                Set("OrgnizationLevel", value);
            }
        }
        /// <summary>
        /// 设置Security缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetOrgnization(string key, object value)
        {
            Set("Orgnization-" + key + "-" + OrgnizationLevel, value);
        }

        /// <summary>
        /// 获取Security缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetOrgnization<T>(string key)
        {
            return Get<T>("Orgnization-" + key + "-" + OrgnizationLevel);
        }

        /// <summary>
        /// 清除Security缓存
        /// </summary>
        public static void ClearOrgnization()
        {
            OrgnizationLevel = OrgnizationLevel + 1;
        }
        #endregion

        #region Parameter caching
        static int ParameterLevel
        {
            get
            {
                return Get<int>("ParameterLevel");
            }
            set
            {
                Set("ParameterLevel", value);
            }
        }
        /// <summary>
        /// 设置Parameter缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetParameter(string key, object value)
        {
            Set("Parameter-" + key + "-" + ParameterLevel, value);
        }

        /// <summary>
        /// 获取Parameter缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetParameter<T>(string key)
        {
            return Get<T>("Parameter-" + key + "-" + ParameterLevel);
        }

        /// <summary>
        /// 清除Parameter缓存
        /// </summary>
        public static void ClearParameter()
        {
            ParameterLevel = ParameterLevel + 1;
        }
        #endregion
    }
}
