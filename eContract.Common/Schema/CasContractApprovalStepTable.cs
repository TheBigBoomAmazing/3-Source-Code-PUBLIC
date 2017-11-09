using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;

namespace eContract.Common.Schema
{
    [Serializable]
    public partial class CasContractApprovalStepTable : TableInfo
    {
        public const string C_TableName = "CAS_CONTRACT_APPROVAL_STEP";

        public const string C_CONTRACT_APPROVAL_STEP_ID = "CONTRACT_APPROVAL_STEP_ID";

        public const string C_COMPANY = "COMPANY";

        public const string C_CONTRACT_TYPE_ID = "CONTRACT_TYPE_ID";

        public const string C_STEP = "STEP";

        public const string C_APPROVAL_ROLE = "APPROVAL_ROLE";

        public const string C_NEED_REGION_MANAGER = "NEED_REGION_MANAGER";

        public const string C_NEED_DEPT_MANAGER = "NEED_DEPT_MANAGER";

        public const string C_NEED_LINE_MANAGER = "NEED_LINE_MANAGER";

        public const string C_APPROVAL_TIME = "APPROVAL_TIME";

        public const string C_IS_DELETED = "IS_DELETED";

        public const string C_CREATED_BY = "CREATED_BY";

        public const string C_CREATE_TIME = "CREATE_TIME";

        public const string C_LAST_MODIFIED_BY = "LAST_MODIFIED_BY";

        public const string C_LAST_MODIFIED_TIME = "LAST_MODIFIED_TIME";


        public CasContractApprovalStepTable()
        {
            _tableName = "CAS_CONTRACT_APPROVAL_STEP";
        }

        protected static CasContractApprovalStepTable _current;
        public static CasContractApprovalStepTable Current
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
            _current = new CasContractApprovalStepTable();

            _current.Add(C_CONTRACT_APPROVAL_STEP_ID, new ColumnInfo(C_CONTRACT_APPROVAL_STEP_ID, "contract_approval_step_id", true, typeof(string)));

            _current.Add(C_COMPANY, new ColumnInfo(C_COMPANY, "company", false, typeof(string)));

            _current.Add(C_CONTRACT_TYPE_ID, new ColumnInfo(C_CONTRACT_TYPE_ID, "contract_type_id", false, typeof(string)));

            _current.Add(C_STEP, new ColumnInfo(C_STEP, "step", false, typeof(int)));

            _current.Add(C_APPROVAL_ROLE, new ColumnInfo(C_APPROVAL_ROLE, "approval_role", false, typeof(int)));

            _current.Add(C_NEED_REGION_MANAGER, new ColumnInfo(C_NEED_REGION_MANAGER, "need_region_manager", false, typeof(bool)));

            _current.Add(C_NEED_DEPT_MANAGER, new ColumnInfo(C_NEED_DEPT_MANAGER, "need_dept_manager", false, typeof(bool)));

            _current.Add(C_NEED_LINE_MANAGER, new ColumnInfo(C_NEED_LINE_MANAGER, "need_line_manager", false, typeof(bool)));

            _current.Add(C_APPROVAL_TIME, new ColumnInfo(C_APPROVAL_TIME, "approval_time", false, typeof(int)));

            _current.Add(C_IS_DELETED, new ColumnInfo(C_IS_DELETED, "is_deleted", false, typeof(bool)));

            _current.Add(C_CREATED_BY, new ColumnInfo(C_CREATED_BY, "created_by", false, typeof(string)));

            _current.Add(C_CREATE_TIME, new ColumnInfo(C_CREATE_TIME, "create_time", false, typeof(DateTime)));

            _current.Add(C_LAST_MODIFIED_BY, new ColumnInfo(C_LAST_MODIFIED_BY, "last_modified_by", false, typeof(string)));

            _current.Add(C_LAST_MODIFIED_TIME, new ColumnInfo(C_LAST_MODIFIED_TIME, "last_modified_time", false, typeof(DateTime)));

        }


        public ColumnInfo CONTRACT_APPROVAL_STEP_ID
        {
            get { return this[C_CONTRACT_APPROVAL_STEP_ID]; }
        }

        public ColumnInfo COMPANY
        {
            get { return this[C_COMPANY]; }
        }

        public ColumnInfo CONTRACT_TYPE_ID
        {
            get { return this[C_CONTRACT_TYPE_ID]; }
        }

        public ColumnInfo STEP
        {
            get { return this[C_STEP]; }
        }

        public ColumnInfo APPROVAL_ROLE
        {
            get { return this[C_APPROVAL_ROLE]; }
        }

        public ColumnInfo NEED_REGION_MANAGER
        {
            get { return this[C_NEED_REGION_MANAGER]; }
        }

        public ColumnInfo NEED_DEPT_MANAGER
        {
            get { return this[C_NEED_DEPT_MANAGER]; }
        }
        public ColumnInfo NEED_LINE_MANAGER
        {
            get { return this[C_NEED_LINE_MANAGER]; }
        }

        public ColumnInfo APPROVAL_TIME
        {
            get { return this[C_APPROVAL_TIME]; }
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
