using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;

namespace eContract.Common.Schema
{
    [Serializable]
    public partial class CasContractTypeTable : TableInfo
    {
        public const string C_TableName = "CAS_CONTRACT_TYPE";

        public const string C_CONTRACT_TYPE_ID = "CONTRACT_TYPE_ID";

        public const string C_CONTRACT_TYPE_NAME = "CONTRACT_TYPE_NAME";

        public const string C_NEED_COMMENT = "NEED_COMMENT";

        public const string C_NOT_DISPLAY_IN_MY_SUPPORT = "NOT_DISPLAY_IN_MY_SUPPORT";

        public const string C_IS_TEMPLATE_CONTRACT = "IS_TEMPLATE_CONTRACT";

        public const string C_CONTRACT_NAME = "CONTRACT_NAME";

        public const string C_INTERNAL_INVERTMENT_ORDER = "INTERNAL_INVERTMENT_ORDER";

        public const string C_BUDGET_AMOUNT = "BUDGET_AMOUNT";

        public const string C_CONTRACT_TERM = "CONTRACT_TERM";

        public const string C_CONTRACT_OWNER = "CONTRACT_OWNER";

        public const string C_CONTRACT_INITIATOR = "CONTRACT_INITIATOR";

        public const string C_FERRERO_ENTITY = "FERRERO_ENTITY";

        public const string C_COUNTERPARTY_EN = "COUNTERPARTY_EN";

        public const string C_COUNTERPARTY_CN = "COUNTERPARTY_CN";

        public const string C_EFFECTIVE_DATE = "EFFECTIVE_DATE";

        public const string C_EXPIRATION_DATE = "EXPIRATION_DATE";

        public const string C_IS_MASTER_AGREEMENT = "IS_MASTER_AGREEMENT";

        public const string C_CONTRACT_ESTIMATED_PRICE = "CONTRACT_ESTIMATED_PRICE";

        //public const string C_ESTIMATED_PRICE = "ESTIMATED_PRICE";

        public const string C_CURRENCY = "CURRENCY";

        public const string C_CAPEX = "CAPEX";

        public const string C_SUPPLEMENTARY = "SUPPLEMENTARY";

        public const string C_REFERENCE_CONTRACT = "REFERENCE_CONTRACT";

        public const string C_PREPAYMENT_AMOUNT = "PREPAYMENT_AMOUNT";

        public const string C_PREPAYMENT_PERCENTAGE = "PREPAYMENT_PERCENTAGE";

        public const string C_CONTRACT_KEY_POINTS = "CONTRACT_KEY_POINTS";

        public const string C_BUDGET_TYPE = "BUDGET_TYPE";

        public const string C_TEMPLATE_NO = "TEMPLATE_NO";

        public const string C_TEMPLATE_NAME = "TEMPLATE_NAME";

        public const string C_SCOPE_OF_APPLICATION = "SCOPE_OF_APPLICATION";

        public const string C_APPLY_DATE = "APPLY_DATE";

        public const string C_HAS_ATTACHMENT = "HAS_ATTACHMENT";

        public const string C_STATUS = "STATUS";

        public const string C_IS_DELETED = "IS_DELETED";

        public const string C_CREATED_BY = "CREATED_BY";

        public const string C_CREATE_TIME = "CREATE_TIME";

        public const string C_LAST_MODIFIED_BY = "LAST_MODIFIED_BY";

        public const string C_LAST_MODIFIED_TIME = "LAST_MODIFIED_TIME";

        public const string C_TEMPLATE_TERM = "TEMPLATE_TERM";

        public const string C_TEMPLATE_OWNER = "TEMPLATE_OWNER";

        public const string C_TEMPLATE_INITIATOR = "TEMPLATE_INITIATOR";
        public CasContractTypeTable()
        {
            _tableName = "CAS_CONTRACT_TYPE";
        }

        protected static CasContractTypeTable _current;
        public static CasContractTypeTable Current
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
            _current = new CasContractTypeTable();

            _current.Add(C_CONTRACT_TYPE_ID, new ColumnInfo(C_CONTRACT_TYPE_ID, "contract_type_id", true, typeof(string)));

            _current.Add(C_CONTRACT_TYPE_NAME, new ColumnInfo(C_CONTRACT_TYPE_NAME, "contract_type_name", false, typeof(string)));

            _current.Add(C_NEED_COMMENT, new ColumnInfo(C_NEED_COMMENT, "need_comment", false, typeof(bool)));

            _current.Add(C_NOT_DISPLAY_IN_MY_SUPPORT, new ColumnInfo(C_NOT_DISPLAY_IN_MY_SUPPORT, "not_display_in_my_support", false, typeof(bool)));

            _current.Add(C_IS_TEMPLATE_CONTRACT, new ColumnInfo(C_IS_TEMPLATE_CONTRACT, "is_template_contract", false, typeof(bool)));

            _current.Add(C_CONTRACT_NAME, new ColumnInfo(C_CONTRACT_NAME, "contract_name", false, typeof(bool)));

            _current.Add(C_INTERNAL_INVERTMENT_ORDER, new ColumnInfo(C_INTERNAL_INVERTMENT_ORDER, "internal_OR_Investment_Order", false, typeof(bool)));

            _current.Add(C_BUDGET_AMOUNT, new ColumnInfo(C_BUDGET_AMOUNT, "budget_amount", false, typeof(bool)));

            _current.Add(C_CONTRACT_TERM, new ColumnInfo(C_CONTRACT_TERM, "contract_term", false, typeof(bool)));

            _current.Add(C_CONTRACT_OWNER, new ColumnInfo(C_CONTRACT_OWNER, "contract_owner", false, typeof(bool)));

            _current.Add(C_CONTRACT_INITIATOR, new ColumnInfo(C_CONTRACT_INITIATOR, "contract_initiator", false, typeof(bool)));

            _current.Add(C_FERRERO_ENTITY, new ColumnInfo(C_FERRERO_ENTITY, "ferrero_entity", false, typeof(string)));

            _current.Add(C_COUNTERPARTY_EN, new ColumnInfo(C_COUNTERPARTY_EN, "counterparty_en", false, typeof(bool)));

            _current.Add(C_COUNTERPARTY_CN, new ColumnInfo(C_COUNTERPARTY_CN, "counterparty_cn", false, typeof(bool)));

            _current.Add(C_EFFECTIVE_DATE, new ColumnInfo(C_EFFECTIVE_DATE, "effective_date", false, typeof(bool)));

            _current.Add(C_EXPIRATION_DATE, new ColumnInfo(C_EXPIRATION_DATE, "expiration_date", false, typeof(bool)));

            _current.Add(C_IS_MASTER_AGREEMENT, new ColumnInfo(C_IS_MASTER_AGREEMENT, "is_master_agreement", false, typeof(bool)));

            _current.Add(C_CONTRACT_ESTIMATED_PRICE, new ColumnInfo(C_CONTRACT_ESTIMATED_PRICE, "contract_price", false, typeof(bool)));

            //_current.Add(C_ESTIMATED_PRICE, new ColumnInfo(C_ESTIMATED_PRICE, "estimated_price", false, typeof(bool)));

            _current.Add(C_CURRENCY, new ColumnInfo(C_CURRENCY, "currency", false, typeof(bool)));

            _current.Add(C_CAPEX, new ColumnInfo(C_CAPEX, "capex", false, typeof(bool)));

            _current.Add(C_SUPPLEMENTARY, new ColumnInfo(C_SUPPLEMENTARY, "supplementary", false, typeof(bool)));

            _current.Add(C_REFERENCE_CONTRACT, new ColumnInfo(C_REFERENCE_CONTRACT, "reference_contract", false, typeof(bool)));

            _current.Add(C_PREPAYMENT_AMOUNT, new ColumnInfo(C_PREPAYMENT_AMOUNT, "prepayment_amount", false, typeof(bool)));

            _current.Add(C_PREPAYMENT_PERCENTAGE, new ColumnInfo(C_PREPAYMENT_PERCENTAGE, "prepayment_percentage", false, typeof(bool)));

            _current.Add(C_CONTRACT_KEY_POINTS, new ColumnInfo(C_CONTRACT_KEY_POINTS, "contract_key_points", false, typeof(bool)));

            _current.Add(C_BUDGET_TYPE, new ColumnInfo(C_BUDGET_TYPE, "budget_type", false, typeof(bool)));

            _current.Add(C_TEMPLATE_NO, new ColumnInfo(C_TEMPLATE_NO, "template_no", false, typeof(bool)));

            _current.Add(C_TEMPLATE_NAME, new ColumnInfo(C_TEMPLATE_NAME, "template_name", false, typeof(bool)));

            _current.Add(C_SCOPE_OF_APPLICATION, new ColumnInfo(C_SCOPE_OF_APPLICATION, "scope_of_application", false, typeof(bool)));

            _current.Add(C_APPLY_DATE, new ColumnInfo(C_APPLY_DATE, "apply_date", false, typeof(bool)));

            _current.Add(C_HAS_ATTACHMENT, new ColumnInfo(C_HAS_ATTACHMENT, "has_attachment", false, typeof(bool)));

            _current.Add(C_STATUS, new ColumnInfo(C_STATUS, "status", false, typeof(int)));

            _current.Add(C_IS_DELETED, new ColumnInfo(C_IS_DELETED, "is_deleted", false, typeof(bool)));

            _current.Add(C_CREATED_BY, new ColumnInfo(C_CREATED_BY, "created_by", false, typeof(string))); 

            _current.Add(C_LAST_MODIFIED_TIME, new ColumnInfo(C_LAST_MODIFIED_TIME, "Last_modified_time", false, typeof(DateTime)));

            _current.Add(C_CREATE_TIME, new ColumnInfo(C_CREATE_TIME, "create_time", false, typeof(DateTime)));

            _current.Add(C_LAST_MODIFIED_BY, new ColumnInfo(C_LAST_MODIFIED_BY, "last_modified_by", false, typeof(string)));

            _current.Add(C_TEMPLATE_TERM, new ColumnInfo(C_TEMPLATE_TERM, "Template_term", false, typeof(bool)));

            _current.Add(C_TEMPLATE_OWNER, new ColumnInfo(C_TEMPLATE_OWNER, "Template_owner", false, typeof(bool)));

            _current.Add(C_TEMPLATE_INITIATOR, new ColumnInfo(C_TEMPLATE_INITIATOR, "Template_initiator", false, typeof(bool)));

        }


        public ColumnInfo CONTRACT_TYPE_ID
        {
            get { return this[C_CONTRACT_TYPE_ID]; }
        }

        public ColumnInfo CONTRACT_TYPE_NAME
        {
            get { return this[C_CONTRACT_TYPE_NAME]; }
        }

        public ColumnInfo NEED_COMMENT
        {
            get { return this[C_NEED_COMMENT]; }
        }

        public ColumnInfo NOT_DISPLAY_IN_MY_SUPPORT
        {
            get { return this[C_NOT_DISPLAY_IN_MY_SUPPORT]; }
        }

        public ColumnInfo IS_TEMPLATE_CONTRACT
        {
            get { return this[C_IS_TEMPLATE_CONTRACT]; }
        }

        public ColumnInfo CONTRACT_NAME
        {
            get { return this[C_CONTRACT_NAME]; }
        }

        public ColumnInfo INTERNAL_INVERTMENT_ORDER
        {
            get { return this[C_INTERNAL_INVERTMENT_ORDER]; }
        }

        public ColumnInfo BUDGET_AMOUNT
        {
            get { return this[C_BUDGET_AMOUNT]; }
        }
        public ColumnInfo CONTRACT_TERM
        {
            get { return this[C_CONTRACT_TERM]; }
        }

        public ColumnInfo CONTRACT_OWNER
        {
            get { return this[C_CONTRACT_OWNER]; }
        }

        public ColumnInfo CONTRACT_INITIATOR
        {
            get { return this[C_CONTRACT_INITIATOR]; }
        }

        public ColumnInfo FERRERO_ENTITY
        {
            get { return this[C_FERRERO_ENTITY]; }
        }

        public ColumnInfo COUNTERPARTY_EN
        {
            get { return this[C_COUNTERPARTY_EN]; }
        }

        public ColumnInfo COUNTERPARTY_CN
        {
            get { return this[C_COUNTERPARTY_CN]; }
        }

        public ColumnInfo EFFECTIVE_DATE
        {
            get { return this[C_EFFECTIVE_DATE]; }
        }

        public ColumnInfo EXPIRATION_DATE
        {
            get { return this[C_EXPIRATION_DATE]; }
        }

        public ColumnInfo IS_MASTER_AGREEMENT
        {
            get { return this[C_IS_MASTER_AGREEMENT]; }
        }

        public ColumnInfo CONTRACT_ESTIMATED_PRICE
        {
            get { return this[C_CONTRACT_ESTIMATED_PRICE]; }
        }

        //public ColumnInfo ESTIMATED_PRICE
        //{
        //    get { return this[C_ESTIMATED_PRICE]; }
        //}

        public ColumnInfo CURRENCY
        {
            get { return this[C_CURRENCY]; }
        }

        public ColumnInfo CAPEX
        {
            get { return this[C_CAPEX]; }
        }

        public ColumnInfo SUPPLEMENTARY
        {
            get { return this[C_SUPPLEMENTARY]; }
        }

        public ColumnInfo REFERENCE_CONTRACT
        {
            get { return this[C_REFERENCE_CONTRACT]; }
        }

        public ColumnInfo PREPAYMENT_AMOUNT
        {
            get { return this[C_PREPAYMENT_AMOUNT]; }
        }

        public ColumnInfo PREPAYMENT_PERCENTAGE
        {
            get { return this[C_PREPAYMENT_PERCENTAGE]; }
        }

        public ColumnInfo CONTRACT_KEY_POINTS
        {
            get { return this[C_CONTRACT_KEY_POINTS]; }
        }

        public ColumnInfo BUDGET_TYPE
        {
            get { return this[C_BUDGET_TYPE]; }
        }

        public ColumnInfo TEMPLATE_NO
        {
            get { return this[C_TEMPLATE_NO]; }
        }

        public ColumnInfo TEMPLATE_NAME
        {
            get { return this[C_TEMPLATE_NAME]; }
        }

        public ColumnInfo SCOPE_OF_APPLICATION
        {
            get { return this[C_SCOPE_OF_APPLICATION]; }
        }

        public ColumnInfo APPLY_DATE
        {
            get { return this[C_APPLY_DATE]; }
        }

        public ColumnInfo HAS_ATTACHMENT
        {
            get { return this[C_HAS_ATTACHMENT]; }
        }

        public ColumnInfo STATUS
        {
            get { return this[C_STATUS]; }
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

        public ColumnInfo TEMPLATE_TERM
        {
            get { return this[C_TEMPLATE_TERM]; }
        }
        public ColumnInfo LAST_MODIFIED_BY
        {
            get { return this[C_LAST_MODIFIED_BY]; }
        }

        public ColumnInfo LAST_MODIFIED_TIME
        {
            get { return this[C_LAST_MODIFIED_TIME]; }
        }

        public ColumnInfo TEMPLATE_OWNER
        {
            get { return this[C_TEMPLATE_OWNER]; }
        }

        public ColumnInfo TEMPLATE_INITIATOR
        {
            get { return this[C_TEMPLATE_INITIATOR]; }
        }
    }
}
