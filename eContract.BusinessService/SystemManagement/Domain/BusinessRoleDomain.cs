using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eContract.Common.Entity;
using eContract.BusinessService.Common;
using eContract.BusinessService.SystemManagement.Service;
using eContract.Common;

namespace eContract.BusinessService.SystemManagement.Domain
{
   public class BusinessRoleDomain:DomainBase
    {
       /// <summary>
       /// 构造函数
       /// </summary>
       /// <param name="SecRoleEntity"></param>
       public BusinessRoleDomain(SecRoleEntity SecRoleEntity)
           : base(SecRoleEntity)
       { }
       
       /// <summary>
       /// Domain持有的角色实体
       /// </summary>
       public SecRoleEntity SecRoleEntity
       {
           get 
           { 
               return Entity as SecRoleEntity;
           }
           set 
           {
               Entity = value;
           }
       }


       protected List<UserDomain> userDomainList;
       /// <summary>
       /// 当前BusinessRoleDomain包含的UserDomain列表 Add By Shakken Xie on 2010 - 4 - 1
       /// </summary> 
       public List<UserDomain> UserDomainList
       {
           get
           {
               if (null == userDomainList)
               {
                   userDomainList = SystemService.UserService.GetUserDomainListByRoleId(SecRoleEntity.SystemName,SecRoleEntity.RoleId);
               }
               return userDomainList;
           }
       }

        #region 数据验证
       /// <summary>
       /// 基本数据验证
       /// </summary>
       /// <returns></returns>
       private string BaseValidate()
       {
           if (string.IsNullOrEmpty(SecRoleEntity.RoleId))
           {
               return "SYSTEM_ROLE_MESSAGE_ROLEID";
           }
           if (string.IsNullOrEmpty(SecRoleEntity.RoleName))
           {
               return "SYSTEM_ROLE_MESSAGE_ROLENAME";
           }
           if (SecRoleEntity.Remark.Length > 500)
           {
               return "SYSTEM_ROLE_MSG_REMARK";
           }
           return string.Empty;
       }

       /// <summary>
       /// 新增角色时验证数据合法性
       /// </summary>
       /// <returns></returns>
       public virtual string ValidateInsert()
       {
           string baseResult = BaseValidate();
           if (!string.IsNullOrEmpty(baseResult))
           {
               return baseResult;
           }
           BusinessRoleDomain businessRoleDomain = SystemService.BusinessionRoleService.GetBusinessRoleDomainByRoleName(SecRoleEntity.RoleName);
           if (null != businessRoleDomain)
           {
               return "SYSTEM_ROLE_MESSAGE_EXISTS_ROLENAME";
           }
           else
           {
               return string.Empty;
           }
       }

       /// <summary>
       /// 修改角色时验证数据合法性
       /// </summary>
       /// <returns></returns>
       public virtual string ValidateUpdate()
       {
           string baseResult = BaseValidate();
           if (!string.IsNullOrEmpty(baseResult))
           {
               return baseResult;
           }
           BusinessRoleDomain businessRoleDomain = SystemService.BusinessionRoleService.GetBusinessRoleDomainByRoleName(SecRoleEntity.RoleName);
           if (null != businessRoleDomain && businessRoleDomain.SecRoleEntity.RoleId != SecRoleEntity.RoleId)
           {
               return "SYSTEM_ROLE_MESSAGE_EXISTS_ROLENAME";
           }
           else
           {
               return string.Empty;
           }
       }
        #endregion
    }
}
