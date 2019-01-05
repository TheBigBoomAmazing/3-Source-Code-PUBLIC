using System;
using System.Collections;
using System.Collections.Generic;
using Suzsoft.Smart.EntityCore;
using Suzsoft.Smart.Data;
using System.Data;

namespace Suzsoft.Smart.Data
{
    /// <summary>
    /// 数据访问
    /// </summary>
    public static class DataAccess
    {
        public const string ParameterPrefix = ":";

        #region select
        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static EntityCollection<T> SelectAll<T>()
            where T : EntityBase, new()
        {
            T schema = new T();
            string sqlString = "SELECT * FROM " + schema.OringTableSchema.TableName;
            return Select<T>(sqlString);
        }

        public static DataSet SelectAllDataSet<T>()
            where T : EntityBase, new()
        {
            T schema = new T();
            string sqlString = "SELECT * FROM " + schema.OringTableSchema.TableName;
            return SelectDataSet(sqlString);
        }

        /// <summary>
        /// 高性能查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static PerformanceEntityCollection<T> PerformanceSelectAll<T>()
            where T : EntityBase, new()
        {
            T schema = new T();
            string sqlString = "SELECT * FROM " + schema.OringTableSchema.TableName;
            return PerformanceSelect<T>(sqlString, null, CommandType.Text);
        }

        /// <summary>
        /// 根据SQL语句查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public static EntityCollection<T> Select<T>(string sqlString)
            where T : EntityBase, new()
        {
            return Select<T>(sqlString, null, CommandType.Text);
        }

        public static DataSet SelectDataSet(string sqlString)
        {
            return Select(sqlString, null);
        }

        public static DataSet SelectDataSet(string sqlString, string instanceName)
        {
            return Select(sqlString, instanceName, null);
        }

        public static int ExecuteNoneQuery(string sqlString, string instanceName)
        {
            int value = 0;
            using (DataAccessBroker broker = DataAccessFactory.Instance(instanceName))
            {
                value = broker.ExecuteNonQuery(sqlString, null, CommandType.Text);
            }
            return value;
        }

        public static int ExecuteNoneQuery(string sqlString)
        {
            int value = 0;
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                value = broker.ExecuteNonQuery(sqlString, null, CommandType.Text);
            }
            return value;
        }

        public static int ExecuteStoreProcedure(string spName, DataAccessBroker broker)
        {
            int value = 0;
            value = broker.ExecuteNonQuery(spName, null, CommandType.StoredProcedure);
            return value;
        }

        public static int ExecuteStoreProcedure(string spName)
        {
            int value = 0;
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                value = broker.ExecuteNonQuery(spName, null, CommandType.StoredProcedure);
            }
            return value;
        }

        public static int ExecuteStoreProcedureHasReturn(string spName, DataAccessParameterCollection parameters)
        {
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                var rowsaffected = broker.ExecuteCommand(spName, parameters);
                return 1;
            }
        }

        public static Dictionary<string, object> ExecuteStoreProcedure(string spName, DataAccessParameterCollection parameters)
        {
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                return broker.ExecuteProcReturnOutput(spName, parameters);
            }
        }

        /// <summary>
        /// 根据SQL语句与参数集合查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static EntityCollection<T> Select<T>(string sqlString, DataAccessParameterCollection parameters)
            where T : EntityBase, new()
        {
            return Select<T>(sqlString, parameters, CommandType.Text);
        }

        public static EntityCollection<T> SelectByBroker<T>(string sqlString, DataAccessParameterCollection parameters, DataAccessBroker broker)
            where T : EntityBase, new()
        {
            return SelectByBroker<T>(sqlString, parameters, CommandType.Text, broker);
        }

        public static EntityCollection<T> SelectAll<T>(string orderby)
           where T : EntityBase, new()
        {
            T schema = new T();
            string sqlString = "SELECT * FROM " + schema.OringTableSchema.TableName + " ORDER BY " + orderby;
            return Select<T>(sqlString);
        }


        public static void ExcuteNoneQuery(string sqlString)
        {
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                broker.ExecuteNonQuery(sqlString, null, CommandType.Text);
            }
        }

        public static void ExcuteNoneQuery(string sqlString, DataAccessBroker broker)
        {
            broker.ExecuteNonQuery(sqlString, null, CommandType.Text);
        }

        public static void ExcuteNoneQuery(string sqlString, DataAccessParameterCollection para, CommandType type)
        {
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                broker.ExecuteNonQuery(sqlString, para, type);
            }
        }


        public static string SelectScalar(string sqlString)
        {
            object value = null;
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                value = broker.ExecuteSQLScalar(sqlString);
            }
            return value == null ? null : value.ToString();
        }

        public static string SelectScalar(string sqlString, DataAccessBroker broker)
        {
            object value = broker.ExecuteSQLScalar(sqlString);
            return value == null ? null : value.ToString();
        }

        public static object SelectScalar(string sqlString, DataAccessParameterCollection paraCollection)
        {
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                return broker.ExecuteScalar(sqlString, paraCollection, CommandType.Text);
            }
        }

        /// <summary>
        /// 根据WhereBuilder查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="wb"></param>
        /// <returns></returns>
        public static EntityCollection<T> Select<T>(WhereBuilder wb)
            where T : EntityBase, new()
        {
            return Select<T>(wb.SQLString, wb.Parameters);
        }

        public static EntityCollection<T> SelectByBroker<T>(WhereBuilder wb, DataAccessBroker broker)
            where T : EntityBase, new()
        {
            return SelectByBroker<T>(wb.SQLString, wb.Parameters, broker);
        }

        public static DataSet SelectDataSet(WhereBuilder wb)
        {
            return Select(wb.SQLString, wb.Parameters);
        }

        /// <summary>
        /// 根据实体条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static EntityCollection<T> Select<T>(T entity)
            where T : EntityBase, new()
        {
            WhereBuilder wb = new WhereBuilder(entity);
            return Select<T>(wb);
        }

        public static EntityCollection<T> SelectByBroker<T>(T entity, DataAccessBroker broker)
            where T : EntityBase, new()
        {
            WhereBuilder wb = new WhereBuilder(entity);
            return SelectByBroker<T>(wb, broker);
        }


        public static T SelectSingle<T>(T entity)
            where T : EntityBase, new()
        {
            T result = null;
            EntityCollection<T> list = Select<T>(entity);
            if (list != null && list.Count > 0)
            {
                result = list[0];
            }
            return result;
        }

        public static T SelectSingleByBroker<T>(T entity, DataAccessBroker broker)
            where T : EntityBase, new()
        {
            T result = null;
            EntityCollection<T> list = SelectByBroker<T>(entity, broker);
            if (list != null && list.Count > 0)
            {
                result = list[0];
            }
            return result;
        }

        public static T SelectSingle<T>(WhereBuilder wb)
            where T : EntityBase, new()
        {
            T result = null;
            EntityCollection<T> list = Select<T>(wb);
            if (list != null && list.Count > 0)
            {
                result = list[0];
            }
            return result;
        }

        public static T SelectSingle<T>(string id)
            where T : EntityBase, new()
        {
            T t = new T();
            t.SetData(t.OringTableSchema.KeyColumnInfo[0].ColumnName, id);
            return SelectSingle<T>(t);
        }

        public static T SelectSingleByBroker<T>(string id, DataAccessBroker broker)
            where T : EntityBase, new()
        {
            T t = new T();
            t.SetData(t.OringTableSchema.KeyColumnInfo[0].ColumnName, id);
            return SelectSingleByBroker<T>(t, broker);
        }


        public static DataSet SelectDataSet(EntityBase entity)
        {
            WhereBuilder wb = new WhereBuilder(entity);
            return SelectDataSet(wb);
        }

        /// <summary>
        /// 跟根据查询参数查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static DataTable QueryDataSet(IQuery query)
        {
            WhereBuilder wb = query.ParseSQL();
            return SelectDataSet(wb).Tables[0];
        }



        /// <summary>
        /// 根据存储过程查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandString"></param>
        /// <returns></returns>
        public static EntityCollection<T> SelectCommand<T>(string commandString)
            where T : EntityBase, new()
        {
            return Select<T>(commandString, null, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 根据存储过程与参数集合查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static EntityCollection<T> SelectCommand<T>(string commandString, DataAccessParameterCollection parameters)
            where T : EntityBase, new()
        {
            return Select<T>(commandString, parameters, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 把一行Reader中的数据装载成Entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="values"></param>
        /// <param name="columns"></param>
        /// <param name="columnType"></param>
        /// <param name="nameChange"></param>
        /// <returns></returns>
        static T CreateEntity<T>(IDataReader reader, object[] values, List<string> columns,
            Dictionary<string, Type> columnType,
            Dictionary<string, string> nameChange)
            where T : EntityBase, new()
        {
            int fieldCount = reader.FieldCount;
            T t = new T();
            reader.GetValues(values);
            for (int i = 0; i < fieldCount; i++)
            {
                if (!columnType.ContainsKey(columns[i]))
                {
                    t.SetData(columns[i], values[i]);
                    continue;
                }

                if (values[i] == DBNull.Value)
                {
                    if (columnType[columns[i]] == typeof(int))
                    {
                        t.SetData(nameChange[columns[i]], 0);
                    }
                    if (columnType[columns[i]] == typeof(decimal))
                    {
                        t.SetData(nameChange[columns[i]], 0.0d);
                    }
                    else if (columnType[columns[i]] == typeof(bool))
                    {
                        t.SetData(nameChange[columns[i]], false);
                    }
                    else if (columnType[columns[i]] == typeof(DateTime))
                    {
                        t.SetData(nameChange[columns[i]], DateTime.MaxValue);
                    }
                    else
                    {
                        t.SetData(nameChange[columns[i]], string.Empty);
                    }
                }
                else
                {
                    if (values[i].GetType() == columnType[columns[i]])
                    {
                        t.SetData(nameChange[columns[i]], values[i]);
                    }
                    else
                    {
                        if (columnType[columns[i]] == Type.GetType("System.Int32"))
                        {
                            t.SetData(nameChange[columns[i]], int.Parse(values[i].ToString()));
                        }
                        else if (columnType[columns[i]] == Type.GetType("System.Boolean"))
                        {
                            if (values[i].ToString() == "0")
                            {
                                t.SetData(nameChange[columns[i]], false);
                            }
                            else
                            {
                                t.SetData(nameChange[columns[i]], true);
                            }
                        }
                        else
                        {
                            t.SetData(nameChange[columns[i]], values[i]);
                        }
                    }
                }
            }
            return t;
        }


        /// <summary>
        /// 获取数据通过Command
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command">SQL</param>
        /// <returns></returns>
        public static EntityCollection<T> Select<T>(string queryString, DataAccessParameterCollection parameters, CommandType cmdType)
            where T : EntityBase, new()
        {
            EntityCollection<T> result = new EntityCollection<T>();
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                IDataReader reader = broker.ExecuteReader(queryString, parameters, cmdType);

                int fieldCount = reader.FieldCount;
                List<string> columns = new List<string>();

                Dictionary<string, Type> columnType = new Dictionary<string, Type>();
                Dictionary<string, string> nameChange = new Dictionary<string, string>();

                T col = new T();
                object[] values = new object[fieldCount];
                for (int i = 0; i < fieldCount; i++)
                {
                    columns.Add(reader.GetName(i));


                    for (int m = 0; m < col.OringTableSchema.AllColumnInfo.Count; m++)
                    {
                        if (col.OringTableSchema.AllColumnInfo[m].ColumnName.ToUpper() == reader.GetName(i).ToUpper())
                        {
                            columnType.Add(reader.GetName(i), col.OringTableSchema.AllColumnInfo[m].DataType);

                            nameChange.Add(reader.GetName(i), col.OringTableSchema.AllColumnInfo[m].ColumnName);
                        }
                    }

                }

                while (reader.Read())
                {
                    T t = new T();
                    reader.GetValues(values);
                    for (int i = 0; i < fieldCount; i++)
                    {
                        if (!(values[i] == DBNull.Value))
                        {
                            if (!columnType.ContainsKey(columns[i]))
                            {
                                continue;
                            }

                            if (values[i].GetType() == columnType[columns[i]])
                            {
                                t.SetData(nameChange[columns[i]], values[i]);
                            }
                            else
                            {
                                if (columnType[columns[i]] == Type.GetType("System.Int32"))
                                {
                                    t.SetData(nameChange[columns[i]], int.Parse(values[i].ToString()));
                                }
                                else if (columnType[columns[i]] == Type.GetType("System.Boolean"))
                                {
                                    if (values[i].ToString() == "0")
                                    {
                                        t.SetData(nameChange[columns[i]], false);
                                    }
                                    else
                                    {
                                        t.SetData(nameChange[columns[i]], true);
                                    }
                                }
                                else if (columnType[columns[i]] == Type.GetType("System.DateTime"))
                                {
                                    t.SetData(nameChange[columns[i]], values[i].ToString());
                                }
                                else if (columnType[columns[i]] == Type.GetType("System.Decimal"))
                                {
                                        t.SetData(nameChange[columns[i]], values[i].ToString());
                                }
                                else
                                {
                                    t.SetData(nameChange[columns[i]], values[i]);
                                }
                            }
                        }
                    }
                    result.Add(t);
                }
                reader.Dispose();
            }
            return result;
        }

        public static EntityCollection<T> SelectByBroker<T>(string queryString, DataAccessParameterCollection parameters, CommandType cmdType, DataAccessBroker broker)
           where T : EntityBase, new()
        {
            EntityCollection<T> result = new EntityCollection<T>();

            IDataReader reader = broker.ExecuteReader(queryString, parameters, cmdType);

            int fieldCount = reader.FieldCount;
            List<string> columns = new List<string>();

            Dictionary<string, Type> columnType = new Dictionary<string, Type>();
            Dictionary<string, string> nameChange = new Dictionary<string, string>();

            T col = new T();
            object[] values = new object[fieldCount];
            for (int i = 0; i < fieldCount; i++)
            {
                columns.Add(reader.GetName(i));


                for (int m = 0; m < col.OringTableSchema.AllColumnInfo.Count; m++)
                {
                    if (col.OringTableSchema.AllColumnInfo[m].ColumnName.ToUpper() == reader.GetName(i).ToUpper())
                    {
                        columnType.Add(reader.GetName(i), col.OringTableSchema.AllColumnInfo[m].DataType);

                        nameChange.Add(reader.GetName(i), col.OringTableSchema.AllColumnInfo[m].ColumnName);
                    }
                }

            }

            while (reader.Read())
            {
                T t = new T();
                reader.GetValues(values);
                for (int i = 0; i < fieldCount; i++)
                {
                    if (!(values[i] == DBNull.Value))
                    {
                        if (!columnType.ContainsKey(columns[i]))
                        {
                            continue;
                        }

                        if (values[i].GetType() == columnType[columns[i]])
                        {
                            t.SetData(nameChange[columns[i]], values[i]);
                        }
                        else
                        {
                            if (columnType[columns[i]] == Type.GetType("System.Int32"))
                            {
                                t.SetData(nameChange[columns[i]], int.Parse(values[i].ToString()));
                            }
                            else if (columnType[columns[i]] == Type.GetType("System.Boolean"))
                            {
                                if (values[i].ToString() == "0")
                                {
                                    t.SetData(nameChange[columns[i]], false);
                                }
                                else
                                {
                                    t.SetData(nameChange[columns[i]], true);
                                }
                            }
                            else
                            {
                                t.SetData(nameChange[columns[i]], values[i]);
                            }
                        }
                    }
                }
                result.Add(t);
            }
            reader.Dispose();

            return result;
        }

        public static DataSet Select(string queryString, DataAccessParameterCollection parameters)
        {
            DataSet ds = null;
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                ds = broker.FillSQLDataSet(queryString, parameters);
            }
            return ds;
        }

        public static DataSet Select(string queryString, string instanceName, DataAccessParameterCollection parameters)
        {
            DataSet ds = null;
            using (DataAccessBroker broker = DataAccessFactory.Instance(instanceName))
            {
                ds = broker.FillSQLDataSet(queryString, parameters);
            }
            return ds;
        }

        public static PerformanceEntityCollection<T> PerformanceSelect<T>(string queryString, DataAccessParameterCollection parameters, CommandType cmdType)
            where T : EntityBase, new()
        {
            PerformanceEntityCollection<T> result = null;
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                IDataReader reader = broker.ExecuteReader(queryString, parameters, cmdType);

                int fieldCount = reader.FieldCount;
                List<string> columns = new List<string>();
                object[] values = new object[fieldCount];
                for (int i = 0; i < fieldCount; i++)
                {
                    columns.Add(reader.GetName(i));
                }
                result = new PerformanceEntityCollection<T>(columns);

                while (reader.Read())
                {
                    reader.GetValues(values);
                    result.Add(values);
                }
                reader.Dispose();
            }
            return result;
        }
        #endregion

        #region update
        /// <summary>
        /// 更新-独立事务
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool Update(EntityBase entity)
        {
            bool result = true;
            try
            {
                using (DataAccessBroker broker = DataAccessFactory.Instance())
                {
                    Update(entity, broker);
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 更新-事务支持 - 若为交易视图表则会根据最后修改时间自动计算表名。
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="broker"></param>
        public static void Update(EntityBase entity, DataAccessBroker broker)
        {
            List<ColumnInfo> listUpdateColumnInfo = new List<ColumnInfo>();
            foreach (ColumnInfo columnInfo in entity.OringTableSchema.ValueColumnInfo)
            {
                if (columnInfo.ColumnName == "CREATE_TIME" || columnInfo.ColumnName == "CREATED_BY" || columnInfo.ColumnName == "EFFECTIVE_DATE" || columnInfo.ColumnName == "EXPIRATION_DATE")
                {
                    continue;
                }
                ColumnInfo updateColumnInfo = new ColumnInfo(columnInfo.ColumnName, columnInfo.ColumnCaption, columnInfo.IsPrimaryKey, columnInfo.DataType);

                listUpdateColumnInfo.Add(updateColumnInfo);
            }

            string sqlString = "";
            sqlString = "UPDATE " + entity.OringTableSchema.TableName + " SET " + ParseSQL(listUpdateColumnInfo, broker.ParameterPrefix, ",") + " WHERE " + ParseSQL(entity.OringTableSchema.KeyColumnInfo, broker.ParameterPrefix, " AND ");
            DataAccessParameterCollection dpc = new DataAccessParameterCollection();
            object tpValue;
            foreach (ColumnInfo field in entity.OringTableSchema.AllColumnInfo)
            {
                if (field.ColumnName == "CREATE_TIME" || field.ColumnName == "CREATED_BY" || field.ColumnName== "EFFECTIVE_DATE"|| field.ColumnName== "EXPIRATION_DATE")
                {
                    continue;
                }
                //dpc.AddWithValue(field.ColumnName, entity.GetData(field.ColumnName));
                tpValue = entity.GetData(field.ColumnName);
                if (tpValue == null)
                {
                    if (field.DataType == typeof(bool))
                    {
                        tpValue = false;
                    }
                    else if (field.DataType == typeof(string))
                    {
                        tpValue = "";
                    }
                    else if (field.DataType == typeof(int))
                    {
                        tpValue = 0;
                    }
                    else if (field.DataType == typeof(decimal))
                    {
                        tpValue = 0;
                    }
                    else if (field.DataType == typeof(DateTime))
                    {
                        tpValue = DateTime.MaxValue;
                    }
                    else
                    {
                        tpValue = "";
                    }
                }
                dpc.AddWithValue(field, tpValue);
            }
            broker.ExecuteSQL(sqlString, dpc);
        }

        /// <summary>
        /// 更新-事务支持 - 若为交易视图表则会根据最后修改时间自动计算表名。
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="broker"></param>
        public static void UpdateEX(EntityBase entity, DataAccessBroker broker)
        {
            string sqlString = "";
            sqlString = "UPDATE " + entity.OringTableSchema.TableName + " SET " + ParseSQL(entity.OringTableSchema.ValueColumnInfo, broker.ParameterPrefix, ",") + " WHERE " + ParseSQL(entity.OringTableSchema.KeyColumnInfo, broker.ParameterPrefix, " AND ");
            DataAccessParameterCollection dpc = new DataAccessParameterCollection();
            object tpValue;
            foreach (ColumnInfo field in entity.OringTableSchema.AllColumnInfo)
            {
                //dpc.AddWithValue(field.ColumnName, entity.GetData(field.ColumnName));
                tpValue = entity.GetData(field.ColumnName);
                if (tpValue == null)
                {
                    if (field.DataType == typeof(bool))
                    {
                        tpValue = false;
                    }
                    else if (field.DataType == typeof(string))
                    {
                        tpValue = "";
                    }
                    else if (field.DataType == typeof(int))
                    {
                        tpValue = 0;
                    }
                    else if (field.DataType == typeof(decimal))
                    {
                        tpValue = 0;
                    }
                    else if (field.DataType == typeof(DateTime))
                    {
                        //tpValue = DateTime.MaxValue;
                    }
                    else
                    {
                        tpValue = "";
                    }
                }
                if (field.DataType == typeof(DateTime) && tpValue != null && tpValue.ToString() == DateTime.MaxValue.ToString())
                {
                    tpValue = null;
                }
                dpc.AddWithValue(field, tpValue);
            }
            broker.ExecuteSQL(sqlString, dpc);
        }

        /// <summary>
        /// 批量更新-独立事务
        /// </summary>
        /// <param name="entity"></param>
        public static bool Update<T>(IEnumerable<T> list)
            where T : EntityBase, new()
        {
            DataAccessBroker broker = DataAccessFactory.Instance();
            bool result = true;
            try
            {
                broker.BeginTransaction();
                Update<T>(list, broker);
                broker.Commit();
            }
            catch (Exception ex)
            {
                result = false;
                broker.RollBack();
                throw ex;
            }
            finally
            {
                broker.Close();
            }
            return result;
        }

        /// <summary>
        /// 批量更新-事务支持
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="broker"></param>
        /// <returns></returns>
        public static void Update<T>(IEnumerable<T> list, DataAccessBroker broker)
            where T : EntityBase
        {
            foreach (T entity in list)
            {
                Update(entity, broker);
            }
        }

        /// <summary>
        /// 批量更新-事务支持
        /// 此方法不更新DateTime为null的情况
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="broker"></param>
        /// <returns></returns>
        public static void UpdateEX<T>(IEnumerable<T> list, DataAccessBroker broker)
            where T : EntityBase
        {
            foreach (T entity in list)
            {
                UpdateEX(entity, broker);
            }
        }
        #endregion

        #region insert
        /// <summary>
        /// 新增-独立事务
        /// </summary>
        /// <param name="entity"></param>
        public static bool Insert(EntityBase entity)
        {
            bool result = true;
            //try
            //{
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                Insert(entity, broker);
            }
            //}
            //catch
            //{
            //   result = false;
            //}
            return result;
        }

        /// <summary>
        /// 批量新增-独立事务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// Jerry 2009年3月23日 老刘修改了此方法 只Creat一次Command 减少内存的损耗 修改后方法见下方395行开始
        //public static bool Insert<T>(List<T> list)
        //    where T : EntityBase
        //{
        //    DataAccessBroker broker = DataAccessFactory.Instance();
        //    bool result = true;
        //    try
        //    {
        //        broker.BeginTransaction();
        //        Insert(list, broker);
        //        broker.Commit();
        //    }
        //    catch
        //    {
        //        result = false;
        //        broker.RollBack();
        //    }
        //    finally
        //    {
        //        broker.Close();
        //    }
        //    return result;
        //}

        public static bool Insert<T>(List<T> list)
            where T : EntityBase
        {
            DataAccessBroker broker = DataAccessFactory.Instance();
            bool result = true;
            try
            {
                if (list != null && list.Count > 0)
                {
                    broker.BeginTransaction();
                    T t = list[0];
                    //string sqlString = "INSERT INTO " + t.OringTableSchema.TableName + " ( " + ParseInsertSQL(t.OringTableSchema.AllColumnInfo, "") + " ) VALUES( " + ParseInsertSQL(t.OringTableSchema.AllColumnInfo, broker.ParameterPrefix) + ")";
                    //broker.CreateCommandE(sqlString);

                    foreach (T entity in list)
                    {
                        Insert(entity, broker);
                    }
                    broker.Commit();
                }
            }
            catch (Exception ex)
            {
                result = false;
                broker.RollBack();
                throw ex;
            }
            finally
            {
                broker.Close();
                broker.Dispose();
            }
            return result;
        }

        /// <summary>
        /// 交易表的表名
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>从12个表改为单表</remarks>
        static string GetTableName(EntityBase entity)
        {
            return entity.OringTableSchema.TableName;
        }

        /// <summary>
        /// 新增-事务支持-若为交易视图表则会根据最后修改时间自动计算表名。
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="broker"></param>
        public static void Insert(EntityBase entity, DataAccessBroker broker)
        {
            string sqlString = "";
            sqlString = "INSERT INTO " + entity.OringTableSchema.TableName + " ( " + ParseInsertSQL(entity.OringTableSchema.NoIdentityColumnInfo, "") + " ) VALUES( " + ParseInsertSQL(entity.OringTableSchema.NoIdentityColumnInfo, broker.ParameterPrefix) + ")";
            DataAccessParameterCollection dpc = new DataAccessParameterCollection();
            object tpValue;
            foreach (ColumnInfo field in entity.OringTableSchema.AllColumnInfo)
            {
                if (field.IsIdentity)
                {
                    continue;
                }

                //dpc.AddWithValue(field.ColumnName, entity.GetData(field.ColumnName));
                tpValue = entity.GetData(field.ColumnName);
                //if (tpValue == null)
                //{
                //    if (field.DataType == typeof(bool))
                //    {
                //        tpValue = DBNull.Value;
                //    }
                //    else if (field.DataType == typeof(string))
                //    {
                //        tpValue = "";
                //    }
                //    else if (field.DataType == typeof(int))
                //    {
                //        tpValue = 0;
                //    }
                //    else if (field.DataType == typeof(decimal))
                //    {
                //        tpValue = 0;
                //    }
                //    else if (field.DataType == typeof(DateTime))
                //    {
                //        if (!field.AllowNull)
                //            tpValue = DateTime.Now;
                //    }
                //    else
                //    {
                //        tpValue = "";
                //    }
                //}
                dpc.AddWithValue(field, tpValue);
            }
            broker.ExecuteSQL(sqlString, dpc);
        }

        /// <summary>
        /// 新增-事务支持-若为交易视图表则会根据最后修改时间自动计算表名。
        /// 此方法不把时间为NULL的替换为最大时间
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="broker"></param>
        public static void InsertEX(EntityBase entity, DataAccessBroker broker)
        {
            string sqlString = "";
            sqlString = "INSERT INTO " + entity.OringTableSchema.TableName + " ( " + ParseInsertSQL(entity.OringTableSchema.NoIdentityColumnInfo, "") + " ) VALUES( " + ParseInsertSQL(entity.OringTableSchema.NoIdentityColumnInfo, broker.ParameterPrefix) + ")";
            DataAccessParameterCollection dpc = new DataAccessParameterCollection();
            object tpValue;
            foreach (ColumnInfo field in entity.OringTableSchema.AllColumnInfo)
            {
                if (field.IsIdentity)
                {
                    continue;
                }

                //dpc.AddWithValue(field.ColumnName, entity.GetData(field.ColumnName));
                tpValue = entity.GetData(field.ColumnName);
                if (tpValue == null)
                {
                    if (field.DataType == typeof(bool))
                    {
                        tpValue = false;
                    }
                    else if (field.DataType == typeof(string))
                    {
                        tpValue = "";
                    }
                    else if (field.DataType == typeof(int))
                    {
                        tpValue = 0;
                    }
                    else if (field.DataType == typeof(decimal))
                    {
                        tpValue = 0;
                    }
                    else if (field.DataType == typeof(DateTime))
                    {
                        //tpValue = DateTime.MaxValue;
                    }
                    else
                    {
                        tpValue = "";
                    }
                }
                if (field.DataType == typeof(DateTime) && tpValue != null && tpValue.ToString() == DateTime.MaxValue.ToString())
                {
                    tpValue = null;
                }
                dpc.AddWithValue(field, tpValue);
            }
            broker.ExecuteSQL(sqlString, dpc);
        }

        /// <summary>
        /// 批量新增-事务支持
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="broker"></param>
        /// <returns></returns>
        public static void Insert<T>(List<T> list, DataAccessBroker broker)
            where T : EntityBase
        {
            foreach (T entity in list)
            {
                Insert(entity, broker);
            }
        }

        /// <summary>
        /// 批量新增-事务支持
        /// 此方法不替换DateTime为NULL的情况
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="broker"></param>
        /// <returns></returns>
        public static void InsertEX<T>(List<T> list, DataAccessBroker broker)
            where T : EntityBase
        {
            foreach (T entity in list)
            {
                InsertEX(entity, broker);
            }
        }
        ///// <summary>
        ///// 批量插入数据
        ///// </summary>
        ///// <param name="dt"></param>
        ///// <param name="tableName"></param>
        //public static void BulkInsert(DataTable dt, string tableName)
        //{
        //    using (DataAccessBroker broker = DataAccessFactory.Instance())
        //    {
        //        broker.BulkInsert(dt, tableName);
        //    }
        //}
        ///// <summary>
        ///// 批量插入数据
        ///// </summary>
        ///// <param name="dt"></param>
        ///// <param name="tableName"></param>
        ///// <param name="broker"></param>
        //public static void BulkInsert(DataTable dt, string tableName, DataAccessBroker broker)
        //{
        //    broker.BulkInsert(dt, tableName);
        //}
        #endregion

        #region delete
        /// <summary>
        /// 删除-独立事务-根据主键删除
        /// </summary>
        /// <param name="entity"></param>
        public static bool Delete(EntityBase entity)
        {
            bool result = true;
            try
            {
                using (DataAccessBroker broker = DataAccessFactory.Instance())
                {
                    Delete(entity, broker);
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 删除-独立事务-根据所有非空字段AND条件删除
        /// </summary>
        /// <param name="entity"></param>
        public static bool DeleteEntity(EntityBase entity)
        {
            bool result = true;
            try
            {
                using (DataAccessBroker broker = DataAccessFactory.Instance())
                {
                    DeleteEntity(entity, broker);
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 删除-事务支持-根据主键删除
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="broker"></param>
        public static void Delete(EntityBase entity, DataAccessBroker broker)
        {
            string sqlString = "DELETE FROM " + entity.OringTableSchema.TableName + " WHERE " + ParseSQL(entity.OringTableSchema.KeyColumnInfo, broker.ParameterPrefix, " AND ");
            DataAccessParameterCollection dpc = new DataAccessParameterCollection();
            foreach (ColumnInfo field in entity.OringTableSchema.KeyColumnInfo)
            {
                dpc.AddWithValue(field, entity.GetData(field.ColumnName));
            }
            broker.ExecuteSQL(sqlString, dpc);
        }

        /// <summary>
        /// 删除-事务支持-根据所有非空字段AND条件删除
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="broker"></param>
        public static void DeleteEntity(EntityBase entity, DataAccessBroker broker)
        {
            WhereBuilder wb = new WhereBuilder("DELETE FROM " + entity.OringTableSchema.TableName);
            wb.AddAndCondition(entity);
            broker.ExecuteSQL(wb.SQLString, wb.Parameters);
        }

        /// <summary>
        /// 批量删除-独立事务-根据主键删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static bool Delete<T>(List<T> list)
            where T : EntityBase
        {
            DataAccessBroker broker = DataAccessFactory.Instance();
            bool result = true;
            try
            {
                broker.BeginTransaction();
                Delete<T>(list, broker);
                broker.Commit();
            }
            catch
            {
                result = false;
                broker.RollBack();
            }
            finally
            {
                broker.Close();
            }
            return result;
        }

        /// <summary>
        /// 批量删除-事务支持-根据主键删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="broker"></param>
        /// <returns></returns>
        public static void Delete<T>(List<T> list, DataAccessBroker broker)
            where T : EntityBase
        {
            foreach (T entity in list)
            {
                Delete(entity, broker);
            }
        }
        #endregion delete

        #region save
        /// <summary>
        /// 根据状态保存-独立事务
        /// </summary>
        /// <param name="entity"></param>
        public static void Save(EntityBase entity)
        {
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                Save(entity, broker);
            }
        }

        /// <summary>
        /// 根据状态保存-事务支持
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="broker"></param>
        public static void Save(EntityBase entity, DataAccessBroker broker)
        {
            switch (entity.State)
            {
                case BusinessState.Added:
                    Insert(entity, broker);
                    break;
                case BusinessState.Modified:
                    Update(entity, broker);
                    break;
                case BusinessState.Deleted:
                    Delete(entity, broker);
                    break;
            }
        }

        /// <summary>
        /// 批量保存-独立事务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static bool Save<T>(List<T> list)
            where T : EntityBase
        {
            DataAccessBroker broker = DataAccessFactory.Instance();
            bool result = true;
            try
            {
                broker.BeginTransaction();
                Save<T>(list, broker);
                broker.Commit();
            }
            catch
            {
                result = false;
                broker.RollBack();
            }
            finally
            {
                broker.Close();
            }
            return result;
        }

        /// <summary>
        /// 批量保存-事务支持
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="broker"></param>
        /// <returns></returns>
        public static void Save<T>(List<T> list, DataAccessBroker broker)
            where T : EntityBase
        {
            foreach (T entity in list)
            {
                Save(entity, broker);
            }
        }
        #endregion

        #region Others
        /// <summary>
        /// ExecuteScalar 并返还回唯一值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="wb"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T ExecuteScalar<T>(WhereBuilder wb, T defaultValue)
        {
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                object result = broker.ExecuteScalar(wb);
                if (result != null && result != DBNull.Value)
                {
                    defaultValue = (T)result;
                }
            }
            return defaultValue;
        }

        public static string SelectMax<T>()
            where T : EntityBase, new()
        {
            T schema = new T();
            string sqlString = "SELECT to_char(MAX(to_number(" + schema.OringTableSchema.KeyColumnInfo[0].ColumnName + "))) FROM " + schema.OringTableSchema.TableName;
            WhereBuilder wb = new WhereBuilder(sqlString);
            return ExecuteScalar<string>(wb, "00000");
        }

        public static Dictionary<string, object> ExecuteProcReturnOutput(string commandString, DataAccessParameter[] dps)
        {
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                DataAccessParameterCollection dpc = new DataAccessParameterCollection();
                if (dps != null && dps.Length > 0)
                {
                    foreach (DataAccessParameter dp in dps)
                    {
                        dpc.Add(dp.ParameterName, dp);
                    }
                }
                return broker.ExecuteProcReturnOutput(commandString, dpc);
            }
        }

        public static Dictionary<string, object> ExecuteProcReturnOutput(DataAccessBroker broker, string commandString, DataAccessParameter[] dps)
        {
            DataAccessParameterCollection dpc = new DataAccessParameterCollection();
            if (dps != null && dps.Length > 0)
            {
                foreach (DataAccessParameter dp in dps)
                {
                    dpc.Add(dp.ParameterName, dp);
                }
            }
            return broker.ExecuteProcReturnOutput(commandString, dpc);
        }
        #endregion

        #region utils
        private static string ParseInsertSQL(List<ColumnInfo> fields, string pre)
        {
            string result = "";
            foreach (ColumnInfo field in fields)
            {
                result += pre + field.ColumnName + ",";
            }
            if (result.Length > 2)
                result = result.Substring(0, result.Length - 1);
            return result;
        }

        private static string ParseSQL(List<ColumnInfo> fields, string pre, string sep)
        {
            string result = "";
            foreach (ColumnInfo field in fields)
            {
                result += field.ColumnName + "=" + pre + field.ColumnName + sep;
            }
            if (result.Length > 2)
                result = result.Substring(0, result.Length - sep.Length);
            return result;
        }
        #endregion
    }
}
