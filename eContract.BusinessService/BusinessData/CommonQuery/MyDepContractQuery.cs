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
    public class MyDepContractQuery:IQuery
    {
        public string keyWord;
        public string CounterParty;
        public string StatusValue;
        public string CONTRACT_SERIAL_NO;
        public string CONTRACT_GROUP;

        public WhereBuilder ParseSQL()
        {
            var sql = new StringBuilder();
            //sql.AppendLine($@" SELECT CON.* FROM  dbo.CAS_CONTRACT CON INNER JOIN dbo.CAS_USER CUS ON CON.SUPPLIER = CUS.USER_ID WHERE CUS.DEPARMENT_CODE=(SELECT DEPARMENT_CODE FROM dbo.CAS_USER WHERE USER_ID='{WebCaching.UserId}') ");
            sql.AppendLine($@" SELECT  CON.CONTRACT_ID,CON.STATUS,SUPPLIER,CON.CONTRACT_NO,CON.CONTRACT_SERIAL_NO,CON.CONTRACT_GROUP,CON.CONTRACT_TYPE_ID,CON.CONTRACT_TYPE_NAME, CON.NEED_COMMENT, CON.CONTRACT_TERM, CON.FERRERO_ENTITY,CON.COUNTERPARTY_EN, CON.COUNTERPARTY_CN,CON.EFFECTIVE_DATE,CON.IS_MASTER_AGREEMENT,CON.APPLY_DATE,CON.CREATED_BY,CON.CREATE_TIME,(CASE WHEN IS_TEMPLATE_CONTRACT = '0' THEN CONTRACT_NAME WHEN IS_TEMPLATE_CONTRACT = '1' THEN TEMPLATE_NAME ELSE '' END) CONTRACT_NAME,(CASE WHEN IS_TEMPLATE_CONTRACT = '0' THEN CONTRACT_OWNER WHEN IS_TEMPLATE_CONTRACT = '1' THEN TEMPLATE_OWNER ELSE '' END) CONTRACT_OWNER,(CASE WHEN IS_TEMPLATE_CONTRACT = '0' THEN CONTRACT_INITIATOR WHEN IS_TEMPLATE_CONTRACT = '1' THEN TEMPLATE_INITIATOR ELSE '' END) CONTRACT_INITIATOR FROM CAS_CONTRACT CON INNER JOIN dbo.CAS_USER CUS ON CON.SUPPLIER = CUS.USER_ID 
 WHERE CUS.DEPARMENT_CODE in (SELECT A.DEPT_CODE FROM dbo.CAS_DEPARTMENT A JOIN dbo.CAS_USER_PERMISSION B 
 ON A.DEPT_ID=B.DEPT_ID WHERE B.USER_ID='{WebCaching.UserId}') ");
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
            var wb = new WhereBuilder(sql.ToString());
            return wb;
        }
    }
}
