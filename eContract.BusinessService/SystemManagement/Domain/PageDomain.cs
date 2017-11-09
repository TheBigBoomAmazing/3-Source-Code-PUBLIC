using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eContract.BusinessService.Common;
using eContract.Common.Entity;
using eContract.BusinessService.SystemManagement.Service;
using eContract.Common.Const;
using eContract.Common;


namespace eContract.BusinessService.SystemManagement.Domain
{
    public class PageDomain : DomainBase
    {
        /// <summary>
        /// Domain持有的Page实体
        /// </summary>
        public SecPageEntity SecPageEntity
        {
            get
            {
                return Entity as SecPageEntity;
            }
            set
            {
                Entity = value;
            }
        }

        PageDomain parentDomain;
        /// <summary>
        /// 父页面Domain
        /// </summary>
        public PageDomain ParentDomain
        {
            get
            {
                if (parentDomain == null)
                {
                    parentDomain = SystemService.PageService.GetDomainById(SecPageEntity.ParentId, SecPageEntity.SystemName);
                }
                return parentDomain;
            }
        }

        List<PageDomain> childsDomain;
        /// <summary>
        /// 子页面
        /// </summary>
        public List<PageDomain> ChildsDomain
        {
            get
            {
                if (childsDomain == null)
                {
                    childsDomain = new List<PageDomain>();
                    //获取子页面
                    List<SecPageEntity> childs = SystemService.PageService.GetChildsByParentId(SecPageEntity.PageId, SecPageEntity.SystemName);
                    foreach (var item in childs)
                    {
                        //组装Domain
                        childsDomain.Add(new PageDomain(item));
                    }
                }
                return childsDomain;
            }
        }



        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="SecPageEntity"></param>
        public PageDomain(SecPageEntity SecPageEntity)
            : base(SecPageEntity)
        {
        }


        #region 验证

        /// <summary>
        /// 验证数据的合法性
        /// </summary>
        /// <returns></returns>
        protected virtual string BaseValidate()
        {
            if (string.IsNullOrEmpty(SecPageEntity.PageName))
            {
                return "SYSTEM_PAGE_MESSAGE_PAGENAME";
            }

            //页面 不超过 50
            if (50 < SecPageEntity.PageName.Length)
            {
                return "SYSTEM_PAGE_MESSAGE_PAGENAMELENGTH";
            } 
            if (!(SecPageEntity.IsMenu == true || SecPageEntity.IsMenu == false))
            {
                return "SYSTEM_PAGE_MESSAGE_ISMENU";
            }
            if (0 > SecPageEntity.MenuLevel)
            {
                return "SYSTEM_PAGE_MESSAGE_MENULEVEL";
            }
            if (0 > SecPageEntity.MenuOrder)
            {
                return "SYSTEM_PAGE_MESSAGE_MENUORDER";
            } 
            //备注 最大500
            if (500 < SecPageEntity.Remark.Length)
            {
                return "SYSTEM_PAGE_MESSAGE_REMARKLENGTH";
            } 
            return string.Empty;
        }

        /// <summary>
        /// 插入验证
        /// </summary>
        /// <returns></returns>
        public virtual string ValidateInsert()
        {
            string baseValidate = BaseValidate();
            if (!string.IsNullOrEmpty(baseValidate))
            {
                return baseValidate;
            }
            return string.Empty;
        }

        /// <summary>
        /// 更新验证
        /// </summary>
        /// <returns></returns>
        public virtual string ValidateUpdate()
        {
            string baseValidate = BaseValidate();
            if (!string.IsNullOrEmpty(baseValidate))
            {
                return baseValidate;
            }
            PageDomain parent = this.ParentDomain;
            while (parent != null)
            {
                if (parent.SecPageEntity.PageId == SecPageEntity.PageId)
                {
                    return "SYSTEM_PAGE_MESSAGE_FATHERPAGE";
                }
                parent = parent.ParentDomain;
            }
            return string.Empty;
        }

        #endregion
    }
}
