using eContract.BusinessService.BusinessData.Service;
using eContract.Common;
using eContract.Common.Entity;
using eContract.Common.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace eContract.Web.Areas.CAS.Controllers
{
    public class ContractCommentController : AuthBaseController
    {
        // GET: CAS/ContractComment
        public ActionResult Index(JqGrid grid, FormCollection data)
        {
            grid.ConvertParams(data);
            if (IsPost)
            {
                return Json(AjaxResult.Success(BusinessDataService.ContractCommentService.ForGrid(grid)));
            }
            return View();
        }

        public ActionResult Edit(string id)
        {
            //合同信息
            CasContractEntity entity = new CasContractEntity();
            if (!string.IsNullOrEmpty(id))
            {
                //获取合同正文信息
                entity = BusinessDataService.ContractApplayService.GetById<CasContractEntity>(id);
                CasContractTypeEntity casContractTypeEntity = BusinessDataService
                    .ContractTypeManagementService
                    .GetById<CasContractTypeEntity>(entity.ContractTypeId);
                ViewBag.ContractType = casContractTypeEntity;
                //获取合同附件
                List<CasAttachmentEntity> list = BusinessDataService.CommonHelperService.GetFilesBySourceId(entity.ContractId);
                var mineFiles = list.Where(f => f.AttachmentType == 1).ToList();
                ViewBag.MineFiles = mineFiles;
                var originalFiles = new List<CasAttachmentEntity>();
                if (!string.IsNullOrEmpty(entity.OriginalContractId))
                {
                    originalFiles = BusinessDataService.CommonHelperService.GetFilesBySourceId(entity.OriginalContractId).Where(f => f.AttachmentType == 1).ToList();
                }
                else
                {
                    originalFiles = list.Where(f => f.AttachmentType == 2).ToList();
                }
                ViewBag.OriginalFiles = originalFiles;

                //获取模板编号合同数据
                CasContractEntity templateNoContract = new CasContractEntity();
                if (!(bool)casContractTypeEntity.IsTemplateContract && (bool)casContractTypeEntity.TemplateNo)
                {
                    templateNoContract = BusinessDataService.ContractApplayService.GetById<CasContractEntity>(entity.TemplateNo);
                }
                ViewBag.templateNoContract = templateNoContract;
            }
            return View(entity);
        }

        //public JsonResult GetCommentResults(JqGrid grid, string id)
        //{
        //    //领导批注结果
        //    DataTable dt = BusinessDataService.ContractCommentService.GetCommentResultDt(id);
        //    grid.DataBind(dt, 10);
        //    return Json(AjaxResult.Success(BusinessDataService.ContractCommentService.ForGrid(grid)));
        //}

        /// <summary>
        /// 领导批注
        /// </summary>
        /// <returns></returns>
        public JsonResult OptionComment(string option, string contractId)
        {
            CasContractApproverEntity casContractApproverEntity = new CasContractApproverEntity
            {
                ContractId = contractId,
                ApproverId = CurrentUser.CasUserEntity.UserId,
                ApproverType = 1,//审批用户类型：领导
                Status = 2//审批结果：待审批
            };
            casContractApproverEntity = BusinessDataService.CommonHelperService.GetApprover(casContractApproverEntity);
            //找不到审批人数据
            if (casContractApproverEntity == null)
            {
                return Json(AjaxResult.Error("Approval data exception!"));
            }
            CasContractApprovalResultEntity casContractApprovalResultEntity = new CasContractApprovalResultEntity
            {
                ContractApprovalResultId = Guid.NewGuid().ToString(),
                ApprovalResult = 1,//审批结果：已批注
                ApproverType = 1,//审批用户类型：领导
                ApprovalOpinions = option,
                ApproverId = CurrentUser.CasUserEntity.UserId,
                ContractApprovalStepId = casContractApproverEntity.ContractApprovalStepId,
                ContractId = casContractApproverEntity.ContractId,
                CreatedBy = CurrentUser.CasUserEntity.UserId,
                CreateTime = DateTime.Now,
                ApprovalTime = DateTime.Now,
                LastModifiedBy = CurrentUser.CasUserEntity.UserId,
                LastModifiedTime = DateTime.Now
            };
            bool flag = BusinessDataService.ContractCommentService.OptionCommnet(casContractApprovalResultEntity);
            return Json(flag ? AjaxResult.Success() : AjaxResult.Error());
        }
    }
}