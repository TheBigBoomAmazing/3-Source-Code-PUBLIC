using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;

namespace eContract.Common.Entity
{
    [Serializable]
    public partial class SecRolePageEntity : EntityBase
    {
        public SecRolePageTable TableSchema
        {
            get
            {
                return SecRolePageTable.Current;
            }
        }


        public SecRolePageEntity()
        {
           
        }

        public override TableInfo OringTableSchema
        {
            get
            {
                return SecRolePageTable.Current;
            }
        }
        #region 属性列表

        public string RoleId
        {
            get { return (string)GetData(SecRolePageTable.C_ROLE_ID).ToString(); }
            set { SetData(SecRolePageTable.C_ROLE_ID, value); }
        }

        public string PageId
        {
            get { return (string)GetData(SecRolePageTable.C_PAGE_ID).ToString(); }
            set { SetData(SecRolePageTable.C_PAGE_ID, value); }
        }

        public string LastModifiedBy
        {
            get { return (string)GetData(SecRolePageTable.C_LAST_MODIFIED_BY); }
            set { SetData(SecRolePageTable.C_LAST_MODIFIED_BY, value); }
        }

        public DateTime LastModifiedTime
        {
            get { return (DateTime)(GetData(SecRolePageTable.C_LAST_MODIFIED_TIME)); }
            set { SetData(SecRolePageTable.C_LAST_MODIFIED_TIME, value); }
        }

        #endregion
    }
}
