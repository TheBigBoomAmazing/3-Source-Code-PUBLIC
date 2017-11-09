using System;
using System.Collections.Generic;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;

namespace eContract.Common.Entity
{
    [Serializable]
    public partial class CasDepartmentEntity : EntityBase
    {
        public CasDepartmentTable TableSchema
        {
            get
            {
                return CasDepartmentTable.Current;
            }
        }


        public CasDepartmentEntity()
        {
            IsDeleted = false;
        }

        public override TableInfo OringTableSchema
        {
            get
            {
                return CasDepartmentTable.Current;
            }
        }
        #region 属性列表

        public string DeptId
        {
            get { return (string)GetData(CasDepartmentTable.C_DEPT_ID); }
            set { SetData(CasDepartmentTable.C_DEPT_ID, value); }
        }

        public string DeptCode
        {
            get { return (string)GetData(CasDepartmentTable.C_DEPT_CODE); }
            set { SetData(CasDepartmentTable.C_DEPT_CODE, value); }
        }

        public string DeptName
        {
            get { return (string)GetData(CasDepartmentTable.C_DEPT_NAME); }
            set { SetData(CasDepartmentTable.C_DEPT_NAME, value); }
        }

        public string DeptAlias
        {
            get { return (string)GetData(CasDepartmentTable.C_DEPT_ALIAS); }
            set { SetData(CasDepartmentTable.C_DEPT_ALIAS, value); }
        }

        public int? DeptType
        {
            get { return (int?)(GetData(CasDepartmentTable.C_DEPT_TYPE)); }
            set { SetData(CasDepartmentTable.C_DEPT_TYPE, value); }
        }

        public string DeptManagerId
        {
            get { return (string)GetData(CasDepartmentTable.C_DEPT_MANAGER_ID); }
            set { SetData(CasDepartmentTable.C_DEPT_MANAGER_ID, value); }
        }

        public string CompanyCode
        {
            get { return (string)GetData(CasDepartmentTable.C_COMPANY_CODE); }
            set { SetData(CasDepartmentTable.C_COMPANY_CODE, value); }
        }

        public bool? IsDeleted
        {
            get { return (bool?)(GetData(CasDepartmentTable.C_IS_DELETED)); }
            set { SetData(CasDepartmentTable.C_IS_DELETED, value); }
        }

        public string CreatedBy
        {
            get { return (string)GetData(CasDepartmentTable.C_CREATED_BY); }
            set { SetData(CasDepartmentTable.C_CREATED_BY, value); }
        }

        public DateTime? CreateTime
        {
            get { return (DateTime?)(GetData(CasDepartmentTable.C_CREATE_TIME)); }
            set { SetData(CasDepartmentTable.C_CREATE_TIME, value); }
        }

        public string LastModifiedBy
        {
            get { return (string)GetData(CasDepartmentTable.C_LAST_MODIFIED_BY); }
            set { SetData(CasDepartmentTable.C_LAST_MODIFIED_BY, value); }
        }

        public DateTime? LastModifiedTime
        {
            get { return (DateTime?)(GetData(CasDepartmentTable.C_LAST_MODIFIED_TIME)); }
            set { SetData(CasDepartmentTable.C_LAST_MODIFIED_TIME, value); }
        }

        public string DeptUserIds
        {
            get;
            set;
        }
        #endregion
    }
}
