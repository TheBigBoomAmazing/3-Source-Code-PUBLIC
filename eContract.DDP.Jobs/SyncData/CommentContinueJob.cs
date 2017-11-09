using eContract.BusinessService.BusinessData.BusinessRule;
using eContract.Common.Entity;
using eContract.DDP.Common.CommonJob;
using Suzsoft.Smart.Data;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace eContract.DDP.Jobs
{
    //批注1天后自动进入审批流程Job
    public class CommentContinueJob : BaseJob
    {
        public override void Run(Hashtable parameters)
        {
            ContractApprovalBLL bll = new ContractApprovalBLL();

            List<CasContractEntity> listCommentedOver1DayContracts = bll.GetAllCommentedOver1DayContracts();
            foreach (CasContractEntity commentedOver1DayContracts in listCommentedOver1DayContracts)
            {
                bll.ContinueCommentedOver1DayContracts(commentedOver1DayContracts);
            }
        }
    }
}
