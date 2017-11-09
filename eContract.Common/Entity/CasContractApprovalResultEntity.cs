using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;

namespace eContract.Common.Entity
{
    [Serializable]
    public partial class CasContractApprovalResultEntity : EntityBase
    {
        public CasContractApprovalResultTable TableSchema
        {
            get
            {
                return CasContractApprovalResultTable.Current;
            }
        }


        public CasContractApprovalResultEntity()
        {
            IsDeleted = false;
        }

        public override TableInfo OringTableSchema
        {
            get
            {
                return CasContractApprovalResultTable.Current;
            }
        }
        #region 属性列表

        public string ContractApprovalResultId
        {
            get { return (string)GetData(CasContractApprovalResultTable.C_CONTRACT_APPROVAL_RESULT_ID); }
            set { SetData(CasContractApprovalResultTable.C_CONTRACT_APPROVAL_RESULT_ID, value); }
        }

        public string ContractId
        {
            get { return (string)GetData(CasContractApprovalResultTable.C_CONTRACT_ID); }
            set { SetData(CasContractApprovalResultTable.C_CONTRACT_ID, value); }
        }

        public string DeptId
        {
            get { return (string)GetData(CasContractApprovalResultTable.C_DEPT_ID); }
            set { SetData(CasContractApprovalResultTable.C_DEPT_ID, value); }
        }

        public int ApproverType
        {
            get { return (int)(GetData(CasContractApprovalResultTable.C_APPROVER_TYPE)); }
            set { SetData(CasContractApprovalResultTable.C_APPROVER_TYPE, value); }
        }

        public string ApproverId
        {
            get { return (string)GetData(CasContractApprovalResultTable.C_APPROVER_ID); }
            set { SetData(CasContractApprovalResultTable.C_APPROVER_ID, value); }
        }

        public string ContractApprovalStepId
        {
            get { return (string)GetData(CasContractApprovalResultTable.C_CONTRACT_APPROVAL_STEP_ID); }
            set { SetData(CasContractApprovalResultTable.C_CONTRACT_APPROVAL_STEP_ID, value); }
        }


        public DateTime? ApprovalTime
        {
            get { return (DateTime?)(GetData(CasContractApprovalResultTable.C_APPROVAL_TIME)); }
            set { SetData(CasContractApprovalResultTable.C_APPROVAL_TIME, value); }
        }

        public int ApprovalResult
        {
            get { return (int)(GetData(CasContractApprovalResultTable.C_APPROVAL_RESULT)); }
            set { SetData(CasContractApprovalResultTable.C_APPROVAL_RESULT, value); }
        }

        public string ApprovalOpinions
        {
            get { return (string)GetData(CasContractApprovalResultTable.C_APPROVAL_OPINIONS); }
            set { SetData(CasContractApprovalResultTable.C_APPROVAL_OPINIONS, value); }
        }

        public bool IsDeleted
        {
            get { return (bool)(GetData(CasContractApprovalResultTable.C_IS_DELETED)); }
            set { SetData(CasContractApprovalResultTable.C_IS_DELETED, value); }
        }

        public string CreatedBy
        {
            get { return (string)GetData(CasContractApprovalResultTable.C_CREATED_BY); }
            set { SetData(CasContractApprovalResultTable.C_CREATED_BY, value); }
        }

        public DateTime? CreateTime
        {
            get { return (DateTime?)(GetData(CasContractApprovalResultTable.C_CREATE_TIME)); }
            set { SetData(CasContractApprovalResultTable.C_CREATE_TIME, value); }
        }

        public string LastModifiedBy
        {
            get { return (string)GetData(CasContractApprovalResultTable.C_LAST_MODIFIED_BY); }
            set { SetData(CasContractApprovalResultTable.C_LAST_MODIFIED_BY, value); }
        }

        public DateTime? LastModifiedTime
        {
            get { return (DateTime?)(GetData(CasContractApprovalResultTable.C_LAST_MODIFIED_TIME)); }
            set { SetData(CasContractApprovalResultTable.C_LAST_MODIFIED_TIME, value); }
        }

        public string ContractApproverId
        {
            get { return (string)GetData(CasContractApprovalResultTable.C_CONTRACT_APPROVER_ID); }
            set { SetData(CasContractApprovalResultTable.C_CONTRACT_APPROVER_ID, value); }
        }
        #endregion
    }
}
