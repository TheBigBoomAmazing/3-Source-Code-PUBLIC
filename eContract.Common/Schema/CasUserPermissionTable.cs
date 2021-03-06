﻿using Suzsoft.Smart.EntityCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eContract.Common.Schema
{
    [Serializable]
    public partial class CasUserPermissionTable: TableInfo
    {
        public const string C_TableName = "CAS_USER_PERMISSION";

        public const string C_PERMISSION_ID = "PERMISSION_ID";

        public const string C_USER_ID = "USER_ID";

        public const string C_DEPT_ID = "DEPT_ID";

        public const string C_IS_DELETED = "IS_DELETED";

        public const string C_CREATED_BY = "CREATED_BY";

        public const string C_CREATE_TIME = "CREATE_TIME";

        public const string C_LAST_MODIFIED_BY = "LAST_MODIFIED_BY";

        public const string C_LAST_MODIFIED_TIME = "LAST_MODIFIED_TIME";


        public CasUserPermissionTable()
        {
            _tableName = "CAS_USER_PERMISSION";
        }

        protected static CasUserPermissionTable _current;
        public static CasUserPermissionTable Current
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
            _current = new CasUserPermissionTable();

            _current.Add(C_PERMISSION_ID, new ColumnInfo(C_PERMISSION_ID, "permission_id", true, typeof(string)));

            _current.Add(C_USER_ID, new ColumnInfo(C_USER_ID, "user_id", false, typeof(string)));

            _current.Add(C_DEPT_ID, new ColumnInfo(C_DEPT_ID, "dept_uid", false, typeof(string)));

            _current.Add(C_IS_DELETED, new ColumnInfo(C_IS_DELETED, "is_deleted", false, typeof(bool)));

            _current.Add(C_CREATED_BY, new ColumnInfo(C_CREATED_BY, "created_by", false, typeof(string)));

            _current.Add(C_CREATE_TIME, new ColumnInfo(C_CREATE_TIME, "create_time", false, typeof(DateTime)));

            _current.Add(C_LAST_MODIFIED_BY, new ColumnInfo(C_LAST_MODIFIED_BY, "last_modified_by", false, typeof(string)));

            _current.Add(C_LAST_MODIFIED_TIME, new ColumnInfo(C_LAST_MODIFIED_TIME, "last_modified_time", false, typeof(DateTime)));

        }


        public ColumnInfo PERMISSION_ID
        {
            get { return this[C_PERMISSION_ID]; }
        }

        public ColumnInfo USER_ID
        {
            get { return this[C_USER_ID]; }
        }

        public ColumnInfo DEPT_ID
        {
            get { return this[C_DEPT_ID]; }
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
