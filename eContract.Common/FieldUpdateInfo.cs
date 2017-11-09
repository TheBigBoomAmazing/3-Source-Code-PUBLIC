using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eContract.Common
{
    public class FieldUpdateInfo
    {
        /// <summary>
        /// 变更字段名称
        /// </summary>
        private string _fieldName = string.Empty;
        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        /// <summary>
        /// 原值
        /// </summary>        
        private string _oldValue = string.Empty;
        public string OldValue
        {
            get { return _oldValue; }
            set { _oldValue = value; }
        }

        /// <summary>
        /// 新值
        /// </summary>        
        private string _newValue = string.Empty;
        public string NewValue
        {
            get { return _newValue; }
            set { _newValue = value; }
        }
    }
}
