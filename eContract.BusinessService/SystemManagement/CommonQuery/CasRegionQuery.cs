using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Entity;
using eContract.Common.Schema;

namespace eContract.BusinessService.SystemManagement.CommonQuery
{
    public class CasRegionQuery : CasRegionEntity, IQuery
    {
        public string keyWord;
        public WhereBuilder ParseSQL()
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("select T1.*,T2." + CasUserTable.C_ENGLISH_NAME + " from " + CasRegionTable.C_TableName + " T1 ");
            strsql.Append(" left join " + CasUserTable.C_TableName + " T2 ");
            strsql.Append(" on T1." + CasRegionTable.C_REGION_MANAGER + " = T2." + CasUserTable.C_USER_ID + " ");
            WhereBuilder wb = new WhereBuilder(strsql.ToString());
            if (!string.IsNullOrEmpty(keyWord))
            {
                wb.AddORCondition(CasRegionTable.C_REGION_CODE, "like", keyWord);
                wb.AddORCondition(CasRegionTable.C_REGION_NAME, "like", keyWord);
            }
            return wb;
        }
    }
}
