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
    public class CasDepartmentDomain : DomainBase
    {
        /// <summary>
        /// Domain持有的部门实体
        /// </summary>
        public CasDepartmentEntity CasDepartmentEntity
        {
            get
            {
                return Entity as CasDepartmentEntity;
            }
            set
            {
                Entity = value;
            }
        }

        List<UserDomain> usersDomain;
        /// <summary>
        /// 部门成员
        /// </summary>
        public List<UserDomain> UsersDomain
        {
            get
            {
                if (usersDomain == null)
                {
                    usersDomain = new List<UserDomain>();
                }
                return usersDomain;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="CasDepartmentEntity">部门实体</param>
        public CasDepartmentDomain( CasDepartmentEntity CasDepartmentEntity)
            : base(CasDepartmentEntity)
        {
        }


        #region 验证

        /// <summary>
        /// 验证数据的合法性
        /// </summary>
        /// <returns></returns>
        protected virtual string BaseValidate()
        {
            if (string.IsNullOrEmpty(CasDepartmentEntity.DeptName))
            {
                return "SYSTEM_PAGE_MESSAGE_DEPTNAME";
            }

            //页面 不超过 50
            if (50 < CasDepartmentEntity.DeptName.Length)
            {
                return "SYSTEM_PAGE_MESSAGE_DEPTNAMELENGTH";
            } 
        
            //if (0 > SecPageEntity.MenuLevel)
            //{
            //    return "SYSTEM_PAGE_MESSAGE_MENULEVEL";
            //}
            //if (0 > SecPageEntity.MenuOrder)
            //{
            //    return "SYSTEM_PAGE_MESSAGE_MENUORDER";
            //} 
            ////备注 最大500
            //if (500 < SecPageEntity.Remark.Length)
            //{
            //    return "SYSTEM_PAGE_MESSAGE_REMARKLENGTH";
            //} 
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
            return string.Empty;
        }

        #endregion
    }
}
