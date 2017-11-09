using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suzsoft.Smart.Data;
using eContract.Common.Schema;
using Suzsoft.Smart.EntityCore;
using eContract.Common;
using eContract.Common.Entity;
using System.Data;
using eContract.Common.Const;

namespace eContract.DataAccessLayer.SystemManagement
{
    public static class UserDA
    {
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="broker"></param>
        public static void DeleteUser(string userId, DataAccessBroker broker)
        {
            DateTime time = DateTime.Now;
            string now = time.Year.ToString() + "-" + time.Month.ToString() + "-" + time.Day.ToString() + " " + time.Hour.ToString() + "-" + time.Minute.ToString() + "-" + time.Second.ToString();
            string sql = "update {0} set {1}={2}, {3}=to_date('{4}','YYYY-MM-DD HH24-MI-SS') where {5}='{6}'";
            broker.ExecuteSQL(sql);
        }

        /// <summary>
        /// 根据RoleID 返回用户实体 列表 Add by shakken xie on 2010-4-1
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static List<CasUserEntity> GetUserEntityListByRoleId(string roleId)
        {
            string sql = "Select * From " + CasUserTable.C_TableName + "  mdb ," + SecUserRoleTable.C_TableName + "  tmpdb Where mdb." + CasUserTable.C_USER_ID + "=tmpdb." + SecUserRoleTable.C_USER_ID;
            sql += " And tmpdb." + SecUserRoleTable.C_ROLE_ID + "='" + roleId + "'";

            return DataAccess.Select<CasUserEntity>(sql);
        }

        /// <summary>
        /// 获取指定用户所在SecRoleEntity列表 Add by Shakken Xie on 2010-4-13
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<SecRoleEntity> GetRoleEntityListByUserId(string systemName,string userId)
        {
            string sql = "Select * From " + SecRoleTable.C_TableName + " mtb," + SecUserRoleTable.C_TableName + " tmp ";
            sql += " Where mtb." + SecRoleTable.C_ROLE_ID + "=tmp." + SecUserRoleTable.C_ROLE_ID;
            sql += " And tmp." + SecUserRoleTable.C_USER_ID + "='" + userId + "'";
            sql += " And mtb." + SecRoleTable.C_SYSTEM_NAME + "='" + systemName + "'";
            return DataAccess.Select<SecRoleEntity>(sql);
        }

        /// <summary>
        /// 获取指定用户所在功能角色列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<SecRoleEntity> GetFunctionRoleEntityListByUserId(string systemName,string userId)
        {
            string sql = "Select * From " + SecRoleTable.C_TableName + " mtb," + SecUserRoleTable.C_TableName + " tmp ";
            sql += " Where mtb." + SecRoleTable.C_ROLE_ID + "=tmp." + SecUserRoleTable.C_ROLE_ID;
            sql += " And tmp." + SecUserRoleTable.C_USER_ID + "='" + userId + "'";
            sql += " And mtb." + SecRoleTable.C_SYSTEM_NAME + "='" + systemName + "'";
            return DataAccess.Select<SecRoleEntity>(sql);
        }

        public static List<SecRolePageEntity> GetRoleMenuEntityByUserId(string systemName,string userId)
        {
            string sql = " select distinct c.* from " + SecUserRoleTable.C_TableName + " a ," + SecRoleTable.C_TableName + " b ," + SecRolePageTable.C_TableName + " c ," + SecPageTable.C_TableName + " d ";
            sql += " where a." + SecUserRoleTable.C_ROLE_ID + " = b." + SecRoleTable.C_ROLE_ID;
            sql += " and b." + SecRoleTable.C_ROLE_ID + " = c." + SecRolePageTable.C_ROLE_ID;
            sql += " and c." + SecRolePageTable.C_PAGE_ID + " = d." + SecPageTable.C_PAGE_ID;
            sql += " and a." + SecUserRoleTable.C_USER_ID + " = '" + userId + "'";
            sql += " And b." + SecRoleTable.C_SYSTEM_NAME + "='" + systemName + "'";
            return DataAccess.Select<SecRolePageEntity>(sql);
        }
    }
}
