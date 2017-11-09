using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Xml.Serialization;
using ComixSDK.EDI.Utils;
using LitJson;

namespace Suzsoft.Smart.EntityCore
{
    //IDictionary<string, object>, 
    /// <summary>
    /// 数据实体基类
    /// </summary>
    [Serializable]
    public class EntityBase : IXmlSerializable
    {
        protected TableInfo _tableSchema;
        /// <summary>
        /// 对应数据库的表信息
        /// </summary>
        public virtual TableInfo OringTableSchema
        {
            get
            {
                return _tableSchema;
            }
        }

        protected Dictionary<string, object> _data;
        /// <summary>
        /// 数据存储
        /// </summary>
        public Dictionary<string, object> DataCollection
        {
            get
            {
                return _data;
            }
        }

        protected BusinessState _state;
        /// <summary>
        /// 状态
        /// </summary>
        public BusinessState State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public EntityBase()
        {
            _data = new Dictionary<string, object>();
            _state = BusinessState.Added;
        }

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetData(string key, object value)
        {
            this[key.Trim()] = value;
        }

        /// <summary>
        /// 提取数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetData(string key)
        {
            object result = null;
            if (ContainsKey(key.Trim()))
                result = this[key.Trim()];
            return result;
        }

        public string EntityToJsonString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[{");
            foreach (ColumnInfo column in OringTableSchema.AllColumnInfo)
            {
                sb.Append("\"" + column.ColumnName + "\":");
                object obj = GetData(column.ColumnName);
                if (obj == null)
                {
                    if (column.DataType == typeof(bool))
                    {
                        sb.Append("\"" + false + "\",");
                    }
                    else
                    {
                        sb.Append("\"" + string.Empty + "\",");
                    }
                }
                else
                {
                    sb.Append("\"" + GetData(column.ColumnName).ToString() + "\",");
                }                
            }
            sb = sb.Remove(sb.Length - 1, 1);
            sb.Append("}]");
            return sb.ToString();
        }

        public void JsonStringToEntity(string jsonValue)
        {
            JsonData jsdata = JsonDataService.GetJsonData(jsonValue);
            string tpValue = "";
            foreach (ColumnInfo column in OringTableSchema.AllColumnInfo)
            {
                tpValue = jsdata[0][column.ColumnName].ToString();
                if (column.DataType == typeof(string))
                {
                    _data.Add(column.ColumnName, tpValue);
                }
                else if (column.DataType == typeof(int))
                {
                    _data.Add(column.ColumnName, tpValue == "" ? 0 : int.Parse(tpValue));
                }
                else if (column.DataType == typeof(decimal))
                {
                    _data.Add(column.ColumnName, tpValue == "" ? 0 : decimal.Parse(tpValue));
                }
                else if (column.DataType == typeof(bool))
                {
                    _data.Add(column.ColumnName, tpValue == "" ? false : bool.Parse(tpValue));
                }
                else if (column.DataType == typeof(DateTime))
                {
                    _data.Add(column.ColumnName, tpValue == "" ? DateTime.MaxValue : DateTime.Parse(tpValue));
                }
                else
                {
                    _data.Add(column.ColumnName, tpValue);
                }
            }    
        }

        #region 实体转换

        /// <summary>
        /// 根据其他EntityBase内容重新初始化
        /// </summary>
        /// <param name="entity"></param>
        public void Initialize(EntityBase entity)
        {
            this._data = entity.DataCollection;
        }

        /// <summary>
        /// 转换成其他实体
        /// </summary>
        /// <typeparam name="targetT"></typeparam>
        /// <returns></returns>
        public targetT Covert<targetT>()
            where targetT : EntityBase, new()
        {
            targetT t = new targetT();
            t.Initialize(this);
            return t;
        }

        /// <summary>
        /// 转换实体的同时不转换Schema
        /// </summary>
        /// <typeparam name="targetT"></typeparam>
        /// <returns></returns>
        public targetT CovertWithoutSchema<targetT>()
            where targetT : EntityBase, new()
        {
            targetT t = new targetT();
            t._tableSchema = this._tableSchema;
            t.Initialize(this);
            return t;
        }

        /// <summary>
        /// 转换成DataRow
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public void ConvertToRow(DataRow row)
            {
            foreach (ColumnInfo column in _tableSchema.AllColumnInfo)
                {
                        row[column.ColumnName] = GetData(column.ColumnName);
                }
        }

        /// <summary>
        /// 从DataRow转换成实体
        /// </summary>
        /// <typeparam name="targetT"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static targetT ConvertToEntity<targetT>(DataRow row)
            where targetT : EntityBase, new()
        {
            targetT t = new targetT();
            foreach (ColumnInfo column in t.OringTableSchema.AllColumnInfo)
            {
                t[column.ColumnName] = row[column.ColumnName];
            }
            return t;
        }

        /// <summary>
        /// 转换成HashTable
        /// </summary>
        /// <param name="ht"></param>
        /// <returns></returns>
        public Hashtable ToHashtable(Hashtable ht)
        {
            foreach (string key in this.Keys)
            {
                ht[key] = this[key];
            }
            return ht;
        }

        /// <summary>
        /// 从Hash转换成实体
        /// </summary>
        /// <param name="ht"></param>
        public static targetT ConvertToEntity<targetT>(Hashtable ht)
            where targetT : EntityBase, new()
        {
            targetT t = new targetT();
            foreach (string key in ht.Keys)
            {
                t[key] = ht[key];
            }
            return t;
        }

        #endregion

        #region == 实体比较
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            EntityBase right = obj as EntityBase;
            if (right == null)
                return false;
            if (this.Count != right.Count)
                return false;

            foreach (string key in this.Keys)
            {
                if (!right.ContainsKey(key))
                    return false;
                if (this[key] != right[key])
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(EntityBase left, object right)
        {
            if (Object.Equals (left, null ))
            {
                if (Object.Equals(right, null))
                    return true;
                else
                    return false;
            }
            return left.Equals(right);
        }

        public static bool operator !=(EntityBase left, object right)
        {
            return !(left == right);
        }

        #endregion

        #region IDictionary<string,object> Members

        public void Add(string key, object value)
        {
            _data.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return _data.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get
            {
                return _data.Keys;
            }
        }

        public bool Remove(string key)
        {
            return _data.Remove(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return _data.TryGetValue(key, out value);
        }

        public ICollection<object> Values
        {
            get { return _data.Values; }
        }

        public object this[string key]
        {
            get
            {
                return _data[key];
            }
            set
            {
                _data[key] = value;
            }
        }

        #endregion

        #region ICollection<KeyValuePair<string,object>> Members

        public void Add(KeyValuePair<string, object> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _data.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return ((ICollection<KeyValuePair<string, object>>)_data).Contains(item);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, object>>)_data).CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _data.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((ICollection<KeyValuePair<string, object>>)_data).IsReadOnly; }
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return ((ICollection<KeyValuePair<string, object>>)_data).Remove(item);
        }

        #endregion

        #region IEnumerable<KeyValuePair<string,object>> Members

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return ((IEnumerable)_data).GetEnumerator();
        //}

        #endregion

        #region IXmlSerializable Members

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        public void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(string));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(object));
            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();
            if (wasEmpty)
                return;
            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");
                reader.ReadStartElement("key");
                string key = (string)keySerializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadStartElement("value");
                object value = (object)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();
                this.Add(key, value);
                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(string));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(object));
            foreach (string key in this.Keys)
            {
                writer.WriteStartElement("item");
                writer.WriteStartElement("key");
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();
                writer.WriteStartElement("value");
                object value = this[key];
                valueSerializer.Serialize(writer, value);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }

        #endregion
    }
}
