using Suzsoft.Smart.EntityCore;
using eContract.BusinessService.Common;
using eContract.Common;
using eContract.Common.Entity;
using eContract.Common.MVC;
using eContract.Common.WebUtils;
using eContract.BusinessService.BusinessData.CommonQuery;
using eContract.BusinessService.BusinessData.Service;
using eContract.BusinessService.SystemManagement.Service;
using System;
using System.Text;
using Suzsoft.Smart.Data;
using System.Data;
using System.Linq;
using System.Collections.Generic;

namespace eContract.BusinessService.BusinessData.BusinessRule
{
    public class ContractManagementBLL : BusinessBase
    {
        public JqGrid ForGrid(JqGrid grid)
        {
            var query = new ContractManagementQuery();
            query.CounterParty = grid.QueryField.ContainsKey("COUNTERPARTY_CN")? grid.QueryField["COUNTERPARTY_CN"]:"";
            grid.QueryField.Remove("COUNTERPARTY_CN");
            query.keyWord = grid.QueryField.ContainsKey("CONTRACT_NAME") ? grid.QueryField["CONTRACT_NAME"] : "";
            grid.QueryField.Remove("CONTRACT_NAME");
            query.homeQuery = grid.QueryField.ContainsKey("HomeQuery") ? grid.QueryField["HomeQuery"] : "";
            grid.QueryField.Remove("HomeQuery");
            query.StatusValue = grid.QueryField.ContainsKey("STATUS") ? grid.QueryField["STATUS"] : "";
            grid.QueryField.Remove("STATUS");
            grid = QueryTableHelper.QueryGrid<CasContractEntity>(query, grid);
            return grid;
        }
        /// <summary>
        /// 针对框架修改查询条件
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public JqGrid CommonCondition(JqGrid grid)
        {
            if (grid.QueryField.ContainsKey("STATUS"))
            {
                grid.QueryField = grid.QueryField.ToDictionary(k => k.Key == "STATUS" ? "CON.STATUS" : k.Key, k => k.Value);
                if (grid.QueryField["CON.STATUS"] == "0")
                {
                    grid.QueryField.Remove("CON.STATUS");
                }
            }
            if (grid.QueryField.ContainsKey("CONTRACT_GROUP"))
            {
                grid.QueryField = grid.QueryField.ToDictionary(k => k.Key == "CONTRACT_GROUP" ? "CON.CONTRACT_GROUP" : k.Key, k => k.Value);
                if (grid.QueryField["CON.CONTRACT_GROUP"] == "0")
                {
                    grid.QueryField.Remove("CON.CONTRACT_GROUP");
                }
            }
            return grid;
        }

        
        /// <summary>
        /// 我审批的合同
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public JqGrid ContractApprovalByMe(JqGrid grid)
        {
            var query = new ContractApprovalByMeQuery();
            query.CounterParty = grid.QueryField.ContainsKey("COUNTERPARTY_CN") ? grid.QueryField["COUNTERPARTY_CN"] : "";
            grid.QueryField.Remove("COUNTERPARTY_CN");
            query.FERRERO_ENTITY = grid.QueryField.ContainsKey("FERRERO_ENTITY") ? grid.QueryField["FERRERO_ENTITY"] : "";
            grid.QueryField.Remove("FERRERO_ENTITY");
            query.CONTRACT_NO = grid.QueryField.ContainsKey("CONTRACT_NO") ? grid.QueryField["CONTRACT_NO"] : "";
            grid.QueryField.Remove("CONTRACT_NO");
            query.CONTRACT_GROUP = grid.QueryField.ContainsKey("CONTRACT_GROUP") ? grid.QueryField["CONTRACT_GROUP"] : "";
            grid.QueryField.Remove("CONTRACT_GROUP");
            query.CONTRACT_SERIAL_NO = grid.QueryField.ContainsKey("CONTRACT_SERIAL_NO") ? grid.QueryField["CONTRACT_SERIAL_NO"] : "";
            grid.QueryField.Remove("CONTRACT_SERIAL_NO");
            query.keyWord = grid.QueryField.ContainsKey("CONTRACT_NAME") ? grid.QueryField["CONTRACT_NAME"] : "";
            grid.QueryField.Remove("CONTRACT_NAME");
            query.StatusValue = grid.QueryField.ContainsKey("STATUS") ? grid.QueryField["STATUS"] : "";
            grid.QueryField.Remove("STATUS");
            grid = CommonCondition(grid);
            grid = QueryTableHelper.QueryGrid<CasContractEntity>(query, grid);
            return grid;
        }

        /// <summary>
        /// 我的部门合同
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public JqGrid MyDepContract(JqGrid grid)
        {
            var query = new MyDepContractQuery();
            query.CounterParty = grid.QueryField.ContainsKey("COUNTERPARTY_CN") ? grid.QueryField["COUNTERPARTY_CN"] : "";
            grid.QueryField.Remove("COUNTERPARTY_CN");
            query.CONTRACT_GROUP = grid.QueryField.ContainsKey("CONTRACT_GROUP") ? grid.QueryField["CONTRACT_GROUP"] : "";
            grid.QueryField.Remove("CONTRACT_GROUP");
            query.CONTRACT_SERIAL_NO = grid.QueryField.ContainsKey("CONTRACT_SERIAL_NO") ? grid.QueryField["CONTRACT_SERIAL_NO"] : "";
            grid.QueryField.Remove("CONTRACT_SERIAL_NO");
            query.keyWord = grid.QueryField.ContainsKey("CONTRACT_NAME") ? grid.QueryField["CONTRACT_NAME"] : "";
            grid.QueryField.Remove("CONTRACT_NAME");
            query.StatusValue = grid.QueryField.ContainsKey("STATUS") ? grid.QueryField["STATUS"] : "";
            grid.QueryField.Remove("STATUS");
            grid = CommonCondition(grid);
            grid = QueryTableHelper.QueryGrid<CasContractEntity>(query, grid);
            return grid;
        }
        /// <summary>
        /// 我支持的合同
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public JqGrid ISupportedContract(JqGrid grid)
        {
            var query = new ISupportedContractQuery();
            query.CounterParty = grid.QueryField.ContainsKey("COUNTERPARTY_CN") ? grid.QueryField["COUNTERPARTY_CN"] : "";
            grid.QueryField.Remove("COUNTERPARTY_CN");
            query.CONTRACT_GROUP = grid.QueryField.ContainsKey("CONTRACT_GROUP") ? grid.QueryField["CONTRACT_GROUP"] : "";
            grid.QueryField.Remove("CONTRACT_GROUP");
            query.CONTRACT_SERIAL_NO = grid.QueryField.ContainsKey("CONTRACT_SERIAL_NO") ? grid.QueryField["CONTRACT_SERIAL_NO"] : "";
            grid.QueryField.Remove("CONTRACT_SERIAL_NO");
            query.keyWord = grid.QueryField.ContainsKey("CONTRACT_NAME") ? grid.QueryField["CONTRACT_NAME"] : "";
            grid.QueryField.Remove("CONTRACT_NAME");
            query.PO = grid.QueryField.ContainsKey("PO") ? grid.QueryField["PO"] : "";
            grid.QueryField.Remove("PO");
            query.PR = grid.QueryField.ContainsKey("PR") ? grid.QueryField["PR"] : "";
            grid.QueryField.Remove("PR");
            query.StatusValue = grid.QueryField.ContainsKey("STATUS") ? grid.QueryField["STATUS"] : "";
            grid.QueryField.Remove("STATUS");
            grid = CommonCondition(grid);
            grid = QueryTableHelper.QueryGrid<CasContractEntity>(query, grid);
            return grid;
        }

        /// <summary>
        /// 全部合同（FFH）
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public JqGrid AllContractFFH(JqGrid grid)
        {
            var query = new AllContractFFHQuery();
            query.CounterParty = grid.QueryField.ContainsKey("COUNTERPARTY_CN") ? grid.QueryField["COUNTERPARTY_CN"] : "";
            grid.QueryField.Remove("COUNTERPARTY_CN");
            query.keyWord = grid.QueryField.ContainsKey("CONTRACT_NAME") ? grid.QueryField["CONTRACT_NAME"] : "";
            grid.QueryField.Remove("CONTRACT_NAME");
            query.StatusValue = grid.QueryField.ContainsKey("STATUS") ? grid.QueryField["STATUS"] : "";
            grid.QueryField.Remove("STATUS");
            grid = CommonCondition(grid);
            grid = QueryTableHelper.QueryGrid<CasContractEntity>(query, grid);
            return grid;
        }
        /// <summary>
        /// 全部合同（FTS）
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public JqGrid AllContractFTS(JqGrid grid)
        {
            var query = new AllContractFTSQuery();
            query.CounterParty = grid.QueryField.ContainsKey("COUNTERPARTY_CN") ? grid.QueryField["COUNTERPARTY_CN"] : "";
            grid.QueryField.Remove("COUNTERPARTY_CN");
            query.keyWord = grid.QueryField.ContainsKey("CONTRACT_NAME") ? grid.QueryField["CONTRACT_NAME"] : "";
            grid.QueryField.Remove("CONTRACT_NAME");
            query.StatusValue = grid.QueryField.ContainsKey("STATUS") ? grid.QueryField["STATUS"] : "";
            grid.QueryField.Remove("STATUS");
            grid = CommonCondition(grid);
            grid = QueryTableHelper.QueryGrid<CasContractEntity>(query, grid);
            return grid;
        }

        public virtual bool CloseContractByAdmin(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return false;
            var strsql = new StringBuilder();
            strsql.AppendFormat(" UPDATE CAS_CONTRACT SET STATUS='7' WHERE CONTRACT_ID={0} ",
                Utils.ToSQLStr(id).Trim());
            var val = DataAccess.ExecuteNoneQuery(strsql.ToString());
            return true;
        }

        public JqGrid GetContractApprolvalRes(JqGrid grid)
        {
            var query = new GetContractApprolvalResQuery();
            query.keyWord = grid.keyWord;
            grid = QueryTableHelper.QueryGrid<CasContractApprovalResultEntity>(query, grid);
            return grid;
        }
        /// <summary>
        /// 导出合同列表时查询条件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataTable ExportData(CasContractEntity entity)
        {

            var sql = $@" SELECT DISTINCT CON.CONTRACT_NAME AS 'Contract Name',CON.CONTRACT_TYPE_NAME AS 'Contract Type',
(case when CON.CONTRACT_GROUP=1 and CON.IS_TEMPLATE_CONTRACT=0 then CON.CONTRACT_NO else CONTRACT_SERIAL_NO end ) AS 'Contract Number',
(CASE  CON.CONTRACT_GROUP WHEN '1' THEN N'Other contract' WHEN '2' THEN N'History contract'else N'Errot' END ) AS 'Contract Group',
CON.FERRERO_ENTITY AS 'Ferrero Party',CON.COUNTERPARTY_EN AS 'Counterparty-EN',COUNTERPARTY_CN AS 'Counterparty-CN',
            CONTRACT_OWNER AS 申请部门,CON.CONTRACT_INITIATOR AS 经办人,(CASE  CON.STATUS WHEN '1' THEN N'Draft' WHEN '2' THEN N'In review process'WHEN '3' THEN N'Approved' WHEN '4' THEN N'Rejected' WHEN '5' THEN N'Completed' WHEN '6' THEN N'Closed' WHEN '7' THEN N'Terminated' WHEN '8' THEN N'In review process'ELSE N'Error' END ) AS 'Status',
            USR.ENGLISH_NAME AS 'Contract Initiator',CON.CONTRACT_PRICE AS 'Contract Price with Tax' ,
            CON.CURRENCY AS 'Currency',PREPAYMENT_AMOUNT AS 'Prepayment Amount', CON.PREPAYMENT_PERCENTAGE AS 'Prepayment Percentage',
            CON.EFFECTIVE_DATE AS 'Validate From',CON.EXPIRATION_DATE AS 'End To',CON.MODIFICATION_POINTS AS 'Summary of revision',
            CON.CONTRACT_KEY_POINTS AS 'Key points of contract',TEMPLATE_NO AS 'Template No',CON.TEMPLATE_NAME AS 'Template Name',
            TEMPLATE_OWNER AS 模板合同申请部门 ,CON.TEMPLATE_INITIATOR AS 模板合同经办人,CON.SCOPE_OF_APPLICATION AS 'Scope of Application',
            CON.APPLY_DATE AS 'Submission Date',USR1.ENGLISH_NAME AS 'Creator',CON.CREATE_TIME AS 'Create Time',USR2.ENGLISH_NAME AS 修改人 ,CON.LAST_MODIFIED_TIME AS 'Modify Time',
            (CASE  CON.IS_MASTER_AGREEMENT WHEN 'True' THEN N'Yes' WHEN 'False' THEN N'No'else N'Other' END ) AS 'Master agreement or not',
            (CASE  CON.CAPEX WHEN 'True' THEN N'Yes' WHEN 'False' THEN N'No'else N'Other' END ) AS 'Capex or not',
            (CASE  CON.SUPPLEMENTARY WHEN 'True' THEN N'Yes' WHEN 'False' THEN N'No'else N'Other' END ) AS 'Supplementary or not',
            (CASE WHEN  CON.STATUS IN('3','5') THEN CAR.APPROVAL_TIME ELSE NULL END) AS 完成审批日期,ATT.UploadTime AS 上传盖章合同时间,
			 STUFF ((SELECT  ','+ CCF.PR_NO FROM  CAS_CONTRACT_FILING CCF

			WHERE  CCF.CONTRACT_ID =CON.CONTRACT_ID AND CCF.STATUS <> {ContractFilingEnum.Save.GetHashCode()}
			 FOR  XML  PATH ('')),1,1,'') AS 'PR Number',
			 STUFF ((SELECT  ','+ CCF.PO_NO FROM  CAS_CONTRACT_FILING CCF

			where CCF.CONTRACT_ID =CON.CONTRACT_ID AND CCF.STATUS <> {ContractFilingEnum.Save.GetHashCode()}
			 FOR  XML  PATH ('')),1,1,'') AS 'PO Number'
             FROM  CAS_CONTRACT CON INNER JOIN dbo.CAS_USER USR ON CON.SUPPLIER=USR.USER_ID INNER JOIN dbo.CAS_USER USR1 ON CON.CREATED_BY=USR1.USER_ID INNER JOIN dbo.CAS_USER USR2 ON CON.CREATED_BY=USR2.USER_ID 
			 LEFT JOIN (SELECT CONTRACT_ID,MAX(APPROVAL_TIME) AS APPROVAL_TIME FROM dbo.CAS_CONTRACT_APPROVAL_RESULT WHERE APPROVAL_RESULT IN ('2','3') GROUP BY CONTRACT_ID) CAR ON CON.CONTRACT_ID=CAR.CONTRACT_ID
			 LEFT JOIN (SELECT SOURCE_ID,MAX(CREATE_TIME) AS UploadTime FROM dbo.CAS_ATTACHMENT WHERE ATTACHMENT_TYPE='3' GROUP  BY SOURCE_ID) ATT ON ATT.SOURCE_ID = CON.CONTRACT_ID 
			 WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(entity.ContractName))
            {
                sql = sql + $@" AND CONTRACT_NAME LIKE N'%{ entity.ContractName.Trim()}%' ";
            }
            if (!string.IsNullOrWhiteSpace(entity.ContractNo))
            {
                sql = sql + $@" AND CONTRACT_NO LIKE N'%{entity.ContractNo.Trim()}%'";
            }
            if (!string.IsNullOrWhiteSpace(entity.FerreroEntity))
            {
                sql = sql + $@" AND FERRERO_ENTITY LIKE N'%{ entity.FerreroEntity.Trim()}%' ";
            }
            if (!string.IsNullOrWhiteSpace(entity.ContractInitiator))
            {
                sql = sql + $@" AND CONTRACT_INITIATOR LIKE N'%{ entity.ContractInitiator.Trim()}%' ";
            }
            if (!string.IsNullOrWhiteSpace(entity.CounterpartyCn))
            {
                sql = sql + $@" AND OUNTERPARTY_CN LIKE N'%{ entity.CounterpartyCn}%' ";
            }
            DateTime dt = DateTime.Parse("1970-01-01 00:00:00");
            if (entity.EffectiveDate > dt)
            {
                sql = sql + $@" AND EFFECTIVE_DATE='{ entity.EffectiveDate}' ";
            }
            if (entity.ExpirationDate > dt)
            {
                sql = sql + $@" AND EXPIRATION_DATE='{ entity.ExpirationDate}' ";
            }
            if (entity.Status != 0)
            {
                sql = sql + $@" AND STATUS='{ entity.Status}' ";
            }
            if (entity.ContractGroup != 0)
            {
                sql = sql + $@" AND CONTRACT_GROUP='{ entity.ContractGroup}' ";
            }
            if (entity.ExportTypeData == "FTS")//全部合同FTS导出
            {
                sql = sql + $@" AND FERRERO_ENTITY='Ferrero Trading (Shanghai) Co., Ltd.' ";
            }
            if (entity.ExportTypeData == "FFH")//全部合同FFH导出
            {
                sql = sql + $@" AND FERRERO_ENTITY='Ferrero Food (Hangzhou) Co., Ltd.' ";
            }
            if (entity.ExportTypeData == "IApplied")//我发起的合同
            {
                sql = sql + $@" AND SUPPLIER='{WebCaching.UserId}' ";
            }
            if (entity.ExportTypeData == "ApprovalByMe")//我审批的合同
            {
                sql = sql.Replace("WHERE 1=1", " INNER JOIN dbo.CAS_CONTRACT_APPROVAL_RESULT CAR ON CON.CONTRACT_ID = CAR.CONTRACT_ID WHERE 1=1 ");
                sql = sql + $@" AND CAR.APPROVER_ID='{WebCaching.UserId}' ";
            }
            if (entity.ExportTypeData == "MyDepart")//我的部门合同
            {
                sql = sql.Replace("WHERE 1=1", " INNER JOIN dbo.CAS_USER CUS ON CON.SUPPLIER = CUS.USER_ID WHERE 1=1");
                sql = sql + $@" AND CUS.DEPARMENT_CODE=(SELECT DEPARMENT_CODE FROM dbo.CAS_USER WHERE USER_ID='{WebCaching.UserId}') ";
            }
            if (entity.ExportTypeData == "ISupport")//我支持的合同
            {
                sql = sql.Replace("WHERE 1=1", " INNER JOIN dbo.CAS_PO_APPROVAL_SETTINGS PAS ON CON.CONTRACT_TYPE_ID=PAS.CONTRACT_TYPE_ID INNER JOIN dbo.CAS_CONTRACT_FILING CCF ON CON.CONTRACT_ID = CCF.CONTRACT_ID WHERE 1=1 ");
                sql = sql + $@" AND CCF.STATUS <> {ContractFilingEnum.Save.GetHashCode()} AND PAS.USER_ID='{WebCaching.UserId}'";
                if (entity.PO!="")
                {
                    sql = sql + $@" AND CCF.PO_NO LIKE N'%{entity.PO}%'";
                }
                if (entity.PR != "")
                {
                    sql = sql + $@" AND CCF.PR_NO LIKE N'%{entity.PR}%'";
                }
            }
            var tableValue = DataAccess.SelectDataSet(sql).Tables[0];
            return tableValue;
        }


        public JqGrid GetContractAttachment(JqGrid grid)
        {
            var query = new ContractAttachmentQuery();
            query.keyWord = grid.keyWord;
            grid = QueryTableHelper.QueryGrid<CasAttachmentEntity>(query, grid);
            return grid;
        }

        public List<CasContractEntity> GetExpireIn45DaysContracts()
        {
            string sql = $@"SELECT * 
                              FROM CAS_CONTRACT
                             WHERE DATEDIFF(DAY, GETDATE(), EXPIRATION_DATE)= 45";
            return DataAccess.Select<CasContractEntity>(sql);
        }
        public List<CasContractEntity> GetExpireIn90DaysContracts()
        {
            string sql = $@"SELECT * 
                              FROM CAS_CONTRACT
                             WHERE DATEDIFF(DAY,GETDATE(),EXPIRATION_DATE)=90";
            return DataAccess.Select<CasContractEntity>(sql);
        }

        public List<CasContractEntity> GetAllApplyContractsByUserId(string userId)
        {
            string sql = $@" SELECT CON.CREATED_BY,CON.CONTRACT_OWNER,CON.TEMPLATE_OWNER,CON.CONTRACT_NAME,CON.TEMPLATE_NAME,
                CON.FERRERO_ENTITY,CON.COUNTERPARTY_EN,CON.COUNTERPARTY_CN,CON.TEMPLATE_NO,CON.CONTRACT_NO,(CASE CON.STATUS WHEN '1'THEN N'未提交' WHEN '2' THEN N'待审批' 
                WHEN '3'THEN N'审批通过'  WHEN '4'THEN N'审批拒绝'WHEN '5'THEN N'签署完成 'WHEN '6'THEN N'关闭'WHEN '7'THEN N'后台关闭'WHEN '8'THEN N'审批拒绝后重新提交' ELSE N'未知' END ) AS STATUS
                 FROM CAS_CONTRACT CON WHERE CON.CREATED_BY = '{userId}'";
            return DataAccess.Select<CasContractEntity>(sql);
        }

        public List<CasContractEntity> GetAllApproveContractsByUserId(string userId)
        {
            string sql = $@"SELECT CAS_CONTRACT.*
                              FROM CAS_CONTRACT_APPROVER
                        INNER JOIN CAS_CONTRACT
                                ON CAS_CONTRACT_APPROVER.CONTRACT_ID = CAS_CONTRACT.CONTRACT_ID
							   AND CAS_CONTRACT_APPROVER.APPROVER_TYPE IN
                                   ({ApproverTypeEnum.LeaderComment.GetHashCode()}
                                   ,{ApproverTypeEnum.DepartmentManager.GetHashCode()}
                                   ,{ApproverTypeEnum.RegionManager.GetHashCode()})
                             WHERE APPROVER_ID = '{userId}'
                             UNION
                            SELECT CAS_CONTRACT.*
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
                             WHERE CAS_USER.USER_ID = '{userId}' ";
            return DataAccess.Select<CasContractEntity>(sql);
        }

        public List<CasContractEntity> GetAllPOApproveContractsByUserId(string userId)
        {
            string sql = $@"SELECT *
                                FROM CAS_CONTRACT
                               WHERE  EXISTS(
                                            SELECT * 
                                              FROM CAS_PO_APPROVAL_SETTINGS 
                                             WHERE CAS_CONTRACT.CONTRACT_TYPE_ID = CAS_PO_APPROVAL_SETTINGS.CONTRACT_TYPE_ID
                                               AND CAS_PO_APPROVAL_SETTINGS.USER_ID = '{userId}')";
            return DataAccess.Select<CasContractEntity>(sql);
        }
    }
}
