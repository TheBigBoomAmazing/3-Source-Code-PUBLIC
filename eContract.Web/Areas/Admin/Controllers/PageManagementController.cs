using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eContract.Common;
using eContract.Common.Entity;
using eContract.BusinessService.SystemManagement.Service;
using eContract.Common.MVC;

namespace eContract.Web.Areas.Admin.Controllers
{
    public class PageManagementController : AuthBaseController
    {
        public ActionResult Index(JqGrid grid, FormCollection data)
        {
            grid.ConvertParams(data);
            if (IsPost)
            {
                return Json(AjaxResult.Success(SystemService.PageService.ForGrid("MDM", grid)));
            }
            return View();
        }

        public ActionResult Edit(SecPageEntity entity, string id, string systemName)
        {
            systemName = "MDM";
            ViewBag.PageList = SystemService.PageService.GetPageList(systemName); //SystemService.PageService.GetParentByPageList("0");
            string strError = "";
            if (!IsPost)
            {
                entity = SystemService.PageService.CreatePageDomain(systemName).SecPageEntity;
                if (!string.IsNullOrEmpty(id))
                {
                    entity = SystemService.PageService.GetById<SecPageEntity>(id);
                }
            }
            else
            {
                var domian = SystemService.PageService.CreatePageDomain(systemName);
                domian.SecPageEntity = entity;
                domian.SecPageEntity.SystemName = systemName;
                if (SystemService.PageService.Save(domian, ref strError))
                {
                    return Json(AjaxResult.Success());
                }
                else
                {
                    return Json(AjaxResult.Error("Update failed"));
                }
            }
            ViewBag.strError = strError;
            return View(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deleteKeys"></param>
        /// <returns></returns>
        public JsonResult Delete(string deleteKeys)
        {
            string strError = "";
            if (SystemService.PageService.DeletePageDomainByIds(deleteKeys, ref strError))
            {
                return Json(AjaxResult.Success());
            }
            return Json(AjaxResult.Error(strError));

        }



    }
}
