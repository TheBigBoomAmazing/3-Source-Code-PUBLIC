using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Suzsoft.Smart.EntityCore;



namespace eContract.Common
{
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
    public class AjaxResult
    {
        public MsgHeader msgHeader { get; set; }

        public object msgBody { get; set; }
        /// <summary>
        /// 信息返回
        /// </summary>
        /// <param name="header"></param>
        /// <param name="msgbody"></param>
        /// <returns></returns>
        public static AjaxResult Result(MsgHeader header,object msgbody)
        {
            return new AjaxResult()
            {
                msgHeader = header,
                msgBody=msgbody
            };
        }
        
        #region Error
        public static AjaxResult Error()
        {
            return new AjaxResult()
            {
                msgHeader = new MsgHeader { IsError = true }
            };
        }
        public static AjaxResult Error(string message)
        {
            return new AjaxResult()
            {
                msgHeader = new MsgHeader
                {
                    IsError = true,
                    StatusCode = -1,
                    Message = message
                }
            };
        }
        public static AjaxResult Error(string message, int statusCode)
        {
            return new AjaxResult()
            {
                msgHeader = new MsgHeader
                  {
                      IsError = true,
                      StatusCode = statusCode,
                      Message = message
                  }
            };
        }
        #endregion

        #region Success
        public static AjaxResult Success()
        {
            return new AjaxResult()
            {
                msgHeader = new MsgHeader
                 {
                     IsError = false,
                     StatusCode = 0
                 }
            };
        }
        public static AjaxResult Success(string message)
        {
            return new AjaxResult()
            {
                msgHeader = new MsgHeader
                 {
                     IsError = false,
                     StatusCode = 0,
                     Message = message
                 }
            };
        }
        public static AjaxResult Success(object data)
        {
            return new AjaxResult()
            {
                msgHeader = new MsgHeader
                 {
                     IsError = false,
                     StatusCode = 0
                 },
                msgBody = data
            };
        }
        public static AjaxResult Success(object data, string message)
        {
            return new AjaxResult()
            {
                msgHeader = new MsgHeader
                {
                    IsError = false,
                    StatusCode = 0,
                    Message = message
                },
                msgBody = data,
            };
        }
        public static AjaxResult SuccessEntity(EntityBase data)
        {
            return new AjaxResult()
            {
                msgHeader = new MsgHeader
                {
                    IsError = false,
                    StatusCode = 0
                },
                msgBody = data.DataCollection
            };
        }
        public static AjaxResult Success(EntityBase data, string message)
        {

            return new AjaxResult()
            {
                msgHeader = new MsgHeader
                   {
                       IsError = false,
                       StatusCode = 0,
                       Message = message
                   },
                msgBody = data.DataCollection
            };
        }
        #endregion

        

        public override string ToString()
        {
            return new JavaScriptSerializer().Serialize(this);
        }
    }
}