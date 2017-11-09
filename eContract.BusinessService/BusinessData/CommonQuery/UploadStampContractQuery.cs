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
    class UploadStampContractQuery : IQuery
    {
        public string keyWord;
        public string CONTRACT_TYPE_NAME;
        public WhereBuilder ParseSQL()
        {
            var sql = new StringBuilder();
            //查询审批通过和签署完成的合同
            sql.AppendLine($@" SELECT CONTRACT_ID,IS_TEMPLATE_CONTRACT,CONTRACT_SERIAL_NO,CREATE_TIME,STATUS,SUPPLIER,APPLY_DATE,CONTRACT_GROUP,CONTRACT_TYPE_ID,CONTRACT_TYPE_NAME, NEED_COMMENT, CONTRACT_TERM, FERRERO_ENTITY, COUNTERPARTY_EN, COUNTERPARTY_CN,EFFECTIVE_DATE,(CASE WHEN IS_TEMPLATE_CONTRACT = '0' THEN CONTRACT_NAME WHEN IS_TEMPLATE_CONTRACT = '1' THEN TEMPLATE_NAME ELSE '' END) CONTRACT_NAME,(CASE WHEN IS_TEMPLATE_CONTRACT = '0' THEN CONTRACT_OWNER WHEN IS_TEMPLATE_CONTRACT = '1' THEN TEMPLATE_OWNER ELSE '' END) CONTRACT_OWNER,(CASE WHEN IS_TEMPLATE_CONTRACT = '0' THEN CONTRACT_INITIATOR WHEN IS_TEMPLATE_CONTRACT = '1' THEN TEMPLATE_INITIATOR ELSE '' END) CONTRACT_INITIATOR FROM CAS_CONTRACT WHERE 1=1 AND STATUS IN ('3','5') AND CREATED_BY = '{ WebCaching.UserId}' ");
            if (keyWord != "")
            {
                sql.AppendLine($@" AND ( TEMPLATE_NAME LIKE N'%{keyWord}%' OR CONTRACT_NAME LIKE N'%{keyWord}%') ");
            }
            if (CONTRACT_TYPE_NAME!="")
            {
                sql.AppendLine($@" AND CONTRACT_TYPE_NAME = N'{CONTRACT_TYPE_NAME}' ");
            }
            var wb = new WhereBuilder(sql.ToString());
            return wb;
        }
    }
}
