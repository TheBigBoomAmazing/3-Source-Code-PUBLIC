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
    public class ProxyApprovalQuery : IQuery
    {
        public string keyword;
        public string startdate;
        public WhereBuilder ParseSQL()
        {
            var sql = new StringBuilder();
            sql.AppendLine($@"SELECT CAS_PROXY_APPROVAL.*
                                     ,AUTHORIZED_USER.ENGLISH_NAME AUTHORIZED_USER_NAME
                                     ,AGENT_USER.ENGLISH_NAME AGENT_USER_NAME
                                FROM CAS_PROXY_APPROVAL
                          INNER JOIN CAS_USER AUTHORIZED_USER
                                  ON CAS_PROXY_APPROVAL.AUTHORIZED_USER_ID = AUTHORIZED_USER.USER_ID
                          INNER JOIN CAS_USER AGENT_USER
                                  ON CAS_PROXY_APPROVAL.AGENT_USER_ID = AGENT_USER.USER_ID
                               WHERE 1 = 1  ");
            if (WebCaching.IsAdmin != "True")
            {
                sql.AppendLine($@" AND CAS_PROXY_APPROVAL.AUTHORIZED_USER_ID = '{WebCaching.UserId}' ");
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                sql.AppendLine(" AND (AGENT_USER.CHINESE_NAME LIKE N'%" + keyword + "%' OR AGENT_USER.ENGLISH_NAME LIKE N'%" + keyword + "%') ");
            }
            if (!string.IsNullOrEmpty(startdate))
            {
                sql.AppendLine(" AND CAS_PROXY_APPROVAL.BEGIN_TIME >='" + startdate + "' ");
            }
            var wb = new WhereBuilder(sql.ToString());
            return wb;
        }
    }
}
