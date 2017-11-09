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
    public class CasRegionDomain : DomainBase
    {
        /// <summary>
        /// Domain持有的大区实体
        /// </summary>
        public CasRegionEntity CasRegionEntity
        {
            get
            {
                return Entity as CasRegionEntity;
            }
            set
            {
                Entity = value;
            }
        }

        List<UserDomain> usersDomain;
        /// <summary>
        /// 大区成员
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
        /// <param name="CasRegionEntity">大区实体</param>
        public CasRegionDomain( CasRegionEntity CasRegionEntity)
            : base(CasRegionEntity)
        {
        }


        #region 验证

        /// <summary>
        /// 验证数据的合法性
        /// </summary>
        /// <returns></returns>
        protected virtual string BaseValidate()
        {
            if (string.IsNullOrEmpty(CasRegionEntity.RegionName))
            {
                return "SYSTEM_PAGE_MESSAGE_RegionNAME";
            }

            //页面 不超过 50
            if (50 < CasRegionEntity.RegionName.Length)
            {
                return "SYSTEM_PAGE_MESSAGE_RegionNAMELENGTH";
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
