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

namespace eContract.BusinessService.BusinessData.BusinessRule
{
    public class ContractCommentBLL : BusinessBase
    {
        public JqGrid ForGrid(JqGrid grid)
        {
            var query = new ContractCommentQuery();
            grid = QueryTableHelper.QueryGrid<CasContractEntity>(query, grid);
            return grid;
        }

        /// <summary>
        /// 获取领导批注结果
        /// </summary>
        /// <returns></returns>
        public DataTable GetCommentResultDt(string id)
        {
            string sql = $@"SELECT t1.*,t2.ENGLISH_NAME FROM dbo.CAS_CONTRACT_APPROVAL_RESULT t1 
                            LEFT JOIN dbo.CAS_USER t2 ON t1.LAST_MODIFIED_BY = t2.USER_ID
                            WHERE t1.CONTRACT_ID = '{id}'
                            AND t1.APPROVER_TYPE = 1 ";

            return DataAccess.SelectDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 领导批注
        /// </summary>
        /// <param name="casContractApprovalResultEntity"></param>
        /// <returns></returns>
        public bool OptionCommnet(CasContractApprovalResultEntity casContractApprovalResultEntity)
        {
            bool flag = false;
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                broker.BeginTransaction();
                try
                {
                    string sql = $@"UPDATE dbo.CAS_CONTRACT_APPROVER SET STATUS = 3 
                                    WHERE APPROVER_TYPE = 1 
                                    AND CONTRACT_ID = '{casContractApprovalResultEntity.ContractId}'
                                    AND APPROVER_ID = '{casContractApprovalResultEntity.ApproverId}'
                                    AND STATUS = 2";

                    //审批人表更改领导批注状态为已批注
                    broker.ExecuteSQL(sql);

                    //插入审批结果
                    flag = DataAccess.Insert(casContractApprovalResultEntity);
                    if (flag)
                    {
                        broker.Commit();
                    }
                    else
                    {
                        broker.RollBack();
                    }
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

    }
}
