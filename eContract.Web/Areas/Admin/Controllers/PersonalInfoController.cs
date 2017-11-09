using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eContract.Common.Entity;
using eContract.BusinessService.SystemManagement.Service;
using eContract.Common.WebUtils;
using eContract.Common;

namespace eContract.Web.Areas.Admin.Controllers
{
    public class PersonalInfoController : AuthBaseController
    {
        public ActionResult Index()
        {
            string strError = "";
            CasUserEntity entity = SystemService.UserService.GetById<CasUserEntity>(WebCaching.UserId);
            if (!IsPost)
            {
                ViewBag.OriPassword = Encryption.Decrypt(entity.Password);
            }
            else
            {
                var domian = SystemService.UserService.CreateUserDomain("MDM");
                domian.CasUserEntity = entity;

                string newPassword = Request.Form["newPassword"].ToString();

                domian.CasUserEntity.Password = Encryption.Encrypt(newPassword);

                if (SystemService.UserService.Save(domian, ref strError))
                {
                    ViewBag.OriPassword = newPassword;

                    ViewBag.SaveResult = "T";
                    ViewBag.PromptInfo = "保存成功。";

                    WebCaching.UserCaching = null;
                }
                else
                {
                    strError = "保存失败, " + strError;
                    ViewBag.OriPassword = Encryption.Decrypt(entity.Password);
                    ViewBag.SaveResult = "F";
                    ViewBag.PromptInfo = strError;
                }
            }
            return View();
        }
    }
}
