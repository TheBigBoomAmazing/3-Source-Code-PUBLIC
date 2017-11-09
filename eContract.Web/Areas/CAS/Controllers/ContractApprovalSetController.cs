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
    public class ContractApprovalSetController : AuthBaseController
    {
        // GET: CAS/ContractApprovalSet
        public ActionResult Index(JqGrid grid, FormCollection data)
        {
            grid.ConvertParams(data);
            if (IsPost)
            {
                return Json(AjaxResult.Success(BusinessDataService.ContractApprovalSetService.ForGrid(grid)));
            }
            return View();
        }

        public ActionResult Edit(CasContractApprovalStepEntity entity, string id, string type)
        {
            string typeValue = "";
            if (type == "check")
            {
                ViewBag.Type = "3";
                typeValue = "3";
            }
            //entity.Status = Convert.ToInt32(entity.submitType);
            string strError = "";
            if (!IsPost)
            {
                entity = BusinessDataService.ContractApprovalSetService.CreateContractApprovalSetEntity("MDM");
                if (!string.IsNullOrEmpty(id))
                {

                    entity = BusinessDataService.ContractApprovalSetService.GetById<CasContractApprovalStepEntity>(id);
                    //编辑
                    var exist = BusinessDataService.ContractApprovalSetService.CheckInProcessContract(entity.ContractApprovalStepId);
                    if (!exist)
                    {
                        ViewBag.Exist = "1";//存在审批中的合同
                    }
                    ViewBag.EditType = (!string .IsNullOrWhiteSpace(typeValue))? typeValue:"0";
                    
                }
                else
                {
                    //新增
                    ViewBag.EditType = "1";
                }
                return View(entity);
            }
            else
            {
                if (BusinessDataService.ContractApprovalSetService.SaveContractApprovalSetEntity(entity, ref strError))
                {
                    return Json(AjaxResult.Success());
                }
                else {
                    strError = "Update failed";
                    return Json(AjaxResult.Error(strError));
                }
                //ViewBag.strError = strError;
                //return View(new CasContractApprovalStepEntity());
            }
        }


        public ActionResult QueryApprovalUser(JqGrid grid, FormCollection data, string id, string appSetid)
        {
            var entity = BusinessDataService.ContractApprovalSetService.GetById<CasContractApprovalStepEntity>(appSetid);
            ViewData["CONTRACT_APPROVAL_STEP_ID"] = entity.ContractApprovalStepId;
            ViewData["COMPANY"] = entity.Company;
            ViewData["CONTRACT_TYPE_NAME"] = entity.ContractTypeId;
            grid.ConvertParams(data);
            if (IsPost)
            {
                grid.keyWord = appSetid;
                return Json(AjaxResult.Success(BusinessDataService.ContractApprovalSetService.QueryApprovalUser(grid)));
            }
            return View();
        }
        /// <summary>
        /// 根据合同类型ID获得存在的审批步骤
        /// </summary>
        /// <param name="ontractTypeId"></param>
        /// <returns></returns>
        public JsonResult GetExistStep(string contractTypeId)
        {
            if (string.IsNullOrWhiteSpace(contractTypeId))
            {
                return new JsonResult();
            }
            var selectedValue = BusinessDataService.ContractApprovalSetService.GetExistApprovalStepByContractTypeId(contractTypeId);
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var ret = JsonConvert.SerializeObject(selectedValue, setting);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获得合同类型申请部门/用户的详细数据
        /// </summary>
        /// <param name="stepId"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public JsonResult GetSelectedRole(string stepId,int? role)
        {
            if (string.IsNullOrWhiteSpace(stepId))
            {
                return new JsonResult();
            }
            var selectedValue = BusinessDataService.ContractApprovalSetService.GetContractTypeApplyUser(stepId, role);
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var ret = JsonConvert.SerializeObject(selectedValue, setting);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSelectedApprovalRole(string ApprovalStepId,string stepId, int? role)
        {
            if (string.IsNullOrWhiteSpace(stepId))
            {
                return new JsonResult();
            }
            var selectedValue = BusinessDataService.ContractApprovalSetService.GetContractTypeApprovalDep(ApprovalStepId,stepId, role);
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var ret = JsonConvert.SerializeObject(selectedValue, setting);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获得合同类型的申请用户/部门
        /// </summary>
        /// <param name="contractTypeId"></param>
        /// <returns></returns>
        public JsonResult GetContractApprovalStepRole(string contractTypeId)
        {
            var selectedValue = BusinessDataService.ContractApprovalSetService.GetContractTypeRole(contractTypeId);
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var ret = JsonConvert.SerializeObject(selectedValue, setting);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delet(CasContractApprovalStepEntity entity, string id)
        {
            var a = BusinessDataService.ContractApprovalSetService.DeleteApprovalStep(id);
            return Json(AjaxResult.Success());
        }
    }
}