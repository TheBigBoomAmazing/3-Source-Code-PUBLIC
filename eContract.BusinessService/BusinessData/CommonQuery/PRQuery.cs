using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common;
using eContract.Common.Entity;

namespace eContract.BusinessService.BusinessData.CommonQuery
{
    public class PRQuery :  IQuery
    {
        public string keyword { get; set; }
        public WhereBuilder ParseSQL()
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM CAS_CONTRACT_FILING WHERE CONTRACT_ID = '" + keyword + "' ");
            var wb = new WhereBuilder(sql.ToString());
            return wb;
        }
    }
}
