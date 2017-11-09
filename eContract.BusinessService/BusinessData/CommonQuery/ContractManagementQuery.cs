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
    public class ContractManagementQuery:IQuery
    {
        public string keyWord;

        public string homeQuery;

        public string CounterParty;

        public string StatusValue;
        public WhereBuilder ParseSQL()
        {
            var sql = new StringBuilder();
            //sql.AppendFormat(" SELECT * FROM CAS_CONTRACT WHERE 1=1 AND SUPPLIER='{0}'", WebCaching.UserId);
            sql.AppendFormat(" SELECT CONTRACT_ID,CONTRACT_NO,CONTRACT_SERIAL_NO,STATUS,SUPPLIER,CONTRACT_GROUP,CONTRACT_TYPE_ID,CONTRACT_TYPE_NAME, NEED_COMMENT, CONTRACT_TERM, FERRERO_ENTITY, COUNTERPARTY_EN, COUNTERPARTY_CN,EFFECTIVE_DATE,(CASE WHEN IS_TEMPLATE_CONTRACT = '0' THEN CONTRACT_NAME WHEN IS_TEMPLATE_CONTRACT = '1' THEN TEMPLATE_NAME ELSE '' END) CONTRACT_NAME,(CASE WHEN IS_TEMPLATE_CONTRACT = '0' THEN CONTRACT_OWNER WHEN IS_TEMPLATE_CONTRACT = '1' THEN TEMPLATE_OWNER ELSE '' END) CONTRACT_OWNER,(CASE WHEN IS_TEMPLATE_CONTRACT = '0' THEN CONTRACT_INITIATOR WHEN IS_TEMPLATE_CONTRACT = '1' THEN TEMPLATE_INITIATOR ELSE '' END) CONTRACT_INITIATOR FROM CAS_CONTRACT WHERE 1=1  AND SUPPLIER='{0}'", WebCaching.UserId);
            if (keyWord!="")
            {
                sql.AppendLine($@" AND ( TEMPLATE_NAME LIKE N'%{keyWord}%' OR CONTRACT_NAME LIKE N'%{keyWord}%') ");
            }
            if (CounterParty!="")
            {
                sql.AppendLine($@" AND ( COUNTERPARTY_CN LIKE N'%{CounterParty}%' OR COUNTERPARTY_EN LIKE N'%{CounterParty}%') ");
            }
            if (StatusValue!="")
            {
                if (StatusValue=="2")
                {
                    sql.AppendLine($@" AND STATUS IN('{ContractStatusEnum.WaitApproval.GetHashCode()}','{ContractStatusEnum.Resubmit.GetHashCode()}') ");
                }
                else
                {
                    sql.AppendLine($@" AND STATUS IN('{StatusValue}') ");
                }
            }
            if (homeQuery!="")
            {
                sql.AppendLine($@" AND STATUS NOT IN('{ContractStatusEnum.SignedCompleted.GetHashCode()}','{ContractStatusEnum.Shutdown.GetHashCode()}','{ContractStatusEnum.BackgroundShutdown.GetHashCode()}') ");
            }
            var wb = new WhereBuilder(sql.ToString());
            return wb;
        }
    }
}
