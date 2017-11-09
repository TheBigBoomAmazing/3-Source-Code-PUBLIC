using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;

namespace eContract.Common.Schema
{
[Serializable]
public partial class CasHolidayTable : TableInfo
{
public const string C_TableName = "CAS_HOLIDAY";

public const string C_HOLIDAY_ID = "HOLIDAY_ID";

public const string C_HOLIDAY_DATE = "HOLIDAY_DATE";

public const string C_IS_DELETED = "IS_DELETED";

public const string C_CREATED_BY = "CREATED_BY";

public const string C_CREATE_TIME = "CREATE_TIME";

public const string C_LAST_MODIFIED_BY = "LAST_MODIFIED_BY";

public const string C_LAST_MODIFIED_TIME = "LAST_MODIFIED_TIME";


public CasHolidayTable()
{
_tableName = "CAS_HOLIDAY";
}

protected static CasHolidayTable _current;
public static CasHolidayTable Current
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
_current = new CasHolidayTable();

_current.Add(C_HOLIDAY_ID, new ColumnInfo(C_HOLIDAY_ID, "holiday_id", true, typeof(string)));

_current.Add(C_HOLIDAY_DATE, new ColumnInfo(C_HOLIDAY_DATE, "holiday_date", false, typeof(DateTime)));

_current.Add(C_IS_DELETED, new ColumnInfo(C_IS_DELETED, "is_deleted", false, typeof(bool)));

_current.Add(C_CREATED_BY, new ColumnInfo(C_CREATED_BY, "created_by", false, typeof(string)));

_current.Add(C_CREATE_TIME, new ColumnInfo(C_CREATE_TIME, "create_time", false, typeof(DateTime)));

_current.Add(C_LAST_MODIFIED_BY, new ColumnInfo(C_LAST_MODIFIED_BY, "last_modified_by", false, typeof(string)));

_current.Add(C_LAST_MODIFIED_TIME, new ColumnInfo(C_LAST_MODIFIED_TIME, "last_modified_time", false, typeof(DateTime)));

}


public ColumnInfo HOLIDAY_ID
{
get { return this[C_HOLIDAY_ID]; }
}

public ColumnInfo HOLIDAY_DATE
{
get { return this[C_HOLIDAY_DATE]; }
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
