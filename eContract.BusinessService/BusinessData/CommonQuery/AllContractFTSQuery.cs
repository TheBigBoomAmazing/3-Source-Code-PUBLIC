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
    public class AllContractFTSQuery : IQuery
    {
        public string keyWord;
        public string CounterParty;
        public string StatusValue;
        public WhereBuilder ParseSQL()
        {
            var sql = new StringBuilder();
            //sql.AppendLine(" SELECT * FROM CAS_CONTRACT WHERE 1=1 AND FERRERO_ENTITY='Ferrero Trading (Shanghai) Co., Ltd.'");
            sql.AppendLine(" SELECT CON.CONTRACT_ID,CON.STATUS,CON.SUPPLIER,CON.CONTRACT_NO,CON.CONTRACT_SERIAL_NO,CON.CONTRACT_GROUP,CON.CONTRACT_TYPE_ID,CON.CONTRACT_TYPE_NAME, CON.NEED_COMMENT, CON.CONTRACT_TERM, CON.FERRERO_ENTITY, CON.COUNTERPARTY_EN, CON.COUNTERPARTY_CN,CON.EFFECTIVE_DATE,(CASE WHEN IS_TEMPLATE_CONTRACT = '0' THEN CONTRACT_NAME WHEN IS_TEMPLATE_CONTRACT = '1' THEN TEMPLATE_NAME ELSE '' END) CONTRACT_NAME,(CASE WHEN IS_TEMPLATE_CONTRACT = '0' THEN CONTRACT_OWNER WHEN IS_TEMPLATE_CONTRACT = '1' THEN TEMPLATE_OWNER ELSE '' END) CONTRACT_OWNER,(CASE WHEN IS_TEMPLATE_CONTRACT = '0' THEN CONTRACT_INITIATOR WHEN IS_TEMPLATE_CONTRACT = '1' THEN TEMPLATE_INITIATOR ELSE '' END) CONTRACT_INITIATOR FROM CAS_CONTRACT CON WHERE 1=1 AND CON.FERRERO_ENTITY='Ferrero Trading (Shanghai) Co., Ltd.'");
            if (keyWord != "")
            {
                sql.AppendLine($@" AND ( CON.TEMPLATE_NAME LIKE N'%{keyWord}%' OR CON.CONTRACT_NAME LIKE N'%{keyWord}%') ");
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
            var wb = new WhereBuilder(sql.ToString());
            return wb;
        }
    }
}
