using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eContract.Common.Entity;

namespace eContract.Common.WebUtils
{
    /// <summary>
    /// 菜单数据项
    /// </summary>
    public class MenuDataItem
    {
        public MenuDataItem RootMenu
        {
            get
            {
                MenuDataItem rootMenu = this;
                while (rootMenu.ParentMenu != null)
                {
                    rootMenu = rootMenu.ParentMenu;
                }
                return rootMenu;
            }
        }

        public MenuDataItem ParentMenu
        {
            get;
            set;
        }

        /// <summary>
        /// 当前菜单对应的Page实体
        /// </summary>
        public SecPageEntity Page
        {
            get;
            set;
        }

        /// <summary>
        /// 子菜单
        /// </summary>
        public List<MenuDataItem> SubMenu
        {
            get;
            set;
        }

        /// <summary>
        /// 页面控件
        /// </summary>
        public List<SecPageControlEntity> PageControls
        {
            get;
            set;
        }
        
        /// <summary>
        /// 根据ID找到定义的控件
        /// </summary>
        /// <param name="controlId"></param>
        /// <returns></returns>
        public SecPageControlEntity GetControl(string controlId)
        {
            foreach (var item in PageControls)
            {
                if (item.ServerId == controlId) return item;
            }
            return null;
        }

        ///// <summary>
        ///// 页面对应的有效控件
        ///// </summary>
        //public List<SecPermissionEntity> ValidControls
        //{
        //    get;
        //    set;
        //}

        public MenuDataItem(SecPageEntity page, MenuDataItem parentMenu, List<SecPageControlEntity> pageControls)
        {
            ParentMenu = parentMenu;
            Page = page;
            SubMenu = new List<MenuDataItem>();
           // ValidControls = new List<SecPermissionEntity>();
            PageControls = pageControls;
            if (pageControls == null) { PageControls = new List<SecPageControlEntity>(); }
        }
    }
}
