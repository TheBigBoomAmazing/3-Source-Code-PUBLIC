using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Entity;
using eContract.Common.Schema;
using eContract.Common.WebUtils;

namespace eContract.BusinessService.BusinessData.CommonQuery
{
    public class GetContractApprolvalResQuery: IQuery
    {
        public string keyWord;
        public WhereBuilder ParseSQL()
        {
            var sql = new StringBuilder();
            sql.AppendFormat(" SELECT CAR.APPROVAL_TIME,CAR.DEPT_ID,CUS.ENGLISH_NAME,CAS.STEP, CCT.SUPPLIER,CAR.APPROVAL_RESULT,CAR.APPROVAL_OPINIONS,CAR.APPROVER_TYPE FROM CAS_CONTRACT_APPROVAL_RESULT CAR LEFT JOIN CAS_CONTRACT CCT ON CAR.CONTRACT_ID = CCT.CONTRACT_ID LEFT JOIN dbo.CAS_USER CUS ON CAR.APPROVER_ID=CUS.USER_ID LEFT JOIN dbo.CAS_CONTRACT_APPROVAL_STEP CAS ON cas.CONTRACT_APPROVAL_STEP_ID=CAR.CONTRACT_APPROVAL_STEP_ID WHERE  CAR.CONTRACT_ID='{0}' ", keyWord);
            var wb = new WhereBuilder(sql.ToString());
            return wb;
        }
    }
}
