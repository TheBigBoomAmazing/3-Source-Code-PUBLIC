using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;

namespace eContract.Common.Entity
{
    [Serializable]
    public partial class SecUserRoleEntity : EntityBase
    {
        public SecUserRoleTable TableSchema
        {
            get
            {
                return SecUserRoleTable.Current;
            }
        }


        public SecUserRoleEntity()
        {
            IsDeleted = false;
        }

        public override TableInfo OringTableSchema
        {
            get
            {
                return SecUserRoleTable.Current;
            }
        }
        #region 属性列表

        public string UserRoleId
        {
            get { return (string)GetData(SecUserRoleTable.C_USER_ROLE_ID); }
            set { SetData(SecUserRoleTable.C_USER_ROLE_ID, value); }
        }

        public string UserId
        {
            get { return (string)GetData(SecUserRoleTable.C_USER_ID); }
            set { SetData(SecUserRoleTable.C_USER_ID, value); }
        }

        public string RoleId
        {
            get { return (string)GetData(SecUserRoleTable.C_ROLE_ID); }
            set { SetData(SecUserRoleTable.C_ROLE_ID, value); }
        }

        public string SystemName
        {
            get { return (string)GetData(SecUserRoleTable.C_SYSTEM_NAME); }
            set { SetData(SecUserRoleTable.C_SYSTEM_NAME, value); }
        }

        public string Remark
        {
            get { return (string)GetData(SecUserRoleTable.C_REMARK); }
            set { SetData(SecUserRoleTable.C_REMARK, value); }
        }

        public bool IsDeleted
        {
            get { return (bool)(GetData(SecUserRoleTable.C_IS_DELETED)); }
            set { SetData(SecUserRoleTable.C_IS_DELETED, value); }
        }

        public string CreatedBy
        {
            get { return (string)GetData(SecUserRoleTable.C_CREATED_BY); }
            set { SetData(SecUserRoleTable.C_CREATED_BY, value); }
        }

        public DateTime CreateTime
        {
            get { return (DateTime)(GetData(SecUserRoleTable.C_CREATE_TIME)); }
            set { SetData(SecUserRoleTable.C_CREATE_TIME, value); }
        }

        public string LastModifiedBy
        {
            get { return (string)GetData(SecUserRoleTable.C_LAST_MODIFIED_BY); }
            set { SetData(SecUserRoleTable.C_LAST_MODIFIED_BY, value); }
        }

        public DateTime LastModifiedTime
        {
            get { return (DateTime)(GetData(SecUserRoleTable.C_LAST_MODIFIED_TIME)); }
            set { SetData(SecUserRoleTable.C_LAST_MODIFIED_TIME, value); }
        }

        #endregion
    }
}
