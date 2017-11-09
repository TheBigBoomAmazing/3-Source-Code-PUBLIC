using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;

namespace eContract.Common.Schema
{
[Serializable]
public partial class CasPoApprovalSettingsTable : TableInfo
{
public const string C_TableName = "CAS_PO_APPROVAL_SETTINGS";

public const string C_PO_APPROVAL_ID = "PO_APPROVAL_ID";

public const string C_COMPANY = "COMPANY";

public const string C_CONTRACT_TYPE_ID = "CONTRACT_TYPE_ID";

public const string C_USER_ID = "USER_ID";

public const string C_IS_DELETED = "IS_DELETED";

public const string C_CREATED_BY = "CREATED_BY";

public const string C_CREATE_TIME = "CREATE_TIME";

public const string C_LAST_MODIFIED_BY = "LAST_MODIFIED_BY";

public const string C_LAST_MODIFIED_TIME = "LAST_MODIFIED_TIME";


public CasPoApprovalSettingsTable()
{
_tableName = "CAS_PO_APPROVAL_SETTINGS";
}

protected static CasPoApprovalSettingsTable _current;
public static CasPoApprovalSettingsTable Current
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
_current = new CasPoApprovalSettingsTable();

_current.Add(C_PO_APPROVAL_ID, new ColumnInfo(C_PO_APPROVAL_ID, "po_approval_id", true, typeof(string)));

_current.Add(C_COMPANY, new ColumnInfo(C_COMPANY, "company", false, typeof(string)));

_current.Add(C_CONTRACT_TYPE_ID, new ColumnInfo(C_CONTRACT_TYPE_ID, "contract_type_id", false, typeof(string)));

_current.Add(C_USER_ID, new ColumnInfo(C_USER_ID, "user_id", false, typeof(string)));

_current.Add(C_IS_DELETED, new ColumnInfo(C_IS_DELETED, "is_deleted", false, typeof(bool)));

_current.Add(C_CREATED_BY, new ColumnInfo(C_CREATED_BY, "created_by", false, typeof(string)));

_current.Add(C_CREATE_TIME, new ColumnInfo(C_CREATE_TIME, "create_time", false, typeof(DateTime)));

_current.Add(C_LAST_MODIFIED_BY, new ColumnInfo(C_LAST_MODIFIED_BY, "last_modified_by", false, typeof(string)));

_current.Add(C_LAST_MODIFIED_TIME, new ColumnInfo(C_LAST_MODIFIED_TIME, "last_modified_time", false, typeof(DateTime)));

}


public ColumnInfo PO_APPROVAL_ID
{
get { return this[C_PO_APPROVAL_ID]; }
}

public ColumnInfo COMPANY
{
get { return this[C_COMPANY]; }
}

public ColumnInfo CONTRACT_TYPE_ID
{
get { return this[C_CONTRACT_TYPE_ID]; }
}

public ColumnInfo USER_ID
{
get { return this[C_USER_ID]; }
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
