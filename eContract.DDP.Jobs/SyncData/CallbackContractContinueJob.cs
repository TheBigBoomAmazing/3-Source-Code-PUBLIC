using eContract.BusinessService.BusinessData.BusinessRule;
using eContract.Common.Entity;
using eContract.DDP.Common.CommonJob;
using Suzsoft.Smart.Data;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace eContract.DDP.Jobs
{
    //打回审批重新提交后3天自动继续审批Job
    public class CallbackContractContinueJob : BaseJob
    {
        public override void Run(Hashtable parameters)
        {
            ContractApprovalBLL bll = new ContractApprovalBLL();
            List<CasContractEntity> listCallbackContinueOver3DaysContracts = bll.GetAllCallbackContinueOver3DaysContracts();
            foreach (CasContractEntity callbackContinueOver3DaysContracts in listCallbackContinueOver3DaysContracts)
            {
                bll.CallbackContinueOver3DaysContractsContinue(callbackContinueOver3DaysContracts);
            }
        }
    }
}
