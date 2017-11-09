using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;

namespace eContract.Common.Entity
{
[Serializable]
public partial class CasCityEntity : EntityBase
{
public CasCityTable TableSchema
{
get
{
return CasCityTable.Current;
}
}


public CasCityEntity()
{
            IsDeleted = false;
}

public override TableInfo OringTableSchema
{
get
{
return CasCityTable.Current;
}
}
#region 属性列表

public string CityId
{
get { return (string)GetData(CasCityTable.C_CITY_ID); }
set { SetData(CasCityTable.C_CITY_ID, value); }
}

public string CityCode
{
get { return (string)GetData(CasCityTable.C_CITY_CODE); }
set { SetData(CasCityTable.C_CITY_CODE, value); }
}

public string CityName
{
get { return (string)GetData(CasCityTable.C_CITY_NAME); }
set { SetData(CasCityTable.C_CITY_NAME, value); }
}

public string RegionId
{
get { return (string)GetData(CasCityTable.C_REGION_ID); }
set { SetData(CasCityTable.C_REGION_ID, value); }
}

public bool IsDeleted
{
get { return (bool)(GetData(CasCityTable.C_IS_DELETED)); }
set { SetData(CasCityTable.C_IS_DELETED, value); }
}

public string CreatedBy
{
get { return (string)GetData(CasCityTable.C_CREATED_BY); }
set { SetData(CasCityTable.C_CREATED_BY, value); }
}

public DateTime CreateTime
{
get { return (DateTime)(GetData(CasCityTable.C_CREATE_TIME)); }
set { SetData(CasCityTable.C_CREATE_TIME, value); }
}

public string LastModifiedBy
{
get { return (string)GetData(CasCityTable.C_LAST_MODIFIED_BY); }
set { SetData(CasCityTable.C_LAST_MODIFIED_BY, value); }
}

public DateTime LastModifiedTime
{
get { return (DateTime)(GetData(CasCityTable.C_LAST_MODIFIED_TIME)); }
set { SetData(CasCityTable.C_LAST_MODIFIED_TIME, value); }
}

#endregion
}
}
