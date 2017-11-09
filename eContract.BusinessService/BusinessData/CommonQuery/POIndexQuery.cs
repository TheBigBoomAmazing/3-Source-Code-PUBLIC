using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common;
using eContract.Common.Entity;
using eContract.Common.WebUtils;
using eContract.BusinessService.SystemManagement.Domain;

namespace eContract.BusinessService.BusinessData.CommonQuery
{
    public class POIndexQuery : IQuery
    {
        public string CONTRACT_TYPE_NAME;
        public WhereBuilder ParseSQL()
        {
            var sql = new StringBuilder();
            sql.AppendLine($@"SELECT A.*,
                                    (CASE WHEN EXISTS(
                                                    SELECT * 
                                                      FROM CAS_CONTRACT_FILING B
                                                     WHERE B.CONTRACT_ID = A.CONTRACT_ID 
                                                       AND (B.STATUS = {ContractFilingEnum.Apply.GetHashCode()}
                                                         OR B.STATUS = {ContractFilingEnum.POSave.GetHashCode()})) 
                                          THEN 1 
                                          ELSE 0 
                                           END) POAPPLYEXISTS 
                                FROM CAS_CONTRACT A 
                               WHERE   (A.STATUS = {ContractStatusEnum.HadApproval.GetHashCode()}  OR A.STATUS = {ContractStatusEnum.SignedCompleted.GetHashCode()})");
            //过滤没有PR提交的直接查询不出来。PO拒绝的，也能在PO查看中进行查看。
            sql.AppendLine($@" AND EXISTS(SELECT CONTRACT_FILING_ID FROM CAS_CONTRACT_FILING T
                             WHERE T.CONTRACT_ID = A.CONTRACT_ID  AND T.STATUS !={ContractFilingEnum.Save.GetHashCode()}
                                    )");

            sql.AppendLine($@" AND EXISTS(
                SELECT * FROM CAS_PO_APPROVAL_SETTINGS C
                WHERE A.CONTRACT_TYPE_ID = C.CONTRACT_TYPE_ID
                AND (C.USER_ID = '{WebCaching.UserId}'
                OR EXISTS (
                SELECT * FROM CAS_PROXY_APPROVAL D 
                WHERE D.AGENT_USER_ID = '{WebCaching.UserId}'
                AND C.USER_ID = D.AUTHORIZED_USER_ID
                AND GETDATE() BETWEEN D.BEGIN_TIME AND D.END_TIME AND D.IS_DELETED=0
                ))
                )");
            if (CONTRACT_TYPE_NAME!="")
            {
                sql.AppendLine($@" AND A.CONTRACT_TYPE_NAME= N'{CONTRACT_TYPE_NAME}'");
            }
            var wb = new WhereBuilder(sql.ToString());
            return wb;
        }
    }
}
