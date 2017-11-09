using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eContract.Common.Entity;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;
using eContract.Common.Const;
using eContract.Common;
using eContract.Common.MVC;
using eContract.Common.WebUtils;

namespace eContract.BusinessService.SystemManagement.CommonQuery
{
    public class QueryManager : CasUserEntity, IQuery
    {
        public LigerGrid ligerGrid;
        public string userId;

        public WhereBuilder ParseSQL()
        {
            string sql = @"WITH MANAGER
                            AS
                            (
                            SELECT * FROM CAS_USER
                            WHERE USER_ID='" + userId + @"'
                            UNION ALL
                            SELECT A.*
                            FROM CAS_USER A JOIN MANAGER B 
                            ON B.LINE_MANAGER_ID = A.USER_CODE
                            )
                            SELECT * FROM MANAGER
                            OPTION(MAXRECURSION 0)";

            WhereBuilder wb = new WhereBuilder(sql);
            return wb;
        }
    }
}
