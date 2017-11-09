using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;

namespace eContract.Common.Entity
{
    [Serializable]
    public partial class CasContractApproverEntity : EntityBase
    {
        public CasContractApproverTable TableSchema
        {
            get
            {
                return CasContractApproverTable.Current;
            }
        }


        public CasContractApproverEntity()
        {
            IsDeleted = false;
        }

        public override TableInfo OringTableSchema
        {
            get
            {
                return CasContractApproverTable.Current;
            }
        }
        #region 属性列表

        public string ContractApproverId
        {
            get { return (string)GetData(CasContractApproverTable.C_CONTRACT_APPROVER_ID); }
            set { SetData(CasContractApproverTable.C_CONTRACT_APPROVER_ID, value); }
        }

        public string ContractId
        {
            get { return (string)GetData(CasContractApproverTable.C_CONTRACT_ID); }
            set { SetData(CasContractApproverTable.C_CONTRACT_ID, value); }
        }

        public string ContractApprovalStepId
        {
            get { return (string)GetData(CasContractApproverTable.C_CONTRACT_APPROVAL_STEP_ID); }
            set { SetData(CasContractApproverTable.C_CONTRACT_APPROVAL_STEP_ID, value); }
        }

        public string ApproverId
        {
            get { return (string)GetData(CasContractApproverTable.C_APPROVER_ID); }
            set { SetData(CasContractApproverTable.C_APPROVER_ID, value); }
        }

        public int ApproverType
        {
            get { return (int)(GetData(CasContractApproverTable.C_APPROVER_TYPE)); }
            set { SetData(CasContractApproverTable.C_APPROVER_TYPE, value); }
        }

        public int Status
        {
            get { return (int)(GetData(CasContractApproverTable.C_STATUS)); }
            set { SetData(CasContractApproverTable.C_STATUS, value); }
        }

        public bool IsDeleted
        {
            get { return (bool)(GetData(CasContractApproverTable.C_IS_DELETED)); }
            set { SetData(CasContractApproverTable.C_IS_DELETED, value); }
        }

        public string CreatedBy
        {
            get { return (string)GetData(CasContractApproverTable.C_CREATED_BY); }
            set { SetData(CasContractApproverTable.C_CREATED_BY, value); }
        }

        public DateTime CreateTime
        {
            get { return (DateTime)(GetData(CasContractApproverTable.C_CREATE_TIME)); }
            set { SetData(CasContractApproverTable.C_CREATE_TIME, value); }
        }

        public string LastModifiedBy
        {
            get { return (string)GetData(CasContractApproverTable.C_LAST_MODIFIED_BY); }
            set { SetData(CasContractApproverTable.C_LAST_MODIFIED_BY, value); }
        }

        public DateTime LastModifiedTime
        {
            get { return (DateTime)(GetData(CasContractApproverTable.C_LAST_MODIFIED_TIME)); }
            set { SetData(CasContractApproverTable.C_LAST_MODIFIED_TIME, value); }
        }

        #endregion
    }
}
