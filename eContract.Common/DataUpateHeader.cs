using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eContract.Common
{
    public class DataUpdateHeader
    {
        /// <summary>
        /// 系统标识
        /// </summary>        
        private string _systemId = string.Empty;
        public string systemId
        {
            get { return _systemId; }
            set { _systemId = value; }
        }

        /// <summary>
        /// 系统名称
        /// </summary>
        private string _systemName = string.Empty;
        public string systemName
        {
            get { return _systemName; }
            set { _systemName = value; }
        }

        /// <summary>
        /// 验证标识
        /// </summary>        
        private string _tokenStr = string.Empty;
        public string tokenStr
        {
            get { return _tokenStr; }
            set { _tokenStr = value; }
        }
    }
}
