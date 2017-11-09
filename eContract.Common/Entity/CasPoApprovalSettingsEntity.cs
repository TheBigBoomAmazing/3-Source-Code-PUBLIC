using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;

namespace eContract.Common.Entity
{
    [Serializable]
    public partial class CasPoApprovalSettingsEntity : EntityBase
    {
        public CasPoApprovalSettingsTable TableSchema
        {
            get
            {
                return CasPoApprovalSettingsTable.Current;
            }
        }


        public CasPoApprovalSettingsEntity()
        {
            IsDeleted = false;
        }

        public override TableInfo OringTableSchema
        {
            get
            {
                return CasPoApprovalSettingsTable.Current;
            }
        }
        #region 属性列表

        public string PoApprovalId
        {
            get { return (string)GetData(CasPoApprovalSettingsTable.C_PO_APPROVAL_ID); }
            set { SetData(CasPoApprovalSettingsTable.C_PO_APPROVAL_ID, value); }
        }

        public string Company
        {
            get { return (string)GetData(CasPoApprovalSettingsTable.C_COMPANY); }
            set { SetData(CasPoApprovalSettingsTable.C_COMPANY, value); }
        }

        public string ContractTypeId
        {
            get { return (string)GetData(CasPoApprovalSettingsTable.C_CONTRACT_TYPE_ID); }
            set { SetData(CasPoApprovalSettingsTable.C_CONTRACT_TYPE_ID, value); }
        }

        public string UserId
        {
            get { return (string)GetData(CasPoApprovalSettingsTable.C_USER_ID); }
            set { SetData(CasPoApprovalSettingsTable.C_USER_ID, value); }
        }

        public bool? IsDeleted
        {
            get { return (bool?)(GetData(CasPoApprovalSettingsTable.C_IS_DELETED)); }
            set { SetData(CasPoApprovalSettingsTable.C_IS_DELETED, value); }
        }

        public string CreatedBy
        {
            get { return (string)GetData(CasPoApprovalSettingsTable.C_CREATED_BY); }
            set { SetData(CasPoApprovalSettingsTable.C_CREATED_BY, value); }
        }

        public DateTime? CreateTime
        {
            get { return (DateTime?)(GetData(CasPoApprovalSettingsTable.C_CREATE_TIME)); }
            set { SetData(CasPoApprovalSettingsTable.C_CREATE_TIME, value); }
        }

        public string LastModifiedBy
        {
            get { return (string)GetData(CasPoApprovalSettingsTable.C_LAST_MODIFIED_BY); }
            set { SetData(CasPoApprovalSettingsTable.C_LAST_MODIFIED_BY, value); }
        }

        public DateTime? LastModifiedTime
        {
            get { return (DateTime?)(GetData(CasPoApprovalSettingsTable.C_LAST_MODIFIED_TIME)); }
            set { SetData(CasPoApprovalSettingsTable.C_LAST_MODIFIED_TIME, value); }
        }
        public string ApprovalUserValue
        {
            get;
            set;
        }

        #endregion
    }
}
