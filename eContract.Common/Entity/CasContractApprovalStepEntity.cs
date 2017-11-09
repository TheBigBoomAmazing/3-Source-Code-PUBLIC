using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;

namespace eContract.Common.Entity
{
    [Serializable]
    public partial class CasContractApprovalStepEntity : EntityBase
    {
        public CasContractApprovalStepTable TableSchema
        {
            get
            {
                return CasContractApprovalStepTable.Current;
            }
        }


        public CasContractApprovalStepEntity()
        {
            IsDeleted = false;
        }

        public override TableInfo OringTableSchema
        {
            get
            {
                return CasContractApprovalStepTable.Current;
            }
        }
        #region 属性列表

        public string ContractApprovalStepId
        {
            get { return (string)GetData(CasContractApprovalStepTable.C_CONTRACT_APPROVAL_STEP_ID); }
            set { SetData(CasContractApprovalStepTable.C_CONTRACT_APPROVAL_STEP_ID, value); }
        }

        public string Company
        {
            get { return (string)GetData(CasContractApprovalStepTable.C_COMPANY); }
            set { SetData(CasContractApprovalStepTable.C_COMPANY, value); }
        }

        public string ContractTypeId
        {
            get { return (string)GetData(CasContractApprovalStepTable.C_CONTRACT_TYPE_ID); }
            set { SetData(CasContractApprovalStepTable.C_CONTRACT_TYPE_ID, value); }
        }

        public int? Step
        {
            get { return (int?)(GetData(CasContractApprovalStepTable.C_STEP)); }
            set { SetData(CasContractApprovalStepTable.C_STEP, value); }
        }

        public int? ApprovalRole
        {
            get { return (int?)(GetData(CasContractApprovalStepTable.C_APPROVAL_ROLE)); }
            set { SetData(CasContractApprovalStepTable.C_APPROVAL_ROLE, value); }
        }

        public bool? NeedRegionManager
        {
            get { return (bool?)(GetData(CasContractApprovalStepTable.C_NEED_REGION_MANAGER)); }
            set { SetData(CasContractApprovalStepTable.C_NEED_REGION_MANAGER, value); }
        }

        public bool? NeedDeptManager
        {
            get { return (bool?)(GetData(CasContractApprovalStepTable.C_NEED_DEPT_MANAGER)); }
            set { SetData(CasContractApprovalStepTable.C_NEED_DEPT_MANAGER, value); }
        }

        public bool? NeedLineManager
        {
            get { return (bool?)(GetData(CasContractApprovalStepTable.C_NEED_LINE_MANAGER)); }
            set { SetData(CasContractApprovalStepTable.C_NEED_LINE_MANAGER, value); }
        }

        public int? ApprovalTime
        {
            get { return (int?)(GetData(CasContractApprovalStepTable.C_APPROVAL_TIME)); }
            set { SetData(CasContractApprovalStepTable.C_APPROVAL_TIME, value); }
        }

        public bool? IsDeleted
        {
            get { return (bool?)(GetData(CasContractApprovalStepTable.C_IS_DELETED)); }
            set { SetData(CasContractApprovalStepTable.C_IS_DELETED, value); }
        }

        public string CreatedBy
        {
            get { return (string)GetData(CasContractApprovalStepTable.C_CREATED_BY); }
            set { SetData(CasContractApprovalStepTable.C_CREATED_BY, value); }
        }

        public DateTime? CreateTime
        {
            get { return (DateTime?)(GetData(CasContractApprovalStepTable.C_CREATE_TIME)); }
            set { SetData(CasContractApprovalStepTable.C_CREATE_TIME, value); }
        }

        public string LastModifiedBy
        {
            get { return (string)GetData(CasContractApprovalStepTable.C_LAST_MODIFIED_BY); }
            set { SetData(CasContractApprovalStepTable.C_LAST_MODIFIED_BY, value); }
        }

        public DateTime? LastModifiedTime
        {
            get { return (DateTime?)(GetData(CasContractApprovalStepTable.C_LAST_MODIFIED_TIME)); }
            set { SetData(CasContractApprovalStepTable.C_LAST_MODIFIED_TIME, value); }
        }
        public string ApprovalDepValue
        {
            get;
            set;
        }
        public string ApprovalUserValue
        {
            get;
            set;
        }

        public string ExaminationValue
        {
            get;
            set;
        }
        #endregion
    }
}
