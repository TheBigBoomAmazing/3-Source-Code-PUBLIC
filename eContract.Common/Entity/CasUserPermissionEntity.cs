using eContract.Common.Schema;
using Suzsoft.Smart.EntityCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eContract.Common.Entity
{
    [Serializable]
    public class CasUserPermissionEntity : EntityBase
    {
        public CasUserPermissionTable TableSchema
        {
            get
            {
                return CasUserPermissionTable.Current;
            }
        }


        public CasUserPermissionEntity()
        {
            IsDeleted = false;
        }

        public override TableInfo OringTableSchema
        {
            get
            {
                return CasUserPermissionTable.Current;
            }
        }
        #region 属性列表

        public string PermissionId
        {
            get { return (string)GetData(CasUserPermissionTable.C_PERMISSION_ID); }
            set { SetData(CasUserPermissionTable.C_PERMISSION_ID, value); }
        }

        public string UserId
        {
            get { return (string)GetData(CasUserPermissionTable.C_USER_ID); }
            set { SetData(CasUserPermissionTable.C_USER_ID, value); }
        }

        public string DeptId
        {
            get { return (string)GetData(CasUserPermissionTable.C_DEPT_ID); }
            set { SetData(CasUserPermissionTable.C_DEPT_ID, value); }
        }
        
        public bool? IsDeleted
        {
            get { return (bool?)(GetData(CasUserPermissionTable.C_IS_DELETED)); }
            set { SetData(CasUserPermissionTable.C_IS_DELETED, value); }
        }

        public string CreatedBy
        {
            get { return (string)GetData(CasUserPermissionTable.C_CREATED_BY); }
            set { SetData(CasUserPermissionTable.C_CREATED_BY, value); }
        }

        public DateTime? CreateTime
        {
            get { return (DateTime?)(GetData(CasUserPermissionTable.C_CREATE_TIME)); }
            set { SetData(CasUserPermissionTable.C_CREATE_TIME, value); }
        }

        public string LastModifiedBy
        {
            get { return (string)GetData(CasUserPermissionTable.C_LAST_MODIFIED_BY); }
            set { SetData(CasUserPermissionTable.C_LAST_MODIFIED_BY, value); }
        }

        public DateTime? LastModifiedTime
        {
            get { return (DateTime?)(GetData(CasUserPermissionTable.C_LAST_MODIFIED_TIME)); }
            set { SetData(CasUserPermissionTable.C_LAST_MODIFIED_TIME, value); }
        }

        #endregion
    }
}
