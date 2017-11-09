using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;

namespace eContract.Common.Entity
{
    [Serializable]
    public partial class CasContractTemplateEntity : EntityBase
    {
        public CasContractTemplateTable TableSchema
        {
            get
            {
                return CasContractTemplateTable.Current;
            }
        }


        public CasContractTemplateEntity()
        {
            IsDeleted = false;
        }

        public override TableInfo OringTableSchema
        {
            get
            {
                return CasContractTemplateTable.Current;
            }
        }
        #region 属性列表

        public string ContractTemplateId
        {
            get { return (string)GetData(CasContractTemplateTable.C_CONTRACT_TEMPLATE_ID); }
            set { SetData(CasContractTemplateTable.C_CONTRACT_TEMPLATE_ID, value); }
        }

        public string ContractTemplateName
        {
            get { return (string)GetData(CasContractTemplateTable.C_CONTRACT_TEMPLATE_NAME); }
            set { SetData(CasContractTemplateTable.C_CONTRACT_TEMPLATE_NAME, value); }
        }

        public bool? IsDeleted
        {
            get { return (bool?)(GetData(CasContractTemplateTable.C_IS_DELETED)); }
            set { SetData(CasContractTemplateTable.C_IS_DELETED, value); }
        }

        public string CreatedBy
        {
            get { return (string)GetData(CasContractTemplateTable.C_CREATED_BY); }
            set { SetData(CasContractTemplateTable.C_CREATED_BY, value); }
        }

        public DateTime? CreateTime
        {
            get { return (DateTime?)(GetData(CasContractTemplateTable.C_CREATE_TIME)); }
            set { SetData(CasContractTemplateTable.C_CREATE_TIME, value); }
        }

        public string LastModifiedBy
        {
            get { return (string)GetData(CasContractTemplateTable.C_LAST_MODIFIED_BY); }
            set { SetData(CasContractTemplateTable.C_LAST_MODIFIED_BY, value); }
        }

        public DateTime? LastModifiedTime
        {
            get { return (DateTime?)(GetData(CasContractTemplateTable.C_LAST_MODIFIED_TIME)); }
            set { SetData(CasContractTemplateTable.C_LAST_MODIFIED_TIME, value); }
        }

        public string Company
        {
            get { return (string)GetData(CasContractTemplateTable.C_COMPANY); }
            set { SetData(CasContractTemplateTable.C_COMPANY, value); }
        }

        public int? status
        {
            get { return (int?)GetData(CasContractTemplateTable.C_STATUS); }
            set { SetData(CasContractTemplateTable.C_STATUS, value); }
        }
        /// <summary>
        /// 上传附件用
        /// </summary>
        public string fileIds
        {
            get;
            set;
        }
        #endregion
    }
}
