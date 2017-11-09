using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;

namespace eContract.Common.Schema
{
[Serializable]
public partial class CasDepartmentTable : TableInfo
{
public const string C_TableName = "CAS_DEPARTMENT";

public const string C_DEPT_ID = "DEPT_ID";

public const string C_DEPT_CODE = "DEPT_CODE";

public const string C_DEPT_NAME = "DEPT_NAME";

public const string C_DEPT_ALIAS = "DEPT_ALIAS";

public const string C_DEPT_TYPE = "DEPT_TYPE";

public const string C_DEPT_MANAGER_ID = "DEPT_MANAGER_ID";

public const string C_COMPANY_CODE = "COMPANY_CODE";

public const string C_IS_DELETED = "IS_DELETED";

public const string C_CREATED_BY = "CREATED_BY";

public const string C_CREATE_TIME = "CREATE_TIME";

public const string C_LAST_MODIFIED_BY = "LAST_MODIFIED_BY";

public const string C_LAST_MODIFIED_TIME = "LAST_MODIFIED_TIME";


public CasDepartmentTable()
{
_tableName = "CAS_DEPARTMENT";
}

protected static CasDepartmentTable _current;
public static CasDepartmentTable Current
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
_current = new CasDepartmentTable();

_current.Add(C_DEPT_ID, new ColumnInfo(C_DEPT_ID, "dept_id", true, typeof(string)));

_current.Add(C_DEPT_CODE, new ColumnInfo(C_DEPT_CODE, "dept_code", false, typeof(string)));

_current.Add(C_DEPT_NAME, new ColumnInfo(C_DEPT_NAME, "dept_name", false, typeof(string)));

_current.Add(C_DEPT_ALIAS, new ColumnInfo(C_DEPT_ALIAS, "dept_alias", false, typeof(string)));

_current.Add(C_DEPT_TYPE, new ColumnInfo(C_DEPT_TYPE, "dept_type", false, typeof(int)));

_current.Add(C_DEPT_MANAGER_ID, new ColumnInfo(C_DEPT_MANAGER_ID, "dept_manager_id", false, typeof(string)));

_current.Add(C_COMPANY_CODE, new ColumnInfo(C_COMPANY_CODE, "company_code", false, typeof(string)));

_current.Add(C_IS_DELETED, new ColumnInfo(C_IS_DELETED, "is_deleted", false, typeof(bool)));

_current.Add(C_CREATED_BY, new ColumnInfo(C_CREATED_BY, "created_by", false, typeof(string)));

_current.Add(C_CREATE_TIME, new ColumnInfo(C_CREATE_TIME, "create_time", false, typeof(DateTime)));

_current.Add(C_LAST_MODIFIED_BY, new ColumnInfo(C_LAST_MODIFIED_BY, "last_modified_by", false, typeof(string)));

_current.Add(C_LAST_MODIFIED_TIME, new ColumnInfo(C_LAST_MODIFIED_TIME, "last_modified_time", false, typeof(DateTime)));

}


public ColumnInfo DEPT_ID
{
get { return this[C_DEPT_ID]; }
}

public ColumnInfo DEPT_CODE
{
get { return this[C_DEPT_CODE]; }
}

public ColumnInfo DEPT_NAME
{
get { return this[C_DEPT_NAME]; }
}

public ColumnInfo DEPT_ALIAS
{
get { return this[C_DEPT_ALIAS]; }
}

public ColumnInfo DEPT_TYPE
{
get { return this[C_DEPT_TYPE]; }
}

public ColumnInfo DEPT_MANAGER_ID
{
get { return this[C_DEPT_MANAGER_ID]; }
}

public ColumnInfo COMPANY_CODE
{
get { return this[C_COMPANY_CODE]; }
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
