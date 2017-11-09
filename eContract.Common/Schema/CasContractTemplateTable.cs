using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;

namespace eContract.Common.Schema
{
    [Serializable]
    public partial class CasContractTemplateTable : TableInfo
    {
        public const string C_TableName = "CAS_CONTRACT_TEMPLATE";

        public const string C_CONTRACT_TEMPLATE_ID = "CONTRACT_TEMPLATE_ID";

        public const string C_CONTRACT_TEMPLATE_NAME = "CONTRACT_TEMPLATE_NAME";

        public const string C_COMPANY = "COMPANY";

        public const string C_STATUS = "STATUS";

        public const string C_IS_DELETED = "IS_DELETED";

        public const string C_CREATED_BY = "CREATED_BY";

        public const string C_CREATE_TIME = "CREATE_TIME";

        public const string C_LAST_MODIFIED_BY = "LAST_MODIFIED_BY";

        public const string C_LAST_MODIFIED_TIME = "LAST_MODIFIED_TIME";


        public CasContractTemplateTable()
        {
            _tableName = "CAS_CONTRACT_TEMPLATE";
        }

        protected static CasContractTemplateTable _current;
        public static CasContractTemplateTable Current
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
            _current = new CasContractTemplateTable();

            _current.Add(C_CONTRACT_TEMPLATE_ID, new ColumnInfo(C_CONTRACT_TEMPLATE_ID, "contract_template_id", true, typeof(string)));

            _current.Add(C_CONTRACT_TEMPLATE_NAME, new ColumnInfo(C_CONTRACT_TEMPLATE_NAME, "contract_template_name", false, typeof(string)));

            _current.Add(C_COMPANY, new ColumnInfo(C_COMPANY, "company", false, typeof(string)));

            _current.Add(C_STATUS, new ColumnInfo(C_STATUS, "status", false, typeof(int)));

            _current.Add(C_IS_DELETED, new ColumnInfo(C_IS_DELETED, "is_deleted", false, typeof(bool)));

            _current.Add(C_CREATED_BY, new ColumnInfo(C_CREATED_BY, "created_by", false, typeof(string)));

            _current.Add(C_CREATE_TIME, new ColumnInfo(C_CREATE_TIME, "create_time", false, typeof(DateTime)));

            _current.Add(C_LAST_MODIFIED_BY, new ColumnInfo(C_LAST_MODIFIED_BY, "last_modified_by", false, typeof(string)));

            _current.Add(C_LAST_MODIFIED_TIME, new ColumnInfo(C_LAST_MODIFIED_TIME, "last_modified_time", false, typeof(DateTime)));

        }


        public ColumnInfo CONTRACT_TEMPLATE_ID
        {
            get { return this[C_CONTRACT_TEMPLATE_ID]; }
        }

        public ColumnInfo CONTRACT_TEMPLATE_NAME
        {
            get { return this[C_CONTRACT_TEMPLATE_NAME]; }
        }

        public ColumnInfo COMPANY
        {
            get { return this[C_COMPANY]; }
        }

        public ColumnInfo IS_DELETED
        {
            get { return this[C_IS_DELETED]; }
        }

        public ColumnInfo STATUS
        {
            get { return this[C_STATUS]; }
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
