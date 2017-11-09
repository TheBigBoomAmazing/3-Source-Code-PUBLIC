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
    public class RolePageBLL : BusinessBase
    {

        /// <summary>
        /// 根据roleId值获取所有关联的RoleMenu实体
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public virtual List<SecRolePageEntity> GetPagesByRoleId(string roleId)
        {
            eContract.Common.Utils.QueryCondition qCondition = eContract.Common.Utils.QueryCondition.Create().Equals(SecRolePageTable.C_ROLE_ID, roleId).Order(SecRolePageTable.C_LAST_MODIFIED_TIME);
            return SelectByCondition<SecRolePageEntity>(qCondition);
        }


        /// <summary>
        /// 根据roleId,moduleCode获取所有关联的RoleMenu实体
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public virtual List<SecRolePageEntity> GetPageMenus(string roleId, string moduleCode)
        {
            return FunctionRoleDA.GetRoleMenu(roleId, moduleCode);
        }

        /// <summary>
        /// 增加/修改角色关联的菜单 lucas 2011-04-08
        /// </summary>
        /// <param name="oldRoleMenu"></param>
        /// <param name="newRoleMenu"></param>
        /// <returns></returns>
        public virtual bool AddRoleMenu(string systemName,List<RolePageDomain> oldRoleMenuDomain, List<RolePageDomain> newRoleMenuDomain)
        {
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                try
                {
                    broker.BeginTransaction();
                    //获取roleMenu、DataScope 实体列表
                    List<SecRolePageEntity> oldRoleMenu = new List<SecRolePageEntity>();
                    foreach (RolePageDomain oldMenu in oldRoleMenuDomain)
                    {
                        oldRoleMenu.Add(oldMenu.SecRolePageEntity);
                    }
                    List<SecRolePageEntity> newRoleMenu = new List<SecRolePageEntity>();
                    foreach (RolePageDomain newMenu in newRoleMenuDomain)
                    {
                        newRoleMenu.Add(newMenu.SecRolePageEntity);
                    }

                    //执行数据操作
                    DataAccess.Delete<SecRolePageEntity>(oldRoleMenu, broker);
                    DataAccess.Insert<SecRolePageEntity>(newRoleMenu, broker);

                    UserDomain user = (new UserBLL()).GetUserDomainByUserAccount(systemName,CurrentUser.CasUserEntity.UserAccount);

                    WebCaching.CurrentUser = user;

                    broker.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    SystemService.LogErrorService.InsertLog(ex);
                    broker.RollBack();
                    return false;
                }
            }
        }

        public virtual bool Save(string roleid, string strPageIds, ref string strError)
        {
            if(string.IsNullOrEmpty(roleid))
            {
                return false;
            }
            
            string[] arr= strPageIds.Split(new char[]{','});
            List<SecRolePageEntity> lst = new List<SecRolePageEntity>();
            if (arr != null && arr.Length > 0)
            {
              
                foreach (var item in arr)
                {
                    lst.Add(new SecRolePageEntity { 
                        RoleId=roleid,
                        PageId=item,
                        LastModifiedBy=WebCaching.UserId,
                        LastModifiedTime=DateTime.Now
                    });
                }
            }
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                try
                {
                    foreach (var item in lst)
                    {
                        DataAccess.ExcuteNoneQuery("delete SEC_ROLE_PAGE where ROLE_ID='" + item.RoleId + "'", broker);
                    }
                    if (lst != null && lst.Count > 0)
                    {
                        DataAccess.Insert<SecRolePageEntity>(lst, broker);
                    }
                    broker.Commit();
                }
                catch (Exception ex)
                {
                    SystemService.LogErrorService.InsertLog(ex);
                    broker.RollBack();
                    return false;
                }
            }
            return true;
        }
    }

 
}
