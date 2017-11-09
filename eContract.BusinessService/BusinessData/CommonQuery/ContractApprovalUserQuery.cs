using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Entity;
using eContract.Common.Schema;
namespace eContract.BusinessService.BusinessData.CommonQuery
{
    public class ContractApprovalUserQuery:IQuery
    {
        public string keyWord;
        public WhereBuilder ParseSQL()
        {
            var sql = new StringBuilder();
            sql.AppendLine($@" SELECT CUS.CHINESE_NAME,CUS.ENGLISH_NAME,CUS.USER_ACCOUNT,CUS.USER_ID,CDT.DEPT_NAME,CDT.DEPT_ALIAS FROM  dbo.CAS_CONTRACT_APPROVAL_STEP_APPROVAL_DEPT ASAD  
              INNER JOIN CAS_DEPT_USER CDU
              ON ASAD.DEPT_ID = CDU.DEPT_ID
              INNER JOIN  dbo.CAS_USER CUS ON CDU.USER_ID = CUS.USER_ID
              INNER JOIN dbo.CAS_DEPARTMENT CDT ON ASAD.DEPT_ID = CDT.DEPT_ID
              WHERE CONTRACT_APPROVAL_STEP_ID = '{keyWord}'");
            //if (keyWord != "")
            //{
            //    sql.AppendLine($@" AND ( CON.TEMPLATE_NAME LIKE N'%{keyWord}%' OR CON.CONTRACT_NAME LIKE N'%{keyWord}%') ");
            //}
            var wb = new WhereBuilder(sql.ToString());
            return wb;
        }
    }
}
