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
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace eContract.Web.Areas.CAS.Controllers
{
    public class ContractApprovalController : AuthBaseController
    {
        // GET: CAS/ContractApproval
        public ActionResult Index(JqGrid grid, FormCollection data)
        {
            grid.ConvertParams(data);
            if (IsPost)
            {
                return Json(AjaxResult.Success(BusinessDataService.ContractApprovalService.ForGrid(grid)));
            }
            return View();
        }

        /// <summary>
        /// 跳转到审批页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Approval(string id)
        {
            //合同信息
            CasContractEntity entity = new CasContractEntity();
            if (!string.IsNullOrEmpty(id))
            {
                //获取审批人信息
                CasContractApproverEntity casContractApproverEntity = new CasContractApproverEntity { ContractApproverId = id };
                casContractApproverEntity = BusinessDataService.CommonHelperService.GetApprover(casContractApproverEntity);
                //获取合同正文信息
                entity = BusinessDataService.ContractApplayService.GetById<CasContractEntity>(casContractApproverEntity.ContractId);
                //获取合同类型信息
                CasContractTypeEntity casContractTypeEntity = BusinessDataService
                    .ContractTypeManagementService
                    .GetById<CasContractTypeEntity>(entity.ContractTypeId);
                ViewBag.ContractType = casContractTypeEntity;
                //获取审批结果信息
                DataTable approvalResultDt = BusinessDataService.CommonHelperService.GetApprovalResultDt(casContractApproverEntity.ContractId);
                JsonSerializerSettings setting = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                var ret = JsonConvert.SerializeObject(approvalResultDt, setting);
                ViewBag.ApprovalResult = ret;
                ViewBag.ContractApproverId = casContractApproverEntity.ContractApproverId;

                List<CasAttachmentEntity> list = BusinessDataService.CommonHelperService.GetFilesBySourceId(entity.ContractId);
                var mineFileIds = list.Where(f => f.AttachmentType == 1).ToList();
                var originalFileIds = list.Where(f => f.AttachmentType == 2).ToList();
                ViewBag.MineFiles = mineFileIds;
                ViewBag.OriginalFiles = originalFileIds;
            }
            return View(entity);
        }

        /// <summary>
        /// 审批人审批
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="contractApproverId"></param>
        /// <returns></returns>
        public JsonResult Submit(int approvalType, string contractApproverId, string comment)
        {
            bool flag = false;
            string msg = "";
            //获取审批人信息
            CasContractApproverEntity casContractApproverEntity = new CasContractApproverEntity { ContractApproverId = contractApproverId };
            casContractApproverEntity = BusinessDataService.CommonHelperService.GetApprover(casContractApproverEntity);
            if (casContractApproverEntity != null)
            {
                CasContractEntity casContractEntity = BusinessDataService.CommonHelperService.GetById<CasContractEntity>(casContractApproverEntity.ContractId);
                CasUserEntity userEntity = BusinessDataService.CommonHelperService.GetById<CasUserEntity>(casContractEntity.CreatedBy);
                if (casContractEntity.Status != ContractStatusEnum.WaitApproval.GetHashCode() && casContractEntity.Status != ContractStatusEnum.Resubmit.GetHashCode())
                {
                    flag = false;
                    msg = "Approve failed: the contract is not in review process.";
                    return Json(AjaxResult.Error(msg));
                }

                if (casContractApproverEntity.Status == ContractApproverStatusEnum.WaitApproval.GetHashCode() || casContractApproverEntity.Status == ContractApproverStatusEnum.OverTime.GetHashCode())
                {
                    CasContractApprovalResultEntity casContractApprovalResultEntity = new CasContractApprovalResultEntity
                    {
                        ContractApprovalResultId = Guid.NewGuid().ToString(),
                        ApprovalResult = approvalType,//审批结果
                        ApproverType = casContractApproverEntity.ApproverType,//审批用户类型
                        ApprovalOpinions = comment,
                        ApproverId = CurrentUser.CasUserEntity.UserId,
                        ContractApprovalStepId = casContractApproverEntity.ContractApprovalStepId,
                        ContractId = casContractApproverEntity.ContractId,
                        CreatedBy = CurrentUser.CasUserEntity.UserId,
                        CreateTime = DateTime.Now,
                        ApprovalTime = DateTime.Now,
                        LastModifiedBy = CurrentUser.CasUserEntity.UserId,
                        LastModifiedTime = DateTime.Now
                    };
                    flag = BusinessDataService.ContractApprovalService.Approval(casContractApprovalResultEntity, casContractApproverEntity);
                    if (flag && approvalType==4)
                    {
                        var title = $@"e-Approval – Contract Reject";
                        var content = $@"Dear:{userEntity.EnglishName},</br>尊敬的：{userEntity.ChineseName}</br></br>The following contract is rejected:</br>以下合同被拒绝：</br></br>Contract Name 合同名称:{casContractEntity.ContractName}  {casContractEntity.TemplateName}</br>Ferrero Entity  费列罗方 ：{casContractEntity.FerreroEntity}</br>Counter Party  相对方:{casContractEntity.CounterpartyEn}  {casContractEntity.CounterpartyCn}</br>Rejected by 拒绝审批人:{ CurrentUser.CasUserEntity.EnglishName}{ CurrentUser.CasUserEntity.ChineseName}</br>Reject Reason 拒绝理由:{comment}</br></br>Please review your contract and resubmit or close the request.</br>请检查合同相关内容，重新提交或关闭合同。</br></br>Ferrero China Contract Approval System</br>费列罗中国合同审批系统";
                        var cc = "chinacontractsys@ferrero.com.cn";
                        var reciever = userEntity.Email;
                        SendEmail.Send(reciever, cc, title, content);
                    }
                }
                else
                {
                    flag = false;
                    msg = "Approve failed: the contract is already approved.";
                }
            }
            else
            {
                flag = false;
                msg = "Operation failed: cannot find the approver's info.";
            }
            return Json(flag ? AjaxResult.Success() : AjaxResult.Error(msg));
        }

    }
}