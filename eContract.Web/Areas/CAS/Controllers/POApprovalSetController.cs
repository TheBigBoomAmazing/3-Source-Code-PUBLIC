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
using Newtonsoft.Json;

namespace eContract.Web.Areas.CAS.Controllers
{
    public class POApprovalSetController :  AuthBaseController
    {
        // GET: CAS/POApprovalSet
        public ActionResult Index(JqGrid grid, FormCollection data)
        {
            grid.ConvertParams(data);
            if (IsPost)
            {
                return Json(AjaxResult.Success(BusinessDataService.POApprovalSetService.ForGrid(grid)));
            }
            return View();
        }
        public ActionResult Edit(CasPoApprovalSettingsEntity entity, string id, string type)
        {
            string typeValue = "";
            if (type == "check")
            {
                ViewBag.Type = "3";
                typeValue = "3";
            }
            string strError = "";
            if (!IsPost)
            {
                entity = BusinessDataService.POApprovalSetService.CreatePoApprovalSettEntity("MDM");
                if (!string.IsNullOrEmpty(id))
                {

                    entity = BusinessDataService.POApprovalSetService.GetById<CasPoApprovalSettingsEntity>(id);
                    //编辑
                    ViewBag.EditType = (!string.IsNullOrWhiteSpace(typeValue)) ? typeValue : "0";
                }
                else
                {
                    //新增
                    ViewBag.EditType = "1";
                }
            }
            else
            {
                if (BusinessDataService.POApprovalSetService.SavePOApprovalSetEntity(entity, ref strError))
                {
                    return Json(AjaxResult.Success());
                }
                    return Json(AjaxResult.Error("Update failed"));
            }
            ViewBag.strError = strError;
            return View(entity);
        }

        public ActionResult Delet(CasPoApprovalSettingsEntity entity, string id, string company)
        {
            var a = BusinessDataService.POApprovalSetService.DeletePoApprovalSet(id, company);
            return Json(AjaxResult.Success());
        }
        public JsonResult GetSelectedApproUser(string approvalId, string contractId,string company)
        {
            var selectedValue = BusinessDataService.POApprovalSetService.GetPOApprovalSelectUser(approvalId, contractId, company);
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var ret = JsonConvert.SerializeObject(selectedValue, setting);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }


    }
}