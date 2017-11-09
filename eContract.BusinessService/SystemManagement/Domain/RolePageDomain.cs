using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eContract.BusinessService.Common;
using eContract.Common.Entity;
using eContract.BusinessService.SystemManagement.Service;
using eContract.Common.Const;

namespace eContract.BusinessService.SystemManagement.Domain
{
    public class RolePageDomain : DomainBase
    {
        /// <summary>
        /// Domain持有的实体
        /// </summary>
        public SecRolePageEntity SecRolePageEntity
        {
            get
            {
                return Entity as SecRolePageEntity;
            }
            set
            {
                Entity = value;
            }
        }

        public RolePageDomain(SecRolePageEntity roleMenu)
            : base(roleMenu)
        { }

        /// <summary>
        /// 根据RoleId获取对应的RoleMenuDomain集
        /// </summary>
        List<RolePageDomain> roleMenuDomains;
        public List<RolePageDomain> RoleMenuDomains
        {
            get
            {
                if (roleMenuDomains == null)
                {
                    List<SecRolePageEntity> roleMenuEtys = SystemService.RoleMenuService.GetPagesByRoleId(SecRolePageEntity.RoleId);
                    foreach (SecRolePageEntity roleMenuEty in roleMenuEtys)
                    {
                        roleMenuDomains.Add(new RolePageDomain(roleMenuEty));
                    }
                }
                return roleMenuDomains;
            }
        }

    }
}
