using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common;
using eContract.Common.Entity;
using eContract.Common.WebUtils;

namespace eContract.BusinessService.BusinessData.CommonQuery
{
    public class ContractDraftsQuery:IQuery
    {
        public WhereBuilder ParseSQL()
        {
            var sql = new StringBuilder();
            //sql.AppendFormat(" SELECT * FROM CAS_CONTRACT WHERE STATUS='{0}' AND CREATED_BY='{1}' ", ContractStatusEnum.Uncommitted.GetHashCode(), WebCaching.UserId);
            sql.AppendFormat(" SELECT CONTRACT_ID,CONTRACT_SERIAL_NO,CONTRACT_GROUP,FERRERO_ENTITY,COUNTERPARTY_EN, CONTRACT_TYPE_NAME,(CASE IS_TEMPLATE_CONTRACT WHEN 0 THEN  CONTRACT_NAME WHEN 1 THEN TEMPLATE_NAME ELSE '' END ) AS CONTRACT_NAME,(CASE IS_TEMPLATE_CONTRACT WHEN 0 THEN  CONTRACT_INITIATOR  WHEN 1 THEN TEMPLATE_INITIATOR ELSE '' END) AS CONTRACT_INITIATOR, APPLY_DATE,STATUS FROM CAS_CONTRACT WHERE (STATUS IN ('{0}','{1}')) AND CREATED_BY='{2}' ", ContractStatusEnum.Uncommitted.GetHashCode(), ContractStatusEnum.ApprovalReject.GetHashCode(), WebCaching.UserId);

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
