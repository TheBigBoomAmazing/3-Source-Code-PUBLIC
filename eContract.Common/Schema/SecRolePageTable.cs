using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;

namespace eContract.Common.Schema
{
[Serializable]
public partial class SecRolePageTable : TableInfo
{
public const string C_TableName = "SEC_ROLE_PAGE";

public const string C_ROLE_ID = "ROLE_ID";

public const string C_PAGE_ID = "PAGE_ID";

public const string C_LAST_MODIFIED_BY = "LAST_MODIFIED_BY";

public const string C_LAST_MODIFIED_TIME = "LAST_MODIFIED_TIME";


public SecRolePageTable()
{
_tableName = "SEC_ROLE_PAGE";
}

protected static SecRolePageTable _current;
public static SecRolePageTable Current
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
_current = new SecRolePageTable();

_current.Add(C_ROLE_ID, new ColumnInfo(C_ROLE_ID, "role_id", true, typeof(string)));

_current.Add(C_PAGE_ID, new ColumnInfo(C_PAGE_ID, "page_id", true, typeof(string)));

_current.Add(C_LAST_MODIFIED_BY, new ColumnInfo(C_LAST_MODIFIED_BY, "last_modified_by", false, typeof(string)));

_current.Add(C_LAST_MODIFIED_TIME, new ColumnInfo(C_LAST_MODIFIED_TIME, "last_modified_time", false, typeof(DateTime)));

}


public ColumnInfo ROLE_ID
{
get { return this[C_ROLE_ID]; }
}

public ColumnInfo PAGE_ID
{
get { return this[C_PAGE_ID]; }
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
