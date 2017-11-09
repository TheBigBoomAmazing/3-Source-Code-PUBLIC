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
    public class ContractCommentQuery : IQuery
    {
        public WhereBuilder ParseSQL()
        {
            var sql = new StringBuilder();
            UserDomain userDomain = (UserDomain)WebCaching.CurrentUserDomain;
            sql.AppendLine($@"SELECT t1.*,t2.STATUS as APPROVE_STATUS,t2.CONTRACT_APPROVER_ID FROM dbo.CAS_CONTRACT t1
                            JOIN dbo.CAS_CONTRACT_APPROVER t2 ON t1.CONTRACT_ID = t2.CONTRACT_ID
                            WHERE 1 = 1 AND t2.APPROVER_TYPE = 1 AND t2.STATUS = 2 AND t1.STATUS IN (2,8)");
            sql.AppendLine($@" AND t2.APPROVER_ID = '{userDomain.CasUserEntity.UserId}'");
            var wb = new WhereBuilder(sql.ToString());
            return wb;
        }
    }
}
