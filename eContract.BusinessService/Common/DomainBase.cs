using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suzsoft.Smart.EntityCore;
using System.Globalization;
using System.Web;
using System.Resources;
using System.Reflection;

namespace eContract.BusinessService.Common
{
    /// <summary>
    /// 领域对象的基类
    /// </summary>
    public class DomainBase
    {
        /// <summary>
        /// Domain持有的实体
        /// </summary>
        public EntityBase Entity { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity"></param>
        public DomainBase(EntityBase entity)
        {
            Entity = entity;
        }

        public DomainBase()
        {
            Entity = new EntityBase();
        }

        public static EntityCollection<U> ConvertToEntityList<U, T>(List<T> listDomain)
            where U : EntityBase, new()
            where T : DomainBase
        {
            if (listDomain == null)
                return null;
            EntityCollection<U> result = new EntityCollection<U>();
            foreach (T domain in listDomain)
            {
                result.Add(domain.Entity as U);
            }
            return result;
        }


    }
}
