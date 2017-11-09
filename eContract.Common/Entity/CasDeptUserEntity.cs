using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;

namespace eContract.Common.Entity
{
    [Serializable]
    public partial class CasDeptUserEntity : EntityBase
    {
        public CasDeptUserTable TableSchema
        {
            get
            {
                return CasDeptUserTable.Current;
            }
        }


        public CasDeptUserEntity()
        {
            IsDeleted = false;
        }

        public override TableInfo OringTableSchema
        {
            get
            {
                return CasDeptUserTable.Current;
            }
        }
        #region 属性列表

        public string DeptId
        {
            get { return (string)GetData(CasDeptUserTable.C_DEPT_ID); }
            set { SetData(CasDeptUserTable.C_DEPT_ID, value); }
        }

        public string UserId
        {
            get { return (string)GetData(CasDeptUserTable.C_USER_ID); }
            set { SetData(CasDeptUserTable.C_USER_ID, value); }
        }

        public bool IsDeleted
        {
            get { return (bool)(GetData(CasDeptUserTable.C_IS_DELETED)); }
            set { SetData(CasDeptUserTable.C_IS_DELETED, value); }
        }

        public string CreatedBy
        {
            get { return (string)GetData(CasDeptUserTable.C_CREATED_BY); }
            set { SetData(CasDeptUserTable.C_CREATED_BY, value); }
        }

        public DateTime CreateTime
        {
            get { return (DateTime)(GetData(CasDeptUserTable.C_CREATE_TIME)); }
            set { SetData(CasDeptUserTable.C_CREATE_TIME, value); }
        }

        public string LastModifiedBy
        {
            get { return (string)GetData(CasDeptUserTable.C_LAST_MODIFIED_BY); }
            set { SetData(CasDeptUserTable.C_LAST_MODIFIED_BY, value); }
        }

        public DateTime LastModifiedTime
        {
            get { return (DateTime)(GetData(CasDeptUserTable.C_LAST_MODIFIED_TIME)); }
            set { SetData(CasDeptUserTable.C_LAST_MODIFIED_TIME, value); }
        }

        #endregion
    }
}
