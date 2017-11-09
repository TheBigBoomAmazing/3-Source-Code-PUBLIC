using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LitJson;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using eContract.BusinessService.SystemManagement.Service;
using eContract.Web.Common;

namespace eContract.Web.Handlers
{
    public class BaseHandler: IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "application/json";

            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                OnRequest(context, jsonSerializer);
            }
            catch (Exception ex)
            {
                SystemService.LogErrorService.InsertLog(ex);
                context.Response.Write(JsonHelper.GetErrorMsg(ex.Message));
                return;
            }
        }

        public bool IsReusable {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="context"></param>
        protected virtual void OnRequest(HttpContext context, JavaScriptSerializer jsonSerializer)
        {
        }

        /// <summary>
        /// 获取Request中的JSON数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected JsonData GetJsonData(HttpContext context)
        {
            try
            {
                using (StreamReader reader = new StreamReader(context.Request.InputStream))
                {
                    string jsonStr = HttpUtility.UrlDecode(reader.ReadToEnd());
                    //SystemService.LogErrorService.InsertMsgLog(context.Request.Path, jsonStr);
                    return JsonMapper.ToObject(jsonStr);
                }
            }
            catch (Exception ex)
            {
                SystemService.LogErrorService.InsertLog(ex);
                context.Response.Write(JsonHelper.GetErrorMsg(ex.Message));
                return null;
            }
        }
    }
}