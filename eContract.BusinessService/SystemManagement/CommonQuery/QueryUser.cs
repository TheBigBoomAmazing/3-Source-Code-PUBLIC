using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eContract.Common.Entity;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;
using eContract.Common.Const;
using eContract.Common;
using eContract.Common.MVC;
using eContract.Common.WebUtils;

namespace eContract.BusinessService.SystemManagement.CommonQuery
{
    public class QueryUser : CasUserEntity, IQuery
    {
        public string systemName = "";
        public LigerGrid ligerGrid;
        public string name = "";

        public WhereBuilder ParseSQL()
        {  
            string sql = "SELECT A.*, ";
            sql += " case a.gender when 0 then '男' when 1 then '女' else '保密' end as xb_name ";
            sql += " FROM  CAS_USER A  where 1 = 1 ";

            if (name != "")
            {
                sql += ($@" AND ( CHINESE_NAME LIKE N'%{name}%' OR ENGLISH_NAME LIKE N'%{name}%') ");
            }
            //if (!string.IsNullOrEmpty(ligerGrid.keyWord))
            //{
            //    sql += " AND (A." + CasUserTable.C_USER_ACCOUNT + "   LIKE '%" + Utils.ToSQLStr(ligerGrid.keyWord) + "%' or A." + CasUserTable.C_CHINESE_NAME + "   LIKE '%" + Utils.ToSQLStr(ligerGrid.keyWord) + "%' or A." + CasUserTable.C_MOBILE_PHONE + "   LIKE '%" + Utils.ToSQLStr(ligerGrid.keyWord) + "%' or A." + CasUserTable.C_EMAIL + "   LIKE '%" + Utils.ToSQLStr(ligerGrid.keyWord) + "%')";
            //}
            WhereBuilder wb = new WhereBuilder(sql);
            return wb;
        }

        public WhereBuilder GetUserRoleSQL()
        {

            string sql = "select * from (select b.USER_ROLE_ID,b.USER_ID ,a.role_id,a.role_name,a.role_type,case a.role_type when 0 then '系统管理' when 1 then '普通用户' end as role_type_name,a.remark ";
            sql += " from sec_role a left join SEC_USER_ROLE b on a.role_id=b.ROLE_ID ";
            string userid = "";
            ligerGrid.HasKey("id", ref userid);
            if (!string.IsNullOrEmpty(userid))
                sql += "and b.USER_ID='" + userid + "'";
            sql += " where 1=1 ";
            if (!string.IsNullOrEmpty(systemName))
            {
                sql += " and a.system_name='" + systemName + "' ";
            }
            sql += ")tb";
            WhereBuilder wb = new WhereBuilder(sql);
            return wb;
        }

        
    }
}
