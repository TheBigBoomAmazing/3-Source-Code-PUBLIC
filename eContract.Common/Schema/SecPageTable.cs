using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;

namespace eContract.Common.Schema
{
[Serializable]
public partial class SecPageTable : TableInfo
{
public const string C_TableName = "SEC_PAGE";

public const string C_PAGE_ID = "PAGE_ID";

public const string C_PAGE_NAME = "PAGE_NAME";

public const string C_PAGE_NAME_EN = "PAGE_NAME_EN";

public const string C_PARENT_ID = "PARENT_ID";

public const string C_PAGE_URL = "PAGE_URL";

public const string C_IS_MENU = "IS_MENU";

public const string C_MENU_LEVEL = "MENU_LEVEL";

public const string C_MENU_ORDER = "MENU_ORDER";

public const string C_PAGE_TYPE = "PAGE_TYPE";

public const string C_SYSTEM_NAME = "SYSTEM_NAME";

public const string C_REMARK = "REMARK";

public const string C_IS_DELETED = "IS_DELETED";

public const string C_CREATED_BY = "CREATED_BY";

public const string C_CREATE_TIME = "CREATE_TIME";

public const string C_LAST_MODIFIED_BY = "LAST_MODIFIED_BY";

public const string C_LAST_MODIFIED_TIME = "LAST_MODIFIED_TIME";


public SecPageTable()
{
_tableName = "SEC_PAGE";
}

protected static SecPageTable _current;
public static SecPageTable Current
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
_current = new SecPageTable();

_current.Add(C_PAGE_ID, new ColumnInfo(C_PAGE_ID, "page_id", true, typeof(string)));

_current.Add(C_PAGE_NAME, new ColumnInfo(C_PAGE_NAME, "page_name", false, typeof(string)));

_current.Add(C_PAGE_NAME_EN, new ColumnInfo(C_PAGE_NAME_EN, "page_name_en", false, typeof(string)));

_current.Add(C_PARENT_ID, new ColumnInfo(C_PARENT_ID, "parent_id", false, typeof(string)));

_current.Add(C_PAGE_URL, new ColumnInfo(C_PAGE_URL, "page_url", false, typeof(string)));

_current.Add(C_IS_MENU, new ColumnInfo(C_IS_MENU, "is_menu", false, typeof(bool)));

_current.Add(C_MENU_LEVEL, new ColumnInfo(C_MENU_LEVEL, "menu_level", false, typeof(int)));

_current.Add(C_MENU_ORDER, new ColumnInfo(C_MENU_ORDER, "menu_order", false, typeof(int)));

_current.Add(C_PAGE_TYPE, new ColumnInfo(C_PAGE_TYPE, "page_type", false, typeof(int)));

_current.Add(C_SYSTEM_NAME, new ColumnInfo(C_SYSTEM_NAME, "system_name", false, typeof(string)));

_current.Add(C_REMARK, new ColumnInfo(C_REMARK, "remark", false, typeof(string)));

_current.Add(C_IS_DELETED, new ColumnInfo(C_IS_DELETED, "is_deleted", false, typeof(bool)));

_current.Add(C_CREATED_BY, new ColumnInfo(C_CREATED_BY, "created_by", false, typeof(string)));

_current.Add(C_CREATE_TIME, new ColumnInfo(C_CREATE_TIME, "create_time", false, typeof(DateTime)));

_current.Add(C_LAST_MODIFIED_BY, new ColumnInfo(C_LAST_MODIFIED_BY, "last_modified_by", false, typeof(string)));

_current.Add(C_LAST_MODIFIED_TIME, new ColumnInfo(C_LAST_MODIFIED_TIME, "last_modified_time", false, typeof(DateTime)));

}


public ColumnInfo PAGE_ID
{
get { return this[C_PAGE_ID]; }
}

public ColumnInfo PAGE_NAME
{
get { return this[C_PAGE_NAME]; }
}

public ColumnInfo PAGE_NAME_EN
{
get { return this[C_PAGE_NAME_EN]; }
}

public ColumnInfo PARENT_ID
{
get { return this[C_PARENT_ID]; }
}

public ColumnInfo PAGE_URL
{
get { return this[C_PAGE_URL]; }
}

public ColumnInfo IS_MENU
{
get { return this[C_IS_MENU]; }
}

public ColumnInfo MENU_LEVEL
{
get { return this[C_MENU_LEVEL]; }
}

public ColumnInfo MENU_ORDER
{
get { return this[C_MENU_ORDER]; }
}

public ColumnInfo PAGE_TYPE
{
get { return this[C_PAGE_TYPE]; }
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
