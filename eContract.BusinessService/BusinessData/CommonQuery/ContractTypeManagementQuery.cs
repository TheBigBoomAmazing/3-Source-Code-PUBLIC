using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common;
using eContract.Common.Entity;

namespace eContract.BusinessService.BusinessData.CommonQuery
{
    public class ContractTypeManagementQuery :  IQuery
    {
        public WhereBuilder ParseSQL()
        {
            var sql = new StringBuilder();
            sql.AppendLine(" SELECT CT.*,CU.ENGLISH_NAME FROM CAS_CONTRACT_TYPE CT LEFT JOIN CAS_USER CU ON CT.CREATED_BY = CU.USER_ID WHERE 1=1 ");
            //if (!string.IsNullOrEmpty(ligerGrid.keyWord))
            //{
            //    sql.AppendLine("AND ( ");
            //    sql.AppendFormat("CUSTOMER_NAME LIKE '%{0}%' ", Utils.ToSQLStr(ligerGrid.keyWord));
            //    sql.AppendFormat("OR SHORT_NAME LIKE '%{0}%' ", Utils.ToSQLStr(ligerGrid.keyWord));
            //    sql.AppendFormat("OR CUSTOMER_CODE LIKE '%{0}%' ", Utils.ToSQLStr(ligerGrid.keyWord));
            //    sql.AppendLine(")");
            //}
            var wb = new WhereBuilder(sql.ToString());
            return wb;
        }
    }
}
