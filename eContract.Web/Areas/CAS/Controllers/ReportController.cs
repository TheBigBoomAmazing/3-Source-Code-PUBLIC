using eContract.BusinessService.BusinessData.Service;
using eContract.Common.Entity;
using Newtonsoft.Json;
using Suzsoft.Smart.Data;
using Suzsoft.Smart.EntityCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eContract.Web.Areas.CAS.Controllers
{
    public class ReportController : AuthBaseController
    {
        // GET: CAS/Report
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TemplateReviewProcessReport(string id)
        {
            CasContractEntity entity = new CasContractEntity();
            if (!string.IsNullOrEmpty(id))
            {
                var sql = @"
SELECT CAS_CONTRACT.CONTRACT_ID,CAS_CONTRACT.TEMPLATE_NO, CAS_CONTRACT.FERRERO_ENTITY,
CAS_CONTRACT.TEMPLATE_NAME,CAS_CONTRACT.CONTRACT_TYPE_NAME,
  CAS_CONTRACT.EFFECTIVE_DATE, CAS_CONTRACT.EXPIRATION_DATE,
CAS_CONTRACT.TEMPLATE_OWNER,CAS_CONTRACT.APPLY_DATE,CAS_CONTRACT.CREATE_TIME, 
CAS_CONTRACT.TEMPLATE_INITIATOR,CAS_CONTRACT.SCOPE_OF_APPLICATION
FROM CAS_CONTRACT
WHERE CAS_CONTRACT.CONTRACT_ID = @CONTRACT_ID";

                DataAccessParameterCollection param = new DataAccessParameterCollection();

                ColumnInfo columnInfo = new ColumnInfo("CONTRACT_ID", "CONTRACT_ID", true, typeof(string));
                param.AddWithValue(columnInfo, id);

                List<CasContractEntity> list = null;
                try
                {
                    list = DataAccess.Select<CasContractEntity>(sql, param);
                }
                catch (Exception)
                {
                    
                }

                if (list != null && list.Any())
                {
                    entity = list[0];
                    //CasContractTypeEntity casContractTypeEntity = BusinessDataService.ContractTypeManagementService.GetById<CasContractTypeEntity>(entity.ContractTypeId);
                    //ViewBag.ContractType = casContractTypeEntity;

                    //List<CasAttachmentEntity> list = BusinessDataService.CommonHelperService.GetFilesBySourceId(id);
                    //var mineFileIds = list.Where(f => f.AttachmentType == 1).ToList();
                    //var originalFileIds = list.Where(f => f.AttachmentType == 2).ToList();
                    //ViewBag.MineFiles = mineFileIds;
                    //ViewBag.OriginalFiles = originalFileIds;

                    #region 合同审批结果
                    //获取审批结果信息
                    DataTable approvalResultDt = BusinessDataService.CommonHelperService.GetReportApprovalResultDt(entity.ContractId);
                    JsonSerializerSettings setting = new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    };
                    var ret = JsonConvert.SerializeObject(approvalResultDt, setting);
                    ViewBag.ApprovalResult = ret;
                    #endregion
                }

            }
            return View(entity);
        }

        public ActionResult ContractReviewProcessReport(string id)
        {
            CasContractEntity entity = new CasContractEntity();
            if (!string.IsNullOrEmpty(id))
            {
                var sql = @"
SELECT CAS_CONTRACT.CONTRACT_ID, CAS_CONTRACT.CONTRACT_NO, CAS_CONTRACT.FERRERO_ENTITY,
  CAS_CONTRACT.CONTRACT_TYPE_NAME, CAS_CONTRACT.COUNTERPARTY_EN,
  CAS_CONTRACT.COUNTERPARTY_CN, CAS_CONTRACT.CONTRACT_NAME,
  CAS_CONTRACT.EFFECTIVE_DATE, CAS_CONTRACT.EXPIRATION_DATE,
  CAS_CONTRACT.CONTRACT_OWNER, CAS_CONTRACT.CONTRACT_INITIATOR,
  CAS_CONTRACT.APPLY_DATE,CAS_CONTRACT.CREATE_TIME, 
--CASE CAS_CONTRACT.Capex WHEN 1 THEN 'YES' ELSE 'NO' END AS CapexResult, 
Capex,
--CASE CAS_CONTRACT.IS_MASTER_AGREEMENT WHEN 1 THEN 'YES'
-- ELSE 'NO' END AS ISMASTERAGREEMENTResult,
IS_MASTER_AGREEMENT,
--CASE CAS_CONTRACT.SUPPLEMENTARY
 --   WHEN 1 THEN 'YES' ELSE 'NO'
 -- END AS SUPPLEMENTARYResult, 
SUPPLEMENTARY,
  -- case CAS_CONTRACT.BUDGET_TYPE WHEN 0 THEN 'Overheads' 
  -- WHEN 1 THEN 'Non-overheads' 
  -- WHEN 2 THEN 'industrial' 
  -- ELSE 'Error'
  --END AS BUDGET_TYPE,
BUDGET_TYPE,
  CAS_CONTRACT.TEMPLATE_NO, CAS_CONTRACT.CONTRACT_PRICE, 
CAS_CONTRACT.CURRENCY,
   --    case CAS_CONTRACT.CURRENCY 
  -- WHEN 1 THEN N'CNY' 
  -- WHEN 2 THEN N'USD' 
  -- WHEN 3 THEN N'EURO' 
  -- WHEN 4 THEN N'SGD' 
  -- WHEN 5 THEN N'HKD' 
  -- WHEN 6 THEN N'GBP' 
  -- WHEN 7 THEN N'CAD' 
  -- WHEN 8 THEN N'AUD' 
  -- WHEN 99 THEN CAS_CONTRACT.CURRENCY 
  -- ELSE ''
  --END AS CURRENCY,
  CAS_CONTRACT.PREPAYMENT_AMOUNT, CAS_CONTRACT.PREPAYMENT_PERCENTAGE,
  CAS_CONTRACT.CONTRACT_KEY_POINTS
FROM CAS_CONTRACT
WHERE CAS_CONTRACT.CONTRACT_ID = @CONTRACT_ID";

                DataAccessParameterCollection param = new DataAccessParameterCollection();

                ColumnInfo columnInfo = new ColumnInfo("CONTRACT_ID", "CONTRACT_ID", true, typeof(string));
                param.AddWithValue(columnInfo, id);

                List<CasContractEntity> list = null;
                try
                {
                    list = DataAccess.Select<CasContractEntity>(sql, param);
                }
                catch (Exception)
                {

                }

                if (list != null && list.Any())
                {
                    entity = list[0];
                    //CasContractTypeEntity casContractTypeEntity = BusinessDataService.ContractTypeManagementService.GetById<CasContractTypeEntity>(entity.ContractTypeId);
                    //ViewBag.ContractType = casContractTypeEntity;

                    //List<CasAttachmentEntity> list = BusinessDataService.CommonHelperService.GetFilesBySourceId(id);
                    //var mineFileIds = list.Where(f => f.AttachmentType == 1).ToList();
                    //var originalFileIds = list.Where(f => f.AttachmentType == 2).ToList();
                    //ViewBag.MineFiles = mineFileIds;
                    //ViewBag.OriginalFiles = originalFileIds;

                    #region 合同审批结果
                    //获取审批结果信息
                    DataTable approvalResultDt = BusinessDataService.CommonHelperService.GetReportApprovalResultDt(entity.ContractId);
                    JsonSerializerSettings setting = new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    };
                    var ret = JsonConvert.SerializeObject(approvalResultDt, setting);
                    ViewBag.ApprovalResult = ret;
                    #endregion
                }

            }
            return View(entity);
        }

    }
}