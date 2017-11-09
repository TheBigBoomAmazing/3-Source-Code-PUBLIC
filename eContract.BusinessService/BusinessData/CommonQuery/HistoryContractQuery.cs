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
    public class HistoryContractQuery : IQuery
    {
        public string CONTRACT_TYPE_NAME;
        public WhereBuilder ParseSQL()
        {
            var sql = new StringBuilder();
            sql.AppendLine($@"SELECT * FROM CAS_CONTRACT WHERE 1 = 1 AND CONTRACT_GROUP = {ContractGroupEnum.HistoryContract.GetHashCode()} ");
            UserDomain userDomain = (UserDomain)WebCaching.CurrentUserDomain;
            if (!(userDomain.CasUserEntity.IsAdmin))
            {
                sql.AppendLine($@" AND CREATED_BY = '{userDomain.CasUserEntity.UserId}'");
            }
            if (CONTRACT_TYPE_NAME!="")
            {
                sql.AppendLine($@" AND CONTRACT_TYPE_NAME = N'{CONTRACT_TYPE_NAME}'");
            }
            var wb = new WhereBuilder(sql.ToString());
            return wb;
        }
    }
}
