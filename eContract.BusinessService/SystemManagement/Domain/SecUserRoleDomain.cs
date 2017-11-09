using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eContract.BusinessService.Common;
using eContract.Common.Entity;
using eContract.BusinessService.SystemManagement.Service;
using System.Text.RegularExpressions;

namespace eContract.BusinessService.SystemManagement.Domain
{
    public class SecUserRoleDomain : DomainBase
    {
        /// <summary>
        /// ParameterDomain持有的Parameter实体
        /// </summary>
        public SecUserRoleEntity SecUserRoleEntity
        {
            get
            {
                return Entity as SecUserRoleEntity;
            }
            set
            {
                Entity = value;
            }
        } 

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="SecPageEntity"></param>
        public SecUserRoleDomain(SecUserRoleEntity SecUserRoleEntity)
            : base(SecUserRoleEntity)
        {
        }

        /// <summary>
        /// 验证基本数据的合法性
        /// </summary>
        /// <returns></returns>
        protected virtual string BaseValidate()
        {
           
            return string.Empty;
        }

        /// <summary>
        /// 新增参数时验证数据合法性
        /// </summary>
        /// <returns></returns>
        public virtual string ValidateInsert()
        {
            string baseResult=BaseValidate();
            if (!string.IsNullOrEmpty(baseResult))
            {
                return baseResult;
            }
            else
            {
              
                    return string.Empty;

            }
        }

        /// <summary>
        /// 修改参数时验证数据合法性
        /// </summary>
        /// <returns></returns>
        public virtual string ValidateUpdate()
        {
            string baseResult = BaseValidate();
            if (!string.IsNullOrEmpty(baseResult))
            {
                return baseResult;
            }
            else
            {

            } 
            return baseResult;
        }
    }
}
