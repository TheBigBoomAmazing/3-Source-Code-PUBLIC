using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;

namespace eContract.Common.Schema
{
[Serializable]
public partial class CasUserTable : TableInfo
{
public const string C_TableName = "CAS_USER";

public const string C_USER_ID = "USER_ID";

public const string C_USER_ACCOUNT = "USER_ACCOUNT";

public const string C_USER_CODE = "USER_CODE";

public const string C_CHINESE_NAME = "CHINESE_NAME";

public const string C_ENGLISH_NAME = "ENGLISH_NAME";

public const string C_PASSWORD = "PASSWORD";

public const string C_COMPANY_CODE = "COMPANY_CODE";

public const string C_STATUS = "STATUS";

public const string C_DEPARMENT_CODE = "DEPARMENT_CODE";

public const string C_DEPARMENT_NAME = "DEPARMENT_NAME";

public const string C_POSITION_CODE = "POSITION_CODE";

public const string C_POSITION_DESCRIPTION = "POSITION_DESCRIPTION";

public const string C_ENGLISH_TITLE = "ENGLISH_TITLE";

public const string C_CHINESE_TITLE = "CHINESE_TITLE";

public const string C_ORG_UNIT_CODE = "ORG_UNIT_CODE";

public const string C_COST_CENTER_CODE = "COST_CENTER_CODE";

public const string C_COST_CENTER_NAME = "COST_CENTER_NAME";

public const string C_ENGLISH_LAST_NAME = "ENGLISH_LAST_NAME";

public const string C_ENGLISH_FIRST_NAME = "ENGLISH_FIRST_NAME";

public const string C_CHINESE_LAST_NAME = "CHINESE_LAST_NAME";

public const string C_CHINESE_FIRST_NAME = "CHINESE_FIRST_NAME";

public const string C_PINYIN_LAST_NAME = "PINYIN_LAST_NAME";

public const string C_PINYIN_FIRST_NAME = "PINYIN_FIRST_NAME";

public const string C_GENDER = "GENDER";

public const string C_CONTACT_NO = "CONTACT_NO";

public const string C_LINE_MANAGER_ID = "LINE_MANAGER_ID";

public const string C_EMAIL = "EMAIL";

public const string C_CITY_CODE = "CITY_CODE";

public const string C_ONBOARDING_DATE = "ONBOARDING_DATE";

public const string C_LAST_WORK_DATE = "LAST_WORK_DATE";

public const string C_EMPLOYEE_VENDOR = "EMPLOYEE_VENDOR";

public const string C_GRADE = "GRADE";

public const string C_REMARK = "REMARK";

public const string C_IS_ADMIN = "IS_ADMIN";

public const string C_IS_LOCK = "IS_LOCK";

public const string C_IS_DELETED = "IS_DELETED";

public const string C_CREATED_BY = "CREATED_BY";

public const string C_CREATE_TIME = "CREATE_TIME";

public const string C_LAST_MODIFIED_BY = "LAST_MODIFIED_BY";

public const string C_LAST_MODIFIED_TIME = "LAST_MODIFIED_TIME";


public CasUserTable()
{
_tableName = "CAS_USER";
}

protected static CasUserTable _current;
public static CasUserTable Current
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
_current = new CasUserTable();

_current.Add(C_USER_ID, new ColumnInfo(C_USER_ID, "user_id", true, typeof(string)));

_current.Add(C_USER_ACCOUNT, new ColumnInfo(C_USER_ACCOUNT, "user_account", false, typeof(string)));

_current.Add(C_USER_CODE, new ColumnInfo(C_USER_CODE, "user_code", false, typeof(string)));

_current.Add(C_CHINESE_NAME, new ColumnInfo(C_CHINESE_NAME, "chinese_name", false, typeof(string)));

_current.Add(C_ENGLISH_NAME, new ColumnInfo(C_ENGLISH_NAME, "english_name", false, typeof(string)));

_current.Add(C_PASSWORD, new ColumnInfo(C_PASSWORD, "password", false, typeof(string)));

_current.Add(C_COMPANY_CODE, new ColumnInfo(C_COMPANY_CODE, "company_code", false, typeof(string)));

_current.Add(C_STATUS, new ColumnInfo(C_STATUS, "status", false, typeof(int)));

_current.Add(C_DEPARMENT_CODE, new ColumnInfo(C_DEPARMENT_CODE, "deparment_code", false, typeof(string)));

_current.Add(C_DEPARMENT_NAME, new ColumnInfo(C_DEPARMENT_NAME, "deparment_name", false, typeof(string)));

_current.Add(C_POSITION_CODE, new ColumnInfo(C_POSITION_CODE, "position_code", false, typeof(string)));

_current.Add(C_POSITION_DESCRIPTION, new ColumnInfo(C_POSITION_DESCRIPTION, "position_description", false, typeof(string)));

_current.Add(C_ENGLISH_TITLE, new ColumnInfo(C_ENGLISH_TITLE, "english_title", false, typeof(string)));

_current.Add(C_CHINESE_TITLE, new ColumnInfo(C_CHINESE_TITLE, "chinese_title", false, typeof(string)));

_current.Add(C_ORG_UNIT_CODE, new ColumnInfo(C_ORG_UNIT_CODE, "org_unit_code", false, typeof(string)));

_current.Add(C_COST_CENTER_CODE, new ColumnInfo(C_COST_CENTER_CODE, "cost_center_code", false, typeof(string)));

_current.Add(C_COST_CENTER_NAME, new ColumnInfo(C_COST_CENTER_NAME, "cost_center_name", false, typeof(string)));

_current.Add(C_ENGLISH_LAST_NAME, new ColumnInfo(C_ENGLISH_LAST_NAME, "english_last_name", false, typeof(string)));

_current.Add(C_ENGLISH_FIRST_NAME, new ColumnInfo(C_ENGLISH_FIRST_NAME, "english_first_name", false, typeof(string)));

_current.Add(C_CHINESE_LAST_NAME, new ColumnInfo(C_CHINESE_LAST_NAME, "chinese_last_name", false, typeof(string)));

_current.Add(C_CHINESE_FIRST_NAME, new ColumnInfo(C_CHINESE_FIRST_NAME, "chinese_first_name", false, typeof(string)));

_current.Add(C_PINYIN_LAST_NAME, new ColumnInfo(C_PINYIN_LAST_NAME, "pinyin_last_name", false, typeof(string)));

_current.Add(C_PINYIN_FIRST_NAME, new ColumnInfo(C_PINYIN_FIRST_NAME, "pinyin_first_name", false, typeof(string)));

_current.Add(C_GENDER, new ColumnInfo(C_GENDER, "gender", false, typeof(int)));

_current.Add(C_CONTACT_NO, new ColumnInfo(C_CONTACT_NO, "contact_no", false, typeof(string)));

_current.Add(C_LINE_MANAGER_ID, new ColumnInfo(C_LINE_MANAGER_ID, "line_manager_id", false, typeof(string)));

_current.Add(C_EMAIL, new ColumnInfo(C_EMAIL, "email", false, typeof(string)));

_current.Add(C_CITY_CODE, new ColumnInfo(C_CITY_CODE, "city_code", false, typeof(string)));

_current.Add(C_ONBOARDING_DATE, new ColumnInfo(C_ONBOARDING_DATE, "onboarding_date", false, typeof(DateTime)));

_current.Add(C_LAST_WORK_DATE, new ColumnInfo(C_LAST_WORK_DATE, "last_work_date", false, typeof(DateTime)));

_current.Add(C_EMPLOYEE_VENDOR, new ColumnInfo(C_EMPLOYEE_VENDOR, "employee_vendor", false, typeof(string)));

_current.Add(C_GRADE, new ColumnInfo(C_GRADE, "grade", false, typeof(string)));

_current.Add(C_REMARK, new ColumnInfo(C_REMARK, "remark", false, typeof(string)));

_current.Add(C_IS_ADMIN, new ColumnInfo(C_IS_ADMIN, "is_admin", false, typeof(bool)));

_current.Add(C_IS_LOCK, new ColumnInfo(C_IS_LOCK, "is_lock", false, typeof(bool)));

_current.Add(C_IS_DELETED, new ColumnInfo(C_IS_DELETED, "is_deleted", false, typeof(bool)));

_current.Add(C_CREATED_BY, new ColumnInfo(C_CREATED_BY, "created_by", false, typeof(string)));

_current.Add(C_CREATE_TIME, new ColumnInfo(C_CREATE_TIME, "create_time", false, typeof(DateTime)));

_current.Add(C_LAST_MODIFIED_BY, new ColumnInfo(C_LAST_MODIFIED_BY, "last_modified_by", false, typeof(string)));

_current.Add(C_LAST_MODIFIED_TIME, new ColumnInfo(C_LAST_MODIFIED_TIME, "last_modified_time", false, typeof(DateTime)));

}


public ColumnInfo USER_ID
{
get { return this[C_USER_ID]; }
}

public ColumnInfo USER_ACCOUNT
{
get { return this[C_USER_ACCOUNT]; }
}

public ColumnInfo USER_CODE
{
get { return this[C_USER_CODE]; }
}

public ColumnInfo CHINESE_NAME
{
get { return this[C_CHINESE_NAME]; }
}

public ColumnInfo ENGLISH_NAME
{
get { return this[C_ENGLISH_NAME]; }
}

public ColumnInfo PASSWORD
{
get { return this[C_PASSWORD]; }
}

public ColumnInfo COMPANY_CODE
{
get { return this[C_COMPANY_CODE]; }
}

public ColumnInfo STATUS
{
get { return this[C_STATUS]; }
}

public ColumnInfo DEPARMENT_CODE
{
get { return this[C_DEPARMENT_CODE]; }
}

public ColumnInfo DEPARMENT_NAME
{
get { return this[C_DEPARMENT_NAME]; }
}

public ColumnInfo POSITION_CODE
{
get { return this[C_POSITION_CODE]; }
}

public ColumnInfo POSITION_DESCRIPTION
{
get { return this[C_POSITION_DESCRIPTION]; }
}

public ColumnInfo ENGLISH_TITLE
{
get { return this[C_ENGLISH_TITLE]; }
}

public ColumnInfo CHINESE_TITLE
{
get { return this[C_CHINESE_TITLE]; }
}

public ColumnInfo ORG_UNIT_CODE
{
get { return this[C_ORG_UNIT_CODE]; }
}

public ColumnInfo COST_CENTER_CODE
{
get { return this[C_COST_CENTER_CODE]; }
}

public ColumnInfo COST_CENTER_NAME
{
get { return this[C_COST_CENTER_NAME]; }
}

public ColumnInfo ENGLISH_LAST_NAME
{
get { return this[C_ENGLISH_LAST_NAME]; }
}

public ColumnInfo ENGLISH_FIRST_NAME
{
get { return this[C_ENGLISH_FIRST_NAME]; }
}

public ColumnInfo CHINESE_LAST_NAME
{
get { return this[C_CHINESE_LAST_NAME]; }
}

public ColumnInfo CHINESE_FIRST_NAME
{
get { return this[C_CHINESE_FIRST_NAME]; }
}

public ColumnInfo PINYIN_LAST_NAME
{
get { return this[C_PINYIN_LAST_NAME]; }
}

public ColumnInfo PINYIN_FIRST_NAME
{
get { return this[C_PINYIN_FIRST_NAME]; }
}

public ColumnInfo GENDER
{
get { return this[C_GENDER]; }
}

public ColumnInfo CONTACT_NO
{
get { return this[C_CONTACT_NO]; }
}

public ColumnInfo LINE_MANAGER_ID
{
get { return this[C_LINE_MANAGER_ID]; }
}

public ColumnInfo EMAIL
{
get { return this[C_EMAIL]; }
}

public ColumnInfo CITY_CODE
{
get { return this[C_CITY_CODE]; }
}

public ColumnInfo ONBOARDING_DATE
{
get { return this[C_ONBOARDING_DATE]; }
}

public ColumnInfo LAST_WORK_DATE
{
get { return this[C_LAST_WORK_DATE]; }
}

public ColumnInfo EMPLOYEE_VENDOR
{
get { return this[C_EMPLOYEE_VENDOR]; }
}

public ColumnInfo GRADE
{
get { return this[C_GRADE]; }
}

public ColumnInfo REMARK
{
get { return this[C_REMARK]; }
}

public ColumnInfo IS_ADMIN
{
get { return this[C_IS_ADMIN]; }
}

public ColumnInfo IS_LOCK
{
get { return this[C_IS_LOCK]; }
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
