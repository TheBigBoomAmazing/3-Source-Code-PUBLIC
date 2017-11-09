using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;

namespace eContract.Common.Schema
{
    [Serializable]
    public partial class CasRegionTable : TableInfo
    {
        public const string C_TableName = "CAS_REGION";

        public const string C_REGION_ID = "REGION_ID";

        public const string C_REGION_CODE = "REGION_CODE";

        public const string C_REGION_NAME = "REGION_NAME";

        public const string C_REGION_MANAGER = "REGION_MANAGER";

        public const string C_IS_DELETED = "IS_DELETED";

        public const string C_CREATED_BY = "CREATED_BY";

        public const string C_CREATE_TIME = "CREATE_TIME";

        public const string C_LAST_MODIFIED_BY = "LAST_MODIFIED_BY";

        public const string C_LAST_MODIFIED_TIME = "LAST_MODIFIED_TIME";


        public CasRegionTable()
        {
            _tableName = "CAS_REGION";
        }

        protected static CasRegionTable _current;
        public static CasRegionTable Current
        {
            get
            {
                if (_current == null)
                {
                    Initial();
                }
                return _current;
            }
        }

        private static void Initial()
        {
            _current = new CasRegionTable();

            _current.Add(C_REGION_ID, new ColumnInfo(C_REGION_ID, "region_id", true, typeof(string)));

            _current.Add(C_REGION_CODE, new ColumnInfo(C_REGION_CODE, "region_code", false, typeof(string)));

            _current.Add(C_REGION_NAME, new ColumnInfo(C_REGION_NAME, "region_name", false, typeof(string)));

            _current.Add(C_REGION_MANAGER, new ColumnInfo(C_REGION_MANAGER, "region_manager", false, typeof(string)));

            _current.Add(C_IS_DELETED, new ColumnInfo(C_IS_DELETED, "is_deleted", false, typeof(bool)));

            _current.Add(C_CREATED_BY, new ColumnInfo(C_CREATED_BY, "created_by", false, typeof(string)));

            _current.Add(C_CREATE_TIME, new ColumnInfo(C_CREATE_TIME, "create_time", false, typeof(DateTime)));

            _current.Add(C_LAST_MODIFIED_BY, new ColumnInfo(C_LAST_MODIFIED_BY, "last_modified_by", false, typeof(string)));

            _current.Add(C_LAST_MODIFIED_TIME, new ColumnInfo(C_LAST_MODIFIED_TIME, "last_modified_time", false, typeof(DateTime)));

        }


        public ColumnInfo REGION_ID
        {
            get { return this[C_REGION_ID]; }
        }

        public ColumnInfo REGION_CODE
        {
            get { return this[C_REGION_CODE]; }
        }

        public ColumnInfo REGION_NAME
        {
            get { return this[C_REGION_NAME]; }
        }

        public ColumnInfo REGION_MANAGER
        {
            get { return this[C_REGION_MANAGER]; }
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
