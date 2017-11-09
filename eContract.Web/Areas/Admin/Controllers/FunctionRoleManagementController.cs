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

namespace eContract.Web.Areas.Admin.Controllers
{
    public class FunctionRoleManagementController : AuthBaseController
    {
        public ActionResult Index(JqGrid grid, FormCollection data)
        {
            grid.ConvertParams(data);
            if (IsPost)
            {
                return Json(AjaxResult.Success(SystemService.FunctionRoleService.ForGrid("MDM",grid)));
            }
            return View();
        }
        public ActionResult About()
        {
            
            return View();
        }
        public ActionResult Welcome()
        {
            return View();
        }
        public ActionResult Edit(SecRoleEntity entity, string roleId)
        {
            string strError = "";
            entity.SystemName = "MDM";
            if (!IsPost)
            {
                entity = SystemService.FunctionRoleService.CreateFunctionRoleDomain("MDM").SecRoleEntity;
                if (!string.IsNullOrEmpty(roleId))
                {
                    entity = SystemService.FunctionRoleService.GetById<SecRoleEntity>(roleId);
                }
            }
            else
            {                
                var domian = SystemService.FunctionRoleService.CreateFunctionRoleDomain("MDM");
                domian.SecRoleEntity = entity;

                if (SystemService.FunctionRoleService.Save(domian, ref strError, "MDM"))
                {
                    return Json(AjaxResult.Success());
                }
                else {
                    return Json(AjaxResult.Error("Update failed"));
                }
            }
            ViewBag.strError = strError;
            return View(entity);
        }

        public JsonResult Delete(string deleteKeys)
        {
            string strError = "";
            if (SystemService.FunctionRoleService.DeleteRoleDomainByIds(deleteKeys, ref strError))
            {
                return Json(AjaxResult.Success());
            }
            return Json(AjaxResult.Error(strError));

        }

        public ActionResult Permissions(string roleId, string systemName)
        {
            ViewBag.RoleId = roleId;
            systemName = "MDM";
            //ViewBag.PageList = SystemService.PageService.GetAllPageTree(id, systemName);
            ViewBag.PageList = SystemService.PageService.GetPortalTree("0", systemName);
            return View();
        }

        public JsonResult Save(string RoleID, string strPageIds)
        {
            string strError = "";
            if (SystemService.RolePageService.Save(RoleID, strPageIds,ref strError))
            {
                return Json(AjaxResult.Success());
            }
            return Json(AjaxResult.Error(strError));
        }

        public ActionResult Export(LigerGrid grid, FormCollection data)
        {     
            string strError = "";
            grid.ConvertParams(data);
            eContract.Common.ExcelConvertHelper.ExcelContext context = new ExcelConvertHelper.ExcelContext();
            if (SystemService.FunctionRoleService.ExportExcel(grid, ref context, ref strError))
            {
                return Json(AjaxResult.Success());
            }
            return Json(AjaxResult.Error(strError));

        }

        public ActionResult GetRolePage(string roleid, string systemName)
        {
            systemName = "MDM";
            List<string> result=SystemService.PageService.GetPortalRoleTree(roleid, systemName);
            return Json(AjaxResult.Success(result));
        }

    }
}
