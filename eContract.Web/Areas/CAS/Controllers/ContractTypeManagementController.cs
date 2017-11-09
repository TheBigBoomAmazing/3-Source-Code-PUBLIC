using System.Web;
using System.Web.Mvc;
using eContract.BusinessService.BusinessData.Service;
using eContract.BusinessService.SystemManagement.BusinessRule;
using eContract.BusinessService.SystemManagement.Service;
using eContract.Common;
using eContract.Common.Entity;
using eContract.Common.MVC;
using eContract.Common.WebUtils;
using System;

namespace eContract.Web.Areas.CAS.Controllers
{
    public class ContractTypeManagementController : AuthBaseController
    {
        /// <summary>
        /// 合同类型管理控制器
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult Index(JqGrid grid, FormCollection data)
        {
            grid.ConvertParams(data);
            if (IsPost)
            {
                return Json(AjaxResult.Success(BusinessDataService.ContractTypeManagementService.ForGrid(grid)));
            }
            return View();
        }

        public ActionResult Edit(CasContractTypeEntity entity, string id, string type)
        {

            if (type == "check")
            {
                ViewBag.Type = "3";
            }
            entity.Status = Convert.ToInt32(entity.submitType);
            string strError = "";
            if (!IsPost)
            {
                entity = BusinessDataService.ContractTypeManagementService.CreateContractTypeEntity("MDM");
                if (!string.IsNullOrEmpty(id))
                {

                    entity = BusinessDataService.ContractTypeManagementService.GetById<CasContractTypeEntity>(id);
                    //编辑
                    ViewBag.EditType = "0";
                }
                else
                {
                    //新增
                    ViewBag.EditType = "1";
                }
            }
            else
            {
                if (BusinessDataService.ContractTypeManagementService.SaveContractTypeEntity(entity, ref strError))
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
        public ActionResult Delet(CasContractTypeEntity entity, string id)
        {
            var a = BusinessDataService.ContractTypeManagementService.DeletContractTypeEntity(id);
            return Json(AjaxResult.Success());
        }
    }
}