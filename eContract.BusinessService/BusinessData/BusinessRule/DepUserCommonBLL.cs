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
    public class DepUserCommonBLL :BusinessBase
    {
        public JqGrid ForGrid(JqGrid grid)
        {
            var query = new DepUserCommonQuery();
            grid = QueryTableHelper.QueryGrid<CasDepartmentEntity>(query, grid);
            return grid;
        }

        /// <summary>
        /// 查询申请部门
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllDepartment()
        {
            var strsql = new StringBuilder();
            strsql.AppendFormat("  SELECT DEPT_ID,((CASE COMPANY_CODE  WHEN '' THEN 'PUBLIC' ELSE COMPANY_CODE  END)  +'--'+ DEPT_ALIAS) AS DEPT_ALIAS FROM CAS_DEPARTMENT WHERE DEPT_TYPE='1' ORDER BY DEPT_ALIAS ASC ");
            var dataSet = DataAccess.SelectDataSet(strsql.ToString());
            return dataSet.Tables[0];
        }

        public DataTable GetAllUser()
        {
            var strsql = new StringBuilder();
            strsql.AppendFormat("  SELECT USER_ID,(ENGLISH_NAME+'-'+ CHINESE_NAME) AS NAME FROM CAS_USER ");
            var dataSet = DataAccess.SelectDataSet(strsql.ToString());
            return dataSet.Tables[0];
        }


        /// <summary>
        /// 查询所有审批部门
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllApprovalDepartment()
        {
            var strsql = new StringBuilder();
            strsql.AppendFormat("   SELECT DEPT_ID,DEPT_ALIAS FROM CAS_DEPARTMENT WHERE DEPT_TYPE='2' ORDER BY DEPT_ALIAS ASC ");
            var dataSet = DataAccess.SelectDataSet(strsql.ToString());
            return dataSet.Tables[0];
        }
    }
}
