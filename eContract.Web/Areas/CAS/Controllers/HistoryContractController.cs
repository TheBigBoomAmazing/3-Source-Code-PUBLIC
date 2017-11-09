using eContract.BusinessService.BusinessData.Service;
using eContract.Common;
using eContract.Common.Entity;
using eContract.Common.MVC;
using Newtonsoft.Json;
using Suzsoft.Smart.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace eContract.Web.Areas.CAS.Controllers
{
    public class HistoryContractController : AuthBaseController
    {
        // GET: CAS/HistoryContract
        public ActionResult Index(JqGrid grid, FormCollection data)
        {
            grid.ConvertParams(data);
            if (IsPost)
            {
                return Json(AjaxResult.Success(BusinessDataService.ContractApplayService.ForHistoryGrid(grid)));
            }
            return View();
        }
        public ActionResult Edit(CasContractEntity entity, string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                entity = BusinessDataService.ContractApplayService.GetById<CasContractEntity>(id);
                List<CasAttachmentEntity> list = BusinessDataService.CommonHelperService.GetFilesBySourceId(id);
                string mineFileIds = "";
                string originalFileIds = "";
                list.Where(f => f.AttachmentType == 1).ToList().ForEach(f => mineFileIds += f.AttachmentId + ",");
                list.Where(f => f.AttachmentType == 2).ToList().ForEach(f => originalFileIds += f.AttachmentId + ",");
                ViewBag.MineFiles = mineFileIds.TrimEnd(',');
                ViewBag.OriginalFiles = originalFileIds.TrimEnd(',');
            }
            return PartialView(entity);
        }

        public JsonResult Save(CasContractEntity entity, string saveType, string fileIds_mine, string fileIds_original)
        {
            string msg = "";
            entity.ContractGroup = ContractGroupEnum.HistoryContract.GetHashCode();
            bool flag = BusinessDataService.ContractApplayService.Save(entity, saveType, fileIds_mine, fileIds_original, ref msg);
            return Json(flag ? AjaxResult.Success(msg) : AjaxResult.Error(msg)); ;
        }

        public JsonResult GetContractTypeList()
        {
            List<CasContractTypeEntity> list = BusinessDataService.ContractTypeManagementService.SelectEnabledContractType();
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var ret = JsonConvert.SerializeObject(list, setting);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllContractTypeList()
        {
            List<CasContractTypeEntity> list = BusinessDataService.ContractTypeManagementService.SelectAllContractType();
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var ret = JsonConvert.SerializeObject(list, setting);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 上传盖章合同
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult StampContract(JqGrid grid, FormCollection data)
        {
            grid.ConvertParams(data);
            if (IsPost)
            {
                return Json(AjaxResult.Success(BusinessDataService.ContractApplayService.UploadStampContract(grid)));
            }
            return View();
        }

        public ActionResult UploadStampContract(CasContractEntity entity, string id)
        {
            string strError = "";
            if (!IsPost)
            {
                entity = BusinessDataService.ContractApplayService.CreateContractEntity("MDM");
                if (!string.IsNullOrEmpty(id))
                {

                    entity = BusinessDataService.ContractApplayService.GetById<CasContractEntity>(id);
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
                if (BusinessDataService.ContractApplayService.SaveContractAttachment(entity, ref strError))
                {
                    return RedirectToAction("StampContract");
                }
                strError = "Update failed";
            }
            ViewBag.strError = strError;
            return View(entity);
        }

        /// <summary>
        /// 删除历史合同，且只能是Draft(未提交)的合同
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteHistoryContract(CasContractEntity entity, string id)
        {
            if (string.IsNullOrWhiteSpace(id)) {
                return Json(AjaxResult.Error());
            }
            else
            {
                var strsql = new StringBuilder();
                strsql.AppendFormat("DELETE FROM CAS_CONTRACT WHERE CONTRACT_ID= {0} AND STATUS='1'",
                    Utils.ToSQLStr(id).Trim());
                var val = DataAccess.SelectScalar(strsql.ToString());
                return Json(AjaxResult.Success());
            }           
        }


        public JsonResult GetUploadFiles(string contractId)
        {
            var selectedValue = BusinessDataService.ContractApplayService.GetUploadFiles(contractId);
            return Json(selectedValue, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取模板合同数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTemplateNoList()
        {
            List<CasContractEntity> list = BusinessDataService.ContractApplayService.GetTemplateNoList();
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var ret = JsonConvert.SerializeObject(list, setting);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取所有已经审批完成的合同数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetOrigialContractList()
        {
            List<CasContractEntity> list = BusinessDataService.ContractApplayService.GetOrigialContractList();
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var ret = JsonConvert.SerializeObject(list, setting);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckTemplateNo(string templateNo, string contractID)
        {
            bool flag = BusinessDataService.ContractApplayService.CheckTemplateNo(templateNo, contractID);
            return Json(flag ? AjaxResult.Success() : AjaxResult.Error(), JsonRequestBehavior.AllowGet); ;
        }
        /// <summary>
        /// 该合同类型的合同申请人是否有领导
        /// </summary>
        /// <param name="contractTypeId"></param>
        /// <returns></returns>
        public JsonResult CheckLeaderExist(string contractTypeId)
        {
            bool flag = BusinessDataService.ContractApplayService.CheckLIeaderExist(contractTypeId);
            return Json(flag ? AjaxResult.Success() : AjaxResult.Error(), JsonRequestBehavior.AllowGet); ;
        }

        /// <summary>
        /// 该合同类型的合同申请人是否有大区总监
        /// </summary>
        /// <param name="contractTypeId"></param>
        /// <returns></returns>
        public JsonResult CheckRegionManagerExist(string contractTypeId)
        {
            bool flag = BusinessDataService.ContractApplayService.CheckLRegionManagerExist(contractTypeId);
            return Json(flag ? AjaxResult.Success() : AjaxResult.Error(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 该合同类型的合同申请人是否有部门总监
        /// </summary>
        /// <param name="contractTypeId"></param>
        /// <returns></returns>
        public JsonResult CheckDEPManagerExist(string contractTypeId)
        {
            bool flag = BusinessDataService.ContractApplayService.CheckLDEPManagerExist(contractTypeId);
            return Json(flag ? AjaxResult.Success() : AjaxResult.Error(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 该合同类型的合同申请人是否有用户领导
        /// </summary>
        /// <param name="contractTypeId"></param>
        /// <returns></returns>
        public JsonResult CheckLineManagerExist(string contractTypeId)
        {
            bool flag = BusinessDataService.ContractApplayService.CheckLineManagerExist(contractTypeId);
            return Json(flag ? AjaxResult.Success() : AjaxResult.Error(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CloseContractByContractID(CasContractEntity entity, string id)
        {
            var a = CloseContractById(id);
            return Json(AjaxResult.Success());
            //return RedirectToAction("Index");
        }

        public bool CloseContractById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return false;
            var strsql = new StringBuilder();
            strsql.AppendFormat(" UPDATE CAS_CONTRACT SET STATUS='6' WHERE CONTRACT_ID={0} ",
                Utils.ToSQLStr(id).Trim());
            var val = DataAccess.ExecuteNoneQuery(strsql.ToString());
            return true;
        }
    }
}