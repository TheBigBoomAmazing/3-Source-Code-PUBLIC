using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eContract.Common.Entity;
using eContract.BusinessService.Common;
using eContract.BusinessService.SystemManagement.Service;
using eContract.Common;
using eContract.Common.WebUtils;
using eContract.Common.Const;

namespace eContract.BusinessService.SystemManagement.Domain
{
    public class UserDomain : DomainBase
    {
        /// <summary>
        /// Domain持有的用户实体
        /// </summary>
        public CasUserEntity CasUserEntity
        {
            get
            {
                return Entity as CasUserEntity;
            }
            set
            {
                Entity = value;
            }
        }

        private string _systemName;
        public string SystemName
        {
            get
            {
                return _systemName;
            }
            set
            {
                _systemName = value;
            }
        }


        /// <summary>
        /// 当前用户有权限的菜单数据项（收藏夹菜单）
        /// </summary>
        private List<MenuDataItem> favoriteMenuDataItems;
        public List<MenuDataItem> FavoriteMenuDataItems
        {
            get
            {
                if (favoriteMenuDataItems == null)
                {
                    favoriteMenuDataItems = SystemService.FunctionRoleService.GetFavoriteMenuByUserId(CasUserEntity.UserId, 0);
                }
                return favoriteMenuDataItems;
            }
            set
            {
                favoriteMenuDataItems = value;
            }
        }

        /// <summary>
        /// 当前用户有权限的菜单数据项（管理端菜单）
        /// </summary>
        private List<MenuDataItem> menuDataItems;
        public List<MenuDataItem> MenuDataItems
        {
            get
            {
                if (menuDataItems == null || menuDataItems.Count<=0)
                {
                    menuDataItems = SystemService.FunctionRoleService.GetMenuDataItemByUserId(SystemName,CasUserEntity.UserId);
                    if (menuDataItems.Count == 0) {
                        //menuDataItems = SystemService.FunctionRoleService.GetFunctionRoleDomainByRoleID("");
                    }
                }
                return menuDataItems;
            }
            set
            {
                menuDataItems = value;
            }
        }
        private List<MenuDataItem> subMenuDataItems;
        public List<MenuDataItem> SubMenuDataItems
        {
            get
            {
                if (subMenuDataItems == null || subMenuDataItems.Count <= 0)
                {
                    menuDataItems = SystemService.FunctionRoleService.GetMenuDataItemByUserId(SystemName, CasUserEntity.UserId);
                    subMenuDataItems = new List<MenuDataItem>();
                    foreach (var menuDataItem in menuDataItems)
                    {
                        subMenuDataItems.AddRange(menuDataItem.SubMenu);
                    }
                }
                return subMenuDataItems;
            }
            set
            {
                subMenuDataItems = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="CasUserEntity">用户实体</param>
        public UserDomain(string systemName,CasUserEntity CasUserEntity)
            : base(CasUserEntity)
        {
            SystemName = systemName;
        }


        /// <summary>
        /// 验证基本数据的合法性
        /// </summary>
        /// <returns></returns>
        protected virtual string BaseValidate()
        {
            if (string.IsNullOrEmpty(CasUserEntity.UserAccount))
            {
                return "登录名不能为空";
            }
            if (string.IsNullOrEmpty(CasUserEntity.ChineseName))
            {
                return "姓名不能为空";
            }

            return string.Empty;
        }

        /// <summary>
        /// 验证数据是否可以插入
        /// </summary>
        /// <returns></returns>
        public virtual string ValidateInsert()
        {
            string baseResult = BaseValidate();
            if (!string.IsNullOrEmpty(baseResult))
            {
                return baseResult;
            }
            //校验用户是否存在
            UserDomain user = SystemService.UserService.GetUserDomainByUserAccount(SystemName,CasUserEntity.UserAccount);
            if (user != null)
            {
                return "登录名已经存在";
            }

            return string.Empty;
        }

        /// <summary>
        /// 验证数据是否可以更新
        /// </summary>
        /// <returns></returns>
        public virtual string ValidateUpdate()
        {
            string baseResult = BaseValidate();
            if (!string.IsNullOrEmpty(baseResult))
            {
                return baseResult;
            }
            UserDomain user = SystemService.UserService.GetUserDomainByUserAccount(SystemName,CasUserEntity.UserAccount);
            if (user != null && user.CasUserEntity.UserId != CasUserEntity.UserId)
            {
                return "登录名已经存在";
            }



            return string.Empty;
        }

        /// <summary>
        /// 验证用户账号是否是订单组成员
        /// </summary>
        /// <returns></returns>
        public string ValidateAdminUser()
        {
            if (MenuDataItems == null || MenuDataItems.Count == 0)
            {
                return "SYSTEM_NO_AUTHORITY";
            }

            return "";
        }


        public MenuDataItem GetPage(string pageNmae, bool isShopPage)
        {
            return GetPage(MenuDataItems, pageNmae, isShopPage);
        }

        MenuDataItem GetPage(List<MenuDataItem> menus, string pageName, bool isShopPage)
        {
            if (menus == null)
            {
                return null;
            }
            foreach (MenuDataItem item in menus)
            {
                string itemPageName = item.Page.PageName;
                if (itemPageName == pageName)
                {
                    return item;
                }
                else
                {
                    MenuDataItem page = GetPage(item.SubMenu, pageName, isShopPage);
                    if (page != null) { return page; }
                }
            }
            return null;
        }

    }
}
