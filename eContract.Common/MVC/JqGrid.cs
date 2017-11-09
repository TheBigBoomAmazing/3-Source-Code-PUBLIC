using eContract.Common.WebUtils;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace eContract.Common.MVC
{
    public class JqGrid
    {

        #region 查询参数

        public bool _search { get; set; }

        public string nd { get; set; }

        public int rows { get; set; }

        public int page { get; set; }

        public string sidx { get; set; }

        public string sord { get; set; }

        #endregion

        public string DbInstanceName { get; set; }
        public string Where { get; set; }
        public List<FilterParam> LstParms { get; set; }
        public bool IsDataEntity { get; set; }
        public int Total { get; set; }
        public object RowsData { get; set; }
        public string CollectQuantity { get; set; }
        public string CollectAmount { get; set; }

        public Dictionary<string, string> QueryField;

        /// <summary>
        /// 将参数转化为键值对列表
        /// </summary>
        /// <param name="data"></param>
        public void ConvertParams(FormCollection data)
        {

            LstParms = new List<FilterParam>();
            if (!string.IsNullOrEmpty(keyWord))
            {
                keyWord = keyWord.Trim();
            }

            string[] keys = new string[] { "pageindex", "pagesize", "sortname", "sortorder", "keyword", "total", "where" };
            QueryField = new Dictionary<string, string>();
            foreach (string key in data.Keys)
            {
                if (!keys.Contains(key.ToLower()))
                {
                    QueryField.Add(key, data[key].Trim());
                }
            }
            NameValueCollection lstNameValues = WebCaching.CurrentContext.Request.QueryString;
            if (lstNameValues != null)
            {
                foreach (var key in lstNameValues.AllKeys)
                {
                    if (!keys.Contains(key) && !QueryField.ContainsKey(key))
                    {
                        QueryField.Add(key, lstNameValues[key].Trim());
                    }
                }

            }

        }

        /// <summary>
        /// 判断参数名是否存在
        /// </summary>
        /// <param name="key">参数名</param>
        /// <param name="Vaule">获取参数的值</param>
        /// <returns></returns>
        public bool HasKey(string key, ref string Vaule)
        {
            if (QueryField != null && QueryField.ContainsKey(key))
            {
                Vaule = QueryField[key];
                if (!string.IsNullOrEmpty(Vaule))
                {
                    return true;
                }
            }
            if (QueryField != null)
            {
                foreach (var item in QueryField.Keys)
                {
                    if (item.ToLower().Equals(key.ToLower()))
                    {
                        Vaule = QueryField[item];
                        if (!string.IsNullOrEmpty(Vaule))
                        {
                            return true;
                        }
                        return false; ;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 获取参数方法
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetHasValue(string key)
        {
            if (QueryField != null && QueryField.ContainsKey(key))
            {
                return QueryField[key].Trim();
            }
            if (QueryField != null)
            {
                foreach (var item in QueryField.Keys)
                {
                    if (item.ToLower().Equals(key.ToLower()))
                    {
                        return QueryField[item].Trim();
                    }
                }

            }
            return string.Empty;
        }

        public string keyWord { get; set; }

        public void DataBind(DataTable DataSource, int dataCount)
        {
            List<Dictionary<string, object>> dicLstRow = new List<Dictionary<string, object>>();
            ToEntityList<T>(DataSource, ref dicLstRow);
            RowsData = dicLstRow;
            Total = dataCount;
        }

        /// <summary>
        /// 实体转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static void ToEntityList<T>(DataTable schemaTable, ref List<Dictionary<string, object>> dicLstRow) where T : class, new()
        {
            // List<T> lstT = new List<T>();
            foreach (DataRow row in schemaTable.Rows)
            {
                //EntityBase entity = new T();
                Dictionary<string, object> dicObject = new Dictionary<string, object>();
                for (int i = 0; i < schemaTable.Columns.Count; i++)
                {
                    string fieldName = schemaTable.Columns[i].ColumnName;

                    object value = row[fieldName];
                    if (value != DBNull.Value)
                    {
                        dicObject.Add(fieldName, row[fieldName]);
                    }

                }
                dicLstRow.Add(dicObject);
            }
        }
    }
}
