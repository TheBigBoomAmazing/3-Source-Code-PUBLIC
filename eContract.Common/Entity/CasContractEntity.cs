using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;

namespace eContract.Common.Entity
{
    [Serializable]
    public partial class CasContractEntity : EntityBase
    {
        public CasContractTable TableSchema
        {
            get
            {
                return CasContractTable.Current;
            }
        }


        public CasContractEntity()
        {
            IsDeleted = false;
        }

        public override TableInfo OringTableSchema
        {
            get
            {
                return CasContractTable.Current;
            }
        }
        #region 属性列表

        public string ContractId
        {
            get { return (string)GetData(CasContractTable.C_CONTRACT_ID); }
            set { SetData(CasContractTable.C_CONTRACT_ID, value); }
        }

        public int? Status
        {
            get { return (int?)(GetData(CasContractTable.C_STATUS)); }
            set { SetData(CasContractTable.C_STATUS, value); }
        }

        public string OriginalContractId
        {
            get { return (string)GetData(CasContractTable.C_ORIGINAL_CONTRACT_ID); }
            set { SetData(CasContractTable.C_ORIGINAL_CONTRACT_ID, value); }
        }

        public string Supplier
        {
            get { return (string)GetData(CasContractTable.C_SUPPLIER); }
            set { SetData(CasContractTable.C_SUPPLIER, value); }
        }

        public int? ContractGroup
        {
            get { return (int?)(GetData(CasContractTable.C_CONTRACT_GROUP)); }
            set { SetData(CasContractTable.C_CONTRACT_GROUP, value); }
        }

        public string ModificationPoints
        {
            get { return (string)GetData(CasContractTable.C_MODIFICATION_POINTS); }
            set { SetData(CasContractTable.C_MODIFICATION_POINTS, value); }
        }

        public string ContractTypeId
        {
            get { return (string)GetData(CasContractTable.C_CONTRACT_TYPE_ID); }
            set { SetData(CasContractTable.C_CONTRACT_TYPE_ID, value); }
        }

        public string ContractTypeName
        {
            get { return (string)GetData(CasContractTable.C_CONTRACT_TYPE_NAME); }
            set { SetData(CasContractTable.C_CONTRACT_TYPE_NAME, value); }
        }

        public bool? NeedComment
        {
            get { return (bool?)(GetData(CasContractTable.C_NEED_COMMENT)); }
            set { SetData(CasContractTable.C_NEED_COMMENT, value); }
        }

        public bool? NotDisplayInMySupport
        {
            get { return (bool?)(GetData(CasContractTable.C_NOT_DISPLAY_IN_MY_SUPPORT)); }
            set { SetData(CasContractTable.C_NOT_DISPLAY_IN_MY_SUPPORT, value); }
        }

        public bool? IsTemplateContract
        {
            get { return (bool?)(GetData(CasContractTable.C_IS_TEMPLATE_CONTRACT)); }
            set { SetData(CasContractTable.C_IS_TEMPLATE_CONTRACT, value); }
        }

        public string ContractName
        {
            get { return (string)GetData(CasContractTable.C_CONTRACT_NAME); }
            set { SetData(CasContractTable.C_CONTRACT_NAME, value); }
        }

        public DateTime? ContractTerm
        {
            get { return (DateTime?)(GetData(CasContractTable.C_CONTRACT_TERM)); }
            set { SetData(CasContractTable.C_CONTRACT_TERM, value); }
        }

        public string ContractOwner
        {
            get { return (string)GetData(CasContractTable.C_CONTRACT_OWNER); }
            set { SetData(CasContractTable.C_CONTRACT_OWNER, value); }
        }

        public string ContractInitiator
        {
            get { return (string)GetData(CasContractTable.C_CONTRACT_INITIATOR); }
            set { SetData(CasContractTable.C_CONTRACT_INITIATOR, value); }
        }

        public string FerreroEntity
        {
            get { return (string)GetData(CasContractTable.C_FERRERO_ENTITY); }
            set { SetData(CasContractTable.C_FERRERO_ENTITY, value); }
        }

        public string CounterpartyEn
        {
            get { return (string)GetData(CasContractTable.C_COUNTERPARTY_EN); }
            set { SetData(CasContractTable.C_COUNTERPARTY_EN, value); }
        }

        public string CounterpartyCn
        {
            get { return (string)GetData(CasContractTable.C_COUNTERPARTY_CN); }
            set { SetData(CasContractTable.C_COUNTERPARTY_CN, value); }
        }

        public DateTime? EffectiveDate
        {
            get { return (DateTime?)(GetData(CasContractTable.C_EFFECTIVE_DATE)); }
            set { SetData(CasContractTable.C_EFFECTIVE_DATE, value); }
        }

        public DateTime? ExpirationDate
        {
            get { return (DateTime?)(GetData(CasContractTable.C_EXPIRATION_DATE)); }
            set { SetData(CasContractTable.C_EXPIRATION_DATE, value); }
        }

        public bool? IsMasterAgreement
        {
            get { return (bool?)(GetData(CasContractTable.C_IS_MASTER_AGREEMENT)); }
            set { SetData(CasContractTable.C_IS_MASTER_AGREEMENT, value); }
        }

        public decimal? ContractPrice
        {
            get { return (decimal?)(GetData(CasContractTable.C_CONTRACT_PRICE)); }
            set { SetData(CasContractTable.C_CONTRACT_PRICE, value); }
        }

        public decimal? EstimatedPrice
        {
            get { return (decimal?)(GetData(CasContractTable.C_ESTIMATED_PRICE)); }
            set { SetData(CasContractTable.C_ESTIMATED_PRICE, value); }
        }

        public string Currency
        {
            get { return (string)GetData(CasContractTable.C_CURRENCY); }
            set { SetData(CasContractTable.C_CURRENCY, value); }
        }

        public bool? Capex
        {
            get { return (bool?)(GetData(CasContractTable.C_CAPEX)); }
            set { SetData(CasContractTable.C_CAPEX, value); }
        }

        public bool? Supplementary
        {
            get { return (bool?)(GetData(CasContractTable.C_SUPPLEMENTARY)); }
            set { SetData(CasContractTable.C_SUPPLEMENTARY, value); }
        }

        public bool? ReferenceContract
        {
            get { return (bool?)(GetData(CasContractTable.C_REFERENCE_CONTRACT)); }
            set { SetData(CasContractTable.C_REFERENCE_CONTRACT, value); }
        }

        public decimal? PrepaymentAmount
        {
            get { return (decimal?)(GetData(CasContractTable.C_PREPAYMENT_AMOUNT)); }
            set { SetData(CasContractTable.C_PREPAYMENT_AMOUNT, value); }
        }

        public decimal? PrepaymentPercentage
        {
            get { return (decimal?)(GetData(CasContractTable.C_PREPAYMENT_PERCENTAGE)); }
            set { SetData(CasContractTable.C_PREPAYMENT_PERCENTAGE, value); }
        }

        public string ContractKeyPoints
        {
            get { return (string)GetData(CasContractTable.C_CONTRACT_KEY_POINTS); }
            set { SetData(CasContractTable.C_CONTRACT_KEY_POINTS, value); }
        }

        public int? BudgetType
        {
            get { return (int?)(GetData(CasContractTable.C_BUDGET_TYPE)); }
            set { SetData(CasContractTable.C_BUDGET_TYPE, value); }
        }

        public int? Tax
        {
            get { return (int?)(GetData(CasContractTable.C_TAX)); }
            set { SetData(CasContractTable.C_TAX, value); }
        }

        public string TemplateNo
        {
            get { return (string)GetData(CasContractTable.C_TEMPLATE_NO); }
            set { SetData(CasContractTable.C_TEMPLATE_NO, value); }
        }

        public string TemplateName
        {
            get { return (string)GetData(CasContractTable.C_TEMPLATE_NAME); }
            set { SetData(CasContractTable.C_TEMPLATE_NAME, value); }
        }

        public DateTime? TemplateTerm
        {
            get { return (DateTime?)(GetData(CasContractTable.C_TEMPLATE_TERM)); }
            set { SetData(CasContractTable.C_TEMPLATE_TERM, value); }
        }

        public string TemplateOwner
        {
            get { return (string)(GetData(CasContractTable.C_TEMPLATE_OWNER)); }
            set { SetData(CasContractTable.C_TEMPLATE_OWNER, value); }
        }

        public string TemplateInitiator
        {
            get { return (string)(GetData(CasContractTable.C_TEMPLATE_INITIATOR)); }
            set { SetData(CasContractTable.C_TEMPLATE_INITIATOR, value); }
        }

        public string ScopeOfApplication
        {
            get { return (string)GetData(CasContractTable.C_SCOPE_OF_APPLICATION); }
            set { SetData(CasContractTable.C_SCOPE_OF_APPLICATION, value); }
        }

        public DateTime? ApplyDate
        {
            get { return (DateTime?)(GetData(CasContractTable.C_APPLY_DATE)); }
            set { SetData(CasContractTable.C_APPLY_DATE, value); }
        }

        public string ContractNo
        {
            get { return (string)GetData(CasContractTable.C_CONTRACT_NO); }
            set { SetData(CasContractTable.C_CONTRACT_NO, value); }
        }

        public string ContractSerialNo
        {
            get { return (string)GetData(CasContractTable.C_CONTRACT_SERIAL_NO); }
            set { SetData(CasContractTable.C_CONTRACT_SERIAL_NO, value); }
        }

        public bool? IsDeleted
        {
            get { return (bool?)(GetData(CasContractTable.C_IS_DELETED)); }
            set { SetData(CasContractTable.C_IS_DELETED, value); }
        }

        public string CreatedBy
        {
            get { return (string)GetData(CasContractTable.C_CREATED_BY); }
            set { SetData(CasContractTable.C_CREATED_BY, value); }
        }

        public DateTime? CreateTime
        {
            get { return (DateTime?)(GetData(CasContractTable.C_CREATE_TIME)); }
            set { SetData(CasContractTable.C_CREATE_TIME, value); }
        }

        public string LastModifiedBy
        {
            get { return (string)GetData(CasContractTable.C_LAST_MODIFIED_BY); }
            set { SetData(CasContractTable.C_LAST_MODIFIED_BY, value); }
        }

        public string explanation
        {
            get { return (string)GetData(CasContractTable.C_EXPLANATION); }
            set { SetData(CasContractTable.C_EXPLANATION, value); }
        }

        public string InternalORInvestmentOrder
        {
            get { return (string)GetData(CasContractTable.C_INTERNAL_INVERTMENT_ORDER); }
            set { SetData(CasContractTable.C_INTERNAL_INVERTMENT_ORDER, value); }
        }

        public decimal? BudgetAmount
        {
            get { return (decimal?)GetData(CasContractTable.C_BUDGET_AMOUNT); }
            set { SetData(CasContractTable.C_BUDGET_AMOUNT, value); }
        }

        public DateTime? LastModifiedTime
        {
            get { return (DateTime?)(GetData(CasContractTable.C_LAST_MODIFIED_TIME)); }
            set { SetData(CasContractTable.C_LAST_MODIFIED_TIME, value); }
        }

        public string PO
        {
            get;set;
        }
        public string PR
        {
            get; set;
        }
        //public LigerGrid ligerGrid
        //{
        //    get;set;
        //}


        public string fileIds
        {
            get;
            set;
        }

        #endregion

        #region 业务参数，不做数据库映射
        public DateTime? ContractEffectiveDate { get; set; }
        public DateTime? ContractExpirationDate { get; set; }
        public string ContractTemplateNoForSel { get; set; }
        public DateTime? ContractApplyDate { get; set; }

        public DateTime? TemplateEffectiveDate { get; set; }
        public DateTime? TemplateExpirationDate { get; set; }
        public string TemplateNoForInput { get; set; }
        public DateTime? TemplateApplyDate { get; set; }
        #endregion

        #region 数据导出时候用
        public string ExportTypeData { get; set; }
        #endregion
    }
}
