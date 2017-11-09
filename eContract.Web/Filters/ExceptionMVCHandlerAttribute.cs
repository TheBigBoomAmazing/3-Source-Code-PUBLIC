using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using eContract.Web.Common;
using eContract.BusinessService.SystemManagement.Service;

namespace eContract.Web.Filters
{
    public class ExceptionMvcHandlerAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {

            var res = new HttpResponseMessage(HttpStatusCode.InternalServerError);

            string errormessage = filterContext.Exception.Message;
            if (filterContext.Exception.InnerException != null)
            {
                errormessage = filterContext.Exception.InnerException.Message;
            }
            string fullErrormessage = "MVC程序错误了：" + errormessage + filterContext.Exception.StackTrace;
            res.Content = new StringContent(fullErrormessage);
            //LogHelper.WriteErrorLog(errormessage);
            LogHelper.WriteLogToFile(fullErrormessage);

            SystemService.LogErrorService.InsertLog(filterContext.Exception);

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            filterContext.HttpContext.Response.StatusDescription = "MVC程序错误了,请重试，如果继续出现问题，请联系技术人员解决。";
            //filterContext.HttpContext.Response.Redirect("/Home/error?Status=" + filterContext.HttpContext.Response.StatusCode);
            filterContext.HttpContext.Response.Write("\"" + errormessage + "\"");
        }
    }
}