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
    public class ContractApprovalByMeQuery:IQuery
    {
        public string keyWord;
        public string CounterParty;
        public string StatusValue;
        public string CONTRACT_SERIAL_NO;
        public string CONTRACT_GROUP;
        public string CONTRACT_NO;
        public string FERRERO_ENTITY;
        public WhereBuilder ParseSQL()
        {
            var sql = new StringBuilder();
            sql.AppendLine($@"SELECT * FROM ( SELECT DISTINCT CAR.APPROVER_ID,CON.CONTRACT_ID,CON.CONTRACT_NO,CON.CONTRACT_SERIAL_NO,CON.STATUS,SUPPLIER,CON.CONTRACT_GROUP,CON.CONTRACT_TYPE_ID,CON.CONTRACT_TYPE_NAME, CON.NEED_COMMENT, CON.CONTRACT_TERM, CON.FERRERO_ENTITY,CON.COUNTERPARTY_EN, CON.COUNTERPARTY_CN,CON.EFFECTIVE_DATE,CON.IS_MASTER_AGREEMENT,CON.APPLY_DATE,CON.CREATED_BY,CON.CREATE_TIME,(CASE WHEN IS_TEMPLATE_CONTRACT = '0' THEN CONTRACT_NAME WHEN IS_TEMPLATE_CONTRACT = '1' THEN TEMPLATE_NAME ELSE '' END) CONTRACT_NAME,(CASE WHEN IS_TEMPLATE_CONTRACT = '0' THEN CONTRACT_OWNER WHEN IS_TEMPLATE_CONTRACT = '1' THEN TEMPLATE_OWNER ELSE '' END) CONTRACT_OWNER,(CASE WHEN IS_TEMPLATE_CONTRACT = '0' THEN CONTRACT_INITIATOR WHEN IS_TEMPLATE_CONTRACT = '1' THEN TEMPLATE_INITIATOR ELSE '' END) CONTRACT_INITIATOR FROM CAS_CONTRACT CON INNER JOIN dbo.CAS_CONTRACT_APPROVAL_RESULT CAR ON CON.CONTRACT_ID = CAR.CONTRACT_ID WHERE 1=1 AND CAR.APPROVER_ID='{WebCaching.UserId}' AND CON.CREATED_BY<>'{WebCaching.UserId}'");
            if (keyWord != "")
            {
                sql.AppendLine($@" AND ( CON.TEMPLATE_NAME LIKE N'%{keyWord}%' OR CON.CONTRACT_NAME LIKE N'%{keyWord}%') ");
            }
            if (CONTRACT_SERIAL_NO!="")
            {
                sql.AppendLine($@" AND CON.CONTRACT_SERIAL_NO LIKE N'%{CONTRACT_SERIAL_NO}%'");
            }
            if (FERRERO_ENTITY != "")
            {
                sql.AppendLine($@" AND CON.FERRERO_ENTITY ='{FERRERO_ENTITY}'");
            }
            if (CONTRACT_NO != "")
            {
                sql.AppendLine($@" AND CON.CONTRACT_NO LIKE N'%{CONTRACT_NO}%'");
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
            sql.AppendLine(" ) result");

            var wb = new WhereBuilder(sql.ToString());
            return wb;
        }
    }
}
