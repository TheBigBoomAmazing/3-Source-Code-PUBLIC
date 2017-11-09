using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Suzsoft.Smart.Data;
using Suzsoft.Smart.EntityCore;
using eContract.BusinessService.Common;
using eContract.Common;
using eContract.Common.Entity;
using eContract.Common.MVC;
using eContract.Common.WebUtils;
using eContract.BusinessService.BusinessData.CommonQuery;
using System.Web.Script.Serialization;
using eContract.BusinessService.SystemManagement.Service;
using eContract.BusinessService.SystemManagement.Domain;

namespace eContract.BusinessService.BusinessData.BusinessRule
{
    public class ContractApprovalBLL : BusinessBase
    {
        public JqGrid ForGrid(JqGrid grid)
        {
            var query = new ContractApplayListQuery();
            //query.queryFieldValue = grid.QueryField;
            query.FERRERO_ENTITY = grid.QueryField.ContainsKey("FERRERO_ENTITY") ? grid.QueryField["FERRERO_ENTITY"] : "";
            grid.QueryField.Remove("FERRERO_ENTITY");
            query.CONTRACT_SERIAL_NO = grid.QueryField.ContainsKey("CONTRACT_SERIAL_NO") ? grid.QueryField["CONTRACT_SERIAL_NO"] : "";
            grid.QueryField.Remove("CONTRACT_SERIAL_NO");
            query.CONTRACT_TYPE_NAME = grid.QueryField.ContainsKey("CONTRACT_TYPE_NAME") ? grid.QueryField["CONTRACT_TYPE_NAME"] : "";
            grid.QueryField.Remove("CONTRACT_TYPE_NAME");
            query.CONTRACT_NAME = grid.QueryField.ContainsKey("CONTRACT_NAME") ? grid.QueryField["CONTRACT_NAME"] : "";
            grid.QueryField.Remove("CONTRACT_NAME");
            query.CONTRACT_INITIATOR = grid.QueryField.ContainsKey("CONTRACT_INITIATOR") ? grid.QueryField["CONTRACT_INITIATOR"] : "";
            grid.QueryField.Remove("CONTRACT_INITIATOR");
            //query.CREATE_TIME = grid.QueryField.ContainsKey("CREATE_TIME") ? grid.QueryField["CREATE_TIME"] : "";
            //grid.QueryField.Remove("CREATE_TIME");
            //grid.QueryField.Remove("FERRERO_ENTITY");
            //grid.QueryField.Remove("CONTRACT_TYPE_NAME");
            //grid.QueryField.Remove("CONTRACT_NAME");
            //grid.QueryField.Remove("CONTRACT_INITIATOR");
            //grid.QueryField.Remove("CREATE_TIME");
            grid = QueryTableHelper.QueryGrid<CasContractEntity>(query, grid);
            return grid;
            //UserDomain userDomain = (UserDomain)WebCaching.CurrentUserDomain;
       //     string sql = $@"SELECT row_number() over(order by LAST_MODIFIED_TIME desc) as Row_Number,* from (
       //                     SELECT t1.*,t2.STATUS as APPROVE_STATUS,t2.APPROVER_TYPE,t2.CONTRACT_APPROVER_ID,t6.STEP   
       //                     FROM dbo.CAS_CONTRACT t1
       //                     JOIN dbo.CAS_CONTRACT_APPROVER t2 ON t1.CONTRACT_ID = t2.CONTRACT_ID
       //                     JOIN dbo.CAS_CONTRACT_APPROVAL_STEP t6 ON t2.CONTRACT_APPROVAL_STEP_ID=t6.CONTRACT_APPROVAL_STEP_ID
       //                     WHERE 1 = 1 AND t2.APPROVER_TYPE != 1 AND t2.STATUS IN (2,4) AND t1.STATUS IN (2,8)
       //                     AND (t2.APPROVER_ID = '{userDomain.CasUserEntity.UserId}' 
       //                     OR t2.APPROVER_ID IN (SELECT t3.AUTHORIZED_USER_ID FROM dbo.CAS_PROXY_APPROVAL t3 
       //                     WHERE t3.AGENT_USER_ID = '{userDomain.CasUserEntity.UserId}' 
       //                     AND GETDATE() BETWEEN t3.BEGIN_TIME AND t3.END_TIME))
       //                     UNION ALL
       //                     SELECT t1.*,t2.STATUS as APPROVE_STATUS,t2.APPROVER_TYPE,t2.CONTRACT_APPROVER_ID,t6.STEP   
       //                     FROM dbo.CAS_CONTRACT t1
       //                     JOIN dbo.CAS_CONTRACT_APPROVER t2 ON t1.CONTRACT_ID = t2.CONTRACT_ID
       //                     JOIN dbo.CAS_DEPARTMENT t4 ON t4.DEPT_ID = t2.APPROVER_ID
       //                     JOIN dbo.CAS_DEPT_USER t5 ON t5.DEPT_ID = t4.DEPT_ID
       //                     JOIN dbo.CAS_CONTRACT_APPROVAL_STEP t6 ON t2.CONTRACT_APPROVAL_STEP_ID=t6.CONTRACT_APPROVAL_STEP_ID
       //                     WHERE 1 = 1 AND t2.APPROVER_TYPE != 1 AND t2.STATUS IN (2,4) AND t1.STATUS IN (2,8)
       //                     AND (t5.USER_ID = '{userDomain.CasUserEntity.UserId}' 
       //                     OR t5.USER_ID IN (SELECT t3.AUTHORIZED_USER_ID FROM dbo.CAS_PROXY_APPROVAL t3 
       //                     WHERE t3.AGENT_USER_ID = '{userDomain.CasUserEntity.UserId}' 
       //                     AND GETDATE() BETWEEN t3.BEGIN_TIME AND t3.END_TIME))
							//UNION ALL 
							//SELECT t1.*,t2.STATUS as APPROVE_STATUS,t2.APPROVER_TYPE,t2.CONTRACT_APPROVER_ID,t6.STEP  FROM dbo.CAS_CONTRACT t1
       //                     JOIN dbo.CAS_CONTRACT_APPROVER t2 ON t1.CONTRACT_ID = t2.CONTRACT_ID
       //                     JOIN dbo.CAS_CONTRACT_APPROVAL_STEP t6 ON t2.CONTRACT_APPROVAL_STEP_ID=t6.CONTRACT_APPROVAL_STEP_ID
       //                     WHERE 1 = 1 AND t2.APPROVER_TYPE = 1 AND t2.STATUS = 2 AND t1.STATUS IN (2,8) 
							//AND t2.APPROVER_ID = '{userDomain.CasUserEntity.UserId}') temp";
            //if (grid.QueryField.ContainsKey("CONTRACT_NAME"))
            //{
            //    var para = $@" 1 = 1 AND t1.CONTRACT_NAME LIKE N'%{grid.QueryField["CONTRACT_NAME"]}%' ";
            //    sql = sql.Replace("1 = 1", para);
            //}
            //if (grid.QueryField.ContainsKey("CONTRACT_TYPE_NAME"))
            //{
            //    var para = $@" 1 = 1 AND t1.CONTRACT_TYPE_NAME LIKE N'%{grid.QueryField["CONTRACT_TYPE_NAME"]}%' ";
            //    sql = sql.Replace("1 = 1", para);
            //}
            //if (grid.QueryField.ContainsKey("FERRERO_ENTITY"))
            //{
            //    var para = $@" 1 = 1 AND t1.FERRERO_ENTITY LIKE N'%{grid.QueryField["FERRERO_ENTITY"]}%' ";
            //    sql = sql.Replace("1 = 1", para);
            //}
            //if (grid.QueryField.ContainsKey("CONTRACT_INITIATOR"))
            //{
            //    var para = $@" 1 = 1 AND t1.CONTRACT_INITIATOR LIKE N'%{grid.QueryField["CONTRACT_INITIATOR"]}%' ";
            //    sql = sql.Replace("1 = 1", para);
            //}
            //if (grid.QueryField.ContainsKey("CREATE_TIME"))
            //{
            //    var para = $@" 1 = 1 AND t1.CREATE_TIME LIKE N'%{grid.QueryField["CREATE_TIME"]}%' ";
            //    sql = sql.Replace("1 = 1", para);
            //}
            //DataTable dt = DataAccess.SelectDataSet(sql).Tables[0];
            //grid.DataBind(dt, dt.Rows.Count);
            //return grid;
        }

        public bool Approval(CasContractApprovalResultEntity casContractApprovalResultEntity, CasContractApproverEntity casContractApproverEntity)
        {
            bool flag = true;
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                broker.BeginTransaction();
                try
                {
                    casContractApproverEntity.Status = 3;//已审批
                    casContractApproverEntity.LastModifiedBy = CurrentUserDomain.CasUserEntity.UserId;
                    casContractApprovalResultEntity.ApproverId = CurrentUserDomain.CasUserEntity.UserId;
                    casContractApprovalResultEntity.ContractApproverId = casContractApproverEntity.ContractApproverId;
                    CasContractEntity contractEntity = GetById<CasContractEntity>(casContractApprovalResultEntity.ContractId);
                    bool isAllProcess = false;
                    //打回重新提交的单据
                    if (contractEntity.Status == ContractStatusEnum.Resubmit.GetHashCode())
                    {
                        //检查当前所有审批单据是否全部审批通过
                        string isAllProcessSql = $@"SELECT 1 FROM dbo.CAS_CONTRACT_APPROVER 
                                        WHERE APPROVER_ID != '{casContractApproverEntity.ApproverId}'
                                        AND CONTRACT_ID = '{casContractApproverEntity.ContractId}'
                                        AND APPROVER_TYPE <> {ApproverTypeEnum.LeaderComment.GetHashCode()}
                                        AND STATUS IN ({ContractApproverStatusEnum.WaitApproval.GetHashCode()}
                                                      ,{ContractApproverStatusEnum.OverTime.GetHashCode()})";
                        isAllProcess = broker.ExecuteSQLScalar(isAllProcessSql) == null;
                    }
                    else
                    {
                        //检查当前审批节点是否全部审批通过
                        string isAllProcessSql = $@"SELECT 1 FROM dbo.CAS_CONTRACT_APPROVER 
                                        WHERE CONTRACT_APPROVAL_STEP_ID = '{casContractApproverEntity.ContractApprovalStepId}' 
                                        AND APPROVER_ID != '{casContractApproverEntity.ApproverId}'
                                        AND CONTRACT_ID = '{casContractApproverEntity.ContractId}'
                                        AND STATUS IN ({ContractApproverStatusEnum.WaitApproval.GetHashCode()}
                                                      ,{ContractApproverStatusEnum.OverTime.GetHashCode()})";
                        isAllProcess = broker.ExecuteSQLScalar(isAllProcessSql) == null;
                    }

                    //审批人表更改状态为已审批
                    DataAccess.Update(casContractApproverEntity, broker);
                    //插入审批结果
                    DataAccess.Insert(casContractApprovalResultEntity, broker);

                    //审批拒绝，合同打回至申请人
                    if (casContractApprovalResultEntity.ApprovalResult == 4)
                    {
                        //更改合同状态为4：审批拒绝
                        string sql = $@"UPDATE dbo.CAS_CONTRACT SET STATUS = 4,LAST_MODIFIED_TIME = GETDATE() WHERE CONTRACT_ID = '{casContractApproverEntity.ContractId}'";
                        broker.ExecuteSQL(sql);
                    }
                    else//审批通过，判断是否要更改下一节点的审批状态
                    {
                        //是
                        if (isAllProcess)
                        {
                            //更改下一节点的审批状态
                            UpdateNextStepASpprovalStatus(casContractApproverEntity.ContractId, casContractApproverEntity.ContractApprovalStepId, broker);
                        }

                    }
                    broker.Commit();
                }
                catch (Exception e)
                {
                    flag = false;
                    broker.RollBack();
                    SystemService.LogErrorService.InsertLog(e);
                }
            }

            return flag;
        }

        /// <summary>
        /// 更改下一节点的审批状态
        /// </summary>
        /// <param name="contractId">合同ID</param>
        /// <param name="contractApprovalStepId">当前步骤ID</param>
        /// <param name="broker"></param>
        public void UpdateNextStepASpprovalStatus(string contractId, string contractApprovalStepId, DataAccessBroker broker)
        {
            //获取下一审批节点ID
            string sql = $@"SELECT TOP 1 t1.CONTRACT_APPROVAL_STEP_ID FROM dbo.CAS_CONTRACT_APPROVER t1 
                                    JOIN dbo.CAS_CONTRACT_APPROVAL_STEP t2 ON t1.CONTRACT_APPROVAL_STEP_ID = t2.CONTRACT_APPROVAL_STEP_ID
                                    WHERE t1.CONTRACT_ID = '{contractId}'
                                    AND t1.CONTRACT_APPROVAL_STEP_ID != '{contractApprovalStepId}'
                                    AND t1.STATUS = 1
                                    ORDER BY t2.STEP ASC ";
            object nextStepObj = broker.ExecuteSQLScalar(sql);
            //不存在下一审批节点，更新合同状态审批通过
            if (nextStepObj == null)
            {
                CasContractEntity contract = DataAccess.Select<CasContractEntity>($@"SELECT TOP 1 * FROM CAS_CONTRACT WHERE CONTRACT_ID = '{contractId}'").FirstOrDefault();

                //审批通过以后，非模板合同，自动分配合同编号，历史合同只有流水号，没有合同编号。
                //普通合同
                if (contract.ContractGroup.Value == ContractGroupEnum.NormalContract.GetHashCode())
                {
                    //非模板合同
                    if (!contract.IsTemplateContract.Value)
                    {
                        CasUserEntity user = GetById<CasUserEntity>(contract.CreatedBy);
                        string companyCode = user.CompanyCode == "FFH" ? "H" : "S";
                        string yearCode = DateTime.Now.ToString("yy");
                        //合同编号格式：company-yy-4位流水号（例：S-17-0001、H-17-0001）
                        sql = "SELECT MAX(CONTRACT_NO) FROM CAS_CONTRACT WHERE CONTRACT_NO LIKE '" + companyCode + "-" + yearCode + "-%' ";
                        string maxContractNo = broker.ExecuteSQLScalar(sql).ToString();

                        if (string.IsNullOrEmpty(maxContractNo))
                        {
                            contract.ContractNo = companyCode + "-" + yearCode + "-0001";
                        }
                        else
                        {
                            contract.ContractNo = companyCode + "-" + yearCode + "-" + (int.Parse(maxContractNo.Substring(5, 4)) + 1).ToString().PadLeft(4, '0');
                        }
                    }
                    contract.Status = ContractStatusEnum.HadApproval.GetHashCode();
                    contract.LastModifiedTime = DateTime.Now;
                    broker.ExecuteSQL(sql);
                }
                //历史合同
                else
                {
                    //sql = "SELECT MAX(CONTRACT_SERIAL_NO) FROM CAS_CONTRACT";
                    //string maxContractSerialNo = broker.ExecuteSQLScalar(sql).ToString();

                    //if (string.IsNullOrEmpty(maxContractSerialNo))
                    //{
                    //    contract.ContractSerialNo = "1";
                    //}
                    //else
                    //{
                    //    contract.ContractSerialNo = (int.Parse(maxContractSerialNo) + 1).ToString();
                    //}
                    contract.ContractNo = contract.ContractSerialNo;
                    contract.Status = ContractStatusEnum.HadApproval.GetHashCode();
                    contract.LastModifiedTime = DateTime.Now;
                }

                //历史合同的流水号格式：2017100001（yyyyMM流水号）、模板合同、正常合同也要有流水号，同一个序列；20171019modify
                //string prefix = DateTime.Now.ToString("yyyyMM");
                //合同编号格式：company-yy-4位流水号（例：S-17-0001、H-17-0001）
                //sql = "SELECT MAX(CONTRACT_SERIAL_NO) FROM CAS_CONTRACT WHERE CONTRACT_SERIAL_NO LIKE '" + prefix + "%' ";
                //string maxContractSerialNo = broker.ExecuteSQLScalar(sql).ToString();

                //if (string.IsNullOrEmpty(maxContractSerialNo))
                //{
                //    contract.ContractSerialNo = prefix + "0001";
                //}
                //else
                //{
                //    contract.ContractSerialNo = prefix + (int.Parse(maxContractSerialNo.Substring(6, 4)) + 1).ToString().PadLeft(4, '0');
                //}

                DataAccess.Update(contract, broker);
            }
            else//存在下一审批节点，激活下一审批节点为待审批
            {
                sql = $@"UPDATE dbo.CAS_CONTRACT_APPROVER SET STATUS = 2,LAST_MODIFIED_TIME = GETDATE() WHERE CONTRACT_ID = '{contractId}' AND CONTRACT_APPROVAL_STEP_ID = '{nextStepObj.ToString()}'";
                broker.ExecuteSQL(sql);


                CasContractEntity contractEntity = GetById<CasContractEntity>(contractId);
                CasUserEntity creatByInfo = GetById<CasUserEntity>(contractEntity.CreatedBy);
                //TODO:发邮件给部门总监和大区总监
                #region 发送邮件给大区总监
                string sqlString = $@" SELECT APPROVER_ID FROM CAS_CONTRACT_APPROVER WHERE CONTRACT_APPROVAL_STEP_ID='{nextStepObj.ToString()}' AND APPROVER_TYPE='2' AND CONTRACT_ID='{contractId}' ";
                var regionManagerValue = DataAccess.SelectScalar(sqlString,broker);
                if (!string.IsNullOrWhiteSpace(regionManagerValue))
                {
                    CasUserEntity managerInfo = GetById<CasUserEntity>(regionManagerValue);
                    var title = "e-Approval - Contract Approval";
                    var content = $@"Dear:{managerInfo.EnglishName},</br>尊敬的：{managerInfo.ChineseName}</br></br> The following contracts your approval:</br>以下合同申请需要您的审核：</br></br>  Initiator 申请人:{creatByInfo.EnglishName}    {creatByInfo.ChineseName}</br>Department 用户部门:{creatByInfo.DeparmentName}</br> Contract Name 合同名称:{contractEntity.ContractName}{contractEntity.TemplateName}</br> Counter Party  相对方:{contractEntity.CounterpartyCn}{contractEntity.CounterpartyEn} </br></br>Ferrero China Contract Approval System</br>费列罗中国合同审批系统";
                    var cc = $@"chinacontractsys@ferrero.com.cn";
                    var reciever = managerInfo.Email;
                    SendEmail.Send(reciever, cc, title, content);
                }
                #endregion

                #region 发送邮件给部门总监
                string sqlDepString = $@" SELECT APPROVER_ID FROM CAS_CONTRACT_APPROVER WHERE CONTRACT_APPROVAL_STEP_ID='{nextStepObj.ToString()}' AND APPROVER_TYPE='3' AND CONTRACT_ID='{contractId}' ";
                var depManagerValue = DataAccess.SelectScalar(sqlDepString,broker);
                if (!string.IsNullOrWhiteSpace(depManagerValue))
                {
                    CasUserEntity depManagerInfo = GetById<CasUserEntity>(depManagerValue);
                    var title = "e-Approval - Contract Approval";
                    var content = $@"Dear:{depManagerInfo.EnglishName},</br>尊敬的：{depManagerInfo.ChineseName}</br></br> The following contracts your approval:</br>以下合同申请需要您的审核：</br></br>  Initiator 申请人:{creatByInfo.EnglishName}    {creatByInfo.ChineseName}</br>Department 用户部门:{creatByInfo.DeparmentName}</br> Contract Name 合同名称:{contractEntity.ContractName}{contractEntity.TemplateName}</br> Counter Party  相对方:{contractEntity.CounterpartyCn}{contractEntity.CounterpartyEn} </br></br>Ferrero China Contract Approval System</br>费列罗中国合同审批系统";
                    var cc = $@"chinacontractsys@ferrero.com.cn";
                    var reciever = depManagerInfo.Email;
                    SendEmail.Send(reciever, cc, title, content);
                }
                #endregion
            }
        }

        /// <summary>
        /// 获取所有存在待审批合同或者超时合同的用户
        /// </summary>
        /// <returns></returns>
        public List<CasUserEntity> GetAllPendingApprovalAndOvertimeContractsApprovers()
        {
            string sql = $@"SELECT CAS_USER.*
                              FROM CAS_CONTRACT_APPROVER
                        INNER JOIN CAS_CONTRACT
                                ON CAS_CONTRACT_APPROVER.CONTRACT_ID = CAS_CONTRACT.CONTRACT_ID
							   AND CAS_CONTRACT_APPROVER.APPROVER_TYPE = {ApproverTypeEnum.DepartmentManager.GetHashCode()}
                        INNER JOIN CAS_USER
	                            ON CAS_CONTRACT_APPROVER.APPROVER_ID = CAS_USER.USER_ID
                             WHERE CAS_CONTRACT_APPROVER.STATUS IN
                                   ({ContractApproverStatusEnum.WaitApproval.GetHashCode()}
                                   ,{ContractApproverStatusEnum.OverTime.GetHashCode()})
                               AND CAS_CONTRACT.STATUS IN
                                   ({ContractStatusEnum.WaitApproval.GetHashCode()}
                                   ,{ContractStatusEnum.Resubmit.GetHashCode()})
                             UNION
                            SELECT CAS_USER.*
                              FROM CAS_CONTRACT_APPROVER
                        INNER JOIN CAS_CONTRACT
                                ON CAS_CONTRACT_APPROVER.CONTRACT_ID = CAS_CONTRACT.CONTRACT_ID
							   AND CAS_CONTRACT_APPROVER.APPROVER_TYPE =  ({ApproverTypeEnum.DepartmentManager.GetHashCode()}
                        INNER JOIN CAS_PROXY_APPROVAL
								ON CAS_CONTRACT_APPROVER.APPROVER_ID = CAS_PROXY_APPROVAL.AUTHORIZED_USER_ID
                               AND GETDATE() BETWEEN CAS_PROXY_APPROVAL.BEGIN_TIME AND CAS_PROXY_APPROVAL.END_TIME AND CAS_PROXY_APPROVAL.IS_DELETED=0
                        INNER JOIN CAS_USER
	                            ON CAS_PROXY_APPROVAL.AGENT_USER_ID = CAS_USER.USER_ID
                             WHERE CAS_CONTRACT_APPROVER.STATUS IN
                                   ({ContractApproverStatusEnum.WaitApproval.GetHashCode()}
                                   ,{ContractApproverStatusEnum.OverTime.GetHashCode()})
                               AND CAS_CONTRACT.STATUS IN
                                   ({ContractStatusEnum.WaitApproval.GetHashCode()}
                                   ,{ContractStatusEnum.Resubmit.GetHashCode()})
                              UNION
                            SELECT CAS_USER.*
                              FROM CAS_CONTRACT_APPROVER
                        INNER JOIN CAS_CONTRACT
                                ON CAS_CONTRACT_APPROVER.CONTRACT_ID = CAS_CONTRACT.CONTRACT_ID
                               AND CAS_CONTRACT_APPROVER.APPROVER_TYPE =
                                   {ApproverTypeEnum.Department.GetHashCode()}
						INNER JOIN CAS_DEPARTMENT
								ON CAS_CONTRACT_APPROVER.APPROVER_ID = CAS_DEPARTMENT.DEPT_ID
						INNER JOIN CAS_DEPT_USER
								ON CAS_DEPARTMENT.DEPT_ID = CAS_DEPT_USER.DEPT_ID
                        INNER JOIN CAS_USER
				                ON CAS_DEPT_USER.USER_ID = CAS_USER.USER_ID
                             WHERE CAS_CONTRACT_APPROVER.STATUS IN
                                   ({ContractApproverStatusEnum.WaitApproval.GetHashCode()}
                                   ,{ContractApproverStatusEnum.OverTime.GetHashCode()})
                               AND CAS_CONTRACT.STATUS IN
                                   ({ContractStatusEnum.WaitApproval.GetHashCode()}
                                   ,{ContractStatusEnum.Resubmit.GetHashCode()})
                             UNION
                            SELECT AGENT_USER.*
                              FROM CAS_CONTRACT_APPROVER
                        INNER JOIN CAS_CONTRACT
                                ON CAS_CONTRACT_APPROVER.CONTRACT_ID = CAS_CONTRACT.CONTRACT_ID
                               AND CAS_CONTRACT_APPROVER.APPROVER_TYPE =
                                   {ApproverTypeEnum.Department.GetHashCode()}
						INNER JOIN CAS_DEPARTMENT
								ON CAS_CONTRACT_APPROVER.APPROVER_ID = CAS_DEPARTMENT.DEPT_ID
						INNER JOIN CAS_DEPT_USER
								ON CAS_DEPARTMENT.DEPT_ID = CAS_DEPT_USER.DEPT_ID
                        INNER JOIN CAS_USER AUTHORIZED_USER
				                ON CAS_DEPT_USER.USER_ID = AUTHORIZED_USER.USER_ID
                        INNER JOIN CAS_PROXY_APPROVAL
								ON AUTHORIZED_USER.USER_ID = CAS_PROXY_APPROVAL.AUTHORIZED_USER_ID
                               AND GETDATE() BETWEEN CAS_PROXY_APPROVAL.BEGIN_TIME AND CAS_PROXY_APPROVAL.END_TIME AND CAS_PROXY_APPROVAL.IS_DELETED=0
                        INNER JOIN CAS_USER AGENT_USER
	                            ON CAS_PROXY_APPROVAL.AGENT_USER_ID = AGENT_USER.USER_ID
                             WHERE CAS_CONTRACT_APPROVER.STATUS IN
                                   ({ContractApproverStatusEnum.WaitApproval.GetHashCode()}
                                   ,{ContractApproverStatusEnum.OverTime.GetHashCode()})
                               AND CAS_CONTRACT.STATUS IN
                                   ({ContractStatusEnum.WaitApproval.GetHashCode()}
                                   ,{ContractStatusEnum.Resubmit.GetHashCode()})";
            return DataAccess.Select<CasUserEntity>(sql);
        }


        /// <summary>
        /// 获取所有存在待审批合同的用户
        /// </summary>
        /// <returns></returns>
        public List<CasUserEntity> GetAllPendingApprovalContractsApprovers()
        {
            string sql = $@"SELECT CAS_USER.*
                              FROM CAS_CONTRACT_APPROVER
                        INNER JOIN CAS_CONTRACT
                                ON CAS_CONTRACT_APPROVER.CONTRACT_ID = CAS_CONTRACT.CONTRACT_ID
							   AND CAS_CONTRACT_APPROVER.APPROVER_TYPE = {ApproverTypeEnum.DepartmentManager.GetHashCode()}
                        INNER JOIN CAS_USER
	                            ON CAS_CONTRACT_APPROVER.APPROVER_ID = CAS_USER.USER_ID
                             WHERE CAS_CONTRACT_APPROVER.STATUS = {ContractApproverStatusEnum.WaitApproval.GetHashCode()}
                               AND CAS_CONTRACT.STATUS IN
                                   ({ContractStatusEnum.WaitApproval.GetHashCode()}
                                   ,{ContractStatusEnum.Resubmit.GetHashCode()})
                             UNION
                            SELECT CAS_USER.*
                              FROM CAS_CONTRACT_APPROVER
                        INNER JOIN CAS_CONTRACT
                                ON CAS_CONTRACT_APPROVER.CONTRACT_ID = CAS_CONTRACT.CONTRACT_ID
							   AND CAS_CONTRACT_APPROVER.APPROVER_TYPE = {ApproverTypeEnum.DepartmentManager.GetHashCode()}
                        INNER JOIN CAS_PROXY_APPROVAL
								ON CAS_CONTRACT_APPROVER.APPROVER_ID = CAS_PROXY_APPROVAL.AUTHORIZED_USER_ID
                               AND GETDATE() BETWEEN CAS_PROXY_APPROVAL.BEGIN_TIME AND CAS_PROXY_APPROVAL.END_TIME AND CAS_PROXY_APPROVAL.IS_DELETED=0
                        INNER JOIN CAS_USER
	                            ON CAS_PROXY_APPROVAL.AGENT_USER_ID = CAS_USER.USER_ID
                             WHERE CAS_CONTRACT_APPROVER.STATUS = {ContractApproverStatusEnum.WaitApproval.GetHashCode()}
                               AND CAS_CONTRACT.STATUS IN
                                   ({ContractStatusEnum.WaitApproval.GetHashCode()}
                                   ,{ContractStatusEnum.Resubmit.GetHashCode()})
                              UNION
                            SELECT CAS_USER.*
                              FROM CAS_CONTRACT_APPROVER
                        INNER JOIN CAS_CONTRACT
                                ON CAS_CONTRACT_APPROVER.CONTRACT_ID = CAS_CONTRACT.CONTRACT_ID
                               AND CAS_CONTRACT_APPROVER.APPROVER_TYPE =
                                   {ApproverTypeEnum.Department.GetHashCode()}
						INNER JOIN CAS_DEPARTMENT
								ON CAS_CONTRACT_APPROVER.APPROVER_ID = CAS_DEPARTMENT.DEPT_ID
						INNER JOIN CAS_DEPT_USER
								ON CAS_DEPARTMENT.DEPT_ID = CAS_DEPT_USER.DEPT_ID
                        INNER JOIN CAS_USER
				                ON CAS_DEPT_USER.USER_ID = CAS_USER.USER_ID
                             WHERE CAS_CONTRACT_APPROVER.STATUS = {ContractApproverStatusEnum.WaitApproval.GetHashCode()}
                               AND CAS_CONTRACT.STATUS IN
                                   ({ContractStatusEnum.WaitApproval.GetHashCode()}
                                   ,{ContractStatusEnum.Resubmit.GetHashCode()})
                             UNION
                            SELECT AGENT_USER.*
                              FROM CAS_CONTRACT_APPROVER
                        INNER JOIN CAS_CONTRACT
                                ON CAS_CONTRACT_APPROVER.CONTRACT_ID = CAS_CONTRACT.CONTRACT_ID
                               AND CAS_CONTRACT_APPROVER.APPROVER_TYPE =
                                   {ApproverTypeEnum.Department.GetHashCode()}
						INNER JOIN CAS_DEPARTMENT
								ON CAS_CONTRACT_APPROVER.APPROVER_ID = CAS_DEPARTMENT.DEPT_ID
						INNER JOIN CAS_DEPT_USER
								ON CAS_DEPARTMENT.DEPT_ID = CAS_DEPT_USER.DEPT_ID
                        INNER JOIN CAS_USER AUTHORIZED_USER
				                ON CAS_DEPT_USER.USER_ID = AUTHORIZED_USER.USER_ID
                        INNER JOIN CAS_PROXY_APPROVAL
								ON AUTHORIZED_USER.USER_ID = CAS_PROXY_APPROVAL.AUTHORIZED_USER_ID
                               AND GETDATE() BETWEEN CAS_PROXY_APPROVAL.BEGIN_TIME AND CAS_PROXY_APPROVAL.END_TIME AND CAS_PROXY_APPROVAL.IS_DELETED=0
                        INNER JOIN CAS_USER AGENT_USER
	                            ON CAS_PROXY_APPROVAL.AGENT_USER_ID = AGENT_USER.USER_ID
                             WHERE CAS_CONTRACT_APPROVER.STATUS = {ContractApproverStatusEnum.WaitApproval.GetHashCode()}
                               AND CAS_CONTRACT.STATUS IN
                                   ({ContractStatusEnum.WaitApproval.GetHashCode()}
                                   ,{ContractStatusEnum.Resubmit.GetHashCode()})";
            return DataAccess.Select<CasUserEntity>(sql);
        }


        /// <summary>
        /// 获取所有存在超时合同的用户
        /// </summary>
        /// <returns></returns>
        public List<CasUserEntity> GetAllPendingOvertimeContractsApprovers()
        {
            string sql = $@"SELECT CAS_USER.*
                              FROM CAS_CONTRACT_APPROVER
                        INNER JOIN CAS_CONTRACT
                                ON CAS_CONTRACT_APPROVER.CONTRACT_ID = CAS_CONTRACT.CONTRACT_ID
							   AND CAS_CONTRACT_APPROVER.APPROVER_TYPE = {ApproverTypeEnum.DepartmentManager.GetHashCode()}
                        INNER JOIN CAS_USER
	                            ON CAS_CONTRACT_APPROVER.APPROVER_ID = CAS_USER.USER_ID
                             WHERE CAS_CONTRACT_APPROVER.STATUS = {ContractApproverStatusEnum.OverTime.GetHashCode()}
                               AND CAS_CONTRACT.STATUS IN
                                   ({ContractStatusEnum.WaitApproval.GetHashCode()}
                                   ,{ContractStatusEnum.Resubmit.GetHashCode()})
                             UNION
                            SELECT CAS_USER.*
                              FROM CAS_CONTRACT_APPROVER
                        INNER JOIN CAS_CONTRACT
                                ON CAS_CONTRACT_APPROVER.CONTRACT_ID = CAS_CONTRACT.CONTRACT_ID
							   AND CAS_CONTRACT_APPROVER.APPROVER_TYPE = {ApproverTypeEnum.DepartmentManager.GetHashCode()}
                        INNER JOIN CAS_PROXY_APPROVAL
								ON CAS_CONTRACT_APPROVER.APPROVER_ID = CAS_PROXY_APPROVAL.AUTHORIZED_USER_ID
                               AND GETDATE() BETWEEN CAS_PROXY_APPROVAL.BEGIN_TIME AND CAS_PROXY_APPROVAL.END_TIME AND CAS_PROXY_APPROVAL.IS_DELETED=0
                        INNER JOIN CAS_USER
	                            ON CAS_PROXY_APPROVAL.AGENT_USER_ID = CAS_USER.USER_ID
                             WHERE CAS_CONTRACT_APPROVER.STATUS = {ContractApproverStatusEnum.OverTime.GetHashCode()}
                               AND CAS_CONTRACT.STATUS IN
                                   ({ContractStatusEnum.WaitApproval.GetHashCode()}
                                   ,{ContractStatusEnum.Resubmit.GetHashCode()})
                              UNION
                            SELECT CAS_USER.*
                              FROM CAS_CONTRACT_APPROVER
                        INNER JOIN CAS_CONTRACT
                                ON CAS_CONTRACT_APPROVER.CONTRACT_ID = CAS_CONTRACT.CONTRACT_ID
                               AND CAS_CONTRACT_APPROVER.APPROVER_TYPE =
                                   {ApproverTypeEnum.Department.GetHashCode()}
						INNER JOIN CAS_DEPARTMENT
								ON CAS_CONTRACT_APPROVER.APPROVER_ID = CAS_DEPARTMENT.DEPT_ID
						INNER JOIN CAS_DEPT_USER
								ON CAS_DEPARTMENT.DEPT_ID = CAS_DEPT_USER.DEPT_ID
                        INNER JOIN CAS_USER
				                ON CAS_DEPT_USER.USER_ID = CAS_USER.USER_ID
                             WHERE CAS_CONTRACT_APPROVER.STATUS = {ContractApproverStatusEnum.OverTime.GetHashCode()}
                               AND CAS_CONTRACT.STATUS IN
                                   ({ContractStatusEnum.WaitApproval.GetHashCode()}
                                   ,{ContractStatusEnum.Resubmit.GetHashCode()})
                             UNION
                            SELECT AGENT_USER.*
                              FROM CAS_CONTRACT_APPROVER
                        INNER JOIN CAS_CONTRACT
                                ON CAS_CONTRACT_APPROVER.CONTRACT_ID = CAS_CONTRACT.CONTRACT_ID
                               AND CAS_CONTRACT_APPROVER.APPROVER_TYPE =
                                   {ApproverTypeEnum.Department.GetHashCode()}
						INNER JOIN CAS_DEPARTMENT
								ON CAS_CONTRACT_APPROVER.APPROVER_ID = CAS_DEPARTMENT.DEPT_ID
						INNER JOIN CAS_DEPT_USER
								ON CAS_DEPARTMENT.DEPT_ID = CAS_DEPT_USER.DEPT_ID
                        INNER JOIN CAS_USER AUTHORIZED_USER
				                ON CAS_DEPT_USER.USER_ID = AUTHORIZED_USER.USER_ID
                        INNER JOIN CAS_PROXY_APPROVAL
								ON AUTHORIZED_USER.USER_ID = CAS_PROXY_APPROVAL.AUTHORIZED_USER_ID
                               AND GETDATE() BETWEEN CAS_PROXY_APPROVAL.BEGIN_TIME AND CAS_PROXY_APPROVAL.END_TIME AND CAS_PROXY_APPROVAL.IS_DELETED=0
                        INNER JOIN CAS_USER AGENT_USER
	                            ON CAS_PROXY_APPROVAL.AGENT_USER_ID = AGENT_USER.USER_ID
                             WHERE CAS_CONTRACT_APPROVER.STATUS = {ContractApproverStatusEnum.OverTime.GetHashCode()}
                               AND CAS_CONTRACT.STATUS IN
                                   ({ContractStatusEnum.WaitApproval.GetHashCode()}
                                   ,{ContractStatusEnum.Resubmit.GetHashCode()})";
            return DataAccess.Select<CasUserEntity>(sql);
        }

        /// <summary>
        /// 获取指定用户的所有待审批和超时合同
        /// </summary>
        /// <param name="approverId"></param>
        /// <returns></returns>
        public List<CasContractApproverEntity> GetPendingApprovalAndOvertimeContractApprovers(string approverId)
        {
            string sql = $@"SELECT CAS_CONTRACT_APPROVER.*
                              FROM CAS_CONTRACT_APPROVER
                        INNER JOIN CAS_CONTRACT
                                ON CAS_CONTRACT_APPROVER.CONTRACT_ID = CAS_CONTRACT.CONTRACT_ID
							   AND CAS_CONTRACT_APPROVER.APPROVER_TYPE = {ApproverTypeEnum.DepartmentManager.GetHashCode()}
                             WHERE CAS_CONTRACT_APPROVER.STATUS IN
                                   ({ContractApproverStatusEnum.WaitApproval.GetHashCode()}
                                   ,{ContractApproverStatusEnum.OverTime.GetHashCode()})
                               AND CAS_CONTRACT.STATUS IN
                                   ({ContractStatusEnum.WaitApproval.GetHashCode()}
                                   ,{ContractStatusEnum.Resubmit.GetHashCode()})
                               AND (APPROVER_ID = '{approverId}'
                                   OR CAS_CONTRACT_APPROVER.APPROVER_ID IN 
                                        (SELECT CAS_PROXY_APPROVAL.AUTHORIZED_USER_ID 
                                           FROM CAS_PROXY_APPROVAL
                                          WHERE CAS_PROXY_APPROVAL.AGENT_USER_ID = '{approverId}'
                                            AND GETDATE() BETWEEN CAS_PROXY_APPROVAL.BEGIN_TIME AND CAS_PROXY_APPROVAL.END_TIME AND CAS_PROXY_APPROVAL.IS_DELETED=0))
                             UNION
                            SELECT CAS_CONTRACT_APPROVER.*
                              FROM CAS_CONTRACT_APPROVER
                        INNER JOIN CAS_CONTRACT
                                ON CAS_CONTRACT_APPROVER.CONTRACT_ID = CAS_CONTRACT.CONTRACT_ID
                               AND CAS_CONTRACT_APPROVER.APPROVER_TYPE = 
                                   {ApproverTypeEnum.Department.GetHashCode()}
						INNER JOIN CAS_DEPARTMENT
								ON CAS_CONTRACT_APPROVER.APPROVER_ID = CAS_DEPARTMENT.DEPT_ID
						INNER JOIN CAS_DEPT_USER
								ON CAS_DEPARTMENT.DEPT_ID = CAS_DEPT_USER.DEPT_ID
                        INNER JOIN CAS_USER
				                ON CAS_DEPT_USER.USER_ID = CAS_USER.USER_ID
                             WHERE CAS_CONTRACT_APPROVER.STATUS IN
                                   ({ContractApproverStatusEnum.WaitApproval.GetHashCode()}
                                   ,{ContractApproverStatusEnum.OverTime.GetHashCode()})
                               AND CAS_CONTRACT.STATUS IN
                                   ({ContractStatusEnum.WaitApproval.GetHashCode()}
                                   ,{ContractStatusEnum.Resubmit.GetHashCode()})
                               AND (CAS_USER.USER_ID = '{approverId}'
                                   OR CAS_USER.USER_ID IN 
                                        (SELECT CAS_PROXY_APPROVAL.AUTHORIZED_USER_ID 
                                           FROM CAS_PROXY_APPROVAL
                                          WHERE CAS_PROXY_APPROVAL.AGENT_USER_ID = '{approverId}'
                                            AND GETDATE() BETWEEN CAS_PROXY_APPROVAL.BEGIN_TIME AND CAS_PROXY_APPROVAL.END_TIME AND CAS_PROXY_APPROVAL.IS_DELETED=0)) ";
            return DataAccess.Select<CasContractApproverEntity>(sql);
        }


        /// <summary>
        /// 获取指定用户的所有待审批合同
        /// </summary>
        /// <param name="approverId"></param>
        /// <returns></returns>
        public List<CasContractApproverEntity> GetPendingApprovalContractApprovers(string approverId)
        {
            string sql = $@"SELECT CAS_CONTRACT_APPROVER.*
                              FROM CAS_CONTRACT_APPROVER
                        INNER JOIN CAS_CONTRACT
                                ON CAS_CONTRACT_APPROVER.CONTRACT_ID = CAS_CONTRACT.CONTRACT_ID
							   AND CAS_CONTRACT_APPROVER.APPROVER_TYPE = {ApproverTypeEnum.DepartmentManager.GetHashCode()}
                             WHERE CAS_CONTRACT_APPROVER.STATUS = {ContractApproverStatusEnum.WaitApproval.GetHashCode()}
                               AND CAS_CONTRACT.STATUS IN
                                   ({ContractStatusEnum.WaitApproval.GetHashCode()}
                                   ,{ContractStatusEnum.Resubmit.GetHashCode()})
                               AND (APPROVER_ID = '{approverId}'
                                   OR CAS_CONTRACT_APPROVER.APPROVER_ID IN 
                                        (SELECT CAS_PROXY_APPROVAL.AUTHORIZED_USER_ID 
                                           FROM CAS_PROXY_APPROVAL
                                          WHERE CAS_PROXY_APPROVAL.AGENT_USER_ID = '{approverId}'
                                            AND GETDATE() BETWEEN CAS_PROXY_APPROVAL.BEGIN_TIME AND CAS_PROXY_APPROVAL.END_TIME AND CAS_PROXY_APPROVAL.IS_DELETED=0))
                             UNION
                            SELECT CAS_CONTRACT_APPROVER.*
                              FROM CAS_CONTRACT_APPROVER
                        INNER JOIN CAS_CONTRACT
                                ON CAS_CONTRACT_APPROVER.CONTRACT_ID = CAS_CONTRACT.CONTRACT_ID
                               AND CAS_CONTRACT_APPROVER.APPROVER_TYPE = 
                                   {ApproverTypeEnum.Department.GetHashCode()}
						INNER JOIN CAS_DEPARTMENT
								ON CAS_CONTRACT_APPROVER.APPROVER_ID = CAS_DEPARTMENT.DEPT_ID
						INNER JOIN CAS_DEPT_USER
								ON CAS_DEPARTMENT.DEPT_ID = CAS_DEPT_USER.DEPT_ID
                        INNER JOIN CAS_USER
				                ON CAS_DEPT_USER.USER_ID = CAS_USER.USER_ID
                             WHERE CAS_CONTRACT_APPROVER.STATUS = {ContractApproverStatusEnum.WaitApproval.GetHashCode()}
                               AND CAS_CONTRACT.STATUS IN
                                   ({ContractStatusEnum.WaitApproval.GetHashCode()}
                                   ,{ContractStatusEnum.Resubmit.GetHashCode()})
                               AND (CAS_USER.USER_ID = '{approverId}'
                                   OR CAS_USER.USER_ID IN 
                                        (SELECT CAS_PROXY_APPROVAL.AUTHORIZED_USER_ID 
                                           FROM CAS_PROXY_APPROVAL
                                          WHERE CAS_PROXY_APPROVAL.AGENT_USER_ID = '{approverId}'
                                            AND GETDATE() BETWEEN CAS_PROXY_APPROVAL.BEGIN_TIME AND CAS_PROXY_APPROVAL.END_TIME AND CAS_PROXY_APPROVAL.IS_DELETED=0)) ";
            return DataAccess.Select<CasContractApproverEntity>(sql);
        }

        /// <summary>
        /// 获取指定用户的所有超时合同
        /// </summary>
        /// <param name="approverId"></param>
        /// <returns></returns>
        public List<CasContractApproverEntity> GetPendingOvertimeContractApprovers(string approverId)
        {
            string sql = $@"SELECT CAS_CONTRACT_APPROVER.*
                              FROM CAS_CONTRACT_APPROVER
                        INNER JOIN CAS_CONTRACT
                                ON CAS_CONTRACT_APPROVER.CONTRACT_ID = CAS_CONTRACT.CONTRACT_ID
							   AND CAS_CONTRACT_APPROVER.APPROVER_TYPE = {ApproverTypeEnum.DepartmentManager.GetHashCode()}
                             WHERE CAS_CONTRACT_APPROVER.STATUS = {ContractApproverStatusEnum.OverTime.GetHashCode()}
                               AND CAS_CONTRACT.STATUS IN
                                   ({ContractStatusEnum.WaitApproval.GetHashCode()}
                                   ,{ContractStatusEnum.Resubmit.GetHashCode()})
                               AND (APPROVER_ID = '{approverId}'
                                   OR CAS_CONTRACT_APPROVER.APPROVER_ID IN 
                                        (SELECT CAS_PROXY_APPROVAL.AUTHORIZED_USER_ID 
                                           FROM CAS_PROXY_APPROVAL
                                          WHERE CAS_PROXY_APPROVAL.AGENT_USER_ID = '{approverId}'
                                            AND GETDATE() BETWEEN CAS_PROXY_APPROVAL.BEGIN_TIME AND CAS_PROXY_APPROVAL.END_TIME AND CAS_PROXY_APPROVAL.IS_DELETED=0))
                             UNION
                            SELECT CAS_CONTRACT_APPROVER.*
                              FROM CAS_CONTRACT_APPROVER
                        INNER JOIN CAS_CONTRACT
                                ON CAS_CONTRACT_APPROVER.CONTRACT_ID = CAS_CONTRACT.CONTRACT_ID
                               AND CAS_CONTRACT_APPROVER.APPROVER_TYPE = 
                                   {ApproverTypeEnum.Department.GetHashCode()}
						INNER JOIN CAS_DEPARTMENT
								ON CAS_CONTRACT_APPROVER.APPROVER_ID = CAS_DEPARTMENT.DEPT_ID
						INNER JOIN CAS_DEPT_USER
								ON CAS_DEPARTMENT.DEPT_ID = CAS_DEPT_USER.DEPT_ID
                        INNER JOIN CAS_USER
				                ON CAS_DEPT_USER.USER_ID = CAS_USER.USER_ID
                             WHERE CAS_CONTRACT_APPROVER.STATUS = {ContractApproverStatusEnum.OverTime.GetHashCode()}
                               AND CAS_CONTRACT.STATUS IN
                                   ({ContractStatusEnum.WaitApproval.GetHashCode()}
                                   ,{ContractStatusEnum.Resubmit.GetHashCode()})
                               AND (CAS_USER.USER_ID = '{approverId}'
                                   OR CAS_USER.USER_ID IN 
                                        (SELECT CAS_PROXY_APPROVAL.AUTHORIZED_USER_ID 
                                           FROM CAS_PROXY_APPROVAL
                                          WHERE CAS_PROXY_APPROVAL.AGENT_USER_ID = '{approverId}'
                                            AND GETDATE() BETWEEN CAS_PROXY_APPROVAL.BEGIN_TIME AND CAS_PROXY_APPROVAL.END_TIME AND CAS_PROXY_APPROVAL.IS_DELETED=0)) ";
            return DataAccess.Select<CasContractApproverEntity>(sql);
        }

        /// <summary>
        /// 获取所有打回审批重新提交后3天(包含批注1天)的合同
        /// </summary>
        /// <returns></returns>
        public List<CasContractEntity> GetAllCallbackContinueOver3DaysContracts()
        {
            string sql = $@"SELECT * FROM CAS_CONTRACT
                             WHERE STATUS =  {ContractStatusEnum.Resubmit.GetHashCode()}
                               AND dbo.AddWorkDay(LAST_MODIFIED_TIME,3) < GETDATE()";
            return DataAccess.Select<CasContractEntity>(sql);
        }

        /// <summary>
        /// 打回审批重新提交后3天(包含批注1天)的合同自动继续下一步审批
        /// </summary>
        /// <param name="casContractEntity"></param>
        /// <returns></returns>
        public bool CallbackContinueOver3DaysContractsContinue(CasContractEntity casContractEntity)
        {
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                broker.BeginTransaction();
                try
                {
                    ////修改状态为待审批
                    //casContractEntity.Status = ContractStatusEnum.WaitApproval.GetHashCode();
                    //casContractEntity.LastModifiedTime = DateTime.Now;
                    //casContractEntity.LastModifiedBy = "system";
                    //Update(casContractEntity, broker);

                    //更新待审批且无审批结果的待办事项为已过期
                    string sql = $@"UPDATE CAS_CONTRACT_APPROVER 
                                       SET STATUS = {ContractApproverStatusEnum.OverTime.GetHashCode()}
                                     WHERE CONTRACT_ID = '{casContractEntity.ContractId}'
                                       AND STATUS = {ContractApproverStatusEnum.WaitApproval.GetHashCode()}
                                       AND NOT EXISTS
                                           (SELECT * 
                                              FROM CAS_CONTRACT_APPROVAL_RESULT
                                              WHERE CAS_CONTRACT_APPROVAL_RESULT.CONTRACT_APPROVER_ID = CAS_CONTRACT_APPROVER.CONTRACT_APPROVER_ID
                                               AND CAS_CONTRACT_APPROVAL_RESULT.CONTRACT_ID = '{casContractEntity.ContractId}')";
                    broker.ExecuteSQL(sql);

                    //更新待审批且已经审批通过的待办事项为已审批
                    sql = $@"UPDATE CAS_CONTRACT_APPROVER 
                                       SET STATUS = {ContractApproverStatusEnum.HadApproval.GetHashCode()}
                                     WHERE CONTRACT_ID = '{casContractEntity.ContractId}'
                                       AND STATUS = {ContractApproverStatusEnum.WaitApproval.GetHashCode()}
                                       AND EXISTS
                                           (SELECT * 
                                              FROM CAS_CONTRACT_APPROVAL_RESULT
                                            WHERE CAS_CONTRACT_APPROVAL_RESULT.CONTRACT_APPROVER_ID = CAS_CONTRACT_APPROVER.CONTRACT_APPROVER_ID
                                               AND CAS_CONTRACT_APPROVAL_RESULT.CONTRACT_ID = '{casContractEntity.ContractId}'
                                               AND (CAS_CONTRACT_APPROVAL_RESULT.APPROVAL_RESULT = {ApproverResultEnum.Approved.GetHashCode()}
                                                    OR CAS_CONTRACT_APPROVAL_RESULT.APPROVAL_RESULT = {ApproverResultEnum.NotApplicable.GetHashCode()})
                                           )
                                            ";
                    broker.ExecuteSQL(sql);

                    //查找已开始的最后一步
                    sql = $@"SELECT TOP 1 CAS_CONTRACT_APPROVER.CONTRACT_APPROVAL_STEP_ID 
                                       FROM CAS_CONTRACT_APPROVER
                                 INNER JOIN CAS_CONTRACT_APPROVAL_STEP
                                         ON CAS_CONTRACT_APPROVER.CONTRACT_APPROVAL_STEP_ID = CAS_CONTRACT_APPROVAL_STEP.CONTRACT_APPROVAL_STEP_ID
                                      WHERE CONTRACT_ID = '{casContractEntity.ContractId}'
                                        AND STATUS <> {ContractApproverStatusEnum.NotBegin.GetHashCode()}
                                   ORDER BY STEP DESC ";

                    string contractApprovalStepId = broker.ExecuteSQLScalar(sql).ToString();

                    //查找是否有已过期的（领导批注除外）
                    sql = $@"SELECT 1 FROM CAS_CONTRACT_APPROVER WHERE APPROVER_TYPE <> {ApproverTypeEnum.LeaderComment.GetHashCode()} AND CONTRACT_APPROVAL_STEP_ID = '{contractApprovalStepId}' AND CONTRACT_ID = '{casContractEntity.ContractId}' AND STATUS = {ContractApproverStatusEnum.OverTime.GetHashCode()}";
                    bool notHaveOverTime = broker.ExecuteSQLScalar(sql) == null;
                    if (notHaveOverTime)
                    {
                        //如果没有已过期的，则进行下一步审批
                        UpdateNextStepASpprovalStatus(casContractEntity.ContractId, contractApprovalStepId, broker);
                    }
                    broker.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    broker.RollBack();
                    return false;
                }
            }
        }

        /// <summary>
        /// 获取所有批注后超过1天的合同（创建的以CreateTime为准，重新提交的以LastModifiedTime为准）
        /// </summary>
        /// <returns></returns>
        public List<CasContractEntity> GetAllCommentedOver1DayContracts()
        {
            string sql = $@"SELECT * 
                              FROM CAS_CONTRACT
                             WHERE
                               (STATUS = {ContractStatusEnum.WaitApproval.GetHashCode()}
                               AND EXISTS
                                    (SELECT * 
                                       FROM CAS_CONTRACT_APPROVER
                                      WHERE CAS_CONTRACT.CONTRACT_ID = CAS_CONTRACT_APPROVER.CONTRACT_ID
                                        AND  APPROVER_TYPE = {ApproverTypeEnum.LeaderComment.GetHashCode()}
                                        AND dbo.AddWorkDay(CAS_CONTRACT.CREATE_TIME,1) < GETDATE()
                                    )
                                )
                               OR 
                               (STATUS = {ContractStatusEnum.Resubmit.GetHashCode()}
                               AND EXISTS
                                    (SELECT * 
                                       FROM CAS_CONTRACT_APPROVER
                                      WHERE CAS_CONTRACT.CONTRACT_ID = CAS_CONTRACT_APPROVER.CONTRACT_ID
                                        AND  APPROVER_TYPE = {ApproverTypeEnum.LeaderComment.GetHashCode()}
                                        AND dbo.AddWorkDay(CAS_CONTRACT.LAST_MODIFIED_TIME,1) < GETDATE()
                                    )
                                )";
            return DataAccess.Select<CasContractEntity>(sql);
        }


        /// <summary>
        /// 继续审批批注后超过1天的合同（创建的以CreateTime为准，重新提交的以LastModifiedTime为准）
        /// </summary>
        /// <param name="casContractEntity"></param>
        /// <returns></returns>
        public bool ContinueCommentedOver1DayContracts(CasContractEntity casContractEntity)
        {
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                broker.BeginTransaction();
                try
                {
                    //1、把所有待审批的批注过期
                    string sql = $@"UPDATE CAS_CONTRACT_APPROVER
                                       SET STATUS = {ContractApproverStatusEnum.OverTime.GetHashCode()},
                                           LAST_MODIFIED_TIME = GETDATE()
                                     WHERE CONTRACT_ID = '{casContractEntity.ContractId}'
                                       AND APPROVER_TYPE = {ApproverTypeEnum.LeaderComment.GetHashCode()}
                                       AND STATUS = {ContractApproverStatusEnum.WaitApproval.GetHashCode()}";
                    broker.ExecuteSQL(sql);

                    //待审批的合同，看是否第一步未开始，打回重新提交的不需要这一步
                    if (casContractEntity.Status == ContractStatusEnum.WaitApproval.GetHashCode())
                    {
                        //2、如果第一步未开始，则开始第一步
                        //获取第一个审批节点ID
                        sql = $@"SELECT TOP 1 CONTRACT_APPROVAL_STEP_ID FROM dbo.CAS_CONTRACT_APPROVER
                                    WHERE CONTRACT_ID = '{casContractEntity.ContractId}'
                                    AND STATUS = {ContractApproverStatusEnum.NotBegin.GetHashCode()}
                                    AND CONTRACT_APPROVAL_STEP_ID = (SELECT TOP 1 CONTRACT_APPROVAL_STEP_ID 
                                                                       FROM CAS_CONTRACT_APPROVAL_STEP 
                                                                      WHERE CONTRACT_TYPE_ID = '{casContractEntity.ContractTypeId}' ORDER BY STEP)";
                        object firstStepObj = broker.ExecuteSQLScalar(sql);
                        if (firstStepObj != null)//存在则激活第一个审批节点为待审批
                        {
                            sql = $@"UPDATE dbo.CAS_CONTRACT_APPROVER 
                                    SET STATUS = {ContractApproverStatusEnum.WaitApproval.GetHashCode()}, 
                                        LAST_MODIFIED_TIME = GETDATE()
                                  WHERE CONTRACT_ID = '{casContractEntity.ContractId}'
                                    AND CONTRACT_APPROVAL_STEP_ID = '{firstStepObj.ToString()}' 
                                    AND STATUS = {ContractApproverStatusEnum.NotBegin.GetHashCode()}";
                            broker.ExecuteSQL(sql);
                        }
                    }

                    broker.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    broker.RollBack();
                    return false;
                }
            }
        }

        /// <summary>
        /// 自动过期所有已过期合同审批
        /// </summary>
        public void OverTimeContractApproval()
        {
            string sql = $@"UPDATE CAS_CONTRACT_APPROVER
                               SET STATUS = {ContractApproverStatusEnum.OverTime.GetHashCode()},
                                   LAST_MODIFIED_TIME = GETDATE()
                             WHERE EXISTS
                                    (SELECT 1 
                                       FROM CAS_CONTRACT_APPROVAL_STEP
                                      WHERE CAS_CONTRACT_APPROVER.CONTRACT_APPROVAL_STEP_ID
                                          = CAS_CONTRACT_APPROVAL_STEP.CONTRACT_APPROVAL_STEP_ID
                                        AND CAS_CONTRACT_APPROVER.STATUS = {ContractApproverStatusEnum.WaitApproval.GetHashCode()}
                                        AND DATEDIFF(DAY,CAS_CONTRACT_APPROVER.LAST_MODIFIED_TIME,GETDATE())
                                          > CAS_CONTRACT_APPROVAL_STEP.APPROVAL_TIME)";
            DataAccess.ExecuteNoneQuery(sql);
        }

        public void TimeBack1Day()
        {
            string sql = $@"UPDATE CAS_CONTRACT 
                               SET CREATE_TIME = DATEADD(DAY, -1, CREATE_TIME),
                                   LAST_MODIFIED_TIME = DATEADD(DAY, -1, LAST_MODIFIED_TIME)";
            DataAccess.ExecuteNoneQuery(sql);

            sql = $@"UPDATE CAS_CONTRACT_APPROVER 
                        SET CREATE_TIME = DATEADD(DAY, -1, CREATE_TIME),
                            LAST_MODIFIED_TIME = DATEADD(DAY, -1, LAST_MODIFIED_TIME)";
            DataAccess.ExecuteNoneQuery(sql);
        }
    }
}
