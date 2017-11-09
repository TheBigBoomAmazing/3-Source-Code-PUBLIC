using eContract.BusinessService.BusinessData.Service;
using eContract.BusinessService.SystemManagement.Service;
using eContract.Common;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using eContract.Common.Entity;
using eContract.Common.WebUtils;
using eContract.BusinessService.SystemManagement.Domain;
using Suzsoft.Smart.Data;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace eContract.Web.Areas.CAS.Controllers
{
    public class EmailController : AuthBaseController
    {
        // GET: CAS/Email
        public ActionResult Index()
        {
            return View();
        }

        ///// <summary>
        ///// 发送邮件
        ///// </summary>
        ///// <param name="title">邮件主题</param>
        ///// <param name="content">邮件内容</param>
        ///// <param name="email">要发送对象的邮箱</param>
        ///// <returns></returns>
        //public bool SendMail(string title, string content, string email)
        //{
        //    return SendMail(title, content, email,"");
        //}

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="title">邮件主题</param>
        /// <param name="content">邮件内容</param>
        /// <param name="email">要发送对象的邮箱</param>
        /// <param name="cc">要抄送对象的邮箱</param>        
        /// <returns></returns>
        public bool SendMail(string title, string content,string  emails,string cc)
        {
            return SendEmail.Send(emails, title, content);

            ////创建发送邮箱的对象
            //SendEmail myEmail = new SendEmail(senderServerIp, toMailAddress, ccMailAddress, fromMailAddress, subjectInfo, bodyInfo, mailUsername, mailPassword, mailPort, true, false);

            //添加附件
            //email.AddAttachments(attachPath);

            //return myEmail.Send();
        }
    }
}