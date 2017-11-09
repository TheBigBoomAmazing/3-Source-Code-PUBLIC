using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;

namespace eContract.Common.Entity
{
    [Serializable]
    public partial class SecRoleEntity : EntityBase
    {
        public SecRoleTable TableSchema
        {
            get
            {
                return SecRoleTable.Current;
            }
        }


        public SecRoleEntity()
        {
            IsDeleted = false;
        }

        public override TableInfo OringTableSchema
        {
            get
            {
                return SecRoleTable.Current;
            }
        }
        #region 属性列表

        public string RoleId
        {
            get { return (string)GetData(SecRoleTable.C_ROLE_ID); }
            set { SetData(SecRoleTable.C_ROLE_ID, value); }
        }

        public string ParentId
        {
            get { return (string)GetData(SecRoleTable.C_PARENT_ID); }
            set { SetData(SecRoleTable.C_PARENT_ID, value); }
        }

        public string RoleName
        {
            get { return (string)GetData(SecRoleTable.C_ROLE_NAME); }
            set { SetData(SecRoleTable.C_ROLE_NAME, value); }
        }

        public int? RoleType
        {
            get { return (int?)(GetData(SecRoleTable.C_ROLE_TYPE)); }
            set { SetData(SecRoleTable.C_ROLE_TYPE, value); }
        }

        public bool? IsValid
        {
            get { return (bool?)(GetData(SecRoleTable.C_IS_VALID)); }
            set { SetData(SecRoleTable.C_IS_VALID, value); }
        }

        public string SystemName
        {
            get { return (string)GetData(SecRoleTable.C_SYSTEM_NAME); }
            set { SetData(SecRoleTable.C_SYSTEM_NAME, value); }
        }

        public string Remark
        {
            get { return (string)GetData(SecRoleTable.C_REMARK); }
            set { SetData(SecRoleTable.C_REMARK, value); }
        }

        public bool? IsDeleted
        {
            get { return (bool?)(GetData(SecRoleTable.C_IS_DELETED)); }
            set { SetData(SecRoleTable.C_IS_DELETED, value); }
        }

        public string CreatedBy
        {
            get { return (string)GetData(SecRoleTable.C_CREATED_BY); }
            set { SetData(SecRoleTable.C_CREATED_BY, value); }
        }

        public DateTime? CreateTime
        {
            get { return (DateTime)(GetData(SecRoleTable.C_CREATE_TIME)); }
            set { SetData(SecRoleTable.C_CREATE_TIME, value); }
        }

        public string LastModifiedBy
        {
            get { return (string)GetData(SecRoleTable.C_LAST_MODIFIED_BY); }
            set { SetData(SecRoleTable.C_LAST_MODIFIED_BY, value); }
        }

        public DateTime? LastModifiedTime
        {
            get { return (DateTime)(GetData(SecRoleTable.C_LAST_MODIFIED_TIME)); }
            set { SetData(SecRoleTable.C_LAST_MODIFIED_TIME, value); }
        }

        #endregion
    }
}
