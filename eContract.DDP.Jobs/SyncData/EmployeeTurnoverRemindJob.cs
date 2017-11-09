using eContract.BusinessService.BusinessData.BusinessRule;
using eContract.BusinessService.SystemManagement.BusinessRule;
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
    //员工离职提醒Job
    public class EmployeeTurnoverRemindJob : BaseJob
    {
        public override void Run(Hashtable parameters)
        {
            ContractManagementBLL contractBll = new ContractManagementBLL();
            UserBLL userBll = new UserBLL();
            DepartmentBLL departmentBll = new DepartmentBLL();

            //所有合同管理员
            List<CasUserEntity> listContractManagers = userBll.GetAllContractManagers();

#if DEBUG
            //TODO:离职员工列表，同步员工数据时判断并获得
            List<CasUserEntity> listTurnoverUsers = new List<CasUserEntity>();
            //TODO：测试数据，汪敏
            listTurnoverUsers.Add(new CasUserEntity() { UserId = "WANGMIN1", ChineseName = "汪敏" });
#else

#endif
            foreach (CasUserEntity user in listTurnoverUsers)
            {
                //部门总监
                CasUserEntity manager = departmentBll.GetDepartmentManagerByUserId(user.UserId);

                List<CasContractEntity> listApplyContracts = contractBll.GetAllApplyContractsByUserId(user.UserId);
                List<CasContractEntity> listApproveContracts = contractBll.GetAllApproveContractsByUserId(user.UserId);
                List<CasContractEntity> listPOApproveContracts = contractBll.GetAllPOApproveContractsByUserId(user.UserId);

                //邮件标题
                string mailTitle = $@"e-Approval – User Leaving";

                //邮件正文
                string mailContent = $@"Dear All,<br/>尊敬的用户<br/><br/><{user.ChineseName}>left Ferrero on <{user.LastWorkDate}>. Following contracts were initiated by <{user.ChineseName}>. Please submit your request in IT Service Order System to transfer the contracts to other employee.<br/><{user.ChineseName}>于<{user.LastWorkDate}>离职，以下为该员工发起的所有合同。请在IT Service Order系统中申请将合同转移至其他员工名下。</br>";
                if (listApplyContracts.Count == 0 && listApproveContracts.Count == 0 && listPOApproveContracts.Count == 0)
                {
                    mailContent += "<b>他没有申请合同，也没有审批合同，也没有PO审批合同，请知悉。</b></br>Best Regards";
                }
                else
                {
                    if (listApplyContracts.Count > 0)
                    {
                        mailContent += $@"</br><table style='font-family:verdana,arial,sans-serif;font-size:11px;color:#333333;border-width: 1px;border-color: #666666;border-collapse:collapse;'><thead>
                            <tr><th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #dedede;'>Initiator 申请人</th>
                            <th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #dedede;'>Contract Owner 合同经办部门</th>
                            <th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #dedede;'>Contract Name 合同名称</th>
                            <th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #dedede;'>Ferrero Entity 费列罗方</th>
                            <th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #dedede;'>Counter Party 相对方</th>
                            <th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #dedede;'>Contract Number 合同编号</th>
                            <th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #dedede;'>Contract Status 合同状态</th>
                            </tr></thead><tbody>";

                        foreach (CasContractEntity applyContract in listApplyContracts)
                        {
                            mailContent += $@"<tr>
                            <td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>{user.ChineseName}</td>
                            <td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>{applyContract.ContractOwner}{applyContract.TemplateOwner}</td>
                            <td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>{applyContract.ContractName}{applyContract.TemplateName}</td>
                            <td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>{applyContract.FerreroEntity}</td>
                            <td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>{applyContract.CounterpartyEn}{applyContract.CounterpartyCn}</td>
                            <td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>{applyContract.ContractNo}{applyContract.TemplateNo}</td>
                            <td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>{applyContract.Status}</td>
                            </tr>";
                        }
                        mailContent += "</tbody></table></br>";
                    }

                    //if (listApproveContracts.Count > 0)
                    //{
                    //    mailContent += "他审批的合同清单如下：<br/><table style='font-family:verdana,arial,sans-serif;font-size:11px;color:#333333;border-width: 1px;border-color: #666666;border-collapse:collapse;'><thead><tr><th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #dedede;'>合同名称</th></tr></thead><tbody>";

                    //    foreach (CasContractEntity approveContract in listApproveContracts)
                    //    {
                    //        mailContent += $@"<tr><td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>{approveContract.ContractName}</td></tr>";
                    //    }
                    //    mailContent += "</tbody></table></br>";
                    //}


                    //if (listPOApproveContracts.Count > 0)
                    //{
                    //    mailContent += "他PO审批的合同清单如下：<br/><table style='font-family:verdana,arial,sans-serif;font-size:11px;color:#333333;border-width: 1px;border-color: #666666;border-collapse:collapse;'><thead><tr><th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #dedede;'>合同名称</th></tr></thead><tbody>";

                    //    foreach (CasContractEntity poApproveContract in listPOApproveContracts)
                    //    {
                    //        mailContent += $@"<tr><td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>{poApproveContract.ContractName}</td></tr>";
                    //    }
                    //    mailContent += "</tbody></table></br>";
                    //}
                    mailContent += "</br><b>Note: The contracts will be transferred to department head after 10 days if we don’t receive any notification from user department.</b></br><b>备注：如在10天之内未收到用户部门任何申请，所有该离职用户的合同将转移至部门总监名下。</b></br></br>Ferrero China Contract Approval System</br>费列罗中国合同审批系统";
                }

                var cc = "chinacontractsys@ferrero.com.cn";

                List<string> listRecievers = new List<string>();
                if (manager != null)
                {
                    listRecievers.Add(manager.Email);
                }
                foreach (CasUserEntity contractManager in listContractManagers)
                {
                    listRecievers.Add(contractManager.Email);
                }
                List<string> ccs = new List<string>();
                ccs.Add(cc);
                SendEmail.Send(listRecievers, ccs, mailTitle, mailContent);


                //SendEmail.Send(reciever, cc,mailTitle, mailContent);
            }
        }
    }
}
