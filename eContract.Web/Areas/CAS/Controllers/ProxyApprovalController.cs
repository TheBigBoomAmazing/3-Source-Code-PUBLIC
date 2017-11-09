using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eContract.Common;
using eContract.BusinessService;
using eContract.Common.Entity;
using eContract.BusinessService.SystemManagement.Service;
using eContract.Common.WebUtils;
using System.Text;
using eContract.Common.MVC;
using Suzsoft.Smart.EntityCore;
using Suzsoft.Smart.Data;
using eContract.BusinessService.BusinessData.Service;
using Newtonsoft.Json;

namespace eContract.Web.Areas.CAS.Controllers
{
    public class ProxyApprovalController : AuthBaseController
    {
        public ActionResult Index(JqGrid grid, FormCollection data)
        {
            grid.ConvertParams(data);
            if (IsPost)
            {
                return Json(AjaxResult.Success(BusinessDataService.ProxyApprovalService.ForGrid(grid)));
            }
            return View();
        }

        public ActionResult Edit(CasProxyApprovalEntity entity, string id = "")
        {
            if (WebCaching.IsAdmin!="True")
            {
                entity.AuthorizedUserId = CurrentUser.CasUserEntity.UserId;
            }
            string strError = "";
            if (!IsPost)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    entity = BusinessDataService.ProxyApprovalService.GetById<CasProxyApprovalEntity>(id);
                    ViewBag.AuthorizedUserName = SystemService.UserService.GetById<CasUserEntity>(entity.AuthorizedUserId).EnglishName;
                }
                else
                {
                    entity = new CasProxyApprovalEntity();
                    entity.AuthorizedUserId = WebCaching.UserId;
                    ViewBag.AuthorizedUserName = SystemService.UserService.GetById<CasUserEntity>(entity.AuthorizedUserId).EnglishName;
                }
            }
            else
            {
                if (BusinessDataService.ProxyApprovalService.Save(entity, ref strError))
                {
                    return Json(AjaxResult.Success());
                }
                else
                {
                    strError = "Update failed";
                    return Json(AjaxResult.Success(strError));
                }
            }
            ViewBag.strError = strError;
            ViewBag.IsAdmin = WebCaching.IsAdmin;
            return View(entity);
        }

        public JsonResult Delete(string deleteKeys)
        {
            string strError = "";
            if (BusinessDataService.ProxyApprovalService.DeleteProxyApprovalByIds(deleteKeys, ref strError))
            {
                return Json(AjaxResult.Success());
            }
            return Json(AjaxResult.Error(strError));

        }
        public JsonResult GetAgentUserList(string proxyApprovalId)
        {
            var selectedValue = BusinessDataService.ProxyApprovalService.GetAgentUserList(proxyApprovalId);
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var ret = JsonConvert.SerializeObject(selectedValue, setting);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAuthorizedUserList(string proxyApprovalId)
        {
            var selectedValue = BusinessDataService.ProxyApprovalService.GetAuthorizedUserList(proxyApprovalId);
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var ret = JsonConvert.SerializeObject(selectedValue, setting);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 中止代理审批操作
        /// </summary>
        /// <param name="id">proxyApprovalId</param>
        /// <returns></returns>
        public ActionResult TerminationDelegation(string id)
        {
            var flag = BusinessDataService.ProxyApprovalService.TerminationDelegationById(id);  
            return Json(flag ? AjaxResult.Success() : AjaxResult.Error());
            
        }

        
    }
}
