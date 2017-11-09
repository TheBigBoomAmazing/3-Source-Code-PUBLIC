using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;

namespace eContract.Common.Schema
{
    [Serializable]
    public partial class CasContractFilingTable : TableInfo
    {
        public const string C_TableName = "CAS_CONTRACT_FILING";

        public const string C_CONTRACT_FILING_ID = "CONTRACT_FILING_ID";

        public const string C_CONTRACT_ID = "CONTRACT_ID";

        public const string C_PR_NO = "PR_NO";

        public const string C_PO_NO = "PO_NO";

        public const string C_STATUS = "STATUS";

        public const string C_REMARK = "REMARK";

        public const string C_IS_DELETED = "IS_DELETED";

        public const string C_CREATED_BY = "CREATED_BY";

        public const string C_CREATE_TIME = "CREATE_TIME";

        public const string C_LAST_MODIFIED_BY = "LAST_MODIFIED_BY";

        public const string C_LAST_MODIFIED_TIME = "LAST_MODIFIED_TIME";


        public CasContractFilingTable()
        {
            _tableName = "CAS_CONTRACT_FILING";
        }

        protected static CasContractFilingTable _current;
        public static CasContractFilingTable Current
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
            _current = new CasContractFilingTable();

            _current.Add(C_CONTRACT_FILING_ID, new ColumnInfo(C_CONTRACT_FILING_ID, "contract_filing_id", true, typeof(string)));

            _current.Add(C_CONTRACT_ID, new ColumnInfo(C_CONTRACT_ID, "contract_id", false, typeof(string)));

            _current.Add(C_PR_NO, new ColumnInfo(C_PR_NO, "pr_no", false, typeof(string)));

            _current.Add(C_PO_NO, new ColumnInfo(C_PO_NO, "po_no", false, typeof(string)));

            _current.Add(C_STATUS, new ColumnInfo(C_STATUS, "status", false, typeof(int)));

            _current.Add(C_REMARK, new ColumnInfo(C_REMARK, "remark", false, typeof(string)));

            _current.Add(C_IS_DELETED, new ColumnInfo(C_IS_DELETED, "is_deleted", false, typeof(bool)));

            _current.Add(C_CREATED_BY, new ColumnInfo(C_CREATED_BY, "created_by", false, typeof(string)));

            _current.Add(C_CREATE_TIME, new ColumnInfo(C_CREATE_TIME, "create_time", false, typeof(DateTime)));

            _current.Add(C_LAST_MODIFIED_BY, new ColumnInfo(C_LAST_MODIFIED_BY, "last_modified_by", false, typeof(string)));

            _current.Add(C_LAST_MODIFIED_TIME, new ColumnInfo(C_LAST_MODIFIED_TIME, "last_modified_time", false, typeof(DateTime)));

        }


        public ColumnInfo CONTRACT_FILING_ID
        {
            get { return this[C_CONTRACT_FILING_ID]; }
        }

        public ColumnInfo CONTRACT_ID
        {
            get { return this[C_CONTRACT_ID]; }
        }

        public ColumnInfo PR_NO
        {
            get { return this[C_PR_NO]; }
        }

        public ColumnInfo PO_NO
        {
            get { return this[C_PO_NO]; }
        }

        public ColumnInfo STATUS
        {
            get { return this[C_STATUS]; }
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
