using eContract.BusinessService.Common;
using Suzsoft.Smart.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using eContract.Common.Entity;

namespace eContract.BusinessService.BusinessData.BusinessRule
{
    public  class UploadSignatureStampContractRemind: BusinessBase
    {
        public List<CasContractEntity> GetNeedRemindContracts()
        {
            var sql = $@"SELECT * FROM CAS_CONTRACT
                        WHERE STATUS =3
                        AND DATEDIFF(day,  GETDATE(),dbo.AddWorkDay(LAST_MODIFIED_TIME,10))=10";
            return  DataAccess.Select<CasContractEntity>(sql);
        }
    }
}
