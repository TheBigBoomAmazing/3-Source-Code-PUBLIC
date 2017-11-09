using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;

namespace eContract.Common.Entity
{
    [Serializable]
    public partial class CasRegionEntity : EntityBase
    {
        public CasRegionTable TableSchema
        {
            get
            {
                return CasRegionTable.Current;
            }
        }


        public CasRegionEntity()
        {
            IsDeleted = false;
        }

        public override TableInfo OringTableSchema
        {
            get
            {
                return CasRegionTable.Current;
            }
        }
        #region 属性列表

        public string RegionId
        {
            get { return (string)GetData(CasRegionTable.C_REGION_ID); }
            set { SetData(CasRegionTable.C_REGION_ID, value); }
        }

        public string RegionCode
        {
            get { return (string)GetData(CasRegionTable.C_REGION_CODE); }
            set { SetData(CasRegionTable.C_REGION_CODE, value); }
        }

        public string RegionName
        {
            get { return (string)GetData(CasRegionTable.C_REGION_NAME); }
            set { SetData(CasRegionTable.C_REGION_NAME, value); }
        }

        public string RegionManager
        {
            get { return (string)GetData(CasRegionTable.C_REGION_MANAGER); }
            set { SetData(CasRegionTable.C_REGION_MANAGER, value); }
        }

        public bool? IsDeleted
        {
            get { return (bool?)(GetData(CasRegionTable.C_IS_DELETED)); }
            set { SetData(CasRegionTable.C_IS_DELETED, value); }
        }

        public string CreatedBy
        {
            get { return (string)GetData(CasRegionTable.C_CREATED_BY); }
            set { SetData(CasRegionTable.C_CREATED_BY, value); }
        }

        public DateTime? CreateTime
        {
            get { return (DateTime?)(GetData(CasRegionTable.C_CREATE_TIME)); }
            set { SetData(CasRegionTable.C_CREATE_TIME, value); }
        }

        public string LastModifiedBy
        {
            get { return (string)GetData(CasRegionTable.C_LAST_MODIFIED_BY); }
            set { SetData(CasRegionTable.C_LAST_MODIFIED_BY, value); }
        }

        public DateTime? LastModifiedTime
        {
            get { return (DateTime?)(GetData(CasRegionTable.C_LAST_MODIFIED_TIME)); }
            set { SetData(CasRegionTable.C_LAST_MODIFIED_TIME, value); }
        }

        #endregion
    }
}
