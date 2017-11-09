using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Globalization;
using ComixSDK.EDI.Utils;
using System.IO;

namespace eContract.Common.WebUtils
{
    public class WebCaching
    {
        public const string UserInfoSessionString = "UserInfo";
        public const string UserCurrentRoleSessionString = "RoleInfo";
        public const string SESSION_CURRENT_LANGUAGE = "Current_Language";
        public const string SESSION_CURRENT_CULTUREINFO = "Current_CultureInfo";
        public const string LastLoginSession = "LastLogin";
        public const string SMSCount = "UserSMSCount";

        #region 获取Web的真实IP
        public static string GetRealIP()
        {
            string ip = "";
            try
            {
                HttpRequest request = HttpContext.Current.Request;
                if (request.ServerVariables["http_VIA"] != null)
                {
                    ip = request.ServerVariables["http_X_FORWARDED_FOR"].ToString().Split(',')[0].Trim();
                }
                else
                {
                    ip = request.UserHostAddress;
                }
            }
            catch (Exception e) {  }
            return ip;
        }
        #endregion

        #region WebContext
        public static HttpContext CurrentContext
        {
            get
            {
                return HttpContext.Current;
            }
        }

        public static HttpSessionState CurrentSession
        {
            get
            {
                return CurrentContext.Session;
            }
        }

        public static HttpCookieCollection CurrentCookie
        {
            get
            {
                return CurrentContext.Request.Cookies;
            }
        }

        public static HttpCookieCollection WriteCookie
        {
            get
            {
                return CurrentContext.Response.Cookies;
            }
        }

        public static System.Web.Caching.Cache CurrentCaching
        {
            get
            {
                return CurrentContext.Cache;
            
            
            }
        }
        #endregion

        #region Caching
        /// <summary>
        /// 加入缓存
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="cacheKey"></param>
        public static void AddCache(Object feature, string cacheKey)
        {
            if (CurrentContext != null)
            {
                DateTime expiration = DateTime.Now.AddDays(1);
                CurrentCaching.Add(cacheKey, feature, null, expiration, TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            }
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public static Object GetCache(string cacheKey)
        {
            Object result = null;
            if (CurrentCaching != null)
            {
                result = CurrentCaching[cacheKey];
            }
            return result;
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="cacheKey"></param>
        public static void RemoveCache(string cacheKey)
        {
            if (CurrentCaching != null)
                CurrentCaching.Remove(cacheKey);
        }

        /// <summary>
        /// 刷新缓存
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="cacheKey"></param>
        public static void RefreshCache(Object feature, string cacheKey)
        {
            if (CurrentCaching != null)
            {
                RemoveCache(cacheKey);
                if (feature != null)
                {
                    AddCache(feature, cacheKey);
                }
            }
        }

        #endregion

        #region User Info
        /// <summary>
        /// 当前用户的IP地址
        /// </summary>
        public static string CurrentIP
        {
            get
            {
                return CurrentContext.Request.UserHostAddress;
            }
        }

        /// <summary>
        /// 当前用户是HostName
        /// </summary>
        public static string CurrentHostName
        {
            get
            {
                return CurrentContext.Request.UserHostName;
            }
        }

        /// <summary>
        /// 当前用户信息
        /// </summary>
        public static object CurrentUser
        {
            get
            {
                return CurrentSession[UserInfoSessionString];
            }
            set
            {
                CurrentSession[UserInfoSessionString] = value;
            }
        }
        /// <summary>
        /// 当前用户信息
        /// </summary>
        public static object CurrentUserDomain
        {
            get
            {
                string CacheKey = "User_" + UserId;
                return CacheHelper.Instance.Get(CacheKey); 
            }
            set
            {
                string CacheKey = "User_" + UserId;
                CacheHelper.Instance.Set(CacheKey,value); 
            }
        }

        public static string UserAccount
        {
            get
            {
                if (CurrentSession["UserAccount"] != null)
                    return CurrentSession["UserAccount"].ToString();
                return string.Empty;
            }
            set
            {
                CurrentSession["UserAccount"] = value;
            }
        }

        public static string IsAdmin
        {
            get
            {
                if (CurrentSession["IsAdmin"] != null)
                    return CurrentSession["IsAdmin"].ToString();
                return string.Empty;
            }
            set
            {
                CurrentSession["IsAdmin"] = value;
            }
        }

        public static string UserId
        {
            get
            {
                if (CurrentSession["UserId"] != null)
                    return CurrentSession["UserId"].ToString();
                return string.Empty;
            }
            set
            {
                CurrentSession["UserId"] = value;
            }
        }

        public static string SystemName
        {
            get
            {
                if (CurrentSession["SystemName"] != null)
                    return CurrentSession["SystemName"].ToString();
                return string.Empty;
            }
            set
            {
                CurrentSession["SystemName"] = value;
            }
        }

        public static string VerifyToken
        {
            get
            {
                if (CurrentSession["VerifyToken"] != null)
                    return CurrentSession["VerifyToken"].ToString();
                return string.Empty;
            }
            set
            {
                CurrentSession["VerifyToken"] = value;
            }
        }

        static IApplicationSession _session;
        /// <summary>
        /// 获取或设置默认的当前用户的登录的信息
        /// </summary>
        /// <returns></returns>
        public static IApplicationSession DefaultSession
        {
            get
            {
                return _session;
            }
            set
            {
                _session = value;
            }
        }

        /// <summary>
        /// 当前用户信息
        /// </summary>
        public static object LastLogin
        {
            get
            {
                return CurrentSession[LastLoginSession];
            }
            set
            {
                CurrentSession[LastLoginSession] = value;
            }
        }
        #endregion

        #region Caching Object
        /// <summary>
        /// 缓存所有系统页
        /// </summary>
        public const string PAGECACHING = "PageCaching";
        public static object PageCaching
        {
            get
            {
                return GetCache(PAGECACHING);
            }
            set
            {
                RefreshCache(value, PAGECACHING);
            }
        }


        /// <summary>
        /// 缓存所有系统页(终端)
        /// </summary>
        public const string SHOPPAGECACHING = "ShopPageCaching";
        public static object ShopPageCaching
        {
            get
            {
                return GetCache(SHOPPAGECACHING);
            }
            set
            {
                RefreshCache(value, SHOPPAGECACHING);
            }
        }


        /// <summary>
        /// 缓存所有控件
        /// </summary>
        public const string PERMISSIONCACHING = "PermissionCaching";
        public static object PermissionCaching
        {
            get
            {
                return GetCache(PERMISSIONCACHING);
            }
            set
            {
                RefreshCache(value, PERMISSIONCACHING);
            }
        }

        /// <summary>
        /// 缓存用户
        /// </summary>
        public const string USERCACHING = "UserCaching";
        public static object UserCaching
        {
            get
            {
                return GetCache(USERCACHING);
            }
            set
            {
                RefreshCache(value, USERCACHING);
            }
        }

        public const string ORGCACHING = "OrgCaching";
        public static object OrgCaching
        {
            get
            {
                return GetCache(ORGCACHING);
            }
            set
            {
                RefreshCache(value, ORGCACHING);
            }
        }


        public const string POSXSJLBCACHING = "PosSxjlbCaching";
        public static object PosSxjlbCaching
        {
            get
            {
                return GetCache(POSXSJLBCACHING);
            }
            set
            {
                RefreshCache(value, POSXSJLBCACHING);
            }
        }

        public const string POSSYTCACHING = "PosSytCaching";
        public static object PosSytCaching
        {
            get
            {
                return GetCache(POSSYTCACHING);
            }
            set
            {
                RefreshCache(value, POSSYTCACHING);
            }
        }

        public const string POSDGYCACHING = "PosDgyCaching";
        public static object PosDgyCaching
        {
            get
            {
                return GetCache(POSDGYCACHING);
            }
            set
            {
                RefreshCache(value, POSDGYCACHING);
            }
        }


        public const string SPTMCACHING = "SptmCaching";
        public static object SptmCaching
        {
            get
            {
                return GetCache(SPTMCACHING);
            }
            set
            {
                RefreshCache(value, SPTMCACHING);
            }
        }

        /// <summary>
        /// 短信数量缓存
        /// </summary>
        public static Dictionary<string, int> SMSCountCaching
        {
            get
            {
                return GetCache(SMSCount) as Dictionary<string,int>;
            }
            set
            {
                RefreshCache(value, SMSCount);
            }
        }
        #endregion



        /// <summary>
        /// 获取当前的语言类型
        /// </summary>
        public static string CurrentLanguage
        {
            get
            {

                string lang = (string)CurrentSession[SESSION_CURRENT_LANGUAGE];
                if(string.IsNullOrEmpty(lang))
                {
                    lang = "zh-CN";
                }
                return lang;
            }
            set
            {
                CurrentSession[SESSION_CURRENT_LANGUAGE] = value;
            }
        }

      

        public static CultureInfo CurrentCulturInfo
        {
            get
            {

                return (CultureInfo)CurrentSession[SESSION_CURRENT_CULTUREINFO];
            }
            set
            {
                CurrentSession[SESSION_CURRENT_CULTUREINFO] = value;
            }
        }

        private const string SupplierList_CAHING = "SupplierList";
        public static object SupplierList
        {
            get
            {
                return GetCache(SupplierList_CAHING);
            }
            set
            {
                RefreshCache(value, SupplierList_CAHING);
            }
        }

        private const string DistributorList_CAHING = "DistributorList";
        public static object DistributorList
        {
            get
            {
                return GetCache(DistributorList_CAHING);
            }
            set
            {
                RefreshCache(value, DistributorList_CAHING);
            }
        }

        private const string CustomerList_CAHING = "CustomerList";
        public static object CustomerList
        {
            get
            {
                return GetCache(CustomerList_CAHING);
            }
            set
            {
                RefreshCache(value, CustomerList_CAHING);
            }
        }

        /// <summary>
        /// 域
        /// </summary>
        public static string DomainCode
        {
            get {
                return CookieHelper.GetCookie("Auth_DomainCode");
            }
            set {
                CookieHelper.AddCookie("Auth_DomainCode", value);
            }
        }

        /// <summary>
        /// 唯一身份标识，打开创建
        /// </summary>
        public static string AUTHUserId
        {
            get
            {
                string value = CookieHelper.GetCookie("system_authuserId");
                if (string.IsNullOrWhiteSpace(value))
                {
                    value = Guid.NewGuid().ToString().Replace("-","").ToLower();
                    CookieHelper.AddCookie("system_authuserId", value);
                }
                return value;
            }
           
        }

        public static bool IsDebug
        {
            get {
                return ComixSDK.EDI.Utils.ConfigHelper.GetConfigBool("IsDebug");
            }
        }

        public static string CustomerCodes {
            get
            {
                if (CurrentSession["CustomerCodes"] != null)
                    return CurrentSession["CustomerCodes"].ToString();
                return string.Empty;
            }
            set
            {
                CurrentSession["CustomerCodes"] = value;
            }
        }
    }
}
