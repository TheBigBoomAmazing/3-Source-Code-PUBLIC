using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common;
using eContract.Common.Entity;

namespace eContract.BusinessService.BusinessData.CommonQuery
{
    public  class DepUserCommonQuery:IQuery
    {
        public WhereBuilder ParseSQL()
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM CAS_DEPARTMENT WHERE 1 = 1 ");
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
