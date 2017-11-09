using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;

namespace eContract.Common.Entity
{
    [Serializable]
    public partial class CasContractApprovalStepApprovalDeptEntity : EntityBase
    {
        public CasContractApprovalStepApprovalDeptTable TableSchema
        {
            get
            {
                return CasContractApprovalStepApprovalDeptTable.Current;
            }
        }


        public CasContractApprovalStepApprovalDeptEntity()
        {
            IsDeleted = false;
        }

        public override TableInfo OringTableSchema
        {
            get
            {
                return CasContractApprovalStepApprovalDeptTable.Current;
            }
        }
        #region 属性列表

        public string ContractApprovalStepApprovalDeptId
        {
            get { return (string)GetData(CasContractApprovalStepApprovalDeptTable.C_CONTRACT_APPROVAL_STEP_APPROVAL_DEPT_ID); }
            set { SetData(CasContractApprovalStepApprovalDeptTable.C_CONTRACT_APPROVAL_STEP_APPROVAL_DEPT_ID, value); }
        }

        public string ContractApprovalStepId
        {
            get { return (string)GetData(CasContractApprovalStepApprovalDeptTable.C_CONTRACT_APPROVAL_STEP_ID); }
            set { SetData(CasContractApprovalStepApprovalDeptTable.C_CONTRACT_APPROVAL_STEP_ID, value); }
        }

        public string DeptId
        {
            get { return (string)GetData(CasContractApprovalStepApprovalDeptTable.C_DEPT_ID); }
            set { SetData(CasContractApprovalStepApprovalDeptTable.C_DEPT_ID, value); }
        }

        public bool IsDeleted
        {
            get { return (bool)(GetData(CasContractApprovalStepApprovalDeptTable.C_IS_DELETED)); }
            set { SetData(CasContractApprovalStepApprovalDeptTable.C_IS_DELETED, value); }
        }

        public string CreatedBy
        {
            get { return (string)GetData(CasContractApprovalStepApprovalDeptTable.C_CREATED_BY); }
            set { SetData(CasContractApprovalStepApprovalDeptTable.C_CREATED_BY, value); }
        }

        public DateTime CreateTime
        {
            get { return (DateTime)(GetData(CasContractApprovalStepApprovalDeptTable.C_CREATE_TIME)); }
            set { SetData(CasContractApprovalStepApprovalDeptTable.C_CREATE_TIME, value); }
        }

        public string LastModifiedBy
        {
            get { return (string)GetData(CasContractApprovalStepApprovalDeptTable.C_LAST_MODIFIED_BY); }
            set { SetData(CasContractApprovalStepApprovalDeptTable.C_LAST_MODIFIED_BY, value); }
        }

        public DateTime LastModifiedTime
        {
            get { return (DateTime)(GetData(CasContractApprovalStepApprovalDeptTable.C_LAST_MODIFIED_TIME)); }
            set { SetData(CasContractApprovalStepApprovalDeptTable.C_LAST_MODIFIED_TIME, value); }
        }

        #endregion
    }
}
