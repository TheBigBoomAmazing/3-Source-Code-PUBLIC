using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;

namespace eContract.Common.Schema
{
[Serializable]
public partial class CasContractApprovalStepApplyDeptTable : TableInfo
{
public const string C_TableName = "CAS_CONTRACT_APPROVAL_STEP_APPLY_DEPT";

public const string C_CONTRACT_APPROVAL_STEP_APPLY_DEPT_ID = "CONTRACT_APPROVAL_STEP_APPLY_DEPT_ID";

public const string C_CONTRACT_APPROVAL_STEP_ID = "CONTRACT_APPROVAL_STEP_ID";

public const string C_APPLY_TYPE = "APPLY_TYPE";

public const string C_DEPT_ID = "DEPT_ID";

public const string C_IS_DELETED = "IS_DELETED";

public const string C_CREATED_BY = "CREATED_BY";

public const string C_CREATE_TIME = "CREATE_TIME";

public const string C_LAST_MODIFIED_BY = "LAST_MODIFIED_BY";

public const string C_LAST_MODIFIED_TIME = "LAST_MODIFIED_TIME";


public CasContractApprovalStepApplyDeptTable()
{
_tableName = "CAS_CONTRACT_APPROVAL_STEP_APPLY_DEPT";
}

protected static CasContractApprovalStepApplyDeptTable _current;
public static CasContractApprovalStepApplyDeptTable Current
{
get
{
if (_current == null )
{
Initial();
}
return _current;
}
}

private static void Initial()
{
_current = new CasContractApprovalStepApplyDeptTable();

_current.Add(C_CONTRACT_APPROVAL_STEP_APPLY_DEPT_ID, new ColumnInfo(C_CONTRACT_APPROVAL_STEP_APPLY_DEPT_ID, "contract_approval_step_apply_dept_id", true, typeof(string)));

_current.Add(C_CONTRACT_APPROVAL_STEP_ID, new ColumnInfo(C_CONTRACT_APPROVAL_STEP_ID, "contract_approval_step_id", false, typeof(string)));

_current.Add(C_APPLY_TYPE, new ColumnInfo(C_APPLY_TYPE, "apply_type", false, typeof(int)));

_current.Add(C_DEPT_ID, new ColumnInfo(C_DEPT_ID, "dept_id", false, typeof(string)));

_current.Add(C_IS_DELETED, new ColumnInfo(C_IS_DELETED, "is_deleted", false, typeof(bool)));

_current.Add(C_CREATED_BY, new ColumnInfo(C_CREATED_BY, "created_by", false, typeof(string)));

_current.Add(C_CREATE_TIME, new ColumnInfo(C_CREATE_TIME, "create_time", false, typeof(DateTime)));

_current.Add(C_LAST_MODIFIED_BY, new ColumnInfo(C_LAST_MODIFIED_BY, "last_modified_by", false, typeof(string)));

_current.Add(C_LAST_MODIFIED_TIME, new ColumnInfo(C_LAST_MODIFIED_TIME, "last_modified_time", false, typeof(DateTime)));

}


public ColumnInfo CONTRACT_APPROVAL_STEP_APPLY_DEPT_ID
{
get { return this[C_CONTRACT_APPROVAL_STEP_APPLY_DEPT_ID]; }
}

public ColumnInfo CONTRACT_APPROVAL_STEP_ID
{
get { return this[C_CONTRACT_APPROVAL_STEP_ID]; }
}

public ColumnInfo APPLY_TYPE
{
get { return this[C_APPLY_TYPE]; }
}

public ColumnInfo DEPT_ID
{
get { return this[C_DEPT_ID]; }
}

public ColumnInfo IS_DELETED
{
get { return this[C_IS_DELETED]; }
}

public ColumnInfo CREATED_BY
{
get { return this[C_CREATED_BY]; }
}

public ColumnInfo CREATE_TIME
{
get { return this[C_CREATE_TIME]; }
}

public ColumnInfo LAST_MODIFIED_BY
{
get { return this[C_LAST_MODIFIED_BY]; }
}

public ColumnInfo LAST_MODIFIED_TIME
{
get { return this[C_LAST_MODIFIED_TIME]; }
}

}
}
