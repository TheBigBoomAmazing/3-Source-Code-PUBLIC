using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Entity;
using eContract.Common.Schema;

namespace eContract.BusinessService.BusinessData.CommonQuery
{
    public class ContractApprovalSetQuery :  IQuery
    {
        public string keyWord;
        public WhereBuilder ParseSQL()
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("SELECT  T1.*,T2.CONTRACT_TYPE_NAME FROM  CAS_CONTRACT_APPROVAL_STEP T1 ");
            strsql.Append(" INNER JOIN  CAS_CONTRACT_TYPE T2 ");
            strsql.Append(" ON  T1.CONTRACT_TYPE_ID = T2.CONTRACT_TYPE_ID  ");
            WhereBuilder wb = new WhereBuilder(strsql.ToString());
            if (!string.IsNullOrEmpty(keyWord))
            {
                wb.AddAndCondition("CONTRACT_TYPE_NAME", "=", keyWord);
            }

            //if (!string.IsNullOrEmpty(keyWord))
            //{
            //    strsql.AppendLine($@" AND T2.CONTRACT_TYPE_NAME LIKE N'%{keyWord}%'");
            //}
            return wb;
        }
    }
}
