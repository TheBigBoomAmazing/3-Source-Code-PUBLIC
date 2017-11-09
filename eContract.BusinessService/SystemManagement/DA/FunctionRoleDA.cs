using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eContract.Common.Schema;
using Suzsoft.Smart.Data;
using System.Data;
using eContract.Common.Entity;

namespace eContract.DataAccessLayer.SystemManagement
{
    public class FunctionRoleDA
    {
        /// <summary>
        /// 验证roleName
        /// </summary>
        /// <param name="parameterType"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public static DataSet CheckSystemRoleName(string roleName, string roleID)
        {
            string sql = "select " + SecRoleTable.C_ROLE_NAME
                + " from " + SecRoleTable.C_TableName
                + " where " + SecRoleTable.C_ROLE_NAME
                + " = '" + roleName + "' and "
                + SecRoleTable.C_ROLE_ID
                + " <> '" + roleID + "'";
            return DataAccess.Select(sql, null);
        }

        /// <summary>
        /// //验证RoleID ,USERID是否已经关联
        /// </summary>
        /// <returns></returns>
        public static DataSet GetSystemUserID(string RoleID, string UserID)
        {
            string sql = "select  " + SecUserRoleTable.C_USER_ID + " from " + SecUserRoleTable.C_TableName
                + " where " + SecUserRoleTable.C_ROLE_ID + "='" + RoleID + "' and  " + SecUserRoleTable.C_USER_ID + "='" + UserID + "'";
            return DataAccess.SelectDataSet(sql);
        }

        /// <summary>
        /// 依据角色Id、模块Id获取角色菜单=
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        public static List<SecRolePageEntity> GetRoleMenu(string roleId, string moduleCode)
        {
            string strSql = "  SELECT srm.* FROM SEC_ROLE_PAGE srm JOIN SEC_PAGE sp ON srm.PAGE_ID =sp.PAGE_ID ";
            strSql += "  WHERE srm.ROLE_ID = '" + roleId + "' ";
            return DataAccess.Select<SecRolePageEntity>(strSql);
        }
    }
}
