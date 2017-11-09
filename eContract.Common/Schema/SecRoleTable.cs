using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;

namespace eContract.Common.Schema
{
[Serializable]
public partial class SecRoleTable : TableInfo
{
public const string C_TableName = "SEC_ROLE";

public const string C_ROLE_ID = "ROLE_ID";

public const string C_PARENT_ID = "PARENT_ID";

public const string C_ROLE_NAME = "ROLE_NAME";

public const string C_ROLE_TYPE = "ROLE_TYPE";

public const string C_IS_VALID = "IS_VALID";

public const string C_SYSTEM_NAME = "SYSTEM_NAME";

public const string C_REMARK = "REMARK";

public const string C_IS_DELETED = "IS_DELETED";

public const string C_CREATED_BY = "CREATED_BY";

public const string C_CREATE_TIME = "CREATE_TIME";

public const string C_LAST_MODIFIED_BY = "LAST_MODIFIED_BY";

public const string C_LAST_MODIFIED_TIME = "LAST_MODIFIED_TIME";


public SecRoleTable()
{
_tableName = "SEC_ROLE";
}

protected static SecRoleTable _current;
public static SecRoleTable Current
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
_current = new SecRoleTable();

_current.Add(C_ROLE_ID, new ColumnInfo(C_ROLE_ID, "role_id", true, typeof(string)));

_current.Add(C_PARENT_ID, new ColumnInfo(C_PARENT_ID, "parent_id", false, typeof(string)));

_current.Add(C_ROLE_NAME, new ColumnInfo(C_ROLE_NAME, "role_name", false, typeof(string)));

_current.Add(C_ROLE_TYPE, new ColumnInfo(C_ROLE_TYPE, "role_type", false, typeof(int)));

_current.Add(C_IS_VALID, new ColumnInfo(C_IS_VALID, "is_valid", false, typeof(bool)));

_current.Add(C_SYSTEM_NAME, new ColumnInfo(C_SYSTEM_NAME, "system_name", false, typeof(string)));

_current.Add(C_REMARK, new ColumnInfo(C_REMARK, "remark", false, typeof(string)));

_current.Add(C_IS_DELETED, new ColumnInfo(C_IS_DELETED, "is_deleted", false, typeof(bool)));

_current.Add(C_CREATED_BY, new ColumnInfo(C_CREATED_BY, "created_by", false, typeof(string)));

_current.Add(C_CREATE_TIME, new ColumnInfo(C_CREATE_TIME, "create_time", false, typeof(DateTime)));

_current.Add(C_LAST_MODIFIED_BY, new ColumnInfo(C_LAST_MODIFIED_BY, "last_modified_by", false, typeof(string)));

_current.Add(C_LAST_MODIFIED_TIME, new ColumnInfo(C_LAST_MODIFIED_TIME, "last_modified_time", false, typeof(DateTime)));

}


public ColumnInfo ROLE_ID
{
get { return this[C_ROLE_ID]; }
}

public ColumnInfo PARENT_ID
{
get { return this[C_PARENT_ID]; }
}

public ColumnInfo ROLE_NAME
{
get { return this[C_ROLE_NAME]; }
}

public ColumnInfo ROLE_TYPE
{
get { return this[C_ROLE_TYPE]; }
}

public ColumnInfo IS_VALID
{
get { return this[C_IS_VALID]; }
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
