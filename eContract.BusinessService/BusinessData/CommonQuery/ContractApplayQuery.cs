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
    public class ContractApplayQuery : IQuery
    {
        public WhereBuilder ParseSQL()
        {
            var sql = new StringBuilder();
            sql.AppendLine($@"SELECT * FROM CAS_CONTRACT WHERE 1 = 1 AND CONTRACT_GROUP = {ContractGroupEnum.NormalContract.GetHashCode()} AND STATUS IN (2,4,8) ");
            UserDomain userDomain = (UserDomain)WebCaching.CurrentUserDomain;
            if (!(userDomain.CasUserEntity.IsAdmin))
            {
                sql.AppendLine($@" AND CREATED_BY = '{userDomain.CasUserEntity.UserId}'");
            }
            var wb = new WhereBuilder(sql.ToString());
            return wb;
        }
    }
}
