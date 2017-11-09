using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Helpers;
using eContract.BusinessService.SystemManagement.Domain;
using eContract.Common.WebUtils;
using eContract.BusinessService.SystemManagement.Service;

namespace eContract.Web
{
    /// <summary>
    /// 权限验证
    /// </summary>
    public class CheckAuthorizeAttribute : AuthorizeAttribute
    {

        /// <summary>
        /// 验证是否登录
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("HttpContext");
            }
            if (httpContext.User == null)
            {
                return false;
            }
            if (httpContext.User.Identity.IsAuthenticated)
            {
                if (WebCaching.CurrentUserDomain == null)
                {
                    WebCaching.CurrentUserDomain = SystemService.UserService.GetUserDomainByUserAccount(WebCaching.SystemName, WebCaching.UserAccount);
                }
                //判断用户是否有进入页面的权限
                //else
                //{
                //    UserDomain domain = WebCaching.CurrentUserDomain as UserDomain;
                //    if (domain == null) return false;
                //    foreach (MenuDataItem domainMenuDataItem in domain.MenuDataItems)
                //    {
                //        foreach (MenuDataItem menu in domainMenuDataItem.SubMenu)
                //        {
                //            if (httpContext.Request.UrlReferrer != null &&
                //                (("/" + menu.Page.PageUrl == httpContext.Request.UrlReferrer.AbsolutePath) || ("/" + menu.Page.PageUrl == httpContext.Request.RawUrl)))
                //                return true;
                //        }
                //    }
                //    return false;
                //}

                return true;
            }

            //if (CurrentUser.User == null)
            //{
            //    return false;
            //}

            return false;
            //  return base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            this.WriteResult(filterContext, "请登录后在访问");
        }

        /// <summary>
        /// 输出错误信息
        /// </summary>
        /// <param name="filterContext"></param>
        /// <param name="strError"></param>
        public void WriteResult(AuthorizationContext filterContext, string strError)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.StatusCode = 200;
                filterContext.Result = new JsonResult
                {
                    //Data = AjaxResult.Error(strError, -100),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                if (filterContext.HttpContext.Response.StatusCode == 401) //对于401错误，默认会跳转到web.config 中定义的  loginUrl
                {
                    filterContext.Result = new RedirectResult("/");
                }
                else
                {
                    base.HandleUnauthorizedRequest(filterContext);
                }
            }
        }

    }
}
