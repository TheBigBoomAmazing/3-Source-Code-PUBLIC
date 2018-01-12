using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using eContract.BusinessService.SystemManagement.Domain;
using eContract.Common.Entity;
using eContract.BusinessService.SystemManagement.Service;
using eContract.BusinessService.SystemManagement.BusinessRule;
using eContract.Common.WebUtils;
using System.Text;
using ComixSDK.EDI.Utils;
using eContract.Common;
using System.IO;
using eContract.Web.Common;

namespace eContract.Web.Controllers
{

    public class AccountController : BaseController
    {


        private string LANG_COOKIE_ID = "XPOS_LANG";
        // **************************************
        // URL: /Account/LogOn
        // **************************************
        public ActionResult Login(CasUserEntity userEntity, string UserAccount, string password, string currentLanguage, string returnUrl, string paras, string viewName = "LoginV3")
        {
            string strError = "";
            string ssologin = ComixSDK.EDI.Utils.ConfigHelper.GetConfigString("SSOLogin");

            string SSOUserId = "";
            string AUTHUserId = "";
            string SSOToken = "";
            object bindModel = null;
            if (!string.IsNullOrEmpty(paras))
            {
                //SortedDictionary<string, string> parasDic = new SortedDictionary<string, string>();
                //parasDic.Add("SSOUserId", "");
                //parasDic.Add("AUTHUserId", Guid.NewGuid().ToString().Replace("-", "").ToLower());
                //parasDic.Add("SSOToken", "");
                //string newparas = JSONHelper.ToJson(parasDic);
                //newparas = ComixSDK.EDI.Utils.EncryptionService.EncryptDES(newparas, eContract.Common.ConfigHelper.GetSetString("EncryptionKey"));
                //paras = newparas;

                paras = ComixSDK.EDI.Utils.EncryptionService.DecryptDES(paras, eContract.Common.ConfigHelper.GetSetString("EncryptionKey"));
                SortedDictionary<string, object> sd = ComixSDK.EDI.Utils.JSONHelper.FromJson<SortedDictionary<string, object>>(paras);
                SSOUserId = sd["SSOUserId"] == null ? "" : sd["SSOUserId"].ToString();
                bindModel = sd["BindModel"];
                SSOToken = sd["SSOToken"] == null ? "" : sd["SSOToken"].ToString();
            }

            ViewBag.Paras = paras;
            ViewBag.ReturnUrl = returnUrl;
            if (IsPost)

                //参数解密
                //SSOUserId = ComixSDK.EDI.Utils.EncryptionService.DecryptDES(SSOUserId, eContract.Common.ConfigHelper.GetSetString("EncryptionKey"));
                //AUTHUserId = ComixSDK.EDI.Utils.EncryptionService.DecryptDES(AUTHUserId, eContract.Common.ConfigHelper.GetSetString("EncryptionKey"));
                if (IsPost || !string.IsNullOrEmpty(SSOUserId))
                {
                    WebCaching.UserCaching = null;
                    UserDomain userDomain = null;
                    if (!string.IsNullOrEmpty(SSOUserId)) //获取第三方登录用户
                    {
                        userEntity = SystemService.UserService.GetUserEntity(SSOUserId);
                        if (userEntity == null)
                        {
                            return View(viewName, userEntity);
                        }
                        userDomain = SystemService.UserService.GetUserDomainByUserAccount("MDM", userEntity.UserAccount);
                    }
                    else
                    {
                        userDomain = SystemService.UserService.GetUserDomainByUserAccount("MDM", userEntity.UserAccount);
                    }

                    if (userDomain == null || Encryption.Decrypt(userDomain.CasUserEntity.Password) != password)
                    {
                        ViewBag.strError = "您输入的账号或密码错误,请重新输入";
                        //strError = "您输入的账号或密码错误,请重新输入";
                        //return Json(AjaxResult.Error(strError));
                        return View(viewName, userEntity);
                    }
                    //if (userDomain.CasUserEntity.IsDelete == 1)
                    //{
                    //    strError = "该用户已被删除";
                    //    return Json(AjaxResult.Error(strError));
                    //}

                    string CacheKey = "User_" + UserId;
                    CacheHelper.Instance.Remove(CacheKey);
                    WebCaching.UserId = userDomain.CasUserEntity.UserId;
                    WebCaching.UserAccount = userDomain.CasUserEntity.UserAccount;
                    WebCaching.IsAdmin = userDomain.CasUserEntity.IsAdmin.ToString();
                    WebCaching.SystemName = "MDM";
                    userDomain.MenuDataItems = SystemService.FunctionRoleService.GetMenuDataItemByUserId("MDM", WebCaching.UserId);
                    this.CurrentUser = userDomain;
                    ComixSDK.EDI.Utils.CookieHelper.AddCookie(LANG_COOKIE_ID, currentLanguage, DateTime.Now.AddYears(1));
                    WebCaching.CurrentLanguage = currentLanguage;
                    FormsAuthentication.SetAuthCookie(userDomain.CasUserEntity.UserAccount, false);
                    userEntity = userDomain.CasUserEntity;
                    BusinessRoleBLL userroleBll = new BusinessRoleBLL();
                    List<SecUserRoleEntity> listUserRoles = userroleBll.GetBusinessRoleUserDomainByUserid(userDomain.CasUserEntity.UserId);
                    //普通员工添加默认权限
                    if (listUserRoles == null || listUserRoles.Count <= 0)
                    {
                        listUserRoles = userroleBll.GetBusinessRoleUserDomainByRoleId("cd3c9135-4446-45c5-b768-550abac4368d");
                        //strError = "您输入的账号没有权限,请重新输入";
                        //if (!IsAjax)
                        //{
                        //    return View(viewName, userEntity);
                        //}
                    }
                    if (!IsAjax)
                    {
                        return Redirect("~/home");
                    }

                    //strError = GetResource(strError);
                    ViewBag.strError = strError;
                    userEntity.Password = password;

                    if (IsAjax)
                    {
                        if (string.IsNullOrEmpty(strError))
                        {
                            return Json(AjaxResult.Success());
                        }
                        return Json(AjaxResult.Error(strError));
                    }
                }
            return View(viewName, userEntity);
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            SystemService.UserService.Logout();
            string CacheKey = "User_" + UserId;
            CacheHelper.Instance.Remove(CacheKey);
            //string ssologin = ComixSDK.EDI.Utils.ConfigHelper.GetConfigString("SSOLogin");
            //if (!string.IsNullOrWhiteSpace(ssologin) && !WebCaching.IsDebug)
            //{
            //    string newreturnUrl = Request.Url.Scheme + "://" + Request.Url.Authority + "/" + FormsAuthentication.DefaultUrl;
            //    return Redirect(ComixSDK.EDI.Utils.ConfigHelper.GetConfigString("SSOLogin") + "/Account/LogOff?returnUrl=" + newreturnUrl);
            //}
            //return RedirectToAction("Login");
            return Redirect("~/home");
        }

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
    }
}
