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

namespace eContract.BusinessService.BusinessData.BusinessRule
{
    public class ContractTypeManagementBLL : BusinessBase
    {
        public JqGrid ForGrid(JqGrid grid)
        {
            var query = new ContractTypeManagementQuery();
            grid = QueryTableHelper.QueryGrid<CasContractTypeEntity>(query, grid);
            return grid;
        }
        public virtual CasContractTypeEntity CreateContractTypeEntity(string systemName = "MDM")
        {
            return new CasContractTypeEntity();
        }

        public virtual bool SaveContractTypeEntity(CasContractTypeEntity contractTypeEntity, ref string strError)
        {
            contractTypeEntity.LastModifiedBy = WebCaching.UserId;
            contractTypeEntity.LastModifiedTime = DateTime.Now;
            contractTypeEntity = CheckInputValue(contractTypeEntity);
            try
            {
                var isExist = IsExist(contractTypeEntity, ref strError);
                if (isExist)
                {
                    //更新
                    if (Update(contractTypeEntity))
                    {
                        return true;
                    }
                }
                else
                {
                    //新增
                    contractTypeEntity.ContractTypeId = Guid.NewGuid().ToString();
                    contractTypeEntity.CreatedBy = WebCaching.UserId;
                    contractTypeEntity.CreateTime = DateTime.Now;
                    contractTypeEntity.IsDeleted = false;
                    if (Insert(contractTypeEntity))
                    {
                        return true;
                    }
                }
            }
            catch (Exception exception)
            {
                strError = exception.Message;
            }
            return false;
        }

        public virtual bool DeletContractTypeEntity(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return false;
            var strsql = new StringBuilder();
            strsql.AppendFormat("DELETE FROM CAS_CONTRACT_TYPE WHERE CONTRACT_TYPE_ID= {0}",
                Utils.ToSQLStr(id).Trim());
            var val = DataAccess.SelectScalar(strsql.ToString());
            return true;
        }
        /// <summary>
        /// 为防止前台胡乱输入做的验证处理
        /// </summary>
        /// <param name="contractTypeEntity"></param>
        /// <returns></returns>
        public CasContractTypeEntity CheckInputValue(CasContractTypeEntity contractTypeEntity)
        {
            if (contractTypeEntity.IsTemplateContract == true)
            {
                contractTypeEntity.CounterpartyCn = false;
                contractTypeEntity.ContractName = false;
                contractTypeEntity.ContractTerm = false;
                contractTypeEntity.ContractOwner = false;
                contractTypeEntity.ContractInitiator = false;
                contractTypeEntity.IsMasterAgreement = false;
                contractTypeEntity.Supplementary = false;
                contractTypeEntity.BudgetType = false;
                contractTypeEntity.BudgetAmount = false;
                contractTypeEntity.InternalORInvestmentOrder = false;
                contractTypeEntity.ContractOREstimatedPrice = false;
                //contractTypeEntity.EstimatedPrice = false;
                contractTypeEntity.Currency = false;
                contractTypeEntity.PrepaymentAmount = false;
                contractTypeEntity.PrepaymentPercentage = false;
                contractTypeEntity.ContractKeyPoints = false;
            }
            if (contractTypeEntity.IsTemplateContract == false)
            {
                contractTypeEntity.TemplateName = false;
                contractTypeEntity.TemplateTerm = false;
                contractTypeEntity.TemplateOwner = false;
                contractTypeEntity.TemplateInitiator = false;
                contractTypeEntity.ScopeOfApplication = false;
            }
            return contractTypeEntity;
        }

        public virtual bool IsExist(CasContractTypeEntity casContractTypeEntity, ref string strError)
        {
            if (casContractTypeEntity.ContractTypeId == null) return false;
            var strsql = new StringBuilder();
            strsql.AppendFormat("SELECT 1 FROM CAS_CONTRACT_TYPE WHERE CONTRACT_TYPE_ID = {0}",
                Utils.ToSQLStr(casContractTypeEntity.ContractTypeId).Trim());
            var val = DataAccess.SelectScalar(strsql.ToString());
            if (string.IsNullOrEmpty(val) || val != "1") return false;
            strError = "该合同类型已存在";
            return true;
        }

        /// <summary>
        /// 获取当前用户有权限申请的合同类型
        /// </summary>
        /// <returns></returns>
        public virtual List<CasContractTypeEntity> SelectEnabledContractType()
        {
            //测试
            string sql = $@"SELECT DISTINCT t1.* FROM CAS_CONTRACT_TYPE t1 
                            JOIN dbo.CAS_CONTRACT_APPROVAL_STEP t2 ON t1.CONTRACT_TYPE_ID = t2.CONTRACT_TYPE_ID
                            JOIN CAS_CONTRACT_APPROVAL_STEP_APPLY_DEPT t3 ON t2.CONTRACT_APPROVAL_STEP_ID= t3.CONTRACT_APPROVAL_STEP_ID
                            WHERE 1=1";
            if (!CurrentUserDomain.CasUserEntity.IsAdmin)
            {
                sql += $@"AND ((t3.APPLY_TYPE = 1 AND EXISTS (SELECT 1 FROM 
												CAS_DEPARTMENT
												INNER JOIN CAS_USER
												ON CAS_DEPARTMENT.DEPT_CODE = CAS_USER.DEPARMENT_CODE
												WHERE USER_ID = '{CurrentUserDomain.CasUserEntity.UserId}'
												AND t3.DEPT_ID = CAS_DEPARTMENT.DEPT_ID))
                            OR (t3.APPLY_TYPE = 2 AND t3.DEPT_ID = '{CurrentUserDomain.CasUserEntity.UserId}')
                            OR (t3.APPLY_TYPE = 3 AND EXISTS (SELECT 1 FROM 
                            CAS_DEPT_USER
                            WHERE USER_ID = '{CurrentUserDomain.CasUserEntity.UserId}'
                            AND t3.DEPT_ID = CAS_DEPT_USER.DEPT_ID)))";
            }
            return DataAccess.Select<CasContractTypeEntity>(sql);
        }


        /// <summary>
        /// 获取所有合同类型
        /// </summary>
        /// <returns></returns>
        public virtual List<CasContractTypeEntity> SelectAllContractType()
        {
            //测试
            string sql = $@"SELECT DISTINCT t1.* FROM CAS_CONTRACT_TYPE t1 
                            JOIN dbo.CAS_CONTRACT_APPROVAL_STEP t2 ON t1.CONTRACT_TYPE_ID = t2.CONTRACT_TYPE_ID
                            JOIN CAS_CONTRACT_APPROVAL_STEP_APPLY_DEPT t3 ON t2.CONTRACT_APPROVAL_STEP_ID= t3.CONTRACT_APPROVAL_STEP_ID
                            WHERE 1=1";
            return DataAccess.Select<CasContractTypeEntity>(sql);
        }

        public virtual List<CasContractTypeEntity> SelectAllSubmitContractType()
        {
            
            string sql = $@"SELECT CONTRACT_TYPE_NAME FROM CAS_CONTRACT_TYPE WHERE 1=1";
            return DataAccess.Select<CasContractTypeEntity>(sql);
        }
    }
}
