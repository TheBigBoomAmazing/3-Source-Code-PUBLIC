using eContract.BusinessService.BusinessData.BusinessRule;
using eContract.DDP.Common.CommonJob;
using System.Collections;

namespace eContract.DDP.Jobs
{
    public class TimeBackJob : BaseJob
    {
        public override void Run(Hashtable parameters)
        {
            ContractApprovalBLL bll = new ContractApprovalBLL();
            bll.TimeBack1Day();
        }
    }
}
