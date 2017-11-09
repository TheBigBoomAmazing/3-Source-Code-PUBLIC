using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eContract.Common.Entity;
using eContract.BusinessService.Common;
using eContract.BusinessService.SystemManagement.Service;

namespace eContract.BusinessService.SystemManagement.Domain
{
    public class LogErrorDomain : DomainBase
    {
        /// <summary>
        /// Domain持有的错误日志实体
        /// </summary>
        public SecLogErrorEntity SecLogErrorEntity
        {
            get
            {
                return Entity as SecLogErrorEntity;
            }
            set
            {
                Entity = value;
            }
        }

         /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="errorEntity">错误日志</param>
        public LogErrorDomain(SecLogErrorEntity errorEntity)
            : base(errorEntity)
        {
        }
    
   }
}
