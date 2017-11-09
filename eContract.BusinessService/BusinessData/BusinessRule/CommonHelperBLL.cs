using eContract.BusinessService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eContract.Common.Entity;
using Suzsoft.Smart.Data;
using eContract.BusinessService.SystemManagement.Service;
using System.Data;
using eContract.Common;

namespace eContract.BusinessService.BusinessData.BusinessRule
{
    public class CommonHelperBLL : BusinessBase
    {
        #region 附件上传
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="casAttachmentEntityList"></param>
        /// <returns></returns>
        public bool SaveFile(CasAttachmentEntity casAttachmentEntity)
        {
            return DataAccess.Insert(casAttachmentEntity);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true:删除成功 false:删除失败</returns>
        public bool DeleteFile(string id)
        {
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                string sql = $"DELETE FROM dbo.CAS_ATTACHMENT WHERE ATTACHMENT_ID = '{id}'";
                return broker.ExecuteSQL(sql) >= 1;
            }
        }

        /// <summary>
        /// 根据源ID获取所有文件
        /// </summary>
        /// <param name="sourceId"></param>
        /// <returns></returns>
        public List<CasAttachmentEntity> GetFilesBySourceId(string sourceId)
        {
            string sql = $"SELECT * FROM  dbo.CAS_ATTACHMENT WHERE SOURCE_ID = '{sourceId}'";
            return DataAccess.Select<CasAttachmentEntity>(sql);
        }

        public List<CasAttachmentEntity> GetFilesDemo(string ids)
        {
            ids = $"'{ids.Replace(",", "','")}'";
            string sql = $"SELECT * FROM  dbo.CAS_ATTACHMENT WHERE ATTACHMENT_ID IN ({ids})";
            return DataAccess.Select<CasAttachmentEntity>(sql);
        }

        #endregion

        #region 审批

        /// <summary>
        /// 获取审批人数据
        /// </summary>
        /// <param name="casContractApproverEntity"></param>
        /// <returns></returns>
        public CasContractApproverEntity GetApprover(CasContractApproverEntity casContractApproverEntity)
        {
            try
            {
                return DataAccess.SelectSingle(casContractApproverEntity);
            }
            catch (Exception e)
            {
                SystemService.LogErrorService.InsertLog(e);
                return null;
            }
        }

        /// <summary>
        /// 根据合同ID获取所有的申请结果
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public List<CasContractApprovalResultEntity> GetApprovalResultList(string contractId)
        {
            string sql = $@"SELECT * FROM dbo.CAS_CONTRACT_APPROVAL_RESULT t1 WHERE t1.CONTRACT_ID = '{contractId}' ";
            return DataAccess.Select<CasContractApprovalResultEntity>(sql);
        }

        public DataTable GetApprovalResultDt(string contractId)
        {
            string sql = $@"SELECT  Result.* FROM (
                               SELECT TOP 10000 t1.APPROVER_TYPE APPROVER_TYPE
                                               ,t1.APPROVAL_TIME APPROVAL_TIME
                                               ,t1.APPROVAL_RESULT APPROVAL_RESULT
                                               ,t1.APPROVAL_OPINIONS APPROVAL_OPINIONS
                                               ,t2.ENGLISH_NAME ENGLISH_NAME
                                               ,t3.STEP STEP
                                           FROM dbo.CAS_CONTRACT_APPROVAL_RESULT t1
                                      LEFT JOIN dbo.CAS_USER t2 
                                             ON t1.APPROVER_ID = t2.USER_ID
                                      LEFT JOIN dbo.CAS_CONTRACT_APPROVAL_STEP t3 
                                             ON t1.CONTRACT_APPROVAL_STEP_ID = t3.CONTRACT_APPROVAL_STEP_ID
                                          WHERE t1.CONTRACT_ID='{contractId}' 
                                       ORDER BY t1.APPROVAL_TIME ASC) Result

                                      UNION ALL
 
                                         SELECT DISTINCT t1.APPROVER_TYPE
                                               ,NULL APPROVAL_TIME
                                               ,0 APPROVAL_RESULT
                                               ,'' APPROVAL_OPINIONS
                                               ,CASE WHEN t2.ENGLISH_NAME IS NULL 
                                                     THEN 
		                                                (SELECT STUFF((SELECT ','+ENGLISH_NAME 
                                                                         FROM CAS_USER
                                                                   INNER JOIN CAS_DEPT_USER
                                                                           ON CAS_USER.USER_ID = CAS_DEPT_USER.USER_ID
                                                                        WHERE CAS_DEPT_USER.DEPT_ID = t4.DEPT_ID
                                                                          for xml path('')),1,1,'')) 
                                                     ELSE t2.ENGLISH_NAME END ENGLISH_NAME
                                                ,CASE WHEN t3.STEP IS NULL THEN 0 ELSE t3.STEP END STEP
                                           FROM CAS_CONTRACT_APPROVER t1
                                      LEFT JOIN dbo.CAS_USER t2
                                             ON t1.APPROVER_ID = t2.USER_ID
                                      LEFT JOIN dbo.CAS_CONTRACT_APPROVAL_STEP t3 
                                             ON t1.CONTRACT_APPROVAL_STEP_ID = t3.CONTRACT_APPROVAL_STEP_ID
                                      LEFT JOIN dbo.CAS_DEPARTMENT t4
                                             ON t1.APPROVER_ID = t4.DEPT_ID
                                      LEFT JOIN dbo.CAS_CONTRACT t5
										     ON t1.CONTRACT_ID = t5.CONTRACT_ID
                                          WHERE ( t1.STATUS ={ContractApproverStatusEnum.WaitApproval.GetHashCode()}
                                               OR t1.STATUS ={ContractApproverStatusEnum.OverTime.GetHashCode()}
                                              AND t1.APPROVER_TYPE <> {ApproverTypeEnum.LeaderComment.GetHashCode()})
	                                        AND t5.STATUS IN ({ContractStatusEnum.WaitApproval.GetHashCode()},{ContractStatusEnum.Resubmit.GetHashCode()})
	                                        AND t1.CONTRACT_ID='{contractId}'";//按照审批时间正序排
            return DataAccess.SelectDataSet(sql).Tables[0];
        }

        public DataTable GetReportApprovalResultDt(string contractId)
        {
            string sql = $@"SELECT ENGLISH_NAME, 
DEPARMENT_NAME,
APPROVAL_TIME,
CASE result.APPROVAL_RESULT WHEN 0 THEN N'In review process'
WHEN 1 THEN N'Comment' 
WHEN 2 THEN N'Approved' 
WHEN 3 THEN N'Not applicable'
WHEN 4 THEN N'Rejected'
WHEN 5 THEN N'Closed'
WHEN 6 THEN N'ReSubmitted'
WHEN 7 THEN N'Submitted'
ELSE N'Error' End as APPROVAL_RESULT,
APPROVAL_OPINIONS FROM 
                                        (SELECT t1.APPROVER_TYPE APPROVER_TYPE
                                               ,t1.APPROVAL_TIME APPROVAL_TIME
                                               ,t1.APPROVAL_RESULT APPROVAL_RESULT
                                               ,t1.APPROVAL_OPINIONS APPROVAL_OPINIONS
                                               ,t2.ENGLISH_NAME ENGLISH_NAME
                                               ,t2.DEPARMENT_NAME DEPARMENT_NAME
                                               ,t3.STEP STEP
                                           FROM dbo.CAS_CONTRACT_APPROVAL_RESULT t1
                                      LEFT JOIN dbo.CAS_USER t2 
                                             ON t1.APPROVER_ID = t2.USER_ID
                                      LEFT JOIN dbo.CAS_CONTRACT_APPROVAL_STEP t3 
                                             ON t1.CONTRACT_APPROVAL_STEP_ID = t3.CONTRACT_APPROVAL_STEP_ID
                                          WHERE t1.CONTRACT_ID='{contractId}' 
                                            AND t1.CONTRACT_APPROVAL_RESULT_ID =
                                                    (SELECT TOP 1 CONTRACT_APPROVAL_RESULT_ID 
                                                       FROM CAS_CONTRACT_APPROVAL_RESULT r
		                                              WHERE t1.APPROVER_ID =  r.APPROVER_ID
		                                                AND t1.CONTRACT_APPROVAL_STEP_ID = r.CONTRACT_APPROVAL_STEP_ID
		                                                AND t1.APPROVER_TYPE = r.APPROVER_TYPE
                                                        AND t1.CONTRACT_ID=r.CONTRACT_ID
                                                   ORDER BY r.CREATE_TIME DESC)
 
                                      UNION ALL
 
                                         SELECT DISTINCT t1.APPROVER_TYPE
                                               ,NULL APPROVAL_TIME
                                               ,0 APPROVAL_RESULT
                                               ,'' APPROVAL_OPINIONS
                                               ,CASE WHEN t2.ENGLISH_NAME IS NULL 
                                                     THEN 
		                                                (SELECT STUFF((SELECT ','+ENGLISH_NAME 
                                                                         FROM CAS_USER
                                                                   INNER JOIN CAS_DEPT_USER
                                                                           ON CAS_USER.USER_ID = CAS_DEPT_USER.USER_ID
                                                                        WHERE CAS_DEPT_USER.DEPT_ID = t4.DEPT_ID
                                                                          for xml path('')),1,1,'')) 
                                                     ELSE t2.ENGLISH_NAME END ENGLISH_NAME
                                                    ,t2.DEPARMENT_NAME DEPARMENT_NAME
                                                ,CASE WHEN t3.STEP IS NULL THEN 0 ELSE t3.STEP END STEP
                                           FROM CAS_CONTRACT_APPROVER t1
                                      LEFT JOIN dbo.CAS_USER t2
                                             ON t1.APPROVER_ID = t2.USER_ID
                                      LEFT JOIN dbo.CAS_CONTRACT_APPROVAL_STEP t3 
                                             ON t1.CONTRACT_APPROVAL_STEP_ID = t3.CONTRACT_APPROVAL_STEP_ID
                                      LEFT JOIN dbo.CAS_DEPARTMENT t4
                                             ON t1.APPROVER_ID = t4.DEPT_ID
                                          WHERE t1.STATUS = 2
	                                        AND t1.CONTRACT_ID='{contractId}'
                                            AND NOT EXISTS(SELECT 1 
                                                             FROM CAS_CONTRACT_APPROVAL_RESULT t5
	                                                        WHERE t5.CONTRACT_APPROVAL_STEP_ID = t1.CONTRACT_APPROVAL_STEP_ID
	                                                          AND t5.CONTRACT_ID = t1.CONTRACT_ID
	                                                          AND ( t5.APPROVER_TYPE <> {ApproverTypeEnum.Department.GetHashCode()} 
	                                                                AND t5.APPROVER_ID = t1.APPROVER_ID
	                                                             OR t5.APPROVER_TYPE = {ApproverTypeEnum.Department.GetHashCode()}
	                                                                AND t5.APPROVER_ID 
                                                                            IN (SELECT USER_ID 
                                                                                  FROM CAS_DEPT_USER
	                                                                             WHERE DEPT_ID = t1.APPROVER_ID)
                                                                    )
                                            )) result
                              ORDER BY result.STEP ASC,result.APPROVAL_TIME ASC";//按照审批时间正序排
            return DataAccess.SelectDataSet(sql).Tables[0];
        }

        #endregion

        public virtual List<CasContractTypeEntity> SelectEnabledContractType()
        {
            //测试
            string sql = $@"SELECT DISTINCT t1.CONTRACT_TYPE_ID,t1.CONTRACT_TYPE_NAME,t1.FERRERO_ENTITY FROM CAS_CONTRACT_TYPE t1 WHERE 1=1 AND STATUS='2' ";

            return DataAccess.Select<CasContractTypeEntity>(sql);
        }
        /// <summary>
        /// 获得所有城市
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllCity()
        {
            var strsql = new StringBuilder();
            strsql.AppendFormat("  SELECT CITY_CODE,CITY_NAME FROM CAS_CITY ");
            var dataSet = DataAccess.SelectDataSet(strsql.ToString());
            return dataSet.Tables[0];
        }
    }
}
