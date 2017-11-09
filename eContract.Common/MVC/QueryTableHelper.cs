using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suzsoft.Smart.EntityCore;
using System.Data;
using System.Reflection;
using Suzsoft.Smart.Data;

namespace eContract.Common.MVC
{
    public class QueryTableHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static LigerGrid QueryTable<T>(IQuery queryObj, LigerGrid grid) where T : EntityBase, new()
        {
            WhereBuilder wb = queryObj.ParseSQL();
            return QueryTable<T>(wb, grid, grid.DbInstanceName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="wb"></param>
        /// <param name="grid"></param>
        /// <param name="DbInstanceName"></param>
        /// <returns></returns>
        public static LigerGrid QueryTable<T>(WhereBuilder wb, LigerGrid grid, string DbInstanceName = "") where T : EntityBase, new()
        {
            return QueryTable<T>(wb.SQLString, wb.Parameters, grid, DbInstanceName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlString">必须带上where 1=1</param>
        /// <param name="lstParameter"></param>
        /// <param name="grid"></param>
        /// <param name="DbInstanceName">数据库实例</param>
        /// <returns></returns>
        public static LigerGrid QueryTable<T>(string sqlString, DataAccessParameterCollection lstParameter, LigerGrid grid, string DbInstanceName = "") where T : EntityBase, new()
        {
            string hql = sqlString;
            string where = grid.Where;
            if (!string.IsNullOrEmpty(where))
            {
                hql += " and (" + where + ")";
            }
            if (lstParameter == null)
            {
                lstParameter = new DataAccessParameterCollection();
            }
            if (grid != null && grid.LstParms != null && grid.LstParms.Count > 0)
            {
                foreach (var item in grid.LstParms)
                {
                    lstParameter.Add(item.Name, new DataAccessParameter(item.Name, item.Value));
                }
            }
            if (!string.IsNullOrEmpty(DbInstanceName))
            {
                using (DataAccessBroker broker = DataAccessFactory.Instance(DbInstanceName))
                {
                    return ExcuteTable<T>(hql, lstParameter, grid, broker);
                }
            }
            else
            {
                using (DataAccessBroker broker = DataAccessFactory.Instance())
                {
                    return ExcuteTable<T>(hql, lstParameter, grid, broker);
                }
            }
        }

        public static LigerGrid ExcuteTable<T>(string hql, DataAccessParameterCollection lstParameter, LigerGrid grid, DataAccessBroker broker) where T : EntityBase, new()
        {
            object total = broker.ExecuteScalar("select count(1) as Total from (" + hql + ") as ttable", lstParameter, CommandType.Text);
            StringBuilder strsql = new StringBuilder();
            if (grid.pageIndex != 0 && grid.pageSize != 0)
            {
                strsql.Append("select * from ( select top " + (grid.pageIndex * grid.pageSize) + " row_number() over(order by " + grid.sortName + " " + grid.sortOrder.ToLower() + ") as Row_Number, ");
                strsql.Append(hql.Replace(":", "@").Substring(7));
                if (!string.IsNullOrEmpty(grid.Where))
                {
                    strsql.Append(" and (" + grid.Where.Replace(":", "@") + ")"); ;
                }//as
                strsql.Append(" )  ttable where Row_Number between " + (((grid.pageIndex - 1) * grid.pageSize) + 1) + " and  " + ((grid.pageIndex) * grid.pageSize));
            }
            else
            {
                strsql.Append(hql);
                if (!string.IsNullOrEmpty(grid.sortName))
                {
                    strsql.Append(" order by " + grid.sortName);
                    if (!string.IsNullOrEmpty(grid.sortOrder))
                    {
                        strsql.Append(" " + grid.sortOrder);
                    }
                    else
                    {
                        strsql.Append(" ASC");
                    }
                }
            }
            DataSet ds = broker.FillSQLDataSet(strsql.ToString(), lstParameter);
            EntityCollection<T> lstEntity = new EntityCollection<T>();
            List<Dictionary<string, object>> dicLstRow = new List<Dictionary<string, object>>();
            ToEntityList<T>(ds.Tables[0], ref dicLstRow, ref lstEntity);

            if (grid.IsDataEntity)
            {
                grid.Rows = lstEntity;
            }
            else
            {
                grid.Rows = dicLstRow;
            }
            grid.Total = (total != null ? Convert.ToInt32(total) : 0);
            return grid;
        }

        /// <summary>
        /// 实体转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static void ToEntityList<T>(DataTable schemaTable, ref List<Dictionary<string, object>> dicLstRow, ref EntityCollection<T> lstEntity) where T : EntityBase, new()
        {
            foreach (DataRow row in schemaTable.Rows)
            {
                T entity = new T();

                Dictionary<string, object> dicObject = new Dictionary<string, object>();
                for (int i = 0; i < schemaTable.Columns.Count; i++)
                {
                    string fieldName = schemaTable.Columns[i].ColumnName;

                    if (fieldName == "IS_DELETED")
                    {
                        string s = "";
                        string s1 = s;
                    }
                    object value = row[fieldName];

                    if (value != DBNull.Value)
                    {
                        if (!entity.DataCollection.ContainsKey(fieldName))
                        {
                            if (row[fieldName].GetType().Name == "Int16")
                            {
                                entity.DataCollection.Add(fieldName, Convert.ToInt32(row[fieldName]));
                            }
                            else
                                entity.DataCollection.Add(fieldName, row[fieldName]);
                        }
                        dicObject.Add(fieldName, row[fieldName]);
                    }

                }
                dicLstRow.Add(dicObject);
                lstEntity.Add(entity);
            }


            // return lstT;
        }

        #region 封装JqGrid
        public static JqGrid QueryGrid<T>(IQuery queryObj, JqGrid grid) where T : EntityBase, new()
        {
            WhereBuilder wb = queryObj.ParseSQL();
            return QueryTable<T>(wb, grid, grid.DbInstanceName);
        }

        private static JqGrid QueryTable<T>(WhereBuilder wb, JqGrid grid, string dbInstanceName) where T : EntityBase, new()
        {
            return QueryTable<T>(wb.SQLString, wb.Parameters, grid, dbInstanceName);
        }

        private static JqGrid QueryTable<T>(string sqlString, DataAccessParameterCollection parameters, JqGrid grid, string dbInstanceName) where T : EntityBase, new()
        {
            string hql = sqlString;
            string where = grid.Where;
            if (!string.IsNullOrEmpty(where))
            {
                hql += " and (" + where + ")";
            }
            if (parameters == null)
            {
                parameters = new DataAccessParameterCollection();
            }
            if (grid != null && grid.LstParms != null && grid.LstParms.Count > 0)
            {
                foreach (var item in grid.LstParms)
                {
                    parameters.Add(item.Name, new DataAccessParameter(item.Name, item.Value));
                }
            }
            if (!string.IsNullOrEmpty(dbInstanceName))
            {
                using (DataAccessBroker broker = DataAccessFactory.Instance(dbInstanceName))
                {
                    return ExcuteTable<T>(hql, parameters, grid, broker);
                }
            }
            else
            {
                using (DataAccessBroker broker = DataAccessFactory.Instance())
                {
                    return ExcuteTable<T>(hql, parameters, grid, broker);
                }
            }
        }

        private static JqGrid ExcuteTable<T>(string hql, DataAccessParameterCollection lstParameter, JqGrid grid, DataAccessBroker broker) where T : EntityBase, new()
        {
            int index = 0;
            StringBuilder whereSql = new StringBuilder(hql);
            if (grid.QueryField["_search"] == "true")
            {
                foreach (KeyValuePair<string, string> kv in grid.QueryField)
                {
                    if (index > 5)
                    {
                        DateTime result = DateTime.Now;
                        if (DateTime.TryParse(kv.Value, out result))
                        {
                            whereSql.AppendLine($" and datediff(day,{kv.Key},'{kv.Value}')=0 ");
                        }
                        else
                        {
                            whereSql.AppendLine($" and {kv.Key} like N{Utils.ToSqlLikeStr(kv.Value)} ");
                        }
                    }
                    index++;
                }
            }
            //as
            object total = broker.ExecuteScalar("select count(1) as Total from (" + whereSql + ")  ttable", lstParameter, CommandType.Text);
            StringBuilder strsql = new StringBuilder();
            if (grid.page != 0 && grid.rows != 0)
            {//top " + (grid.page * grid.rows) + " 
                strsql.Append("select * from ( select row_number() over(order by " + grid.sidx + " " + grid.sord.ToLower() + ") as Row_Number, ");
                strsql.Append(whereSql.ToString().Substring(7));
                if (!string.IsNullOrEmpty(grid.Where))
                {
                    strsql.Append(" and (" + grid.Where + ")"); ;
                }

                //as
                strsql.Append(" )  ttable where Row_Number between " + (((grid.page - 1) * grid.rows) + 1) + " and  " + ((grid.page) * grid.rows));
            }
            else
            {
                strsql.Append(whereSql);
                if (!string.IsNullOrEmpty(grid.sidx))
                {
                    strsql.Append(" order by " + grid.sidx);
                    if (!string.IsNullOrEmpty(grid.sord))
                    {
                        strsql.Append(" " + grid.sord);
                    }
                    else
                    {
                        strsql.Append(" ASC");
                    }
                }
            }
            DataSet ds = broker.FillSQLDataSet(strsql.ToString(), lstParameter);
            EntityCollection<T> lstEntity = new EntityCollection<T>();
            List<Dictionary<string, object>> dicLstRow = new List<Dictionary<string, object>>();
            ToEntityList<T>(ds.Tables[0], ref dicLstRow, ref lstEntity);

            if (grid.IsDataEntity)
            {
                grid.RowsData = lstEntity;
            }
            else
            {
                grid.RowsData = dicLstRow;
            }
            grid.Total = (total != null ? Convert.ToInt32(total) : 0);
            return grid;
        }

        #endregion
    }
}
