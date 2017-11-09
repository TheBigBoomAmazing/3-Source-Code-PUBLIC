using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;

namespace eContract.Common.Schema
{
[Serializable]
public partial class SecLogErrorTable : TableInfo
{
public const string C_TableName = "SEC_LOG_ERROR";

public const string C_LOG_ERROR_ID = "LOG_ERROR_ID";

public const string C_MESSAGE = "MESSAGE";

public const string C_STACK_TRACE = "STACK_TRACE";

public const string C_MACHINE_NAME = "MACHINE_NAME";

public const string C_IP = "IP";

public const string C_LOG_TIME = "LOG_TIME";

public const string C_PAGENAME = "PAGENAME";

public const string C_IS_DELETED = "IS_DELETED";

public const string C_CREATED_BY = "CREATED_BY";

public const string C_CREATE_TIME = "CREATE_TIME";

public const string C_LAST_MODIFIED_BY = "LAST_MODIFIED_BY";

public const string C_LAST_MODIFIED_TIME = "LAST_MODIFIED_TIME";


public SecLogErrorTable()
{
_tableName = "SEC_LOG_ERROR";
}

protected static SecLogErrorTable _current;
public static SecLogErrorTable Current
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
_current = new SecLogErrorTable();

_current.Add(C_LOG_ERROR_ID, new ColumnInfo(C_LOG_ERROR_ID, "log_error_id", true, typeof(string)));

_current.Add(C_MESSAGE, new ColumnInfo(C_MESSAGE, "message", false, typeof(string)));

_current.Add(C_STACK_TRACE, new ColumnInfo(C_STACK_TRACE, "stack_trace", false, typeof(string)));

_current.Add(C_MACHINE_NAME, new ColumnInfo(C_MACHINE_NAME, "machine_name", false, typeof(string)));

_current.Add(C_IP, new ColumnInfo(C_IP, "ip", false, typeof(string)));

_current.Add(C_LOG_TIME, new ColumnInfo(C_LOG_TIME, "log_time", false, typeof(DateTime)));

_current.Add(C_PAGENAME, new ColumnInfo(C_PAGENAME, "pagename", false, typeof(string)));

_current.Add(C_IS_DELETED, new ColumnInfo(C_IS_DELETED, "is_deleted", false, typeof(bool)));

_current.Add(C_CREATED_BY, new ColumnInfo(C_CREATED_BY, "created_by", false, typeof(string)));

_current.Add(C_CREATE_TIME, new ColumnInfo(C_CREATE_TIME, "create_time", false, typeof(DateTime)));

_current.Add(C_LAST_MODIFIED_BY, new ColumnInfo(C_LAST_MODIFIED_BY, "last_modified_by", false, typeof(string)));

_current.Add(C_LAST_MODIFIED_TIME, new ColumnInfo(C_LAST_MODIFIED_TIME, "last_modified_time", false, typeof(DateTime)));

}


public ColumnInfo LOG_ERROR_ID
{
get { return this[C_LOG_ERROR_ID]; }
}

public ColumnInfo MESSAGE
{
get { return this[C_MESSAGE]; }
}

public ColumnInfo STACK_TRACE
{
get { return this[C_STACK_TRACE]; }
}

public ColumnInfo MACHINE_NAME
{
get { return this[C_MACHINE_NAME]; }
}

public ColumnInfo IP
{
get { return this[C_IP]; }
}

public ColumnInfo LOG_TIME
{
get { return this[C_LOG_TIME]; }
}

public ColumnInfo PAGENAME
{
get { return this[C_PAGENAME]; }
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
