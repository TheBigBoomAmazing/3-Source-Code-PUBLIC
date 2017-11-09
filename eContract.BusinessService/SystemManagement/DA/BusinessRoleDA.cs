using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using eContract.Common.Schema;
using Suzsoft.Smart.Data;
using eContract.Common.Entity;
using eContract.Common;
using Suzsoft.Smart.EntityCore;


namespace eContract.DataAccessLayer.SystemManagement
{
   public class BusinessRoleDA
    {
        /// <summary>
        /// 根据roleType查出List<SecRoleEntity>, Add by Alex on 2010.3.20
        /// </summary>
        /// <param name="roleType"></param>
        /// <returns></returns>
        public static List<SecRoleEntity> GetRoleEntityByRoleType(string roleType)
        {
            List<SecRoleEntity> roleEntityList = new List<SecRoleEntity>();
            string sql = "select * from " + SecRoleTable.C_TableName;
            WhereBuilder wb = new WhereBuilder(sql);
            wb.FixFirstCondition = true;
            wb.AddAndCondition("1", "1");
            //根据RoleName来排序
            string sqlExtends = " order by " + SecRoleTable.C_TableName + "." + SecRoleTable.C_ROLE_NAME;
            wb.AddCondition(sqlExtends);
            roleEntityList = DataAccess.Select<SecRoleEntity>(wb);
            return roleEntityList;
        }

    }
}
