using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;

namespace eContract.Common.Entity
{
    [Serializable]
    public partial class SecPageEntity : EntityBase
    {
        public SecPageTable TableSchema
        {
            get
            {
                return SecPageTable.Current;
            }
        }


        public SecPageEntity()
        {
            IsDeleted = false;
        }

        public override TableInfo OringTableSchema
        {
            get
            {
                return SecPageTable.Current;
            }
        }
        #region 属性列表

        public string PageId
        {
            get {
                if (GetData(SecPageTable.C_PAGE_ID) == null) {
                    return "";
                }
                return GetData(SecPageTable.C_PAGE_ID).ToString(); }
            set { SetData(SecPageTable.C_PAGE_ID, value); }
        }

        public string PageName
        {
            get { return (string)GetData(SecPageTable.C_PAGE_NAME); }
            set { SetData(SecPageTable.C_PAGE_NAME, value); }
        }

        public string PageNameEn
        {
            get { return (string)GetData(SecPageTable.C_PAGE_NAME_EN); }
            set { SetData(SecPageTable.C_PAGE_NAME_EN, value); }
        }

        public string ParentId
        {
            get
            {
                if (GetData(SecPageTable.C_PARENT_ID) == null)
                {
                    return "";
                }
                return GetData(SecPageTable.C_PARENT_ID).ToString();
            }
            set { SetData(SecPageTable.C_PARENT_ID, value); }
        }

        public string PageUrl
        {
            get { return (string)GetData(SecPageTable.C_PAGE_URL); }
            set { SetData(SecPageTable.C_PAGE_URL, value); }
        }

        public bool IsMenu
        {
            get
            {
                if (GetData(SecPageTable.C_IS_MENU) == null)
                {
                    return false;
                }
                return (bool)(GetData(SecPageTable.C_IS_MENU));
            }
            set { SetData(SecPageTable.C_IS_MENU, value); }
        }

        public int MenuLevel
        {
            get
            {
                if (GetData(SecPageTable.C_MENU_LEVEL) == null)
                {

                    return -999;
                }
                return (int)(GetData(SecPageTable.C_MENU_LEVEL));
            }
            set { SetData(SecPageTable.C_MENU_LEVEL, value); }
        }

        public int MenuOrder
        {
            get {
                if (GetData(SecPageTable.C_MENU_ORDER) == null) {
                    return -999;
                }
                return (int)(GetData(SecPageTable.C_MENU_ORDER)); }
            set { SetData(SecPageTable.C_MENU_ORDER, value); }
        }

        public int PageType
        {
            get {
                if (GetData(SecPageTable.C_PAGE_TYPE) == null) {
                    return -999;
                }
                return (int)(GetData(SecPageTable.C_PAGE_TYPE)); }
            set { SetData(SecPageTable.C_PAGE_TYPE, value); }
        }

        public string SystemName
        {
            get { return (string)GetData(SecPageTable.C_SYSTEM_NAME); }
            set { SetData(SecPageTable.C_SYSTEM_NAME, value); }
        }

        public string Remark
        {
            get { return (string)GetData(SecPageTable.C_REMARK); }
            set { SetData(SecPageTable.C_REMARK, value); }
        }

        public bool IsDeleted
        {
            get {
                if (GetData(SecPageTable.C_IS_DELETED) == null) {
                    return false;
                }
                return (bool)(GetData(SecPageTable.C_IS_DELETED)); }
            set { SetData(SecPageTable.C_IS_DELETED, value); }
        }

        public string CreatedBy
        {
            get { return (string)GetData(SecPageTable.C_CREATED_BY); }
            set { SetData(SecPageTable.C_CREATED_BY, value); }
        }

        public DateTime CreateTime
        {
            get {
                if ((GetData(SecPageTable.C_CREATE_TIME) == null)){
                    return DateTime.MinValue;
                }
                return (DateTime)(GetData(SecPageTable.C_CREATE_TIME)); }
            set { SetData(SecPageTable.C_CREATE_TIME, value); }
        }

        public string LastModifiedBy
        {
            get { return (string)GetData(SecPageTable.C_LAST_MODIFIED_BY); }
            set { SetData(SecPageTable.C_LAST_MODIFIED_BY, value); }
        }

        public DateTime LastModifiedTime
        {
            get {
                if (GetData(SecPageTable.C_LAST_MODIFIED_TIME) == null) {
                    return DateTime.MinValue;
                }
                return (DateTime)(GetData(SecPageTable.C_LAST_MODIFIED_TIME)); }
            set { SetData(SecPageTable.C_LAST_MODIFIED_TIME, value); }
        }

        #endregion
    }
}
