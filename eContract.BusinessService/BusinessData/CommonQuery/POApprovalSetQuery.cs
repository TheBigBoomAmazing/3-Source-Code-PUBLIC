using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Entity;
using eContract.Common.Schema;

namespace eContract.BusinessService.BusinessData.CommonQuery
{

    public class POApprovalSetQuery : IQuery
    {
        public string keyWord;
        public WhereBuilder ParseSQL()
        {
            //StringBuilder strsql = new StringBuilder();

            //strsql.Append("SELECT T1.*,T2.CONTRACT_TYPE_NAME FROM CAS_PO_APPROVAL_SETTINGS T1 INNER JOIN CAS_CONTRACT_TYPE T2  ON T1.CONTRACT_TYPE_ID=T2.CONTRACT_TYPE_ID ");
            var strsql=$@" SELECT * FROM (SELECT DISTINCT STUFF(( SELECT  ',' + T3.USER_ID FROM  CAS_PO_APPROVAL_SETTINGS  T3 WHERE T1.CONTRACT_TYPE_ID = T3.CONTRACT_TYPE_ID AND t1.COMPANY =t3.COMPANY  FOR  XML PATH('') ), 1, 1, '')   AS USER_ID,T1.COMPANY,T1.CONTRACT_TYPE_ID,MIN(T2.CONTRACT_TYPE_NAME) AS CONTRACT_TYPE_NAME,MIN(T1.PO_APPROVAL_ID) AS PO_APPROVAL_ID FROM CAS_PO_APPROVAL_SETTINGS T1  INNER JOIN  CAS_CONTRACT_TYPE T2  ON T1.CONTRACT_TYPE_ID=T2.CONTRACT_TYPE_ID WHERE 1=1 GROUP BY t1.COMPANY,t1.CONTRACT_TYPE_ID ) a " ;

            if (!string.IsNullOrEmpty(keyWord))
            {
                var test = $@"WHERE 1=1 AND T1.COMPANY LIKE N'%{keyWord}%' OR  T2.CONTRACT_TYPE_NAME LIKE N'%{keyWord}%'";
                strsql=strsql.Replace("WHERE 1=1", test.ToString());
                //strsql.Append(" WHERE (T1.COMPANY LIKE '%" + keyWord + "%' OR  T2.CONTRACT_TYPE_NAME LIKE '%" + keyWord + "%') ");
            }
            WhereBuilder wb = new WhereBuilder(strsql.ToString());
            return wb;
        }

    }
}
