using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common;
using eContract.Common.Entity;

namespace eContract.BusinessService.BusinessData.CommonQuery
{
    public class ContractTemplateAttachmentQuery:IQuery
    {
        public string id;
        public WhereBuilder ParseSQL()
        {
            var sql = new StringBuilder();
            sql.AppendFormat(" SELECT * FROM CAS_ATTACHMENT WHERE 1=1 ");
            var wb = new WhereBuilder(sql.ToString());
            return wb;
        }
    }
}
