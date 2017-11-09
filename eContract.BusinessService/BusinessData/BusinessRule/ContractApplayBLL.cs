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
using eContract.BusinessService.BusinessData.Service;
using eContract.BusinessService.SystemManagement.Service;
using System.Web.Script.Serialization;
using eContract.BusinessService.SystemManagement.BusinessRule;

namespace eContract.BusinessService.BusinessData.BusinessRule
{
    public class ContractApplayBLL : BusinessBase
    {
        public JqGrid ForGrid(JqGrid grid)
        {
            var query = new ContractApplayQuery();
            grid = QueryTableHelper.QueryGrid<CasContractEntity>(query, grid);
            return grid;
        }
        public JqGrid ForHistoryGrid(JqGrid grid)
        {
            var query = new HistoryContractQuery();
            query.CONTRACT_TYPE_NAME = grid.QueryField.ContainsKey("CONTRACT_TYPE_NAME") ? grid.QueryField["CONTRACT_TYPE_NAME"] : "";
            grid.QueryField.Remove("CONTRACT_TYPE_NAME");

            grid = QueryTableHelper.QueryGrid<CasContractEntity>(query, grid);
            return grid;
        }

        public virtual CasContractEntity CreateContractEntity(string systemName = "MDM")
        {
            return new CasContractEntity();
        }
        /// <summary>
        /// 合同草稿箱查询
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public JqGrid GetContractDrafts(JqGrid grid)
        {
            var query = new ContractDraftsQuery();
            grid = QueryTableHelper.QueryGrid<CasContractEntity>(query, grid);
            return grid;
        }
        /// <summary>
        /// 上传盖章合同
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public JqGrid UploadStampContract(JqGrid grid)
        {
            var query = new UploadStampContractQuery();
            query.keyWord = grid.QueryField.ContainsKey("CONTRACT_NAME") ? grid.QueryField["CONTRACT_NAME"] : "";
            grid.QueryField.Remove("CONTRACT_NAME");
            query.CONTRACT_TYPE_NAME = grid.QueryField.ContainsKey("CONTRACT_TYPE_NAME") ? grid.QueryField["CONTRACT_TYPE_NAME"] : "";
            grid.QueryField.Remove("CONTRACT_TYPE_NAME");
            grid = QueryTableHelper.QueryGrid<CasContractEntity>(query, grid);
            return grid;
        }
        /// <summary>
        /// 在附件表更新sourceID，从而和合同关联
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool SaveContractAttachment(CasContractEntity entity, ref string strError)
        {
            if (!string.IsNullOrWhiteSpace(entity.fileIds))
            {
                string[] filesids = entity.fileIds.ToString().Split(',');
                string sqlFileids = "";
                for (int i = 0; i < filesids.Length; i++) //这个FOR循环就是加单引号
                {
                    filesids[i] = "'" + filesids[i] + "'";
                    sqlFileids = sqlFileids + filesids[i] + ",";
                }
                sqlFileids = sqlFileids.Substring(0, sqlFileids.Length - 1);
                var strsql = new StringBuilder();
                strsql.AppendFormat("UPDATE CAS_ATTACHMENT SET SOURCE_ID='{0}',ATTACHMENT_TYPE={1} WHERE ATTACHMENT_ID IN ({2})", entity.ContractId, AttachmentTypeEnum.Stampntract.GetHashCode(), sqlFileids);

                if (entity.Status == ContractStatusEnum.WaitApproval.GetHashCode())
                {
                    var consql = new StringBuilder();
                    consql.AppendFormat("UPDATE CAS_CONTRACT SET STATUS= {0} WHERE CONTRACT_ID='{1}'", ContractStatusEnum.SignedCompleted.GetHashCode(), entity.ContractId);
                    var conval = DataAccess.ExecuteNoneQuery(consql.ToString());
                }
                var val = DataAccess.ExecuteNoneQuery(strsql.ToString());
                if (val <= 0)
                {
                    strError = "保存合同附件失败";
                    return false;
                }

                return true;
            }
            strError = "无附件";
            return false;
        }

        public string GetUploadFiles(string contractId)
        {
            var strsql = new StringBuilder();
            if (string.IsNullOrWhiteSpace(contractId))
            {
                contractId = "1";
            }
            strsql.AppendFormat(" SELECT ATTACHMENT_ID FROM CAS_ATTACHMENT WHERE SOURCE_ID='{0}' AND ATTACHMENT_TYPE='{1}' ", contractId, AttachmentTypeEnum.Stampntract.GetHashCode());
            DataSet userDataSet = DataAccess.SelectDataSet(strsql.ToString());
            string[] userAry = Array.ConvertAll<DataRow, string>(userDataSet.Tables[0].Rows.Cast<DataRow>().ToArray(), r => r["ATTACHMENT_ID"].ToString());
            string sqlFileids = "";
            for (int i = 0; i < userAry.Length; i++)
            {
                sqlFileids = sqlFileids + userAry[i] + ",";
            }
            if (sqlFileids.Length > 1)
            {
                sqlFileids = sqlFileids.Substring(0, sqlFileids.Length - 1);
            }
            return sqlFileids;
        }
        /// <summary>
        /// 草稿箱删除合同
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public bool DeleteContractById(List<string> idList)
        {
            return Delete<CasContractEntity>(idList);
        }
        /// <summary>
        /// 生成流水号
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string GenerateSeriaNo(CasContractEntity entity, DataAccessBroker broker)
        {
            //历史合同的流水号格式：2017100001（yyyyMM流水号）、模板合同、正常合同也要有流水号，同一个序列；20171019modify
            string prefix = DateTime.Now.ToString("yyyyMM");
            //合同编号格式：company-yy-4位流水号（例：S-17-0001、H-17-0001）
            var sql = "SELECT MAX(CONTRACT_SERIAL_NO) FROM CAS_CONTRACT WHERE CONTRACT_SERIAL_NO LIKE '" + prefix + "%' ";
            string maxContractSerialNo = broker.ExecuteSQLScalar(sql).ToString();

            if (string.IsNullOrEmpty(maxContractSerialNo))
            {
                entity.ContractSerialNo = prefix + "0001";
            }
            else
            {
                entity.ContractSerialNo = prefix + (int.Parse(maxContractSerialNo.Substring(6, 4)) + 1).ToString().PadLeft(4, '0');
            }
            return entity.ContractSerialNo;
        }

        /// <summary>
        /// 保存合同申请
        /// </summary>
        /// <returns></returns>
        public bool Save(CasContractEntity entity, string saveType, string fileIds_mine, string fileIds_original, ref string msg)
        {
            //保存：保存合同申请
            //提交：保存合同申请，审批结果表添加记录（领导批注+审批人审批）
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                broker.BeginTransaction();
                bool flag = false;
                try
                {
                    CasContractEntity oldContractEntity = BusinessDataService.ContractApplayService.GetById<CasContractEntity>(entity.ContractId);
                    CasContractEntity newEntity = PrepareEntity(entity);

                    if ((bool)newEntity.IsTemplateContract)
                    {
                        flag = CheckTemplateNo(newEntity.TemplateNo, newEntity.ContractId);
                        if (!flag)
                        {
                            msg = "This template number is existing, please re-input.";
                            broker.RollBack();
                            return flag;
                        }
                    }

                    //打回后重新提交的合同，状态为重新提交
                    if (oldContractEntity != null && oldContractEntity.Status == ContractStatusEnum.ApprovalReject.GetHashCode()
                        && saveType == SaveTypeEnum.Submit.GetHashCode().ToString())
                    {
                        newEntity.Status = ContractStatusEnum.Resubmit.GetHashCode();//重新提交
                        #region 合同申请用户操作记录
                        AddResultInApprovalResult(oldContractEntity.ContractId, 6, 6);
                        #endregion

                    }
                    //暂时不用了                                                                                                                                                 
                    //#region  合同申请用户操作记录
                    //if (oldContractEntity.Status != ContractStatusEnum.ApprovalReject.GetHashCode() && saveType == SaveTypeEnum.Submit.GetHashCode().ToString())//新申请
                    //{
                    //    AddResultInApprovalResult(oldContractEntity.ContractId, 7, 7);
                    //}
                    //#endregion
                    //打回后保存的合同，状态保持审批拒绝
                    else if (oldContractEntity != null && oldContractEntity.Status == ContractStatusEnum.ApprovalReject.GetHashCode()
                       && saveType == SaveTypeEnum.Save.GetHashCode().ToString())
                    {
                        newEntity.Status = ContractStatusEnum.ApprovalReject.GetHashCode();//状态不变
                    }
                    //提交普通合同，状态为待审批
                    else if (saveType == SaveTypeEnum.Submit.GetHashCode().ToString() && entity.ContractGroup == ContractGroupEnum.NormalContract.GetHashCode())
                    {
                        newEntity.Status = ContractStatusEnum.WaitApproval.GetHashCode();//待审批
                    }
                    //提交历史合同，状态为签署完成
                    else if (saveType == SaveTypeEnum.Submit.GetHashCode().ToString() && entity.ContractGroup == ContractGroupEnum.HistoryContract.GetHashCode())
                    {
                        newEntity.Status = ContractStatusEnum.SignedCompleted.GetHashCode();//签署完成
                        //历史合同的流水号格式：2017100001（yyyyMM流水号）、模板合同、正常合同也要有流水号，同一个序列
                        string prefix = DateTime.Now.ToString("yyyyMM");
                        //合同编号格式：company-yy-4位流水号（例：S-17-0001、H-17-0001）
                        string sql = "SELECT MAX(CONTRACT_SERIAL_NO) FROM CAS_CONTRACT WHERE CONTRACT_SERIAL_NO LIKE '" + prefix + "%' ";
                        string maxContractSerialNo = broker.ExecuteSQLScalar(sql).ToString();
                        if (string.IsNullOrEmpty(maxContractSerialNo))
                        {
                            newEntity.ContractSerialNo = prefix + "0001";
                        }
                        else
                        {
                            newEntity.ContractSerialNo = prefix + (int.Parse(maxContractSerialNo.Substring(6, 4)) + 1).ToString().PadLeft(4, '0');
                        }
                    }
                    //保存合同
                    else if (saveType == SaveTypeEnum.Save.GetHashCode().ToString())
                    {
                        newEntity.Status = ContractStatusEnum.Uncommitted.GetHashCode();//未提交
                        //生成一个流水号
                        if (string.IsNullOrWhiteSpace(newEntity.ContractSerialNo) && entity.ContractGroup != ContractGroupEnum.HistoryContract.GetHashCode())
                        {
                            newEntity.ContractSerialNo = GenerateSeriaNo(newEntity, broker);
                        }
                    }
                    else
                    {
                        newEntity.Status = ContractStatusEnum.Error.GetHashCode();//异常状态
                    }

                    //新增
                    if (string.IsNullOrEmpty(newEntity.ContractId))
                    {

                        newEntity.ContractId = Guid.NewGuid().ToString();
                        //如果没有流水号，就新生成一个,且不是历史合同
                        if (string.IsNullOrWhiteSpace(newEntity.ContractSerialNo) && entity.ContractGroup != ContractGroupEnum.HistoryContract.GetHashCode())
                        {
                            newEntity.ContractSerialNo = GenerateSeriaNo(newEntity, broker);
                        }
                        DataAccess.Insert(newEntity, broker);
                        #region 合同申请用户操作记录
                        AddResultInApprovalResult(newEntity.ContractId, 7, 7);
                        #endregion

                    }
                    else//修改
                    {
                        DataAccess.Update(newEntity, broker);
                    }

                    int attachmentTyep;//合同原件
                    int oldContractTye;//原始合同文件
                    attachmentTyep = AttachmentTypeEnum.OriginalContract.GetHashCode();
                    oldContractTye = AttachmentTypeEnum.OldContract.GetHashCode();
                    #region 保存附件
                    //#region 如果是历史合同附件类型都是盖章合同
                    //int attachmentTyep;//合同原件
                    //int oldContractTye;//原始合同文件
                    //if (entity.ContractGroup == ContractGroupEnum.HistoryContract.GetHashCode())
                    //{
                    //    attachmentTyep = AttachmentTypeEnum.Stampntract.GetHashCode();
                    //    oldContractTye = AttachmentTypeEnum.Stampntract.GetHashCode();
                    //}
                    //else
                    //{
                    //    attachmentTyep = AttachmentTypeEnum.OriginalContract.GetHashCode();
                    //    oldContractTye = AttachmentTypeEnum.OldContract.GetHashCode();
                    //}
                    //#endregion
                    //合同原件
                    if (fileIds_mine != null)
                    {
                        string paramFileIdMines = $@"'{fileIds_mine.Replace(",", "','")}'";
                        string uploadSql = $@"UPDATE dbo.CAS_ATTACHMENT SET SOURCE_ID = '{newEntity.ContractId}'
                                            ,ATTACHMENT_TYPE = {attachmentTyep} WHERE ATTACHMENT_ID IN ({paramFileIdMines})";
                        broker.ExecuteSQL(uploadSql);
                    }

                    //原始合同文件
                    if ((bool)newEntity.Supplementary)
                    {
                        //再次校验原始合同
                        if (!string.IsNullOrEmpty(newEntity.OriginalContractId) && !string.IsNullOrEmpty(fileIds_original))
                        {
                            msg = "The document is exception, please refresh the page and re-save.";//原始合同异常，请刷新该页面再次保存
                            broker.RollBack();
                            flag = false;
                            return flag;
                        }

                        if (fileIds_original != null)
                        {

                            string paramFileIdOriginals = $@"'{fileIds_original.Replace(",", "','")}'";
                            string uploadSql = $@"UPDATE dbo.CAS_ATTACHMENT SET SOURCE_ID = '{newEntity.ContractId}'
                                            ,ATTACHMENT_TYPE = {oldContractTye} WHERE ATTACHMENT_ID IN ({paramFileIdOriginals})";
                            broker.ExecuteSQL(uploadSql);
                        }
                    }
                    else//删除原始合同文件
                    {
                        string delUploadSql = $@"DELETE FROM dbo.CAS_ATTACHMENT WHERE SOURCE_ID = '{newEntity.ContractId}' AND ATTACHMENT_TYPE = {oldContractTye}";
                        broker.ExecuteSQL(delUploadSql);
                    }
                    #endregion

                    //提交
                    if (saveType == SaveTypeEnum.Submit.GetHashCode().ToString())
                    {
                        //历史合同
                        if (entity.ContractGroup == ContractGroupEnum.HistoryContract.GetHashCode())
                        {
                            //历史合同的话把所有附件全部变为盖章合同
                            string uploadSql = $@"UPDATE dbo.CAS_ATTACHMENT SET ATTACHMENT_TYPE = {AttachmentTypeEnum.Stampntract.GetHashCode()} WHERE SOURCE_ID ='{newEntity.ContractId}'";
                            broker.ExecuteSQL(uploadSql);
                            //新生成一个流水号，注释掉，前面已经生成过了  
                            //if (string.IsNullOrWhiteSpace(entity.ContractSerialNo))
                            //{
                            //    entity.ContractSerialNo = GenerateSeriaNo(entity, broker);
                            //}
                            flag = true;
                        }
                        //普通合同
                        else if (entity.ContractGroup == ContractGroupEnum.NormalContract.GetHashCode())
                        {
                            if (oldContractEntity != null && oldContractEntity.Status == 4)//拒绝后重新提交
                            {
                                //更新以前已审批的审批节点所有审批人的状态为待审批
                                string updateApproverStatusSql = $@"UPDATE dbo.CAS_CONTRACT_APPROVER 
                                                                       SET STATUS = {ContractApproverStatusEnum.WaitApproval.GetHashCode()},
                                                                           LAST_MODIFIED_TIME = GETDATE()
                                                                     WHERE CONTRACT_APPROVAL_STEP_ID IN 
                                                                           (SELECT DISTINCT t1.CONTRACT_APPROVAL_STEP_ID 
                                                                              FROM dbo.CAS_CONTRACT_APPROVER t1 
                                                                             WHERE t1.CONTRACT_ID = '{entity.ContractId}'
                                                                               AND t1.STATUS = {ContractApproverStatusEnum.HadApproval.GetHashCode()} )
                                                                       AND CONTRACT_ID = '{entity.ContractId}'";
                                broker.ExecuteSQL(updateApproverStatusSql);
                                flag = true;
                            }
                            else
                            {
                                #region 添加待办事项：领导批注 审批人审批

                                //准备待办事项
                                List<CasContractApproverEntity> casContractApproverList = new List<CasContractApproverEntity>();

                                //获取合同类型的审批步骤集合
                                DataTable approveStepDt = GetContractApprovalStepsByTypeId(newEntity.ContractTypeId);

                                #region 领导批注
                                if (newEntity.NeedComment != null && (bool)newEntity.NeedComment)
                                {
                                    DataTable lineManagerDt = GetLineManager();
                                    if (lineManagerDt.Rows.Count > 0)
                                    {
                                        for (int index = 0; index < lineManagerDt.Rows.Count; index++)
                                        {
                                            CasContractApproverEntity lineManagerApproveEntity = new CasContractApproverEntity
                                            {
                                                ContractApproverId = Guid.NewGuid().ToString(),
                                                ApproverType = 1,
                                                ApproverId = lineManagerDt.Rows[index]["USER_ID"].ToString(),
                                                ContractId = newEntity.ContractId,
                                                CreatedBy = CurrentUserDomain.CasUserEntity.UserId,
                                                CreateTime = DateTime.Now,
                                                IsDeleted = false,
                                                LastModifiedBy = CurrentUserDomain.CasUserEntity.UserId,
                                                LastModifiedTime = DateTime.Now,
                                                Status = ContractApproverStatusEnum.WaitApproval.GetHashCode()
                                            };
                                            casContractApproverList.Add(lineManagerApproveEntity);
                                            #region 发送邮件给领导
                                            var title = $@"e-Approval - Contract Approval";
                                            var content = $@"Dear:{lineManagerDt.Rows[index]["ENGLISH_NAME"].ToString()},</br>尊敬的：{lineManagerDt.Rows[index]["CHINESE_NAME"].ToString()}</br></br> The following contract has been submitted to Contract Approval System for approval. As manager of the contract initiator, you may provide your comment on the contract (if any) within 1 working day.  Your comment will serve as reference when department head approves the contract:</br>以下合同已在系统中被提交审批。作为合同申请人的上级主管/经理，您可在1个工作日内提供您对合同的意见（如有）。您的意见将作为部门总监审批合同的参考：</br></br>  Initiator 申请人:{CurrentUserDomain.CasUserEntity.ChineseName}    {CurrentUserDomain.CasUserEntity.EnglishName}</br>
                              Department 用户部门:{CurrentUserDomain.CasUserEntity.DeparmentName}</br> 
                              Contract Name 合同名称:{newEntity.ContractName}{newEntity.TemplateName}</br> 
                              Counter Party  相对方:{newEntity.CounterpartyCn}{newEntity.CounterpartyEn} </br>
                              Contract Owner 合同经办部门：{newEntity.ContractOwner}{newEntity.TemplateOwner}</br>

                              </br>Ferrero China Contract Approval System
                              </br>费列罗中国合同审批系统";
                                            var reciever = lineManagerDt.Rows[index]["EMAIL"].ToString();
                                            var cc = "chinacontractsys@ferrero.com.cn";
                                            SendEmail.Send(reciever, cc, title, content);
                                            #endregion

                                        }
                                    }
                                }
                                #endregion

                                #region 审批

                                CasContractTypeEntity contractType = BusinessDataService.ContractTypeManagementService.GetById<CasContractTypeEntity>(newEntity.ContractTypeId);

                                //是否有领导批注 true没有 false有
                                bool noComment = newEntity.NeedComment == null
                                    || !(bool)newEntity.NeedComment
                                    || casContractApproverList.Count == 0
                                    || !contractType.NeedComment.Value;


                                //确定待审批步骤
                                string step = approveStepDt.Rows[0]["STEP"].ToString();//1

                                DataView dv = approveStepDt.DefaultView;
                                DataRow[] drRegionMans = dv.ToTable(true, new string[] { "CONTRACT_APPROVAL_STEP_ID"
                            , "STEP", "NEED_REGION_MANAGER" }).Select("NEED_REGION_MANAGER='True'");
                                DataRow[] drDeptMans = dv.ToTable(true, new string[] { "CONTRACT_APPROVAL_STEP_ID"
                            , "STEP", "NEED_DEPT_MANAGER" }).Select("NEED_DEPT_MANAGER='True'");
                                DataRow[] drLineMans = dv.ToTable(true, new string[] { "CONTRACT_APPROVAL_STEP_ID"
                            , "STEP", "NEED_LINE_MANAGER" }).Select("NEED_LINE_MANAGER='True'");

                                #region 需要大区总监审批
                                foreach (DataRow dr in drRegionMans)
                                {
                                    int approveStatus = ContractApproverStatusEnum.NotBegin.GetHashCode();//审批状态默认未开始
                                                                                                          //没有领导批注且是第一节点
                                    if (dr["STEP"].ToString() == step && noComment)
                                    {
                                        approveStatus = ContractApproverStatusEnum.WaitApproval.GetHashCode();//待审批
                                        #region 发送邮件给大区总监
                                        var regionManagerID = GetRegionManagerId();
                                        if (!string.IsNullOrWhiteSpace(regionManagerID))
                                        {
                                            CasUserEntity managerInfo = GetById<CasUserEntity>(regionManagerID);
                                            var title = "e-Approval - Contract Approval";
                                            var content = $@"Dear:{managerInfo.EnglishName},</br>尊敬的：{managerInfo.ChineseName}</br></br>The following contract has been submitted for your approval (please kindly confirm your approval or rejection within 2 working days):</br>以下合同申请需要您的审核（请在2个工作日内批准或拒绝申请）：</br>
                                        </br> Contract Name 合同名称:{newEntity.ContractName}{newEntity.TemplateName}
                                        </br> Counter Party  相对方:{newEntity.CounterpartyEn}{newEntity.CounterpartyCn} 
                                        </br> Initiator 申请人:{CurrentUserDomain.CasUserEntity.EnglishName} {CurrentUserDomain.CasUserEntity.ChineseName}
                                        </br> Contract Owner 合同经办部门:{CurrentUserDomain.CasUserEntity.DeparmentName}                                       
                                        </br></br> Ferrero China Contract Approval System
                                        </br> 费列罗中国合同审批系统";
                                            var cc = $@"chinacontractsys@ferrero.com.cn";
                                            var reciever = managerInfo.Email;
                                            SendEmail.Send(reciever, cc, title, content);
                                        }
                                        #endregion
                                    }
                                    CasContractApproverEntity regionApproveEntity = new CasContractApproverEntity
                                    {
                                        ContractApproverId = Guid.NewGuid().ToString(),
                                        ApproverType = ApproverTypeEnum.RegionManager.GetHashCode(),
                                        ApproverId = GetRegionManagerId(),
                                        ContractApprovalStepId = dr["CONTRACT_APPROVAL_STEP_ID"].ToString(),
                                        ContractId = newEntity.ContractId,
                                        CreatedBy = CurrentUserDomain.CasUserEntity.UserId,
                                        CreateTime = DateTime.Now,
                                        IsDeleted = false,
                                        LastModifiedBy = CurrentUserDomain.CasUserEntity.UserId,
                                        LastModifiedTime = DateTime.Now,
                                        Status = approveStatus
                                    };
                                    casContractApproverList.Add(regionApproveEntity);

                                }
                                #endregion

                                #region 需要部门总监审批
                                foreach (DataRow dr in drDeptMans)
                                {
                                    int approveStatus = ContractApproverStatusEnum.NotBegin.GetHashCode();//审批状态默认未开始
                                                                                                          //没有领导批注且是第一节点
                                    if (dr["STEP"].ToString() == step && noComment)
                                    {
                                        approveStatus = ContractApproverStatusEnum.WaitApproval.GetHashCode();//待审批
                                        #region 发送邮件给部门总监
                                        var depManagerID = GetDeptManagerId();
                                        if (!string.IsNullOrWhiteSpace(depManagerID))
                                        {
                                            CasUserEntity depManagerInfo = GetById<CasUserEntity>(depManagerID);
                                            var title = "e-Approval - Contract Approval";
                                            var content = $@"Dear:{depManagerInfo.EnglishName},</br>尊敬的：{depManagerInfo.ChineseName}</br></br>The following contract has been submitted for your approval (please kindly confirm your approval or rejection within 2 working days):</br>以下合同申请需要您的审核（请在2个工作日内批准或拒绝申请）：</br>
                                        </br> Contract Name 合同名称:{newEntity.ContractName}{newEntity.TemplateName}
                                        </br> Counter Party  相对方:{newEntity.CounterpartyEn}{newEntity.CounterpartyCn} 
                                        </br> Initiator 申请人:{CurrentUserDomain.CasUserEntity.EnglishName} {CurrentUserDomain.CasUserEntity.ChineseName}
                                        </br> Contract Owner 合同经办部门:{CurrentUserDomain.CasUserEntity.DeparmentName}                                       
                                        </br></br> Ferrero China Contract Approval System
                                        </br> 费列罗中国合同审批系统";
                                            var cc = $@"chinacontractsys@ferrero.com.cn";
                                            var reciever = depManagerInfo.Email;
                                            SendEmail.Send(reciever, cc, title, content);
                                        }
                                        #endregion
                                    }
                                    CasContractApproverEntity deptManagerEntity = new CasContractApproverEntity
                                    {
                                        ContractApproverId = Guid.NewGuid().ToString(),
                                        ApproverType = ApproverTypeEnum.DepartmentManager.GetHashCode(),
                                        ContractApprovalStepId = dr["CONTRACT_APPROVAL_STEP_ID"].ToString(),
                                        ApproverId = GetDeptManagerId(),
                                        ContractId = newEntity.ContractId,
                                        CreatedBy = CurrentUserDomain.CasUserEntity.UserId,
                                        CreateTime = DateTime.Now,
                                        IsDeleted = false,
                                        LastModifiedBy = CurrentUserDomain.CasUserEntity.UserId,
                                        LastModifiedTime = DateTime.Now,
                                        Status = approveStatus
                                    };
                                    casContractApproverList.Add(deptManagerEntity);
                                }
                                #endregion

                                #region 需要领导审批
                                //Line manager为部门总监的跳过审批,申请人为部门总监则跳过审批
                                string departManagerID = GetDeptManagerId();
                                if (!departManagerID.Equals(CurrentUserDomain.CasUserEntity.UserId))
                                {
                                    DataTable dtSendLineMans = GetLineManager();//获取当前用户领导集合
                                    //每步领导审批处理
                                    foreach (DataRow dr in drLineMans)
                                    {
                                        int approveStatus = ContractApproverStatusEnum.NotBegin.GetHashCode();//审批状态默认未开始
                                                                                                              //没有领导批注且是第一节点
                                        if (dr["STEP"].ToString() == step && noComment)
                                        {
                                            approveStatus = ContractApproverStatusEnum.WaitApproval.GetHashCode();//待审批
                                            #region 发送邮件给用户领导
                                            //DataTable dtSendLineMans = GetLineManager();
                                            if (dtSendLineMans.Rows.Count>0)
                                            {
                                                try
                                                {
                                                    foreach (DataRow lineMan in dtSendLineMans.Rows)
                                                    {
                                                        CasUserEntity lineManagerInfo = GetById<CasUserEntity>(lineMan["USER_ID"].ToString());
                                                        var title = "e-Approval - Contract Approval";
                                                        var content = $@"Dear:{lineManagerInfo.EnglishName},</br>尊敬的：{lineManagerInfo.ChineseName}</br></br>The following contract has been submitted for your approval (please kindly confirm your approval or rejection within 2 working days):</br>以下合同申请需要您的审核（请在2个工作日内批准或拒绝申请）：</br>
                                        </br> Contract Name 合同名称:{newEntity.ContractName}{newEntity.TemplateName}
                                        </br> Counter Party  相对方:{newEntity.CounterpartyEn}{newEntity.CounterpartyCn} 
                                        </br> Initiator 申请人:{CurrentUserDomain.CasUserEntity.EnglishName} {CurrentUserDomain.CasUserEntity.ChineseName}
                                        </br> Contract Owner 合同经办部门:{CurrentUserDomain.CasUserEntity.DeparmentName}                                       
                                        </br></br> Ferrero China Contract Approval System
                                        </br> 费列罗中国合同审批系统";
                                                        var cc = $@"chinacontractsys@ferrero.com.cn";
                                                        var reciever = lineManagerInfo.Email;
                                                        SendEmail.Send(reciever, cc, title, content);
                                                    }
                                                }
                                                catch 
                                                {
                                                    
                                                }
                                            }
                                            #endregion
                                        }
                                        //插入待审批步骤记录，m个步骤*n个审批领导
                                        if (dtSendLineMans.Rows.Count > 0)
                                        {
                                            foreach (DataRow lineMan in dtSendLineMans.Rows)
                                            {
                                                CasContractApproverEntity lineManagerEntity = new CasContractApproverEntity
                                                {
                                                    ContractApproverId = Guid.NewGuid().ToString(),
                                                    ApproverType = ApproverTypeEnum.LeaderApprove.GetHashCode(),
                                                    ContractApprovalStepId = dr["CONTRACT_APPROVAL_STEP_ID"].ToString(),
                                                    ApproverId = lineMan["USER_ID"].ToString(),
                                                    ContractId = newEntity.ContractId,
                                                    CreatedBy = CurrentUserDomain.CasUserEntity.UserId,
                                                    CreateTime = DateTime.Now,
                                                    IsDeleted = false,
                                                    LastModifiedBy = CurrentUserDomain.CasUserEntity.UserId,
                                                    LastModifiedTime = DateTime.Now,
                                                    Status = approveStatus
                                                };
                                                casContractApproverList.Add(lineManagerEntity);
                                            }
                                                
                                        }
                                    }
                                }
                                
                                #endregion

                                #region 审批部门审批
                                foreach (DataRow dr in approveStepDt.Rows)
                                {
                                    //当前步骤没有审批部门，则跳过
                                    if (string.IsNullOrWhiteSpace(dr["DEPT_ID"].ToString()))
                                    {
                                        continue;
                                    }
                                    int approveStatus = ContractApproverStatusEnum.NotBegin.GetHashCode();//审批状态默认未开始
                                                                                                          //没有领导批注且是第一节点
                                    if (dr["STEP"].ToString() == step && noComment)
                                    {
                                        approveStatus = ContractApproverStatusEnum.WaitApproval.GetHashCode();//待审批


                                    }
                                    CasContractApproverEntity deptApproveEntity = new CasContractApproverEntity
                                    {
                                        ContractApproverId = Guid.NewGuid().ToString(),
                                        ApproverType = ApproverTypeEnum.Department.GetHashCode(),
                                        ApproverId = dr["DEPT_ID"].ToString(),
                                        ContractApprovalStepId = dr["CONTRACT_APPROVAL_STEP_ID"].ToString(),
                                        ContractId = newEntity.ContractId,
                                        CreatedBy = CurrentUserDomain.CasUserEntity.UserId,
                                        CreateTime = DateTime.Now,
                                        IsDeleted = false,
                                        LastModifiedBy = CurrentUserDomain.CasUserEntity.UserId,
                                        LastModifiedTime = DateTime.Now,
                                        Status = approveStatus
                                    };
                                    casContractApproverList.Add(deptApproveEntity);
                                }
                                #endregion
                                #endregion

                                #endregion
                                DataAccess.Insert(casContractApproverList, broker);
                                flag = true;
                            }
                        }

                        #region 打回后重新提交的合同,发送邮件通知修改要点
                        //打回后重新提交的合同，状态为重新提交,并需要发送邮件通知修改要点
                        if (oldContractEntity != null && oldContractEntity.Status == ContractStatusEnum.ApprovalReject.GetHashCode()
                            && saveType == SaveTypeEnum.Submit.GetHashCode().ToString())
                        {

                            DataTable dt = GetSendUsersAfterResubmit(entity.ContractId);
                            List<string> recivers = new List<string>();
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    string email = dr["Email"].ToString();
                                    if (!string.IsNullOrEmpty(email))
                                    {
                                        recivers.Add(dr["Email"].ToString());
                                    }
                                }
                            }
                            #region 
                            var title = $@"e-Approval - Contract Approval";
                            var content = $@"尊敬的审批人：以下申请人的合同已在系统中修改要点后重新提交审批。以下合同申请需要您的审核：</br>  
                              合同修改要点：{newEntity.explanation}<br/>
                              Initiator 申请人:{CurrentUserDomain.CasUserEntity.ChineseName}    {CurrentUserDomain.CasUserEntity.EnglishName}</br>
                              Department 用户部门:{CurrentUserDomain.CasUserEntity.DeparmentName}</br> 
                              Contract Name 合同名称:{newEntity.ContractName}{newEntity.TemplateName}</br> 
                              Counter Party  相对方:{newEntity.CounterpartyCn}{newEntity.CounterpartyEn} </br>
                              Contract Owner 合同经办部门：{newEntity.ContractOwner}{newEntity.TemplateOwner}</br>
                              </br>Ferrero China Contract Approval System
                              </br>费列罗中国合同审批系统";
                            string cc = "chinacontractsys@ferrero.com.cn";
                            List<string> ccs = new List<string>();
                            ccs.Add(cc);
                            if (recivers.Count > 0)
                            {
                                SendEmail.Send(recivers, ccs, title, content);
                            }
                            #endregion

                        }
                        #endregion
                    }
                    else if (saveType == SaveTypeEnum.Save.GetHashCode().ToString())//保存
                    {
                        flag = true;
                    }
                    else//异常
                    {
                        flag = false;
                    }

                    if (flag)
                    {
                        broker.Commit();
                        msg = "Successfully submitted.";
                    }
                    else
                    {
                        broker.Commit();
                        msg = "Operation failed: saving type is exception.";
                    }
                }
                catch (Exception e)
                {
                    broker.RollBack();
                    flag = false;
                    msg = $@"操作失败：{e.Message}";
                    SystemService.LogErrorService.InsertLog(e);
                }
                return flag;
            }
        }
        /// <summary>
        /// 该合同类型的申请合同用户时候有部门总监
        /// </summary>
        /// <param name="contractTypeId"></param>
        /// <returns></returns>
        public bool CheckLDEPManagerExist(string contractTypeId)
        {
            string sql = $@"SELECT COUNT(*) FROM dbo.CAS_CONTRACT_TYPE CCT LEFT JOIN dbo.CAS_CONTRACT_APPROVAL_STEP CAS ON cct.CONTRACT_TYPE_ID=CAS.CONTRACT_TYPE_ID WHERE CCT.CONTRACT_TYPE_ID='{contractTypeId}' AND CAS.NEED_DEPT_MANAGER='1'";
            var resultValue = DataAccess.SelectScalar(sql);
            if (resultValue != "0")
            {
                bool outValue;
                string managerSQL = $@" SELECT COUNT(*) FROM dbo.CAS_USER CUS
                        LEFT JOIN dbo.CAS_DEPARTMENT CDT ON CUS.DEPARMENT_CODE=CDT.DEPT_CODE
                        WHERE CUS.USER_ID='{CurrentUserDomain.CasUserEntity.UserId}' AND CDT.DEPT_MANAGER_ID<>''";
                var managerValue = DataAccess.SelectScalar(managerSQL);
                if (managerValue != "0")
                {
                    outValue = true;
                }
                else
                {
                    outValue = false;
                }
                return outValue;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// 该合同类型的申请合同用户时候有大区总监
        /// </summary>
        /// <param name="contractTypeId"></param>
        /// <returns></returns>
        public bool CheckLRegionManagerExist(string contractTypeId)
        {
            string sql = $@"SELECT COUNT(*) FROM dbo.CAS_CONTRACT_TYPE CCT LEFT JOIN dbo.CAS_CONTRACT_APPROVAL_STEP CAS ON cct.CONTRACT_TYPE_ID=CAS.CONTRACT_TYPE_ID WHERE CCT.CONTRACT_TYPE_ID='{contractTypeId}' AND CAS.NEED_REGION_MANAGER='1'";
            var resultValue = DataAccess.SelectScalar(sql);
            if (resultValue != "0")
            {
                bool outvalue;
                string managerSQL = $@" SELECT COUNT(*) FROM  dbo.CAS_USER CUS
                    LEFT JOIN dbo.CAS_CITY CIT ON CUS.CITY_CODE=CIT.CITY_CODE
                    LEFT JOIN dbo.CAS_REGION REG ON CIT.REGION_ID=REG.REGION_ID
                     WHERE USER_ID ='{CurrentUserDomain.CasUserEntity.UserId}' AND REG.REGION_MANAGER<>'' ";
                var managerValue = DataAccess.SelectScalar(managerSQL);
                if (managerValue != "0")
                {
                    outvalue = true;
                }
                else
                {
                    outvalue = false;
                }
                return outvalue;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// 该合同类型的申请合同用户时候有用户领导
        /// </summary>
        /// <param name="contractTypeId"></param>
        /// <returns></returns>
        public bool CheckLineManagerExist(string contractTypeId)
        {
            string sql = $@"SELECT COUNT(*) FROM dbo.CAS_CONTRACT_TYPE CCT LEFT JOIN dbo.CAS_CONTRACT_APPROVAL_STEP CAS ON cct.CONTRACT_TYPE_ID=CAS.CONTRACT_TYPE_ID WHERE CCT.CONTRACT_TYPE_ID='{contractTypeId}' AND CAS.NEED_LINE_MANAGER='1'";
            var resultValue = DataAccess.SelectScalar(sql);
            if (resultValue != "0")
            {
                bool outValue;
                string managerSQL = $@" SELECT COUNT(*) FROM dbo.CAS_USER CUS
                        WHERE CUS.USER_ID='{CurrentUserDomain.CasUserEntity.UserId}' AND CUS.LINE_MANAGER_ID<>''";
                var managerValue = DataAccess.SelectScalar(managerSQL);
                if (managerValue != "0")
                {
                    outValue = true;
                }
                else
                {
                    outValue = false;
                }
                return outValue;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// 该合同类型的合同申请人是否有领导
        /// </summary>
        /// <param name="contractTypeId"></param>
        /// <returns></returns>
        public bool CheckLIeaderExist(string contractTypeId)
        {
            string sql = $@"SELECT 1 FROM dbo.CAS_CONTRACT_TYPE WHERE CONTRACT_TYPE_ID='{contractTypeId}' AND NEED_COMMENT='1'";
            var resultValue = DataAccess.SelectScalar(sql);
            if (resultValue == "1")
            {
                string deptManagerId = GetDeptManagerId();
                //自己是department head，则算有领导
                if (CurrentUser.CasUserEntity.UserId == deptManagerId)
                {
                    return true;
                }
                CasUserEntity lineManager = DataAccess.SelectSingle<CasUserEntity>(new CasUserEntity() { UserAccount = CurrentUser.CasUserEntity.LineManagerId });
                //line manager就是department head，则算有领导
                if (lineManager.UserId == deptManagerId)
                {
                    return true;
                }

                DataTable managers = GetLineManager();
                if (managers.Rows.Count > 0)
                {
                    return true;
                }
                return false;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// 校验模板合同的模板编号
        /// </summary>
        /// <param name="templateNo"></param>
        /// <returns>true 不存在，false 存在</returns>
        public bool CheckTemplateNo(string templateNo, string contractID)
        {
            string sql = $@"SELECT 1 FROM dbo.CAS_CONTRACT WHERE TEMPLATE_NO = '{templateNo}' AND IS_TEMPLATE_CONTRACT = 1 AND CONTRACT_ID <>'{contractID}' ";
            return DataAccess.SelectScalar(sql) == null;
        }

        public List<CasContractEntity> GetOrigialContractList()
        {
            string sql = $@"SELECT * FROM dbo.CAS_CONTRACT WHERE STATUS = " + ContractStatusEnum.HadApproval.GetHashCode();
            List<CasContractEntity> list = DataAccess.Select<CasContractEntity>(sql);
            return list;
        }

        public List<CasContractEntity> GetTemplateNoList()
        {
            string sql = $@"SELECT * FROM dbo.CAS_CONTRACT WHERE IS_TEMPLATE_CONTRACT = 1 AND (STATUS = " + ContractStatusEnum.SignedCompleted.GetHashCode() + " OR STATUS =" + ContractStatusEnum.HadApproval.GetHashCode() +")";
            List<CasContractEntity> list = DataAccess.Select<CasContractEntity>(sql);
            return list;
        }

        public CasContractEntity PrepareEntity(CasContractEntity oldEntity)
        {

            #region 问题
            //这一块一直有问题
            #endregion
            CasContractEntity newEntity = new CasContractEntity();
            CasContractTypeEntity casContractTypeEntity = BusinessDataService.ContractTypeManagementService
                .GetById<CasContractTypeEntity>(oldEntity.ContractTypeId);
            newEntity.ContractId = oldEntity.ContractId;//合同ID
            newEntity.ContractTypeId = oldEntity.ContractTypeId;//合同类型ID
            newEntity.ContractTypeName = casContractTypeEntity.ContractTypeName;//合同类型名称
            newEntity.CreatedBy = CurrentUserDomain.CasUserEntity.UserId;
            newEntity.BudgetType = (bool)casContractTypeEntity.BudgetType ? oldEntity.BudgetType : null;
            newEntity.Capex = (bool)casContractTypeEntity.Capex ? oldEntity.Capex : false;
            newEntity.ContractGroup = oldEntity.ContractGroup;//合同类别：普通合同
            newEntity.explanation = oldEntity.explanation;//重新提交合同的补充说明
            newEntity.ContractInitiator = (bool)casContractTypeEntity.ContractInitiator ? oldEntity.ContractInitiator : null;
            newEntity.ContractKeyPoints = (bool)casContractTypeEntity.ContractKeyPoints ? oldEntity.ContractKeyPoints : null;
            newEntity.ContractName = (bool)casContractTypeEntity.ContractName ? oldEntity.ContractName : null;
            newEntity.ContractOwner = (bool)casContractTypeEntity.ContractOwner ? oldEntity.ContractOwner : null;
            newEntity.ContractPrice = (bool)casContractTypeEntity.ContractOREstimatedPrice ? oldEntity.ContractPrice : null;
            newEntity.Tax = (bool)casContractTypeEntity.IsMasterAgreement ? oldEntity.Tax : null;
            newEntity.EstimatedPrice = (bool)casContractTypeEntity.ContractOREstimatedPrice ? oldEntity.EstimatedPrice : null;
            newEntity.ContractTerm = (bool)casContractTypeEntity.ContractTerm ? oldEntity.ContractTerm : DateTime.Now;
            newEntity.CounterpartyCn = (bool)casContractTypeEntity.CounterpartyCn ? oldEntity.CounterpartyCn : null;
            newEntity.CounterpartyEn = (bool)casContractTypeEntity.CounterpartyEn ? oldEntity.CounterpartyEn : null;
            newEntity.CreateTime = DateTime.Now;
            newEntity.BudgetAmount = (bool)casContractTypeEntity.BudgetAmount ? oldEntity.BudgetAmount : null;
            newEntity.InternalORInvestmentOrder = (bool)casContractTypeEntity.InternalORInvestmentOrder ? oldEntity.InternalORInvestmentOrder : null;
            newEntity.Currency = (bool)casContractTypeEntity.Currency ? oldEntity.Currency : null;
            if ((bool)casContractTypeEntity.ContractTerm)
            {
                newEntity.EffectiveDate = oldEntity.ContractEffectiveDate;
                newEntity.ExpirationDate = oldEntity.ContractExpirationDate;
            }
            else if ((bool)casContractTypeEntity.TemplateTerm)
            {
                newEntity.EffectiveDate = oldEntity.TemplateEffectiveDate;
                newEntity.ExpirationDate = oldEntity.TemplateExpirationDate;
            }
            newEntity.FerreroEntity = oldEntity.FerreroEntity;
            newEntity.IsMasterAgreement = (bool)casContractTypeEntity.IsMasterAgreement ? oldEntity.IsMasterAgreement : null;

            newEntity.IsTemplateContract = (bool)casContractTypeEntity.IsTemplateContract;
            if ((bool)casContractTypeEntity.IsTemplateContract)
            {
                newEntity.ApplyDate = (bool)casContractTypeEntity.ApplyDate ? oldEntity.TemplateApplyDate : null;
                newEntity.TemplateNo = (bool)casContractTypeEntity.TemplateNo ? oldEntity.TemplateNoForInput : null;
            }
            else
            {
                newEntity.ApplyDate = (bool)casContractTypeEntity.ApplyDate ? oldEntity.ContractApplyDate : null;
                newEntity.TemplateNo = (bool)casContractTypeEntity.TemplateNo ? oldEntity.ContractTemplateNoForSel : null;
            }
            newEntity.LastModifiedBy = CurrentUserDomain.CasUserEntity.UserId;
            newEntity.LastModifiedTime = DateTime.Now;
            newEntity.ModificationPoints = oldEntity.ModificationPoints;
            newEntity.NeedComment = (bool)casContractTypeEntity.NeedComment ? true : false;
            newEntity.NotDisplayInMySupport = (bool)casContractTypeEntity.NotDisplayInMySupport;
            newEntity.OriginalContractId = ((bool)casContractTypeEntity.Supplementary && (bool)oldEntity.Supplementary) ? oldEntity.OriginalContractId : null;//原始合同ID
            newEntity.PrepaymentAmount = (bool)casContractTypeEntity.PrepaymentAmount ? oldEntity.PrepaymentAmount : null;
            newEntity.PrepaymentPercentage = (bool)casContractTypeEntity.PrepaymentPercentage ? oldEntity.PrepaymentPercentage : null;
            newEntity.ReferenceContract = casContractTypeEntity.ReferenceContract!=null ? oldEntity.ReferenceContract : false;
            newEntity.ScopeOfApplication = (bool)casContractTypeEntity.ScopeOfApplication ? oldEntity.ScopeOfApplication : null;
            newEntity.Status = oldEntity.Status;
            newEntity.Supplementary = ((bool)casContractTypeEntity.Supplementary && (bool)oldEntity.Supplementary) ? oldEntity.Supplementary : false;//是否是补充合同
            newEntity.Supplier = CurrentUserDomain.CasUserEntity.UserId;//申请人
            newEntity.TemplateInitiator = (bool)casContractTypeEntity.TemplateInitiator ? oldEntity.TemplateInitiator : null;
            newEntity.TemplateName = (bool)casContractTypeEntity.TemplateName ? oldEntity.TemplateName : null;
            newEntity.TemplateOwner = (bool)casContractTypeEntity.TemplateOwner ? oldEntity.TemplateOwner : null;
            newEntity.TemplateTerm = (bool)casContractTypeEntity.TemplateTerm ? oldEntity.TemplateTerm : null;
            newEntity.ContractSerialNo = oldEntity.ContractSerialNo;//流水号
            newEntity.IsDeleted = false;
            return newEntity;
        }

        /// <summary>
        /// 根据合同类型ID获取合同审批步骤集合
        /// APPLY_TYPE 1部门 2用户 3所有部门
        /// </summary>
        /// <returns></returns>
        public DataTable GetContractApprovalStepsByTypeId(string contractTypeId)
        {
            string sql = $@"SELECT DISTINCT t1.CONTRACT_APPROVAL_STEP_ID,t1.STEP,t1.NEED_REGION_MANAGER,t1.NEED_LINE_MANAGER
                            ,t1.NEED_DEPT_MANAGER,t3.DEPT_ID FROM dbo.CAS_CONTRACT_APPROVAL_STEP t1
                            LEFT JOIN CAS_CONTRACT_APPROVAL_STEP_APPLY_DEPT t2 ON t1.CONTRACT_APPROVAL_STEP_ID = t2.CONTRACT_APPROVAL_STEP_ID
                            LEFT JOIN dbo.CAS_CONTRACT_APPROVAL_STEP_APPROVAL_DEPT t3 ON t1.CONTRACT_APPROVAL_STEP_ID = t3.CONTRACT_APPROVAL_STEP_ID
                            LEFT JOIN dbo.CAS_DEPARTMENT t4 ON t4.DEPT_ID = t3.DEPT_ID
                            WHERE t1.CONTRACT_TYPE_ID = '{contractTypeId}' ";
            if (!CurrentUserDomain.CasUserEntity.IsAdmin)
            {
                sql += $@" AND ((t2.APPLY_TYPE = 1 AND t2.DEPT_ID = 
                            (SELECT DEPT_ID FROM dbo.CAS_DEPARTMENT WHERE DEPT_CODE = '{CurrentUserDomain.CasUserEntity.DeparmentCode}'))
                            OR (t2.APPLY_TYPE = 2 AND t2.DEPT_ID = '{CurrentUserDomain.CasUserEntity.UserId}')
                            OR (t2.APPLY_TYPE = 3 AND t2.DEPT_ID = 
                            (SELECT DEPT_ID FROM dbo.CAS_DEPARTMENT WHERE DEPT_CODE = '{CurrentUserDomain.CasUserEntity.DeparmentCode}'))
                            )";
            }
            sql += " ORDER BY t1.STEP ASC";
            DataTable dt = DataAccess.SelectDataSet(sql).Tables[0];
            return dt;
        }
        /// <summary>
        /// 获得用户的邮箱地址
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserEmail(string userId)
        {
            string sql = $@"SELECT EMAIL FROM dbo.CAS_USER WHERE USER_ID='{userId}'";
            return DataAccess.SelectScalar(sql);
        }


        /// <summary>
        /// 获取当前用户的大区总监
        /// </summary>
        /// <returns></returns>
        public string GetRegionManagerId()
        {
            string sql = $@"SELECT t3.REGION_MANAGER FROM dbo.CAS_USER t1
                        JOIN dbo.CAS_CITY t2 ON t1.CITY_CODE = t2.CITY_CODE
                        JOIN dbo.CAS_REGION t3 ON t2.REGION_ID = t3.REGION_ID
                        WHERE t1.USER_ID = '{CurrentUserDomain.CasUserEntity.UserId}'";

            return DataAccess.SelectScalar(sql);
        }

        /// <summary>
        /// 获取当前用户的部门总监
        /// </summary>
        /// <returns></returns>
        public string GetDeptManagerId()
        {
            string sql = $@"SELECT t2.DEPT_MANAGER_ID FROM dbo.CAS_USER t1
                            JOIN dbo.CAS_DEPARTMENT t2 ON t1.DEPARMENT_CODE = t2.DEPT_CODE
                            AND t2.DEPT_TYPE = 1
                            WHERE t1.USER_ID = '{CurrentUserDomain.CasUserEntity.UserId}'";
            return DataAccess.SelectScalar(sql);
        }

        /// <summary>
        /// 获取当前用户的领导集合
        /// </summary>
        /// <returns></returns>
        public DataTable GetLineManager()
        {
            string sql = $@"with temp ([USER_ID],USER_CODE,LINE_MANAGER_ID,EMAIL,CHINESE_NAME,ENGLISH_NAME)
                            as
                            (
                            select [USER_ID],USER_CODE, LINE_MANAGER_ID,EMAIL,CHINESE_NAME,ENGLISH_NAME
                            from dbo.CAS_USER
                            where [USER_ID] = '{CurrentUserDomain.CasUserEntity.UserId}'
                            union all
                            select a.[USER_ID],a.USER_CODE, a.LINE_MANAGER_ID,a.EMAIL,a.CHINESE_NAME,a.ENGLISH_NAME
                            from CAS_USER a
                            inner join temp on a.USER_CODE = temp.LINE_MANAGER_ID
                            )
                            select * from temp";
            DataTable dtResult = DataAccess.SelectDataSet(sql).Tables[0];
            //去掉所有Department Head

            //获取所有Department Head
            DepartmentBLL departmentBLL = new DepartmentBLL();
            List<CasUserEntity> listDepartmentManagers = departmentBLL.GetAllDepartmentManagers();

            //LineManager列表中去掉自己
            if (dtResult.Rows.Count > 0)
            {
                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    if (dtResult.Rows[i]["USER_ID"].ToString() == CurrentUserDomain.CasUserEntity.UserId)
                    {
                        dtResult.Rows.RemoveAt(i);
                        break;
                    }
                }
            }
            //LineManager列表中去掉Department Head，即为最终的LineManager列表
            if (dtResult.Rows.Count > 0)
            {
                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    if (listDepartmentManagers.Where(p => p.UserId == dtResult.Rows[i]["USER_ID"].ToString()).Count() > 0)
                    {
                        dtResult.Rows.RemoveAt(i);
                        i--;
                    }
                }
            }

            return dtResult;
        }

        public DataTable GetSendUsersAfterResubmit(string contractId)
        {
            string sql = $@"select A.USER_ID,A.USER_ACCOUNT,A.EMAIL from dbo.CAS_USER A
 inner join dbo.CAS_CONTRACT_APPROVAL_RESULT B 
 ON A.USER_ID=B.APPROVER_ID
 WHERE B.CONTRACT_ID = '{contractId}'";
            return DataAccess.SelectDataSet(sql).Tables[0];
        }
        /// <summary>
        /// 用户操作记录
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="approverType"></param>
        /// <param name="approvalResult"></param>
        /// <returns></returns>
        public bool AddResultInApprovalResult(string contractId, int approverType, int approvalResult)
        {
            CasContractApprovalResultEntity approvalResultEntity = new CasContractApprovalResultEntity();
            approvalResultEntity.ContractApprovalResultId = Guid.NewGuid().ToString();
            approvalResultEntity.ContractId = contractId;
            approvalResultEntity.ApproverType = approverType;
            approvalResultEntity.ApproverId = CurrentUserDomain.CasUserEntity.UserId;
            approvalResultEntity.ContractApprovalStepId = "";
            approvalResultEntity.ApprovalTime = DateTime.Now;
            approvalResultEntity.ApprovalResult = approvalResult;
            approvalResultEntity.IsDeleted = false;
            approvalResultEntity.CreatedBy = CurrentUserDomain.CasUserEntity.UserId;
            approvalResultEntity.CreateTime = DateTime.Now;
            approvalResultEntity.LastModifiedBy = CurrentUserDomain.CasUserEntity.UserId;
            approvalResultEntity.LastModifiedTime = DateTime.Now;
            return DataAccess.Insert(approvalResultEntity);
        }
        /// <summary>
        /// 查询草稿箱文件数量
        /// </summary>
        /// <returns></returns>
        public int GetDraftNumber()
        {
            string sql = $@"  SELECT COUNT(*) FROM CAS_CONTRACT WHERE STATUS IN('{ContractStatusEnum.Uncommitted.GetHashCode()}','{ContractStatusEnum.ApprovalReject.GetHashCode()}') AND CREATED_BY='{CurrentUserDomain.CasUserEntity.UserId}' ";
            var number = DataAccess.SelectScalar(sql.ToString());
            return Convert.ToInt32(number);
        }

        /// <summary>
        /// 获得当前用户的任务数量
        /// </summary>
        /// <returns></returns>
        public int GetMyTaskNumber()
        {
            string sql = $@"SELECT COUNT(*) from (
                            SELECT t1.*,t2.STATUS as APPROVE_STATUS,t2.APPROVER_TYPE,t2.CONTRACT_APPROVER_ID,t6.STEP   
                            FROM dbo.CAS_CONTRACT t1
                            JOIN dbo.CAS_CONTRACT_APPROVER t2 ON t1.CONTRACT_ID = t2.CONTRACT_ID
                            LEFT JOIN dbo.CAS_CONTRACT_APPROVAL_STEP t6 ON t2.CONTRACT_APPROVAL_STEP_ID=t6.CONTRACT_APPROVAL_STEP_ID
                            WHERE 1 = 1 AND t2.APPROVER_TYPE != 1 AND t2.STATUS IN (2,4) AND t1.STATUS IN (2,8)
                            AND (t2.APPROVER_ID = '{CurrentUserDomain.CasUserEntity.UserId}' 
                            OR t2.APPROVER_ID IN (SELECT t3.AUTHORIZED_USER_ID FROM dbo.CAS_PROXY_APPROVAL t3 
                            WHERE t3.AGENT_USER_ID = '{CurrentUserDomain.CasUserEntity.UserId}' 
                            AND GETDATE() BETWEEN t3.BEGIN_TIME AND t3.END_TIME AND t3.IS_DELETED=0))
                            UNION ALL
                            SELECT t1.*,t2.STATUS as APPROVE_STATUS,t2.APPROVER_TYPE,t2.CONTRACT_APPROVER_ID,t6.STEP   
                            FROM dbo.CAS_CONTRACT t1
                            JOIN dbo.CAS_CONTRACT_APPROVER t2 ON t1.CONTRACT_ID = t2.CONTRACT_ID
                            JOIN dbo.CAS_DEPARTMENT t4 ON t4.DEPT_ID = t2.APPROVER_ID
                            JOIN dbo.CAS_DEPT_USER t5 ON t5.DEPT_ID = t4.DEPT_ID
                            LEFT  JOIN dbo.CAS_CONTRACT_APPROVAL_STEP t6 ON t2.CONTRACT_APPROVAL_STEP_ID=t6.CONTRACT_APPROVAL_STEP_ID
                            WHERE 1 = 1 AND t2.APPROVER_TYPE != 1 AND t2.STATUS IN (2,4) AND t1.STATUS IN (2,8)
                            AND (t5.USER_ID = '{CurrentUserDomain.CasUserEntity.UserId}' 
                            OR t5.USER_ID IN (SELECT t3.AUTHORIZED_USER_ID FROM dbo.CAS_PROXY_APPROVAL t3 
                            WHERE t3.AGENT_USER_ID = '{CurrentUserDomain.CasUserEntity.UserId}' 
                            AND GETDATE() BETWEEN t3.BEGIN_TIME AND t3.END_TIME AND t3.IS_DELETED=0))
							UNION ALL 
							SELECT t1.*,t2.STATUS as APPROVE_STATUS,t2.APPROVER_TYPE,t2.CONTRACT_APPROVER_ID,t6.STEP  FROM dbo.CAS_CONTRACT t1
                            JOIN dbo.CAS_CONTRACT_APPROVER t2 ON t1.CONTRACT_ID = t2.CONTRACT_ID
                            LEFT JOIN dbo.CAS_CONTRACT_APPROVAL_STEP t6 ON t2.CONTRACT_APPROVAL_STEP_ID=t6.CONTRACT_APPROVAL_STEP_ID
                            WHERE 1 = 1 AND t2.APPROVER_TYPE = 1 AND t2.STATUS = 2 AND t1.STATUS IN (2,8) 
							AND t2.APPROVER_ID = '{CurrentUserDomain.CasUserEntity.UserId}') temp";
            var number = DataAccess.SelectScalar(sql.ToString());
            return Convert.ToInt32(number);
        }
    }
}
