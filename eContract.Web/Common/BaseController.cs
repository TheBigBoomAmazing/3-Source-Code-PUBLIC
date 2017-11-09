
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.SessionState;
using System.Web.Routing;
using eContract.Common.WebUtils;
using System.Globalization;
using System.Web.Compilation;
using eContract.Common;
using eContract.BusinessService.SystemManagement.Domain;
using eContract.Common.Entity;
using System.IO;
using ComixSDK.EDI.Utils;
using eContract.Web.Filters;


namespace eContract.Web
{
 
    [Auth]
    [ExceptionMvcHandler]
    public class BaseController : Controller, IApplicationSession, IControllerActivator, IRequiresSessionState
    {

        /// <summary>
        /// 判断是否是Post
        /// </summary>
        public bool IsPost
        {
            get
            {
                if (Request.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 判断是否是Post
        /// </summary>
        public bool IsAjax
        {
            get
            {
                return Request.IsAjaxRequest();
            }
        }
        #region Request Variables
        public string GetIPAddress()
        {
            try
            {
              
                string user_IP = string.Empty;
                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                    {
                        user_IP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                    }
                    else
                    {
                        user_IP = System.Web.HttpContext.Current.Request.UserHostAddress;
                    }
                }
                else
                {
                    user_IP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                }
                return user_IP;
            }
            catch (Exception ex)
            {

            }
            return "";
        }

        public string GetScriptPath()
        {
            string Paths = System.Web.HttpContext.Current.Request.ServerVariables["PATH_TRANSLATED"].ToString();
            return Paths.Remove(Paths.LastIndexOf("\\"));
        }
        #endregion

        private bool m_isweb = true;
        public bool IsWeb
        {
            get
            {
                return m_isweb;
            }
            set
            {
                m_isweb = value;
            }
        }

        public string IPAddress
        {
            get
            {
                return GetIPAddress();
            }
            set
            {

            }
        }

        public string FormAddress
        {
            get
            {
                return System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            }
            set
            {

            }
        }

        public string ServerAddress
        {
            get
            {
                return GetScriptPath();
            }
            set
            {

            }
        }

        #region Enviornment Variables
       
       

       
        #endregion

        public IController Create(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return DependencyResolver.Current
               .GetService(controllerType) as IController;
        }

        #region 错误处理
        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            WriteResult(filterContext, filterContext.Exception.Message,filterContext.Exception.StackTrace);
        }
        /// <summary>
        /// 输出错误信息
        /// </summary>
        /// <param name="filterContext"></param>
        /// <param name="strError"></param>
        public void WriteResult(ExceptionContext filterContext, string strError,string msgContent)
        {
            var areaName = "";
            var actionName = filterContext.RouteData.Values["Action"].ToString();
            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            if (filterContext.RouteData.DataTokens["area"] != null)
            {
                areaName = filterContext.RouteData.DataTokens["area"].ToString();
            }
           // new LogErrorService().Save((!string.IsNullOrEmpty(areaName) ? areaName + "/" + controllerName : controllerName), actionName, strError, strError);
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.StatusCode = 200;
                filterContext.Result = new JsonResult
                {
                 //   Data = AjaxResult.Error(strError, -100),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                filterContext.Result = View(strError);
            }

        }
        public string GetResource(string strKey)
        {
            if (WebCaching.CurrentCulturInfo == null)
                WebCaching.CurrentCulturInfo = new CultureInfo(WebCaching.CurrentLanguage);
            if (strKey.Length == 0)
                return strKey;
            try
            {
                ResourceExpressionFields fields = GetResourceFields(string.Format("Resource,{0}", strKey), "~/");
                object obj = System.Web.HttpContext.GetGlobalResourceObject(fields.ClassKey, fields.ResourceKey, WebCaching.CurrentCulturInfo);
                if (obj != null)
                {
                    return obj.ToString();
                }
            }
            catch (Exception)
            {
                
            }
           
            return strKey;
                
        }
        static ResourceExpressionFields GetResourceFields(string expression, string virtualPath)
        {
            var context = new ExpressionBuilderContext(virtualPath);
            var builder = new ResourceExpressionBuilder();
            return (ResourceExpressionFields)builder.ParseExpression(expression, typeof(string), context);
        }
        #endregion


        public string UserId
        {
            get {
                return WebCaching.UserId;
            }
        }

        public string UserAccount
        {
            get { return WebCaching.UserAccount; }
        }

        public UserDomain CurrentUser
        {
            get
            {
                if (WebCaching.CurrentUserDomain != null)
                {
                    return (UserDomain)WebCaching.CurrentUserDomain;
                }
                return null;
            }
            set {
                WebCaching.CurrentUserDomain = value;
            }
        }
        public FileInfo[] LoginBackFile
        {
            get
            {
                string CacheKey = "System_LoginPath";
                FileInfo[] infos = CacheHelper.Instance.Get<FileInfo[]>(CacheKey);
                if (infos == null)
                {
                    infos = FileObj.GetFiles(Server.MapPath(ComixSDK.EDI.Utils.ConfigHelper.GetConfigString("System_LoginPath")));
                    CacheHelper.Instance.Set(CacheKey, infos);
                }
                return infos;
            }
        }
    }
}
