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
    public class ISupportedContractQuery:IQuery
    {
        public string keyWord;
        public string CounterParty;
        public string PO;
        public string PR;
        public string StatusValue;
        public string CONTRACT_GROUP;
        public string CONTRACT_SERIAL_NO;
        public WhereBuilder ParseSQL()
        {
            var sql = new StringBuilder();
            //sql.AppendLine($@" SELECT  CON.CONTRACT_ID,CON.STATUS,CON.CONTRACT_NO,CON.CONTRACT_SERIAL_NO,SUPPLIER,CON.CONTRACT_GROUP,CON.CONTRACT_TYPE_ID,CON.CONTRACT_TYPE_NAME, CON.NEED_COMMENT, CON.CONTRACT_TERM, CON.FERRERO_ENTITY,CON.COUNTERPARTY_EN, CON.COUNTERPARTY_CN,CON.EFFECTIVE_DATE,CON.IS_MASTER_AGREEMENT,CON.APPLY_DATE,CON.CREATED_BY,CON.CREATE_TIME,(CASE WHEN IS_TEMPLATE_CONTRACT = '0' THEN CONTRACT_NAME WHEN IS_TEMPLATE_CONTRACT = '1' THEN TEMPLATE_NAME ELSE '' END) CONTRACT_NAME,(CASE WHEN IS_TEMPLATE_CONTRACT = '0' THEN CONTRACT_OWNER WHEN IS_TEMPLATE_CONTRACT = '1' THEN TEMPLATE_OWNER ELSE '' END) CONTRACT_OWNER,(CASE WHEN IS_TEMPLATE_CONTRACT = '0' THEN CONTRACT_INITIATOR WHEN IS_TEMPLATE_CONTRACT = '1' THEN TEMPLATE_INITIATOR ELSE '' END) CONTRACT_INITIATOR FROM CAS_CONTRACT CON INNER JOIN dbo.CAS_PO_APPROVAL_SETTINGS PAS ON CON.CONTRACT_TYPE_ID=PAS.CONTRACT_TYPE_ID INNER JOIN
            //  dbo.CAS_CONTRACT_FILING CCF ON CON.CONTRACT_ID = CCF.CONTRACT_ID WHERE 1=1 AND NOT_DISPLAY_IN_MY_SUPPORT ='0' AND CCF.STATUS='4' AND PAS.USER_ID='{WebCaching.UserId}'");
            
            //PO页面功能合并至ISupportedContract页面
            sql.AppendLine($@"SELECT CON.CONTRACT_ID,CON.STATUS,CON.CONTRACT_NO,CON.CONTRACT_SERIAL_NO,
CON.SUPPLIER,CON.CONTRACT_GROUP,CON.CONTRACT_TYPE_ID,CON.CONTRACT_TYPE_NAME, 
CON.NEED_COMMENT, CON.CONTRACT_TERM, CON.FERRERO_ENTITY,
CON.COUNTERPARTY_EN, CON.COUNTERPARTY_CN,CON.EFFECTIVE_DATE,CON.IS_MASTER_AGREEMENT,
CON.APPLY_DATE,CON.CREATED_BY,CON.CREATE_TIME,
(CASE WHEN IS_TEMPLATE_CONTRACT = '0' THEN CONTRACT_NAME 
WHEN IS_TEMPLATE_CONTRACT = '1' THEN TEMPLATE_NAME ELSE '' END) CONTRACT_NAME,
(CASE WHEN IS_TEMPLATE_CONTRACT = '0' THEN CONTRACT_OWNER 
WHEN IS_TEMPLATE_CONTRACT = '1' THEN TEMPLATE_OWNER ELSE '' END) CONTRACT_OWNER,
(CASE WHEN IS_TEMPLATE_CONTRACT = '0' THEN CONTRACT_INITIATOR 
WHEN IS_TEMPLATE_CONTRACT = '1' THEN TEMPLATE_INITIATOR ELSE '' END) CONTRACT_INITIATOR,
                                    (CASE WHEN EXISTS(
                                                    SELECT * 
                                                      FROM CAS_CONTRACT_FILING B
                                                     WHERE B.CONTRACT_ID = CON.CONTRACT_ID 
                                                       AND (B.STATUS = {ContractFilingEnum.Apply.GetHashCode()}
                                                         OR B.STATUS = {ContractFilingEnum.POSave.GetHashCode()})) 
                                          THEN 1 
                                          ELSE 0 
                                           END) POAPPLYEXISTS 
                                FROM CAS_CONTRACT CON 
                               WHERE   (CON.STATUS = {ContractStatusEnum.HadApproval.GetHashCode()}  OR CON.STATUS = {ContractStatusEnum.SignedCompleted.GetHashCode()})");
            //过滤没有PR提交的直接查询不出来。PO拒绝的，也能在PO查看中进行查看。
            sql.AppendLine($@" AND EXISTS(SELECT CONTRACT_FILING_ID FROM CAS_CONTRACT_FILING T
                             WHERE T.CONTRACT_ID = CON.CONTRACT_ID  AND T.STATUS !={ContractFilingEnum.Save.GetHashCode()}
                            AND 1=1
                                    )");

            sql.AppendLine($@" AND EXISTS(
                SELECT * FROM CAS_PO_APPROVAL_SETTINGS C
                WHERE CON.CONTRACT_TYPE_ID = C.CONTRACT_TYPE_ID
                AND (C.USER_ID = '{WebCaching.UserId}'
                OR EXISTS (
                SELECT * FROM CAS_PROXY_APPROVAL D 
                WHERE D.AGENT_USER_ID = '{WebCaching.UserId}'
                AND C.USER_ID = D.AUTHORIZED_USER_ID
                AND GETDATE() BETWEEN D.BEGIN_TIME AND D.END_TIME AND D.IS_DELETED=0
                ))
                )");
            if (keyWord != "")
            {
                sql.AppendLine($@" AND ( CON.TEMPLATE_NAME LIKE N'%{keyWord}%' OR CON.CONTRACT_NAME LIKE N'%{keyWord}%') ");
            }
            if (CONTRACT_SERIAL_NO != "")
            {
                sql.AppendLine($@" AND CON.CONTRACT_SERIAL_NO LIKE N'%{CONTRACT_SERIAL_NO}%'");
            }
            if (CONTRACT_GROUP != "")
            {
                sql.AppendLine($@" AND CON.CONTRACT_GROUP ='{CONTRACT_GROUP}'");
            }
            if (CounterParty != "")
            {
                sql.AppendLine($@" AND ( COUNTERPARTY_CN LIKE N'%{CounterParty}%' OR COUNTERPARTY_EN LIKE N'%{CounterParty}%') ");
            }
            if (StatusValue != "")
            {
                if (StatusValue == "2")
                {
                    sql.AppendLine($@" AND CON.STATUS IN('{ContractStatusEnum.WaitApproval.GetHashCode()}','{ContractStatusEnum.Resubmit.GetHashCode()}') ");
                }
                else
                {
                    sql.AppendLine($@" AND CON.STATUS IN('{StatusValue}') ");
                }
            }
            if (PO != "")
            {
                var POQuery = $@"1=1 AND T.PO_NO LIKE N'%{PO}%'";
                sql.Replace("1=1", POQuery);
            }
            if (PR != "")
            {
                var PRQuery = $@"1=1 AND T.PR_NO LIKE N'%{PR}%'";
                sql.Replace("1=1", PRQuery);
            }
            var wb = new WhereBuilder(sql.ToString());
            return wb;
        }
    }
}
