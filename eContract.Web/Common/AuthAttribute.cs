using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace eContract.Web
{
    public class AuthAttribute :ActionFilterAttribute 
    {
        /// <summary>
        /// get请求是否需要验证权限 默认是
        /// </summary>
        public bool IsGet { get; set; }
        /// <summary>
        /// post请求是否需要验证权限 默认是
        /// </summary>
        public bool IsPost { get; set; }

        /// <summary>
        /// 与其它ActionName权限一样
        /// </summary>
        public string ActionCode { get; set; }

        public AuthAttribute()
        {
            IsGet = true;
            IsPost = true;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var areaName = "";
            var actionName = filterContext.RouteData.Values["Action"].ToString();

            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            if (filterContext.RouteData.DataTokens["area"] != null)
            {
                areaName = filterContext.RouteData.DataTokens["area"].ToString();
            }

            if (!filterContext.ActionDescriptor.IsDefined(typeof(AuthAttribute), true) )//不需要验证权限
            {

            }
            else
            {
                //权限验证
                if ((IsPost && filterContext.HttpContext.Request.HttpMethod.Equals("POST"))
                    ||(IsGet && filterContext.HttpContext.Request.HttpMethod.Equals("GET"))
                    )
                {
                    //if (CurrentUser.User == null)
                    //{
                    //    WriteResult(filterContext, "对不起，您无权访问");
                    //    return;
                    //}
                    //else
                    //{
                    //    bool flag = false;
                    //    string RouteUrl = ((!string.IsNullOrEmpty(areaName)) ? areaName + "/" : "") + controllerName + "/" + (!string.IsNullOrEmpty(this.ActionCode) ? this.ActionCode : actionName);
                    //    var userPermission = CurrentUser.UserPermission.Where(x => x.RouteUrl == RouteUrl || x.RouteUrl == RouteUrl.Replace("/Index", "")).ToList();
                    //    if (userPermission != null && userPermission.Count > 0)
                    //    {
                    //        if (userPermission.Where(x => x.IsDisabled).FirstOrDefault() != null)//权限是否被禁用
                    //        {
                    //            WriteResult(filterContext,"对不起，您无权访问");
                    //            return;
                    //        }
                    //        else if (userPermission.Where(x => !x.IsDisabled).FirstOrDefault() != null)//是否可以访问
                    //        {
                    //            flag = true;
                    //        }


                    //    }
                    //    if (!flag)
                    //    {
                    //        WriteResult(filterContext, "对不起，您无权访问");
                    //        return;
                    //    }

                    //}


                }
            }
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 输出错误信息
        /// </summary>
        /// <param name="filterContext"></param>
        /// <param name="strError"></param>
        public void WriteResult(ActionExecutingContext filterContext, string strError)
        {
            var areaName = "";
            var actionName = filterContext.RouteData.Values["Action"].ToString();
            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            if (filterContext.RouteData.DataTokens["area"] != null)
            {
                areaName = filterContext.RouteData.DataTokens["area"].ToString();
            }
            //new LogErrorService().Save((!string.IsNullOrEmpty(areaName)?areaName+"/"+controllerName:controllerName),actionName,strError,strError);
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.StatusCode = 200;
                filterContext.Result = new JsonResult
                {
                    //Data = AjaxResult.Error(strError, -100),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {

                filterContext.Result = new EmptyResult();


            }
           
        }
    }
}
