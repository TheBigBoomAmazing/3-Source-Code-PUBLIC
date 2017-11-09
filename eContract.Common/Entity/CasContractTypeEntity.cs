using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;

namespace eContract.Common.Entity
{
    [Serializable]
    public partial class CasContractTypeEntity : EntityBase
    {
        public CasContractTypeTable TableSchema
        {
            get
            {
                return CasContractTypeTable.Current;
            }
        }


        public CasContractTypeEntity()
        {
            IsDeleted = false;
        }

        public override TableInfo OringTableSchema
        {
            get
            {
                return CasContractTypeTable.Current;
            }
        }
        #region 属性列表

        public string ContractTypeId
        {
            get { return (string)GetData(CasContractTypeTable.C_CONTRACT_TYPE_ID); }
            set { SetData(CasContractTypeTable.C_CONTRACT_TYPE_ID, value); }
        }

        public string ContractTypeName
        {
            get { return (string)GetData(CasContractTypeTable.C_CONTRACT_TYPE_NAME); }
            set { SetData(CasContractTypeTable.C_CONTRACT_TYPE_NAME, value); }
        }

        public bool? NeedComment
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_NEED_COMMENT)); }
            set { SetData(CasContractTypeTable.C_NEED_COMMENT, value); }
        }

        public bool? NotDisplayInMySupport
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_NOT_DISPLAY_IN_MY_SUPPORT)); }
            set { SetData(CasContractTypeTable.C_NOT_DISPLAY_IN_MY_SUPPORT, value); }
        }

        public bool? IsTemplateContract
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_IS_TEMPLATE_CONTRACT)); }
            set { SetData(CasContractTypeTable.C_IS_TEMPLATE_CONTRACT, value); }
        }

        public bool? ContractName
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_CONTRACT_NAME)); }
            set { SetData(CasContractTypeTable.C_CONTRACT_NAME, value); }
        }

        public bool? ContractTerm
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_CONTRACT_TERM)); }
            set { SetData(CasContractTypeTable.C_CONTRACT_TERM, value); }
        }

        public bool? ContractOwner
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_CONTRACT_OWNER)); }
            set { SetData(CasContractTypeTable.C_CONTRACT_OWNER, value); }
        }

        public bool? ContractInitiator
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_CONTRACT_INITIATOR)); }
            set { SetData(CasContractTypeTable.C_CONTRACT_INITIATOR, value); }
        }

        public string FerreroEntity
        {
            get { return (string)GetData(CasContractTypeTable.C_FERRERO_ENTITY); }
            set { SetData(CasContractTypeTable.C_FERRERO_ENTITY, value); }
        }

        public bool? CounterpartyEn
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_COUNTERPARTY_EN)); }
            set { SetData(CasContractTypeTable.C_COUNTERPARTY_EN, value); }
        }

        public bool? CounterpartyCn
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_COUNTERPARTY_CN)); }
            set { SetData(CasContractTypeTable.C_COUNTERPARTY_CN, value); }
        }

        public bool? EffectiveDate
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_EFFECTIVE_DATE)); }
            set { SetData(CasContractTypeTable.C_EFFECTIVE_DATE, value); }
        }

        public bool? ExpirationDate
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_EXPIRATION_DATE)); }
            set { SetData(CasContractTypeTable.C_EXPIRATION_DATE, value); }
        }

        public bool? IsMasterAgreement
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_IS_MASTER_AGREEMENT)); }
            set { SetData(CasContractTypeTable.C_IS_MASTER_AGREEMENT, value); }
        }

        public bool? ContractOREstimatedPrice
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_CONTRACT_ESTIMATED_PRICE)); }
            set { SetData(CasContractTypeTable.C_CONTRACT_ESTIMATED_PRICE, value); }
        }

        public bool? InternalORInvestmentOrder
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_INTERNAL_INVERTMENT_ORDER)); }
            set { SetData(CasContractTypeTable.C_INTERNAL_INVERTMENT_ORDER, value); }
        }

        public bool? BudgetAmount
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_BUDGET_AMOUNT)); }
            set { SetData(CasContractTypeTable.C_BUDGET_AMOUNT, value); }
        }

        public bool? Currency
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_CURRENCY)); }
            set { SetData(CasContractTypeTable.C_CURRENCY, value); }
        }

        public bool? Capex
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_CAPEX)); }
            set { SetData(CasContractTypeTable.C_CAPEX, value); }
        }

        public bool? Supplementary
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_SUPPLEMENTARY)); }
            set { SetData(CasContractTypeTable.C_SUPPLEMENTARY, value); }
        }

        public bool? ReferenceContract
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_REFERENCE_CONTRACT)); }
            set { SetData(CasContractTypeTable.C_REFERENCE_CONTRACT, value); }
        }

        public bool? PrepaymentAmount
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_PREPAYMENT_AMOUNT)); }
            set { SetData(CasContractTypeTable.C_PREPAYMENT_AMOUNT, value); }
        }

        public bool? PrepaymentPercentage
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_PREPAYMENT_PERCENTAGE)); }
            set { SetData(CasContractTypeTable.C_PREPAYMENT_PERCENTAGE, value); }
        }

        public bool? ContractKeyPoints
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_CONTRACT_KEY_POINTS)); }
            set { SetData(CasContractTypeTable.C_CONTRACT_KEY_POINTS, value); }
        }

        public bool? BudgetType
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_BUDGET_TYPE)); }
            set { SetData(CasContractTypeTable.C_BUDGET_TYPE, value); }
        }

        public bool? TemplateNo
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_TEMPLATE_NO)); }
            set { SetData(CasContractTypeTable.C_TEMPLATE_NO, value); }
        }

        public bool? TemplateName
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_TEMPLATE_NAME)); }
            set { SetData(CasContractTypeTable.C_TEMPLATE_NAME, value); }
        }

        public bool? ScopeOfApplication
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_SCOPE_OF_APPLICATION)); }
            set { SetData(CasContractTypeTable.C_SCOPE_OF_APPLICATION, value); }
        }

        public bool? ApplyDate
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_APPLY_DATE)); }
            set { SetData(CasContractTypeTable.C_APPLY_DATE, value); }
        }

        public bool? HasAttachment
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_HAS_ATTACHMENT)); }
            set { SetData(CasContractTypeTable.C_HAS_ATTACHMENT, value); }
        }

        public int? Status
        {
            get { return (int?)(GetData(CasContractTypeTable.C_STATUS)); }
            set { SetData(CasContractTypeTable.C_STATUS, value); }
        }

        public bool? IsDeleted
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_IS_DELETED)); }
            set { SetData(CasContractTypeTable.C_IS_DELETED, value); }
        }

        public bool? TemplateTerm
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_TEMPLATE_TERM)); }
            set { SetData(CasContractTypeTable.C_TEMPLATE_TERM, value); }
        }

        public bool? TemplateOwner
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_TEMPLATE_OWNER)); }
            set { SetData(CasContractTypeTable.C_TEMPLATE_OWNER, value); }
        }

        public bool? TemplateInitiator
        {
            get { return (bool?)(GetData(CasContractTypeTable.C_TEMPLATE_INITIATOR)); }
            set { SetData(CasContractTypeTable.C_TEMPLATE_INITIATOR, value); }
        }
        
        public string CreatedBy
        {
            get { return (string)GetData(CasContractTypeTable.C_CREATED_BY); }
            set { SetData(CasContractTypeTable.C_CREATED_BY, value); }
        }

        public DateTime CreateTime
        {
            get
            {
                if (GetData(CasContractTypeTable.C_CREATE_TIME) == null)
                {
                    return DateTime.MinValue;
                }
                return (DateTime)(GetData(CasUserTable.C_CREATE_TIME));
            }
            set { SetData(CasContractTypeTable.C_CREATE_TIME, value); }
        }

        public string LastModifiedBy
        {
            get { return (string)GetData(CasContractTypeTable.C_LAST_MODIFIED_BY); }
            set { SetData(CasContractTypeTable.C_LAST_MODIFIED_BY, value); }
        }

        public DateTime LastModifiedTime
        {
            get {
                if (GetData(CasContractTypeTable.C_LAST_MODIFIED_TIME)==null)
                {
                    return DateTime.MinValue;
                }
                return (DateTime)(GetData(CasContractTypeTable.C_LAST_MODIFIED_TIME)); }
            set { SetData(CasContractTypeTable.C_LAST_MODIFIED_TIME, value); }
        }

        #endregion

        public string submitType
        {
            get;
            set;
        }
    }
}
