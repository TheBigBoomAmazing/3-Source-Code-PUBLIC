using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eContract.Web.Models.Interface
{
    public class SSOLoginModel
    {
        public MsgHeader MsgHeader { get; set; }
        public object MsgBody { get; set;}
    }

    public class MsgHeader
    {
        private bool isError = true;
        /// <summary>
        /// 是否错误
        /// </summary>
        public bool IsError { get { return isError; } set { isError = value; } }
        /// <summary>
        /// 错误或者成功消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 特殊错误Code
        /// </summary>
        public int StatusCode { get; set; }
        /// <summary>
        /// token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserNo
        {
            get
            {
                string userno = "";
                userno = System.Web.HttpContext.Current.Request.QueryString["UserNo"];
                if (string.IsNullOrEmpty(userno))
                {
                    userno = System.Web.HttpContext.Current.Request.Form["UserNo"];
                }
                return userno;
            }
        }

        /// <summary>
        /// 日志Id
        /// </summary>
        public string LogId { get; set; }

        /// <summary>
        /// 表单Id
        /// </summary>
        public string FormId { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 跳转url
        /// </summary>
        public object strTargetUrl { get; set; }
    }

}