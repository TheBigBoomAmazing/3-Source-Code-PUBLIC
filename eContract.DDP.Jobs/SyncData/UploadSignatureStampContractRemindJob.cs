using eContract.BusinessService.BusinessData.BusinessRule;
using eContract.Common.Entity;
using eContract.DDP.Common.CommonJob;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using eContract.Common;

namespace eContract.DDP.Jobs
{
    //提醒上传签字盖章合同
    public class UploadSignatureStampContractRemindJob: BaseJob
    {
        public override void Run(Hashtable parameters)
        {
            UploadSignatureStampContractRemind SignStapRe = new UploadSignatureStampContractRemind();
            ContractManagementBLL contractManagementBLL = new ContractManagementBLL();
            List<CasContractEntity> contracts = SignStapRe.GetNeedRemindContracts();
            foreach (CasContractEntity contract in contracts)
            {
                CasUserEntity userEntity = contractManagementBLL.GetById<CasUserEntity>(contract.CreatedBy);
                var title = $@"e-Approval – Notification of Upload Signature Stamp Contract";
                var content = $@"Dear {userEntity.EnglishName},<br/>尊敬的：{userEntity.ChineseName} <br/><br/>The following has been completed and approved before 10 working days, please upload the contract signed and sealed: <br/>以下合同10个工作日之前审批完成，请上传签字盖章合同：<br/><br/>
                  Contract Name 合同名称:{contract.ContractName}{contract.TemplateName}<br/>Ferrero Entity  费列罗方:{contract.FerreroEntity}</br>Counter Party  相对方:{contract.CounterpartyEn}{contract.CounterpartyCn}</br>Contract Number 合同编号:{contract.ContractNo}{contract.TemplateNo}</br></br>Ferrero China Contract Approval System</br>费列罗中国合同审批系统";
                var cc = "chinacontractsys@ferrero.com.cn";
                var reciever = userEntity.Email;
                SendEmail.Send(reciever, cc, title, content);

            }

        }
    }
}
