using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eContract.DDP.Common.CommonJob;
using System.Collections;
using eContract.Log;
using System.IO;
using System.Reflection;
using System.Data;
using System.Net.Mail;
using System.Net;
using Suzsoft.Smart.Data;
using eContract.BusinessService.BusinessData.BusinessRule;
using eContract.Common.Entity;
using eContract.Common;

namespace eContract.DDP.Jobs
{
    //PO审批提醒Job
    public class POApprovalRemindJob : BaseJob
    {
        public override void Run(Hashtable parameters)
        {
            POBLL poBLL = new POBLL();
            ContractManagementBLL contractManagementBLL = new ContractManagementBLL();
            List<CasUserEntity> listUsers = poBLL.GetAllHaveApplyPOUsers();
            foreach (CasUserEntity user in listUsers)
            {
                List<CasContractFilingEntity> listAllApplyPO = poBLL.GetAllApplyPOs(user.UserId);

                //邮件标题
                string mailTitle = $@"您有{listAllApplyPO.Count}条PO未审批({DateTime.Now.ToString("yyyy年MM月dd日")})";

                string mailContent = $@"Dear {user.ChineseName},<br/>您的未审批PO清单如下：<br/><table style='font-family:verdana,arial,sans-serif;font-size:11px;color:#333333;border-width: 1px;border-color: #666666;border-collapse:collapse;'><thead><tr><th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #dedede;'>合同名称</th><th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #dedede;'>PR号</th></tr></thead><tbody>";

                foreach (CasContractFilingEntity applyPO in listAllApplyPO)
                {
                    CasContractEntity contract = contractManagementBLL.GetById<CasContractEntity>(applyPO.ContractId);
                    mailContent += $@"<tr><td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>{contract.ContractName}</td><td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>{applyPO.PrNo}</td></tr>";
                }
                mailContent += "</tbody></table></br>Best Regards";
#if DEBUG
                string reciever = "1049200020@qq.com";
#else
                string reciever = approver.Email;
#endif
                SendEmail.Send(reciever, mailTitle, mailContent);
            }
        }
    }
}
