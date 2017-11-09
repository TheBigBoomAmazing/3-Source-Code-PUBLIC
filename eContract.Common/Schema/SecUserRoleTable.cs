using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;

namespace eContract.Common.Schema
{
[Serializable]
public partial class SecUserRoleTable : TableInfo
{
public const string C_TableName = "SEC_USER_ROLE";

public const string C_USER_ROLE_ID = "USER_ROLE_ID";

public const string C_USER_ID = "USER_ID";

public const string C_ROLE_ID = "ROLE_ID";

public const string C_SYSTEM_NAME = "SYSTEM_NAME";

public const string C_REMARK = "REMARK";

public const string C_IS_DELETED = "IS_DELETED";

public const string C_CREATED_BY = "CREATED_BY";

public const string C_CREATE_TIME = "CREATE_TIME";

public const string C_LAST_MODIFIED_BY = "LAST_MODIFIED_BY";

public const string C_LAST_MODIFIED_TIME = "LAST_MODIFIED_TIME";


public SecUserRoleTable()
{
_tableName = "SEC_USER_ROLE";
}

protected static SecUserRoleTable _current;
public static SecUserRoleTable Current
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
_current = new SecUserRoleTable();

_current.Add(C_USER_ROLE_ID, new ColumnInfo(C_USER_ROLE_ID, "user_role_id", true, typeof(string)));

_current.Add(C_USER_ID, new ColumnInfo(C_USER_ID, "user_id", false, typeof(string)));

_current.Add(C_ROLE_ID, new ColumnInfo(C_ROLE_ID, "role_id", false, typeof(string)));

_current.Add(C_SYSTEM_NAME, new ColumnInfo(C_SYSTEM_NAME, "system_name", false, typeof(string)));

_current.Add(C_REMARK, new ColumnInfo(C_REMARK, "remark", false, typeof(string)));

_current.Add(C_IS_DELETED, new ColumnInfo(C_IS_DELETED, "is_deleted", false, typeof(bool)));

_current.Add(C_CREATED_BY, new ColumnInfo(C_CREATED_BY, "created_by", false, typeof(string)));

_current.Add(C_CREATE_TIME, new ColumnInfo(C_CREATE_TIME, "create_time", false, typeof(DateTime)));

_current.Add(C_LAST_MODIFIED_BY, new ColumnInfo(C_LAST_MODIFIED_BY, "last_modified_by", false, typeof(string)));

_current.Add(C_LAST_MODIFIED_TIME, new ColumnInfo(C_LAST_MODIFIED_TIME, "last_modified_time", false, typeof(DateTime)));

}


public ColumnInfo USER_ROLE_ID
{
get { return this[C_USER_ROLE_ID]; }
}

public ColumnInfo USER_ID
{
get { return this[C_USER_ID]; }
}

public ColumnInfo ROLE_ID
{
get { return this[C_ROLE_ID]; }
}

public ColumnInfo SYSTEM_NAME
{
get { return this[C_SYSTEM_NAME]; }
}

public ColumnInfo REMARK
{
get { return this[C_REMARK]; }
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
