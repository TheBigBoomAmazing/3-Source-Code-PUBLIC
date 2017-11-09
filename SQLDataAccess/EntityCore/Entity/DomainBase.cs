using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace Suzsoft.Smart.EntityCore
{
    /// <summary>
    /// 领域模型基类
    /// </summary>
    public class DomainBase
    {
        public DomainBase(EntityBase entity)
        {
            _baseentity = entity;
        }
        public DomainBase()
        {
            _baseentity = new EntityBase();
        }
        protected EntityBase _baseentity;
        /// <summary>
        /// 对应数据实体
        /// </summary>
        public EntityBase BaseEntity
        {
            get
            {
                return _baseentity;
            }
            set
            {
                _baseentity = value;
            }
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
                result.Add(domain.BaseEntity as U);
            }
            return result;
        }
    }
}
