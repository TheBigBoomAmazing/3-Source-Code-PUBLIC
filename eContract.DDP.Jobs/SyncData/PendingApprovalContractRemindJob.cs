using eContract.BusinessService.BusinessData.BusinessRule;
using eContract.Common;
using eContract.Common.Entity;
using eContract.DDP.Common.CommonJob;
using Suzsoft.Smart.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace eContract.DDP.Jobs
{
    //待审批合同提醒Job+超时审批提醒Job
    public class PendingApprovalContractRemindJob : BaseJob
    {
        public override void Run(Hashtable parameters)
        {
            ContractApprovalBLL bll = new ContractApprovalBLL();
            ContractManagementBLL contractManagementBLL = new ContractManagementBLL();

            List<CasUserEntity> listApprovers = bll.GetAllPendingApprovalAndOvertimeContractsApprovers();
            foreach (CasUserEntity approver in listApprovers)
            {
                //待审批和超时合同
                List<CasContractApproverEntity> listPendingApprovalAndOverTimeContractApprovers = bll.GetPendingApprovalAndOvertimeContractApprovers(approver.UserId);

                //待审批合同
                List<CasContractApproverEntity> listPendingApprovalContractApprovers = listPendingApprovalAndOverTimeContractApprovers.Where(p => p.Status == ContractApproverStatusEnum.WaitApproval.GetHashCode()).ToList();

                //超时合同
                List<CasContractApproverEntity> listOverTimeContractApprovers = listPendingApprovalAndOverTimeContractApprovers.Where(p => p.Status == ContractApproverStatusEnum.OverTime.GetHashCode()).ToList();


                //邮件标题
                string mailTitle = $@"e-Approval - Contract Approval List";
                //邮件正文
                string mailContent = $@"Dear {approver.EnglishName},<br/>尊敬的：{approver.ChineseName}";
                if (listPendingApprovalContractApprovers.Count>0)
                {
                    mailContent += $@"<br/>The following contracts have been submitted for your approval(please kindly confirm your approval or rejection within 3 working days):<br/>以下合同申请需要您的审核（请在3个工作日内批准或拒绝申请）：<br/>
                            </br><table style='font-family:verdana,arial,sans-serif;font-size:11px;color:#333333;border-width: 1px;border-color: #666666;border-collapse:collapse;'><thead>
                            <tr><th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #dedede;'>Initiator 申请人</th>
                            <th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #dedede;'>Contract Owner 合同经办部门</th>
                            <th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #dedede;'>Contract Name 合同名称</th>
                            <th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #dedede;'>Ferrero Entity 费列罗方</th>
                            <th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #dedede;'>Counter Party 相对方</th>
                            </tr></thead><tbody>";
                    foreach (var pendingApproval in listPendingApprovalContractApprovers)
                    {
                        CasContractEntity contract = contractManagementBLL.GetById<CasContractEntity>(pendingApproval.ContractId);
                        CasUserEntity userEntity = contractManagementBLL.GetById<CasUserEntity>(contract.CreatedBy);
                        mailContent += $@"<tr>
                            <td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>{userEntity.ChineseName}</td>
                            <td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>{contract.ContractOwner}{contract.TemplateOwner}</td>
                            <td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>{contract.ContractName}{contract.TemplateName}</td>
                            <td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>{contract.FerreroEntity}</td>
                            <td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>{contract.CounterpartyEn}{contract.CounterpartyCn}</td>
                            </tr>";
                    }
                    mailContent += $@"</tbody></table></br>";
                }

                if (listOverTimeContractApprovers.Count>0)
                {
                    mailContent += $@"<br/>Note below contracts are overdue, please review the contracts at your earliest convenient.<br/>请注意以下合同已超期，请尽快进行审批。<br/>
                            </br><table style='font-family:verdana,arial,sans-serif;font-size:11px;color:#333333;border-width: 1px;border-color: #666666;border-collapse:collapse;'><thead>
                            <tr><th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #dedede;'>Initiator 申请人</th>
                            <th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #dedede;'>Contract Owner 合同经办部门</th>
                            <th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #dedede;'>Contract Name 合同名称</th>
                            <th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #dedede;'>Ferrero Entity 费列罗方</th>
                            <th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #dedede;'>Counter Party 相对方</th>
                            </tr></thead><tbody>";
                    foreach (var listOverTime in listOverTimeContractApprovers)
                    {
                        CasContractEntity contract = contractManagementBLL.GetById<CasContractEntity>(listOverTime.ContractId);
                        CasUserEntity userEntity = contractManagementBLL.GetById<CasUserEntity>(contract.CreatedBy);
                        mailContent += $@"<tr>
                            <td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>{userEntity.ChineseName}</td>
                            <td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>{contract.ContractOwner}{contract.TemplateOwner}</td>
                            <td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>{contract.ContractName}{contract.TemplateName}</td>
                            <td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>{contract.FerreroEntity}</td>
                            <td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>{contract.CounterpartyEn}{contract.CounterpartyCn}</td>
                            </tr>";
                    }
                    mailContent += $@"</tbody></table></br>";
                }


                //foreach (CasContractApproverEntity casContractApproverEntity in listPendingApprovalAndOverTimeContractApprovers)
                //{
                //    CasContractEntity contract = contractManagementBLL.GetById<CasContractEntity>(casContractApproverEntity.ContractId);

                //    string statusName = "";
                //    switch (casContractApproverEntity.Status)
                //    {
                //        case (2): statusName = "待审批"; break;
                //        case (4): statusName = "超时"; break;
                //        default: statusName = "待审批"; break;
                //    }
                //    string approveTypeName = "";
                //    switch (casContractApproverEntity.ApproverType)
                //    {
                //        case (1): approveTypeName = "领导"; break;
                //        case (2): approveTypeName = "大区总监"; break;
                //        case (3): approveTypeName = "部门总监"; break;
                //        case (4): approveTypeName = "审批部门"; break;
                //        default: approveTypeName = "审批部门"; break;
                //    }
                //    mailContent += $@"<tr><td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>{contract.ContractName}</td><td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>{approveTypeName}</td><td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>{statusName}</td></tr>";
                //}
                mailContent += "</br></br>Ferrero China Contract Approval System</br>费列罗中国合同审批系统";
                var cc = "chinacontractsys@ferrero.com.cn";
                
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
