using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Entity;
using eContract.Common.Schema;

namespace eContract.BusinessService.SystemManagement.CommonQuery
{
    public class CasDepartmentQuery : CasDepartmentEntity, IQuery
    {
        public string keyWord;
        public string depType;
        public WhereBuilder ParseSQL()
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("select T1.*,T2." + CasUserTable.C_ENGLISH_NAME + " from " + CasDepartmentTable.C_TableName + " T1 ");
            strsql.Append(" left join " + CasUserTable.C_TableName + " T2 ");
            strsql.Append(" on T1." + CasDepartmentTable.C_DEPT_MANAGER_ID + " = T2." + CasUserTable.C_USER_ID + " ");
            WhereBuilder wb = new WhereBuilder(strsql.ToString());
            if (!string.IsNullOrEmpty(keyWord))
            {
                wb.AddORCondition(CasDepartmentTable.C_DEPT_CODE, "like", keyWord);
                wb.AddORCondition(CasDepartmentTable.C_DEPT_NAME, "like", keyWord);
                wb.AddORCondition(CasDepartmentTable.C_DEPT_ALIAS, "like", keyWord);
            }
            if (!string.IsNullOrEmpty(depType))
            {
                wb.AddORCondition(CasDepartmentTable.C_DEPT_TYPE, "like", depType);
            }
            return wb;
        }
    }
}
