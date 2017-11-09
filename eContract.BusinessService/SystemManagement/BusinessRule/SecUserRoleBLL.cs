using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;
using eContract.Common.Entity;
using eContract.BusinessService.Common;
using Suzsoft.Smart.Data;
using eContract.Common.Const;
using eContract.BusinessService.SystemManagement.Domain;
using eContract.DataAccessLayer.SystemManagement;
using eContract.Common.WebUtils;
using eContract.BusinessService.SystemManagement.Service;

namespace eContract.BusinessService.SystemManagement.BusinessRule
{
    public class SecUserRoleBLL: BusinessBase
    {

        /// <summary>
        /// 根据roleId值获取所有关联的RoleMenu实体
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public virtual List<SecUserRoleEntity> GetPagesByRoleId(string roleId)
        {
            eContract.Common.Utils.QueryCondition qCondition = eContract.Common.Utils.QueryCondition.Create().Equals(SecUserRoleTable.C_ROLE_ID, roleId);
            return SelectByCondition<SecUserRoleEntity>(qCondition);
        }

        /// <summary>
        /// 删除批量
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public virtual bool DeleteUserRoleDomainByIds(List<string> list)
        {
            if (Delete<SecUserRoleEntity>(list))
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除批量
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public virtual bool DeleteUserRoleDomainByIds(string deletekeys, ref string strError)
        {
            List<string> list = deletekeys.Split(new char[] { ';', ',' }).ToList<string>();
            try
            {
                return DeleteUserRoleDomainByIds(list);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            return false;
        }
    }
}
