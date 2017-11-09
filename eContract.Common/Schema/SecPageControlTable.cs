using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;

namespace eContract.Common.Schema
{
[Serializable]
public partial class SecPageControlTable : TableInfo
{
public const string C_TableName = "SEC_PAGE_CONTROL";

public const string C_CONTROL_ID = "CONTROL_ID";

public const string C_PAGE_ID = "PAGE_ID";

public const string C_CONTROL_NAME = "CONTROL_NAME";

public const string C_SERVER_ID = "SERVER_ID";

public const string C_PAGE_TYPE = "PAGE_TYPE";

public const string C_IS_DELETED = "IS_DELETED";

public const string C_CREATED_BY = "CREATED_BY";

public const string C_CREATE_TIME = "CREATE_TIME";

public const string C_LAST_MODIFIED_BY = "LAST_MODIFIED_BY";

public const string C_LAST_MODIFIED_TIME = "LAST_MODIFIED_TIME";


public SecPageControlTable()
{
_tableName = "SEC_PAGE_CONTROL";
}

protected static SecPageControlTable _current;
public static SecPageControlTable Current
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
_current = new SecPageControlTable();

_current.Add(C_CONTROL_ID, new ColumnInfo(C_CONTROL_ID, "control_id", true, typeof(string)));

_current.Add(C_PAGE_ID, new ColumnInfo(C_PAGE_ID, "page_id", false, typeof(string)));

_current.Add(C_CONTROL_NAME, new ColumnInfo(C_CONTROL_NAME, "control_name", false, typeof(string)));

_current.Add(C_SERVER_ID, new ColumnInfo(C_SERVER_ID, "server_id", false, typeof(string)));

_current.Add(C_PAGE_TYPE, new ColumnInfo(C_PAGE_TYPE, "page_type", false, typeof(string)));

_current.Add(C_IS_DELETED, new ColumnInfo(C_IS_DELETED, "is_deleted", false, typeof(bool)));

_current.Add(C_CREATED_BY, new ColumnInfo(C_CREATED_BY, "created_by", false, typeof(string)));

_current.Add(C_CREATE_TIME, new ColumnInfo(C_CREATE_TIME, "create_time", false, typeof(DateTime)));

_current.Add(C_LAST_MODIFIED_BY, new ColumnInfo(C_LAST_MODIFIED_BY, "last_modified_by", false, typeof(string)));

_current.Add(C_LAST_MODIFIED_TIME, new ColumnInfo(C_LAST_MODIFIED_TIME, "last_modified_time", false, typeof(DateTime)));

}


public ColumnInfo CONTROL_ID
{
get { return this[C_CONTROL_ID]; }
}

public ColumnInfo PAGE_ID
{
get { return this[C_PAGE_ID]; }
}

public ColumnInfo CONTROL_NAME
{
get { return this[C_CONTROL_NAME]; }
}

public ColumnInfo SERVER_ID
{
get { return this[C_SERVER_ID]; }
}

public ColumnInfo PAGE_TYPE
{
get { return this[C_PAGE_TYPE]; }
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
