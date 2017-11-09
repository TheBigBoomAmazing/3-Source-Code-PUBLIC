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
using eContract.Common.Entity;
using eContract.BusinessService.BusinessData.BusinessRule;
using eContract.Common;

namespace eContract.DDP.Jobs
{
    //合同到期提醒Job
    public class ContractExpirationReminderJob : BaseJob
    {
        public override void Run(Hashtable parameters)
        {
            ContractManagementBLL contractManagementBLL = new ContractManagementBLL();
            DepUserCommonBLL userBll = new DepUserCommonBLL();
            //45天后过期的合同
            List<CasContractEntity> listExpireIn45DaysContracts = contractManagementBLL.GetExpireIn45DaysContracts();
            //90天后过期的合同
            List<CasContractEntity> listExpireIn90DaysContracts = contractManagementBLL.GetExpireIn90DaysContracts();
            
            foreach (CasContractEntity contract in listExpireIn45DaysContracts)
            {
                CasUserEntity user = userBll.GetById<CasUserEntity>(contract.CreatedBy);
                string title = $@"e-Approval – Notification of Contract Expiration";
                string content = $@"Dear {user.EnglishName},<br/>尊敬的：{user.ChineseName} <br/><br/>The following contract will expire in 45 days: <br/>以下合同将在45天后过期：<br/><br/>
                  Contract Name 合同名称:{contract.ContractName}{contract.TemplateName}<br/>Ferrero Entity  费列罗方:{contract.FerreroEntity}</br>Counter Party  相对方:{contract.CounterpartyEn}{contract.CounterpartyCn}</br>Contract Number 合同编号:{contract.ContractNo}{contract.TemplateNo}</br></br>Please check and consider if you need to renew the contract</br>请确认并考虑是否需要续签此合同。</br></br>Ferrero China Contract Approval System</br>费列罗中国合同审批系统";
                var cc = "chinacontractsys@ferrero.com.cn";

                string reciever = user.Email;
         
                SendEmail.Send(reciever, cc,title, content);
            }

            foreach (CasContractEntity contract in listExpireIn90DaysContracts)
            {
                CasUserEntity user = userBll.GetById<CasUserEntity>(contract.CreatedBy);
                string title = $@"e-Approval – Notification of Contract Expiration";
                string content = $@"Dear {user.EnglishName},<br/>尊敬的：{user.ChineseName} <br/><br/>The following contract will expire in 90 days: <br/>以下合同将在90天后过期：<br/><br/>
                  Contract Name 合同名称:{contract.ContractName}{contract.TemplateName}<br/>Ferrero Entity  费列罗方:{contract.FerreroEntity}</br>Counter Party  相对方:{contract.CounterpartyEn}{contract.CounterpartyCn}</br>Contract Number 合同编号:{contract.ContractNo}{contract.TemplateNo}</br></br>Please check and consider if you need to renew the contract</br>请确认并考虑是否需要续签此合同。</br></br>Ferrero China Contract Approval System</br>费列罗中国合同审批系统";
                var cc = "chinacontractsys@ferrero.com.cn";

                string reciever = user.Email;
       
                SendEmail.Send(reciever, cc, title, content);
            }
        }
    }
}
