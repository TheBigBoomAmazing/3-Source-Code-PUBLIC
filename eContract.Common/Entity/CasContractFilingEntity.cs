using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;

namespace eContract.Common.Entity
{
    [Serializable]
    public partial class CasContractFilingEntity : EntityBase
    {
        public CasContractFilingTable TableSchema
        {
            get
            {
                return CasContractFilingTable.Current;
            }
        }


        public CasContractFilingEntity()
        {
            IsDeleted = false;
        }

        public override TableInfo OringTableSchema
        {
            get
            {
                return CasContractFilingTable.Current;
            }
        }
        #region 属性列表

        public string ContractFilingId
        {
            get { return (string)GetData(CasContractFilingTable.C_CONTRACT_FILING_ID); }
            set { SetData(CasContractFilingTable.C_CONTRACT_FILING_ID, value); }
        }

        public string ContractId
        {
            get { return (string)GetData(CasContractFilingTable.C_CONTRACT_ID); }
            set { SetData(CasContractFilingTable.C_CONTRACT_ID, value); }
        }

        public string PrNo
        {
            get { return (string)GetData(CasContractFilingTable.C_PR_NO); }
            set { SetData(CasContractFilingTable.C_PR_NO, value); }
        }

        public string PoNo
        {
            get { return (string)GetData(CasContractFilingTable.C_PO_NO); }
            set { SetData(CasContractFilingTable.C_PO_NO, value); }
        }

        public int Status
        {
            get { return (int)(GetData(CasContractFilingTable.C_STATUS)); }
            set { SetData(CasContractFilingTable.C_STATUS, value); }
        }

        public string Remark
        {
            get { return (string)(GetData(CasContractFilingTable.C_REMARK)); }
            set { SetData(CasContractFilingTable.C_REMARK, value); }
        }

        public bool IsDeleted
        {
            get { return (bool)(GetData(CasContractFilingTable.C_IS_DELETED)); }
            set { SetData(CasContractFilingTable.C_IS_DELETED, value); }
        }

        public string CreatedBy
        {
            get { return (string)GetData(CasContractFilingTable.C_CREATED_BY); }
            set { SetData(CasContractFilingTable.C_CREATED_BY, value); }
        }

        public DateTime CreateTime
        {
            get { return (DateTime)(GetData(CasContractFilingTable.C_CREATE_TIME)); }
            set { SetData(CasContractFilingTable.C_CREATE_TIME, value); }
        }

        public string LastModifiedBy
        {
            get { return (string)GetData(CasContractFilingTable.C_LAST_MODIFIED_BY); }
            set { SetData(CasContractFilingTable.C_LAST_MODIFIED_BY, value); }
        }

        public DateTime LastModifiedTime
        {
            get { return (DateTime)(GetData(CasContractFilingTable.C_LAST_MODIFIED_TIME)); }
            set { SetData(CasContractFilingTable.C_LAST_MODIFIED_TIME, value); }
        }

        #endregion
    }
}
