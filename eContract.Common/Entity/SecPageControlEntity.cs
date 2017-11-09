using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;

namespace eContract.Common.Entity
{
    [Serializable]
    public partial class SecPageControlEntity : EntityBase
    {
        public SecPageControlTable TableSchema
        {
            get
            {
                return SecPageControlTable.Current;
            }
        }


        public SecPageControlEntity()
        {
            IsDeleted = false;
        }

        public override TableInfo OringTableSchema
        {
            get
            {
                return SecPageControlTable.Current;
            }
        }
        #region 属性列表

        public string ControlId
        {
            get { return (string)GetData(SecPageControlTable.C_CONTROL_ID); }
            set { SetData(SecPageControlTable.C_CONTROL_ID, value); }
        }

        public string PageId
        {
            get { return (string)GetData(SecPageControlTable.C_PAGE_ID); }
            set { SetData(SecPageControlTable.C_PAGE_ID, value); }
        }

        public string ControlName
        {
            get { return (string)GetData(SecPageControlTable.C_CONTROL_NAME); }
            set { SetData(SecPageControlTable.C_CONTROL_NAME, value); }
        }

        public string ServerId
        {
            get { return (string)GetData(SecPageControlTable.C_SERVER_ID); }
            set { SetData(SecPageControlTable.C_SERVER_ID, value); }
        }

        public string PageType
        {
            get { return (string)GetData(SecPageControlTable.C_PAGE_TYPE); }
            set { SetData(SecPageControlTable.C_PAGE_TYPE, value); }
        }

        public bool IsDeleted
        {
            get { return (bool)(GetData(SecPageControlTable.C_IS_DELETED)); }
            set { SetData(SecPageControlTable.C_IS_DELETED, value); }
        }

        public string CreatedBy
        {
            get { return (string)GetData(SecPageControlTable.C_CREATED_BY); }
            set { SetData(SecPageControlTable.C_CREATED_BY, value); }
        }

        public DateTime CreateTime
        {
            get { return (DateTime)(GetData(SecPageControlTable.C_CREATE_TIME)); }
            set { SetData(SecPageControlTable.C_CREATE_TIME, value); }
        }

        public string LastModifiedBy
        {
            get { return (string)GetData(SecPageControlTable.C_LAST_MODIFIED_BY); }
            set { SetData(SecPageControlTable.C_LAST_MODIFIED_BY, value); }
        }

        public DateTime LastModifiedTime
        {
            get { return (DateTime)(GetData(SecPageControlTable.C_LAST_MODIFIED_TIME)); }
            set { SetData(SecPageControlTable.C_LAST_MODIFIED_TIME, value); }
        }

        #endregion
    }
}
