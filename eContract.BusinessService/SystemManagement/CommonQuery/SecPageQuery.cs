using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Entity;
using eContract.Common.Schema;
using eContract.Common.MVC;

namespace eContract.BusinessService.SystemManagement.CommonQuery
{
    public class SecPageQuery : SecPageEntity, IQuery
    {
        public string keyWord = "";
        public string systemName = "";
        //public JqGrid grid;
        public WhereBuilder ParseSQL()
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("select T1.* from " + SecPageTable.C_TableName +" T1 ");
            
            WhereBuilder wb = new WhereBuilder(strsql.ToString());
            //if (!string.IsNullOrEmpty(keyWord))
            //{
            //    wb.AddORCondition(SecPageTable.C_PAGE_NAME, "like", "%" + keyWord + "%");
            //    wb.AddORCondition(SecPageTable.C_PAGE_NAME_EN, "like", "%" + keyWord + "%");
            //}
            if (!string.IsNullOrEmpty(systemName))
            {
                wb.AddAndCondition(SecPageTable.C_SYSTEM_NAME, systemName);
            }           
            return wb;
        }
    }
}
