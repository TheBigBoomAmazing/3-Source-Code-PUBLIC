using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eContract.Common
{
    public class DataUpdateBody
    {
        /// <summary>
        /// 表名称
        /// </summary>
        private string _tableName = string.Empty;
        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        /// <summary>
        /// 字段列表
        /// </summary>
        private List<FieldUpdateInfo> _fieldInfos = new List<FieldUpdateInfo>();
        public List<FieldUpdateInfo> FieldInfos
        {
            get { return _fieldInfos; }
            set { _fieldInfos = value; }
        }
    }
}
