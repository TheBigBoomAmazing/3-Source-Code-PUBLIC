using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eContract.BusinessService.Common;
using eContract.BusinessService.SystemManagement.Domain;
using eContract.Common.Entity;
using Suzsoft.Smart.Data;
using eContract.DataAccessLayer;
using eContract.Common.Schema;
using System.Data;
using eContract.DataAccessLayer.SystemManagement;
using eContract.Common;
using Suzsoft.Smart.EntityCore; 
using eContract.BusinessService.SystemManagement.Service; 

namespace eContract.BusinessService.SystemManagement.BusinessRule
{
    public class BusinessRoleBLL : BusinessBase
    {
        /// <summary>
        /// 根据roleId获取SecUserRoleEntity集
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public virtual List<SecUserRoleEntity> GetBusinessRoleUserDomainByRoleId(string roleId)
        {
            eContract.Common.Utils.QueryCondition qCondition = eContract.Common.Utils.QueryCondition.Create().Equals(SecUserRoleTable.C_ROLE_ID, roleId);
            return SelectByCondition<SecUserRoleEntity>(qCondition);
        }

        /// <summary>
        /// 根据roleName获取BusinessRoleDomain
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public virtual BusinessRoleDomain GetBusinessRoleDomainByRoleName(string roleName)
        {
            SecRoleEntity SecRoleEntity = new SecRoleEntity();
            SecRoleEntity.RoleName = roleName;
            SecRoleEntity = DataAccess.SelectSingle<SecRoleEntity>(SecRoleEntity);
            if (SecRoleEntity == null)
            {
                return null;
            }
            else
            {
                return new BusinessRoleDomain(SecRoleEntity);
            }
        }

        /// <summary>
        /// 根据用户userid获取SecUserRoleEntity集
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public virtual List<SecUserRoleEntity> GetBusinessRoleUserDomainByUserid(string userid)
        {
            eContract.Common.Utils.QueryCondition qCondition = eContract.Common.Utils.QueryCondition.Create().Equals(SecUserRoleTable.C_USER_ID, userid);
            return SelectByCondition<SecUserRoleEntity>(qCondition);
        }

    }
}
