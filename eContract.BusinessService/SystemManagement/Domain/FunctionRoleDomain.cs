using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eContract.Common.Entity;
using eContract.BusinessService.Common;
using eContract.BusinessService.SystemManagement.Service;
using eContract.BusinessService.SystemManagement.BusinessRule;
using eContract.DataAccessLayer.SystemManagement;
using System.Data;
using eContract.Common.WebUtils;

namespace eContract.BusinessService.SystemManagement.Domain
{
    public class FunctionRoleDomain : DomainBase
    {
        #region 属性
        /// <summary>
        /// Domain持有的实体
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

        ///// <summary>
        ///// 该角色拥有权限的菜单数据项
        ///// </summary>
        //private List<MenuDataItem> menuDataItem;
        //public List<MenuDataItem> MenuDataItem
        //{
        //    get 
        //    {
        //        if (menuDataItem == null)
        //        {
        //            menuDataItem = SystemService.FunctionRoleService.GetMenuDataItemByRoleId(SecRoleEntity.RoleId);
        //        }
        //        return menuDataItem;
        //    }
        //    set 
        //    {
        //        menuDataItem = value;
        //    }
        //}
        #endregion

        #region 方法

        /// <summary>
        ///   构造函数
        /// </summary>
        /// <param name="SecRoleEntity"></param>
        public FunctionRoleDomain(SecRoleEntity SecRoleEntity)
            : base(SecRoleEntity)
        {

        }




        protected virtual string ValidateBase()
        {
            if (string.IsNullOrEmpty(SecRoleEntity.RoleId))
            {
                return "SYSTEM_ROLE_MESSAGE_ROLEID";
            }
            if (string.IsNullOrEmpty(SecRoleEntity.RoleName))
            {
                return "SYSTEM_ROLE_MESSAGE_ROLENAME";
            }
            if (SecRoleEntity.RoleName.Length > 40)
            {
                return "SYSTEM_ROLE_MSG_ROLENAMELENGTH";
            }
            if (SecRoleEntity.Remark.Length > 500)
            {
                return "SYSTEM_ROLE_MSG_REMARK";
            }
            return string.Empty;
        }

        /// <summary>
        /// 验证用户数据的合法性Update
        /// </summary>
        /// <returns></returns>
        public virtual string ValidateUpdate()
        {
            string strreturn=ValidateBase();
            if (!string.IsNullOrEmpty(strreturn))
                return strreturn;
            if (!SystemService.FunctionRoleService.CheckSystemRoleNameUpdate(SecRoleEntity.RoleName, SecRoleEntity.RoleId))
            {
                return "SYSTEM_ROLE_MESSAGE_EXISTS_ROLENAME";
            }
           
            return string.Empty;
        }

        /// <summary>
        /// 验证用户数据的合法性Insert
        /// </summary>
        /// <returns></returns>
        public virtual string ValidateInsert()
        {
            string strreturn = ValidateBase();
            if (!string.IsNullOrEmpty(strreturn))
                return strreturn;
            if (!SystemService.FunctionRoleService.CheckSystemRoleNameInsert(SecRoleEntity.RoleName))
            {
                return "SYSTEM_ROLE_MESSAGE_EXISTS_ROLENAME";
            }
            return string.Empty;
        }

        /// <summary>
        /// 验证角色名称是否位字母组成
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public virtual string ValidateRoleNaeme(string roleName)
        {
            char[] chrRole = roleName.ToCharArray();
            string Result = string.Empty;
            for (int i = 0; i < chrRole.Length; i++)
            {
                if (!ValidateAscii(chrRole[i]))
                {
                    Result = "SYSTEM_ROLE_MSG_CHECKROLENAME";
                }
            }
            if (!string.IsNullOrEmpty(Result))
            {

                return Result;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 判断是否为字母
        /// </summary>
        /// <param name="chrRole"></param>
        /// <returns></returns>
        public virtual bool ValidateAscii(char chrRole)
        {
            if (char.IsLetter(chrRole))
            {
                if (eContract.Common.Validator.IsChinese(chrRole.ToString()))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }

        }
        #endregion
    }
}

