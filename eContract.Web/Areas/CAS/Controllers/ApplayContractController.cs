using eContract.BusinessService.BusinessData.Service;
using eContract.Common;
using eContract.Common.Entity;
using eContract.Common.MVC;
using Newtonsoft.Json;
using Suzsoft.Smart.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
//当前页面用来测试分支了 
namespace eContract.Web.Areas.CAS.Controllers
{
    public class ApplayContractController : AuthBaseController
    {
        // GET: CAS/ApplayContract
        public ActionResult Index(JqGrid grid, FormCollection data)
        {
            grid.ConvertParams(data);
            if (IsPost)
            {
                return Json(AjaxResult.Success(BusinessDataService.ContractApplayService.ForGrid(grid)));
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


        public ActionResult ItemDetails(CasContractEntity entity, string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                string sqlstring = $@"SELECT CONTRACT_ID,CONTRACT_SERIAL_NO,STATUS,ORIGINAL_CONTRACT_ID,SUPPLIER,CONTRACT_GROUP,MODIFICATION_POINTS,CONTRACT_TYPE_ID,CONTRACT_TYPE_NAME,NEED_COMMENT,NOT_DISPLAY_IN_MY_SUPPORT,IS_TEMPLATE_CONTRACT,CONTRACT_NAME,CONTRACT_TERM,CONTRACT_OWNER,CONTRACT_INITIATOR,FERRERO_ENTITY,COUNTERPARTY_EN,COUNTERPARTY_CN,EFFECTIVE_DATE,EXPIRATION_DATE,IS_MASTER_AGREEMENT,TAX,CONTRACT_PRICE,ESTIMATED_PRICE,CURRENCY,CAPEX,SUPPLEMENTARY,BUDGET_AMOUNT,INTERNAL_INVERTMENT_ORDER,REFERENCE_CONTRACT,PREPAYMENT_AMOUNT,PREPAYMENT_PERCENTAGE,CONTRACT_KEY_POINTS,BUDGET_TYPE,CASE contract.IS_TEMPLATE_CONTRACT
                    when 1
                    then contract.TEMPLATE_NO
                    else 
                    (select template.TEMPLATE_NO from CAS_CONTRACT template
                    where contract.TEMPLATE_NO = template.CONTRACT_ID)
                    end                    TEMPLATE_NO,TEMPLATE_NAME,TEMPLATE_TERM,TEMPLATE_OWNER,TEMPLATE_INITIATOR,SCOPE_OF_APPLICATION,APPLY_DATE,IS_DELETED,CREATED_BY,CREATE_TIME,LAST_MODIFIED_BY,LAST_MODIFIED_TIME  FROM CAS_CONTRACT contract WHERE contract.CONTRACT_ID='{id}'";
                entity = DataAccess.Select<CasContractEntity>(sqlstring).FirstOrDefault();
                //entity = BusinessDataService.ContractApplayService.GetById<CasContractEntity>(id);
                CasContractTypeEntity casContractTypeEntity = BusinessDataService.ContractTypeManagementService.GetById<CasContractTypeEntity>(entity.ContractTypeId);
                ViewBag.ContractType = casContractTypeEntity;

                List<CasAttachmentEntity> list = BusinessDataService.CommonHelperService.GetFilesBySourceId(id);
                var mineFileIds = list.Where(f => f.AttachmentType == 1).ToList();
                var originalFileIds = list.Where(f => f.AttachmentType == 2).ToList();
                ViewBag.MineFiles = mineFileIds;
                ViewBag.OriginalFiles = originalFileIds;

                #region 合同审批结果
                //获取审批结果信息
                DataTable approvalResultDt = BusinessDataService.CommonHelperService.GetApprovalResultDt(entity.ContractId);
                JsonSerializerSettings setting = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                var ret = JsonConvert.SerializeObject(approvalResultDt, setting);
                ViewBag.ApprovalResult = ret;
                #endregion
            }
            return View(entity);
        }

        /// <summary>
        /// 合同查看页得到审批列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetContractApprovalForm(JqGrid grid, FormCollection data, string id, string conid)
        {
            //var entity = BusinessDataService.ContractManagementService.GetById<CasContractEntity>(id);
            //ViewData["CONTRACT_ID"] = entity.ContractId;
            //ViewData["FERRERO_ENTITY"] = entity.FerreroEntity;
            //ViewData["CONTRACT_GROUP"] = entity.ContractGroup;
            //ViewData["CONTRACT_NAME"] = entity.ContractName;
            //ViewData["CONTRACT_INITIATOR"] = entity.ContractInitiator;
            //ViewData["CONTRACT_OWNER"] = !string.IsNullOrWhiteSpace(entity.ContractOwner.ToString()) ? entity.ContractOwner : entity.TemplateOwner;
            //ViewData["COUNTERPARTY_CN"] = entity.CounterpartyCn;
            //ViewData["CONTRACT_PRICE"] = entity.ContractPrice;
            //ViewData["EFFECTIVE_DATE"] = entity.EffectiveDate;
            //ViewData["EXPIRATION_DATE"] = entity.ExpirationDate;
            //ViewData["IsTemplateContract"] = entity.IsTemplateContract;
            //ViewData["Status"] = entity.Status;
            grid.ConvertParams(data);
            if (IsPost)
            {
                grid.keyWord = conid;
                return Json(AjaxResult.Success(BusinessDataService.ContractManagementService.GetContractApprolvalRes(grid)));
            }
            return View();
        }


        public JsonResult Save(CasContractEntity entity, string saveType, string fileIds_mine, string fileIds_original)
        {
            string msg = "";
            entity.ContractGroup = ContractGroupEnum.NormalContract.GetHashCode();
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

        public JsonResult GetAllSubmitContractTypeList()
        {
            List<CasContractTypeEntity> list = BusinessDataService.ContractTypeManagementService.SelectAllSubmitContractType();
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                
            };
            var ret = JsonConvert.SerializeObject(list, setting);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 合同草稿箱
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult ContractDrafts(JqGrid grid, FormCollection data)
        {
            grid.ConvertParams(data);
            if (IsPost)
            {
                return Json(AjaxResult.Success(BusinessDataService.ContractApplayService.GetContractDrafts(grid)));
            }
            return View();
        }
        /// <summary>
        /// 草稿箱的删除
        /// </summary>
        /// <param name="deleteKeys"></param>
        /// <returns></returns>
        public JsonResult Delete(string deleteKeys)
        {
            List<string> list = deleteKeys.Split(new char[] { ';', ',' }).ToList<string>();
            if (BusinessDataService.ContractApplayService.DeleteContractById(list))
            {
                return Json(AjaxResult.Success());
            }
            return Json(AjaxResult.Error("删除合同失败"));
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
                    return Json(AjaxResult.Success());
                }
                else
                {
                    strError = "Update failed";
                    return Json(AjaxResult.Success(strError));
                }
            }
            ViewBag.strError = strError;
            return View(entity);
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

        public JsonResult CheckTemplateNo(string templateNo,string contractID)
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

        public  bool CloseContractById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return false;
            var strsql = new StringBuilder();
            strsql.AppendFormat(" UPDATE CAS_CONTRACT SET STATUS='6' WHERE CONTRACT_ID={0} ",
                Utils.ToSQLStr(id).Trim());
            var val = DataAccess.ExecuteNoneQuery(strsql.ToString());
            #region 合同申请用户操作记录
            BusinessDataService.ContractApplayService.AddResultInApprovalResult(id,5,5);
            #endregion
            return true;
        }
    }
}