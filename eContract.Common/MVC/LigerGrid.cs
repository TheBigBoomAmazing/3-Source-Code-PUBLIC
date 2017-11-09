using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eContract.Common.WebUtils;
using ComixSDK.EDI.Utils;
using System.Web.Mvc;
using NPOI.SS.Formula.Functions;

namespace eContract.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class LigerGrid
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public string sortName { get; set; }
        public string sortOrder { get; set; }

        #region  bootstrap 框架
        public int iDisplayStart { get; set; }
        public int iDisplayLength { get; set; }

        public int iSortCol_0 { get; set; }
        public string sSortDir_0 { get; set; }

        public string sSearch { get; set; }
        #endregion
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string keyWord { get; set; }

        public object Rows { get; set; }
        public int Total { get; set; }

        public string DbInstanceName { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public string Where { get; set; }

        public FilterGroup Group { get; set; }

        public List<FilterParam> LstParms { get; set; }

        public Dictionary<string, string> QueryField;

        /// <summary>
        /// 数据是否以entity方式呈现
        /// </summary>
        public bool IsDataEntity { get; set; }

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
        /// <summary>
        /// 将参数转化为键值对列表
        /// </summary>
        /// <param name="data"></param>
        public void ConvertParams(FormCollection data)
        {
            if (iDisplayStart > 0)
            {
                pageIndex = iDisplayStart / iDisplayLength + 1;
            }
            if (iDisplayLength > 0)
            {
                pageSize = iDisplayLength;
            }


            if (!string.IsNullOrWhiteSpace(sSortDir_0))
            {
                sortOrder = sSortDir_0;
            }
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }

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
            if (!string.IsNullOrEmpty(Where))
            {
                //反序列化Filter Group JSON
                Group = JSONHelper.FromJson<FilterGroup>(Where);
                if (Group != null)
                {
                    StringBuilder strWhere = new StringBuilder();
                    var filter = new FilterTranslator(Group);
                    var lstParms = new List<FilterParam>();
                    filter.Translate(ref lstParms);
                    Where = filter.ToString();
                }
            }
            string sortname = "";
            HasKey("mDataProp_" + iSortCol_0.ToString(), ref sortname);
            sortName = sortname;
        }
        public LigerGrid()
        {

        }

        public void DataBind(DataTable DataSource, int dataCount)
        {
            List<Dictionary<string, object>> dicLstRow = new List<Dictionary<string, object>>();
            ToEntityList<T>(DataSource, ref dicLstRow);
            Rows = dicLstRow;
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
