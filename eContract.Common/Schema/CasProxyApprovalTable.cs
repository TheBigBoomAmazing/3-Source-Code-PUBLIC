using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;

namespace eContract.Common.Schema
{
[Serializable]
public partial class CasProxyApprovalTable : TableInfo
{
public const string C_TableName = "CAS_PROXY_APPROVAL";

public const string C_PROXY_APPROVAL_ID = "PROXY_APPROVAL_ID";

public const string C_AUTHORIZED_USER_ID = "AUTHORIZED_USER_ID";

public const string C_AGENT_USER_ID = "AGENT_USER_ID";

public const string C_BEGIN_TIME = "BEGIN_TIME";

public const string C_END_TIME = "END_TIME";

public const string C_IS_DELETED = "IS_DELETED";

public const string C_CREATED_BY = "CREATED_BY";

public const string C_CREATE_TIME = "CREATE_TIME";

public const string C_LAST_MODIFIED_BY = "LAST_MODIFIED_BY";

public const string C_LAST_MODIFIED_TIME = "LAST_MODIFIED_TIME";

public const string C_TERMINATION_TIME = "TERMINATION_TIME";


public CasProxyApprovalTable()
{
_tableName = "CAS_PROXY_APPROVAL";
}

protected static CasProxyApprovalTable _current;
public static CasProxyApprovalTable Current
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
_current = new CasProxyApprovalTable();

_current.Add(C_PROXY_APPROVAL_ID, new ColumnInfo(C_PROXY_APPROVAL_ID, "proxy_approval_id", true, typeof(string)));

_current.Add(C_AUTHORIZED_USER_ID, new ColumnInfo(C_AUTHORIZED_USER_ID, "authorized_user_id", false, typeof(string)));

_current.Add(C_AGENT_USER_ID, new ColumnInfo(C_AGENT_USER_ID, "agent_user_id", false, typeof(string)));

_current.Add(C_BEGIN_TIME, new ColumnInfo(C_BEGIN_TIME, "begin_time", false, typeof(DateTime)));

_current.Add(C_END_TIME, new ColumnInfo(C_END_TIME, "end_time", false, typeof(DateTime)));

_current.Add(C_IS_DELETED, new ColumnInfo(C_IS_DELETED, "is_deleted", false, typeof(bool)));

_current.Add(C_CREATED_BY, new ColumnInfo(C_CREATED_BY, "created_by", false, typeof(string)));

_current.Add(C_CREATE_TIME, new ColumnInfo(C_CREATE_TIME, "create_time", false, typeof(DateTime)));

_current.Add(C_LAST_MODIFIED_BY, new ColumnInfo(C_LAST_MODIFIED_BY, "last_modified_by", false, typeof(string)));

_current.Add(C_LAST_MODIFIED_TIME, new ColumnInfo(C_LAST_MODIFIED_TIME, "last_modified_time", false, typeof(DateTime)));

_current.Add(C_TERMINATION_TIME, new ColumnInfo(C_TERMINATION_TIME, "termination_time", false, typeof(DateTime)));

        }


public ColumnInfo PROXY_APPROVAL_ID
        {
get { return this[C_PROXY_APPROVAL_ID]; }
}

public ColumnInfo AUTHORIZED_USER_ID
{
get { return this[C_AUTHORIZED_USER_ID]; }
}

public ColumnInfo AGENT_USER_ID
{
get { return this[C_AGENT_USER_ID]; }
}

public ColumnInfo BEGIN_TIME
{
get { return this[C_BEGIN_TIME]; }
}

public ColumnInfo END_TIME
{
get { return this[C_END_TIME]; }
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

public ColumnInfo TERMINATION_TIME
        {
get { return this[C_TERMINATION_TIME]; }
}

}
}
