using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eContract.Common.Entity;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;
using eContract.Common.Const;
using eContract.Common;
using eContract.BusinessService.Common;

namespace eContract.BusinessService.SystemManagement.CommonQuery
{
    public class SecRoleQuery : IQuery
    {
        public string systemName = "";
        public LigerGrid ligerGrid;
        public WhereBuilder ParseSQL()
        {
            string roletype = "";
            string strSql = "select a.role_id,a.role_name,a.role_type,a.remark,a.LAST_MODIFIED_TIME from sec_role a where 1=1 ";
            //if (!string.IsNullOrEmpty(ligerGrid.keyWord))
            //{
            //    strSql += " and (a.role_id like '%" + Utils.ToSQLStr(ligerGrid.keyWord) + "%' or a.role_name like '%" + Utils.ToSQLStr(ligerGrid.keyWord) + "%')";
            //}
            //if (ligerGrid.HasKey("role_type_name", ref roletype))
            //{
            //    strSql += " and a.role_type=" + roletype;
            //}
            //if (!string.IsNullOrEmpty(systemName))
            //{
            //    strSql += " and a.system_name='" + systemName + "' ";
            //}

            //strSql += ")tb";/*select * from (*/
            WhereBuilder wb = new WhereBuilder(strSql);
            return wb;
        }

        public WhereBuilder RoleAddSQL()
        {
            string userid = "";
            string strSql = "select a.*  from SEC_ROLE a left join SEC_USER_ROLE b on a.ROLE_ID=b.ROLE_ID";
            ligerGrid.HasKey("id", ref userid);
            strSql += " and [USER_ID]='" + userid + "' where b.ROLE_ID IS NULL";
            if (!string.IsNullOrEmpty(systemName))
                strSql += " and a.system_name='" + systemName + "' ";
            WhereBuilder wb = new WhereBuilder(strSql);
            return wb;
        }
    }
}
