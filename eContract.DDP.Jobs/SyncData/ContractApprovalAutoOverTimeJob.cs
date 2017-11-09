using eContract.BusinessService.BusinessData.BusinessRule;
using eContract.Common;
using eContract.Common.Entity;
using eContract.DDP.Common.CommonJob;
using Suzsoft.Smart.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace eContract.DDP.Jobs
{
    //合同审批自动过期Job
    public class ContractApprovalAutoOverTimeJob : BaseJob
    {
        public override void Run(Hashtable parameters)
        {
            ContractApprovalBLL bll = new ContractApprovalBLL();
            bll.OverTimeContractApproval();
        }
    }
}
