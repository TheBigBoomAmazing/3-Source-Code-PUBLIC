using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;

namespace eContract.Common.Entity
{
[Serializable]
public partial class CasContractApprovalStepApplyDeptEntity : EntityBase
{
public CasContractApprovalStepApplyDeptTable TableSchema
{
get
{
return CasContractApprovalStepApplyDeptTable.Current;
}
}


public CasContractApprovalStepApplyDeptEntity()
{
            IsDeleted = false;
}

public override TableInfo OringTableSchema
{
get
{
return CasContractApprovalStepApplyDeptTable.Current;
}
}
#region 属性列表

public string ContractApprovalStepApplyDeptId
{
get { return (string)GetData(CasContractApprovalStepApplyDeptTable.C_CONTRACT_APPROVAL_STEP_APPLY_DEPT_ID); }
set { SetData(CasContractApprovalStepApplyDeptTable.C_CONTRACT_APPROVAL_STEP_APPLY_DEPT_ID, value); }
}

public string ContractApprovalStepId
{
get { return (string)GetData(CasContractApprovalStepApplyDeptTable.C_CONTRACT_APPROVAL_STEP_ID); }
set { SetData(CasContractApprovalStepApplyDeptTable.C_CONTRACT_APPROVAL_STEP_ID, value); }
}

public int ApplyType
{
get { return (int)(GetData(CasContractApprovalStepApplyDeptTable.C_APPLY_TYPE)); }
set { SetData(CasContractApprovalStepApplyDeptTable.C_APPLY_TYPE, value); }
}

public string DeptId
{
get { return (string)GetData(CasContractApprovalStepApplyDeptTable.C_DEPT_ID); }
set { SetData(CasContractApprovalStepApplyDeptTable.C_DEPT_ID, value); }
}

public bool IsDeleted
{
get { return (bool)(GetData(CasContractApprovalStepApplyDeptTable.C_IS_DELETED)); }
set { SetData(CasContractApprovalStepApplyDeptTable.C_IS_DELETED, value); }
}

public string CreatedBy
{
get { return (string)GetData(CasContractApprovalStepApplyDeptTable.C_CREATED_BY); }
set { SetData(CasContractApprovalStepApplyDeptTable.C_CREATED_BY, value); }
}

public DateTime CreateTime
{
get { return (DateTime)(GetData(CasContractApprovalStepApplyDeptTable.C_CREATE_TIME)); }
set { SetData(CasContractApprovalStepApplyDeptTable.C_CREATE_TIME, value); }
}

public string LastModifiedBy
{
get { return (string)GetData(CasContractApprovalStepApplyDeptTable.C_LAST_MODIFIED_BY); }
set { SetData(CasContractApprovalStepApplyDeptTable.C_LAST_MODIFIED_BY, value); }
}

public DateTime LastModifiedTime
{
get { return (DateTime)(GetData(CasContractApprovalStepApplyDeptTable.C_LAST_MODIFIED_TIME)); }
set { SetData(CasContractApprovalStepApplyDeptTable.C_LAST_MODIFIED_TIME, value); }
}

#endregion
}
}
