using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;

namespace eContract.Common.Entity
{
    [Serializable]
    public partial class CasHolidayEntity : EntityBase
    {
        public CasHolidayTable TableSchema
        {
            get
            {
                return CasHolidayTable.Current;
            }
        }


        public CasHolidayEntity()
        {
            IsDeleted = false;
        }

        public override TableInfo OringTableSchema
        {
            get
            {
                return CasHolidayTable.Current;
            }
        }
        #region 属性列表

        public string HolidayId
        {
            get { return (string)GetData(CasHolidayTable.C_HOLIDAY_ID); }
            set { SetData(CasHolidayTable.C_HOLIDAY_ID, value); }
        }

        public DateTime HolidayDate
        {
            get { return (DateTime)GetData(CasHolidayTable.C_HOLIDAY_DATE); }
            set { SetData(CasHolidayTable.C_HOLIDAY_DATE, value); }
        }

        public bool IsDeleted
        {
            get { return (bool)(GetData(CasHolidayTable.C_IS_DELETED)); }
            set { SetData(CasHolidayTable.C_IS_DELETED, value); }
        }

        public string CreatedBy
        {
            get { return (string)GetData(CasHolidayTable.C_CREATED_BY); }
            set { SetData(CasHolidayTable.C_CREATED_BY, value); }
        }

        public DateTime CreateTime
        {
            get { return (DateTime)(GetData(CasHolidayTable.C_CREATE_TIME)); }
            set { SetData(CasHolidayTable.C_CREATE_TIME, value); }
        }

        public string LastModifiedBy
        {
            get { return (string)GetData(CasHolidayTable.C_LAST_MODIFIED_BY); }
            set { SetData(CasHolidayTable.C_LAST_MODIFIED_BY, value); }
        }

        public DateTime LastModifiedTime
        {
            get { return (DateTime)(GetData(CasHolidayTable.C_LAST_MODIFIED_TIME)); }
            set { SetData(CasHolidayTable.C_LAST_MODIFIED_TIME, value); }
        }

        #endregion
    }
}
