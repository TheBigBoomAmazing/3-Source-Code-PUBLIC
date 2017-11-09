using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using eContract.DDP.Common;
using Suzsoft.Smart.Data;
using System.Xml;
using System.IO;
using System.Collections;
using eContract.DDP.Common.CommonJob;
using System.Windows.Forms;
using Suzsoft.Smart.EntityCore;
using eContract.Log;

namespace eContract.DDP.Common.Command
{
    /// <summary>
    /// 数据库命令
    /// </summary>
    public class DatabaseCommand : BaseCommand
    {
        #region 成员变量

        /// <summary>
        /// 源信息
        /// </summary>
        public ConnectionEntity SourceConnEntity = null;

        /// <summary>
        /// 目标信息
        /// </summary>
        public ConnectionEntity TargetConnEntity = null;

        /// <summary>
        /// 源配置对象
        /// </summary>
        public DataAccessConfiguration SourceDataAccessCfg = null;

        /// <summary>
        /// 目标配置对象
        /// </summary>
        public DataAccessConfiguration TargetDataAccessCfg = null;

        /// <summary>
        /// 表映射关系
        /// </summary>
        public List<TableMap> TableMapList = new List<TableMap>();

        /// <summary>
        /// 最大记录数
        /// </summary>
        public int MaxCount = -1;

        #endregion

        #region ICommand Members



        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="node"></param>
        public override void Initialize(Hashtable parameters, XmlNode node)
        {
            // 先调基类函数
            base.Initialize(parameters, node);


            // 取出最大记录数参数
            if (parameters.ContainsKey(DDPConst.Param_MaxCount) == true)
            {
                this.MaxCount = Convert.ToInt32(parameters[DDPConst.Param_MaxCount].ToString());
            }

            // 源连接
            XmlNode nodeSourceConnection = node.SelectSingleNode("SourceConnection");
            if (nodeSourceConnection != null)
            {
                this.SourceConnEntity = new ConnectionEntity(nodeSourceConnection);
                if (this.SourceConnEntity.Type == DDPConst.Conn_TYPE_ORACLE)
                {
                    this.SourceDataAccessCfg = new DataAccessConfiguration();
                    this.SourceDataAccessCfg.DBType = DataAccessFactory.DBTYPE_ORACLE;
                    this.SourceDataAccessCfg.Parameters["server"] = this.SourceConnEntity.Server;
                    this.SourceDataAccessCfg.Parameters["user"] = this.SourceConnEntity.UserID;
                    this.SourceDataAccessCfg.Parameters["pwd"] = this.SourceConnEntity.Password;
                }
                else if (this.SourceConnEntity.Type == DDPConst.Conn_TYPE_SQLSERVER)
                {
                    this.SourceDataAccessCfg = new DataAccessConfiguration();
                    this.SourceDataAccessCfg.DBType = "SQLSERVER";//DataAccessFactory.DBTYPE_SQL;
                    this.SourceDataAccessCfg.Parameters["server"] = this.SourceConnEntity.Server;
                    this.SourceDataAccessCfg.Parameters["user"] = this.SourceConnEntity.UserID;
                    this.SourceDataAccessCfg.Parameters["pwd"] = this.SourceConnEntity.Password;
                    this.SourceDataAccessCfg.Parameters["database"] = this.SourceConnEntity.Database;
                }
            }

            // 目标连接
            XmlNode nodeTargetConnection = node.SelectSingleNode("TargetConnection");
            if (nodeTargetConnection != null)
            {
                this.TargetConnEntity = new ConnectionEntity(nodeTargetConnection);
                if (this.TargetConnEntity.Type == DDPConst.Conn_TYPE_ORACLE)
                {
                    this.TargetDataAccessCfg = new DataAccessConfiguration();
                    this.TargetDataAccessCfg.DBType = DataAccessFactory.DBTYPE_ORACLE;
                    this.TargetDataAccessCfg.Parameters["server"] = this.TargetConnEntity.Server;
                    this.TargetDataAccessCfg.Parameters["user"] = this.TargetConnEntity.UserID;
                    this.TargetDataAccessCfg.Parameters["pwd"] = this.TargetConnEntity.Password;
                }
                else if (this.TargetConnEntity.Type == DDPConst.Conn_TYPE_SQLSERVER)
                {
                    this.TargetDataAccessCfg = new DataAccessConfiguration();
                    this.TargetDataAccessCfg.DBType = DataAccessFactory.DBTYPE_ORACLE;
                    this.TargetDataAccessCfg.Parameters["server"] = this.TargetConnEntity.Server;
                    this.TargetDataAccessCfg.Parameters["user"] = this.TargetConnEntity.UserID;
                    this.TargetDataAccessCfg.Parameters["pwd"] = this.TargetConnEntity.Password;
                    this.TargetDataAccessCfg.Parameters["database"] = this.TargetConnEntity.Database;
                }
            }

            // 表映射关系,支持多表
            XmlNodeList tableMapNodeList = node.SelectNodes("TableMap");
            for (int i = 0; i < tableMapNodeList.Count; i++)
            {
                TableMap tableMap = new TableMap(tableMapNodeList[i]);
                this.TableMapList.Add(tableMap);
            }

        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public override ResultCode Execute(ref Hashtable parameters, out string error)
        {
            error = "";

            bool hasDataIDList = false;
            if (parameters.ContainsKey(DDPConst.Param_HasDataIDList) == true)
                hasDataIDList = (bool)parameters[DDPConst.Param_HasDataIDList];
            if (hasDataIDList == true)
            {
                TableMap tableMap = this.TableMapList[0];// 应该只有一个表

                // 获取来源数据
                List<string> dataIDList = (List<string>)parameters[DDPConst.Param_DataIDList];
                // 写日志
                LogManager.Current.WriteCommonLog(this.JobCode, "程序传入来源数据，记录数为" + dataIDList.Count.ToString() + "",this.ThreadName);

                string splitOperator = (string)parameters[DDPConst.Param_SplitOperator];
                DataTable dtSource = this.GetSourceByIDList(tableMap,
                    tableMap.SourceTable.PrimaryKeys,
                    dataIDList,
                    splitOperator);              

                // 写日志
                LogManager.Current.WriteCommonLog(this.JobCode, "根据ID获得的实际记录数为" + dtSource.Rows.Count.ToString(), this.ThreadName);

                // 无数据时不再继续
                if (dtSource.Rows.Count == 0)
                    return ResultCode.Break;

                // 加入参数里，传给后面的命令
                parameters[DDPConst.Param_PrimaryKeys] = tableMap.SourceTable.PrimaryKeys;
                parameters[DDPConst.Param_DataAccessCfg] = this.SourceDataAccessCfg;

                // 保存到目标
                this.SaveToTarget(tableMap, dtSource, true);
            }
            else
            {
                for (int i = 0; i < TableMapList.Count; i++)
                {
                    TableMap tableMap = TableMapList[i];
                    tableMap.SourceTable.FileName = BaseCommand.ReplaceParameters(parameters, tableMap.SourceTable.FileName);

                    // 获取来源数据
                    DataTable dtSource = this.GetSourceData(parameters, tableMap);

                    // 无数据时不再继续
                    if (dtSource.Rows.Count == 0)
                        return ResultCode.Break;

                    // 只取第1个表的主键值，存放到list
                    if (i == 0 && tableMap.SourceTable.PrimaryKeyList != null && dtSource != null)
                    {
                        List<string> dataIDList = GetDataIDList(dtSource, tableMap.SourceTable.PrimaryKeys, DDPConst.C_ValueSplitOperator);

                        // 加入参数里，传给后面的命令
                        parameters[DDPConst.Param_PrimaryKeys] = tableMap.SourceTable.PrimaryKeys;
                        parameters[DDPConst.Param_DataIDList] = dataIDList;
                        parameters[DDPConst.Param_DataAccessCfg] = this.SourceDataAccessCfg;
                    }

                    // 保存到目标
                    this.SaveToTarget(tableMap, dtSource, true);

                }
            }

            return ResultCode.Success;
        }



        /// <summary>
        /// 设置ID集合到参数
        /// </summary>
        /// <param name="dtSource">数据table</param>
        /// <param name="fieldNames">字段名，多个以;分号隔开</param>
        /// <param name="paramters"></param>
        public static List<string> GetDataIDList(DataTable dtSource, string fieldNames,string valueSplitOperator)
        {
            string[] fieldList = fieldNames.Split(new char[] {','});

            // 处理的数据ID集合
            List<string> dataIDList = new List<string>();
            for (int j = 0; j < dtSource.Rows.Count; j++)
            {
                DataRow row = dtSource.Rows[j];
               
                string keyValues = GetFieldValue(row,fieldList,valueSplitOperator);
                dataIDList.Add(keyValues);
            }

            return dataIDList;
        }

        public string GetFieldValue(DataRow row, string fieldNames, string valueSplitOperator)
        {
            string[] fieldList = fieldNames.Split(new char[] { ',' });
            return GetFieldValue(row, fieldList, valueSplitOperator);
        }

        /// <summary>
        /// fieldNames必须以","逗分隔
        /// </summary>
        /// <param name="row"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        public static string GetFieldValue(DataRow row, string[] fieldList, string valueSplitOperator)
        {
            string keyValues = "";

            // 支持多个主键
            for (int x = 0; x < fieldList.Length; x++)
            {
                string key = fieldList[x];
                string value = row[key].ToString();
                if (keyValues != "")
                    keyValues += valueSplitOperator;
                keyValues += value;
            }

            return keyValues;
        }


        #endregion

        #region 获取来源数据

        /// <summary>
        /// 得到来源数据
        /// </summary>
        /// <param name="tableMap"></param>
        /// <returns></returns>
        public DataTable GetSourceData(Hashtable parameters,TableMap tableMap)
        {
            DataTable dt = null;
            if (this.SourceConnEntity.Type == DDPConst.Conn_TYPE_ORACLE
                || this.SourceConnEntity.Type == DDPConst.Conn_TYPE_SQLSERVER)
            {
                dt = this.GetSourceDataFromDB(parameters,tableMap);
            }
            else
            {
                dt = DatabaseCommand.GetSourceDataFromFile(parameters,tableMap,false,"",0);
            }

            return dt;
        }

        /// <summary>
        /// 从数据库得到来源数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetSourceDataFromDB(Hashtable parameters, TableMap tableMap)
        {
            DataTable dtSource = null;

            DataAccessBroker brokerSource = DataAccessFactory.Instance(this.SourceDataAccessCfg);
            try
            {
                string sqlString = tableMap.GetSourceSelectSQL(this.MaxCount);

                // 替换sql中的宏变量
                sqlString = BaseCommand.ReplaceParameters(this.InitialParameters, sqlString);

                DataSet dataSetSource = brokerSource.FillSQLDataSet(sqlString);

                // 源数据为空时，不做任何操作
                if (dataSetSource == null || dataSetSource.Tables.Count == 0)
                    return null;

                // 取第一个表
                dtSource = dataSetSource.Tables[0];
            }
            finally
            {
                brokerSource.Close();
            }

            // 写日志
            LogManager.Current.WriteCommonLog(this.JobCode, "从数据表" + tableMap.SourceTable.TableName + "获得" + dtSource.Rows.Count.ToString() + "笔记录。", this.ThreadName);

            return dtSource;
        }

     

        /// <summary>
        /// 从文件中得到来源数据
        /// </summary>
        /// <param name="tableMap"></param>
        /// <returns></returns>
        public static DataTable GetSourceDataFromFile(Hashtable parameters, TableMap tableMap, bool bIgnoreInsert, string strIgnoreFields, int iStartLine)
        {
            List<string> IgnoreFields = new List<string>();

            if (bIgnoreInsert && !string.IsNullOrEmpty(strIgnoreFields))
            {
                string[] IgnoreFieldNameArray = strIgnoreFields.Split(new char[] { ',' });
                for (int i = 0; i < IgnoreFieldNameArray.Length; i++)
                {
                    string field = IgnoreFieldNameArray[i].Trim();
                    if (field != "")
                        IgnoreFields.Add(field);
                }
            }

            string fileName = BaseCommand.ReplaceParameters(parameters, tableMap.SourceTable.FileName);

            // 创建表结构
            string[] fields = tableMap.SourceTable.FieldNames.Split(new char[] { ',' });
            DataTable dtSource = new DataTable();
            for (int i = 0; i < fields.Length; i++)
            {
                string field = fields[i].Trim();
                DataColumn col = new DataColumn(field);
                dtSource.Columns.Add(col);
            }

            if (tableMap.TargetTable.GUIDPrimaryKey != "")
            {
                DataColumn col = new DataColumn(tableMap.TargetTable.GUIDPrimaryKey);
                dtSource.Columns.Add(col);
            }

            int iLine = 0;

            // 读取文件中的数据            
            StreamReader sr = new StreamReader(fileName, Encoding.UTF8);//Default);  //this.JobEntity.TableMap.SourceFile
            try
            {
                while (sr.EndOfStream == false)
                {
                    Application.DoEvents();

                    iLine += 1;

                    // 读取一行
                    string line = sr.ReadLine().Trim();

                    if (iLine == iStartLine)
                        continue;

                    if (line == "")
                        continue;

                    // 按分隔符拆分字段
                    string[] fieldValues = line.Split(new char[] { tableMap.SourceTable.FieldSplitOperator });
                    if (bIgnoreInsert)
                    {
                        if ((fields.Length + IgnoreFields.Count) != fieldValues.Length)
                        {
                            LogManager.Current.WriteCommonLog("获取数据", fieldValues.ToString() + "配置的字段数量'" + fields.Length.ToString() + "'个与数据中的字段数量'" + fieldValues.Length.ToString() + "'个不一致", Guid.NewGuid().ToString());
                            continue;
                        }

                    }
                    else
                    {
                        if (fields.Length != fieldValues.Length)
                        {
                            LogManager.Current.WriteCommonLog("获取数据", fieldValues.ToString() + "配置的字段数量'" + fields.Length.ToString() + "'个与数据中的字段数量'" + fieldValues.Length.ToString() + "'个不一致", Guid.NewGuid().ToString());
                            continue;
                        }
                    }

                    // 创建行，并给各字段赋值
                    DataRow row = dtSource.NewRow();
                    int Ignore = 0;
                    for (int i = 0; i < fieldValues.Length; i++)
                    {
                        bool bIgnore = false;
                        if (bIgnoreInsert)
                        {
                            foreach (string strIgnore in IgnoreFields)
                            {
                                if (strIgnore.Equals(i.ToString()))
                                {
                                    bIgnore = true;
                                    Ignore += 1;
                                    break;
                                }
                            }
                        }

                        if (!bIgnore)
                        {
                            string fieldName = fields[i - Ignore].Trim();
                            string fieldValue = fieldValues[i];

                            if (tableMap.SourceTable.UTCTimeFields.IndexOf(fieldName) != -1)
                            {
                                DateTime leftDate = DateTime.Parse(fieldValue);
                                fieldValue = leftDate.ToString("yyyy-MM-dd HH:mm:ss");
                            }

                            if (tableMap.SourceTable.IntFields.IndexOf(fieldName) != -1)
                            {
                                fieldValue = ((int)decimal.Parse(fieldValue)).ToString();
                            }

                            if (tableMap.SourceTable.ArticleFields.IndexOf(fieldName) != -1)
                            {
                                string[] strArticleNos = fieldValue.Split('-');
                                foreach (string strArticle in strArticleNos)
                                {
                                    if (strArticle.Trim().Length == 6)
                                        fieldValue = strArticle;
                                }

                            }

                            row[fieldName] = fieldValue;
                        }
                    }

                    if (tableMap.TargetTable.GUIDPrimaryKey != "")
                    {
                        row[tableMap.TargetTable.GUIDPrimaryKey] = Guid.NewGuid().ToString();
                    }

                    dtSource.Rows.Add(row);
                }
            }
            finally
            {
                sr.Close();
            }

            // 写日志
            //LogManager.Current.WriteCommonLog(jobCode, "从文件" + fileName + "获得" + dtSource.Rows.Count.ToString() + "笔记录。", threadName);


            return dtSource;
        }

        /// <summary>
        /// 根据Data ID List取数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetSourceByIDList(TableMap tableMap,
            string fieldNames, 
            List<string> dataIDList,
            string dataSplitOperator)
        {
            DataTable retTable = new DataTable();

            DataAccessBroker brokerSource = DataAccessFactory.Instance(this.SourceDataAccessCfg);
            try
            {
                for (int x = 0; x < dataIDList.Count;)
                {
                    // 每批50个
                    int count = 50;
                    if (x + count > dataIDList.Count)
                        count = dataIDList.Count - x;

                    // 多条数据以or拼起来
                    List<string> tempDataIDList = new List<string>();
                    for (int y = 0; y < count; y++)
                    {
                        tempDataIDList.Add(dataIDList[x+y]);
                    }
                    x += count;

                    // 得到where语句
                    string whereSql = BaseCommand.MakeDataIDWhereSql(fieldNames, tempDataIDList, dataSplitOperator);

                    string sql = "select " + tableMap.SourceTable.FieldNames
                        + " from " + tableMap.SourceTable.TableName
                        + whereSql;

                    DataSet dataSet = brokerSource.FillSQLDataSet(sql);                    
                    if (dataSet == null || dataSet.Tables.Count == 0)
                        continue;// 继续下一批50

                    DataTable dtTemp = dataSet.Tables[0];

                    // 第一次时初始化列                   
                    if (retTable.Columns.Count == 0)
                    {
                        for (int x1 = 0; x1 < dtTemp.Columns.Count; x1++)
                        {
                            retTable.Columns.Add(dtTemp.Columns[x1].ColumnName, dtTemp.Columns[x1].DataType);
                        }
                    }

                    // 将临时表的数据复制到返回的表中
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        DataRow row = retTable.NewRow();
                        DataRow dtRow = dtTemp.Rows[i];
                        for (int j = 0; j < retTable.Columns.Count; j++)
                        {
                            row[j] = dtRow[j];
                        }
                        retTable.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                brokerSource.Close();
            }

            return retTable;
        }

       #endregion

        #region 保存到目标

        /// <summary>
        /// 保存到目标
        /// </summary>
        public void SaveToTarget(TableMap tableMap, DataTable dtSource,bool bMaxCount)
        {
            if (this.TargetConnEntity.Type == DDPConst.Conn_TYPE_TEXT)
            {
                this.SaveToTargetFile(tableMap, dtSource, bMaxCount);
            }
            else
            {
                SaveToTargetDB(tableMap, dtSource);
            }
        }

        /// <summary>
        /// 保存到目标文件
        /// </summary>
        /// <param name="dtSource"></param>
        public void SaveToTargetFile(TableMap tableMap, DataTable dtSource,bool bMaxCount)
        {
            // 先将文件名中的参数替换掉
            string targetFileName = BaseCommand.ReplaceParameters(this.InitialParameters, tableMap.TargetTable.FileName);

            // 确保目录存在
            int nIndex = targetFileName.LastIndexOf("\\");
            if (nIndex != -1)
            {
                string dir = targetFileName.Substring(0, nIndex);
                if (Directory.Exists(dir) == false)
                    Directory.CreateDirectory(dir);
            }

            // 将数据保存到目标文件
            StreamWriter sw = null;
            if (String.Compare(tableMap.TargetTable.Encoding, "UTF8", true) == 0)
                sw = new StreamWriter(targetFileName, false, Encoding.UTF8);  //C2-CI指定utf8
            else
                sw = new StreamWriter(targetFileName, false);
            try
            {

                // 最大记录数限制
                int nCount = dtSource.Rows.Count;
                if (bMaxCount == true)
                {
                    if (this.MaxCount != -1 && nCount > this.MaxCount)
                        nCount = this.MaxCount;
                }

                // 判断是否第一行输入行数
                if(tableMap.TargetTable.OneRowRecordsNum == true)
                    sw.WriteLine("#" + nCount.ToString());



                int addFieldCount = 0;
                if (tableMap.SourceTable.PrimaryKeys != "")
                    addFieldCount = tableMap.SourceTable.PrimaryKeyList.Length;

                // 导出每一行
                for (int i = 0; i < nCount; i++)
                {
                    DataRow row = dtSource.Rows[i];

                    string line = "";
                    for (int j = 0; j < row.Table.Columns.Count - addFieldCount; j++)  //注意要减去自已加的主键列
                    {
                        if (line != "")
                            line += tableMap.TargetTable.FieldSplitOperator;
                        line += row[j].ToString();
                    }
                    sw.WriteLine(line);
                }
            }
            finally
            {
                sw.Close();
            }

            // 写日志
            LogManager.Current.WriteCommonLog(this.JobCode, "成功将数据保存到目标文件" + targetFileName, this.ThreadName);
        }

        /// <summary>
        /// 保存到目标数据库
        /// </summary>
        /// <param name="tableMap"></param>
        /// <param name="dtSource"></param>
        private  void SaveToTargetDB(TableMap tableMap, DataTable dtSource)
        {         

            // 目标为数据表的情况
            string curProcess = "";
            DataAccessBroker broker = DataAccessFactory.Instance(this.TargetDataAccessCfg);
            try
            {
                // 事务开始
                curProcess = "事务开始";
                broker.BeginTransaction();

                DatabaseCommand.SaveToTargetDB(broker, tableMap, dtSource);

                // 事务提交
                curProcess = "事务提交";
                broker.Commit();
            }
            catch (Exception ex)
            {
                // 事务回滚
                broker.RollBack();

                throw ex;
            }
            finally
            {

                broker.Close();
            }

            // 写日志
            LogManager.Current.WriteCommonLog(this.JobCode, "成功将数据保存到目标表" + tableMap.TargetTable.TableName, this.ThreadName);
        }


        public static void SaveToTargetDB(DataAccessBroker broker,TableMap tableMap, DataTable dtSource)
        {

            // 目标为数据表的情况
            string curProcess = "";
            try
            {
                if (tableMap.IsFullData == true)
                {
                    // 清空目标表数据
                    curProcess = "清空目标表数据";
                    string deleteSql = tableMap.GetTargetDeleteAllSQL();
                    broker.ExecuteSQL(deleteSql);
                }

                // 保存数据
                curProcess = "保存数据";
                // 确保TableMap有FieldMap
                tableMap.InitialFieldMapByDataTable(dtSource);
                string insertSql = tableMap.GetTargetInsertSQL(broker.ParameterPrefix.ToString());
                for (int i = 0; i < dtSource.Rows.Count; i++)
                {
                    Application.DoEvents();

                    // 给目标表新增记录
                    DataRow row = dtSource.Rows[i];
                    DataAccessParameterCollection dpc = tableMap.GetTargetParameters(row, tableMap.TargetTable.NullFieldNameList);
                    broker.ExecuteSQL(insertSql, dpc);
                }
            }
            catch (Exception ex)
            {
                // 写日志
                string description = "运行中异常，目前执行到'" + curProcess + "',单一事务，事务回滚.";

                // 继续抛出异常
                throw new InvalidOperationException(description + ex.Message);
            }

        }

        #endregion

        public static void Insert<T>(DataAccessBroker broker, List<T> list) where T : EntityBase
        {

            // 目标为数据表的情况
            string curProcess = "";
            try
            {
                // 保存数据
                curProcess = "保存数据";
                if (list.Count > 0)
                {
                    string sqlString = "INSERT INTO " + list[0].OringTableSchema.TableName + " ( " + ParseInsertSQL(list[0].OringTableSchema.AllColumnInfo, "") + " ) VALUES( " + ParseInsertSQL(list[0].OringTableSchema.AllColumnInfo, ParameterPrefix) + ")";
                    foreach (T entity in list)
                    {
                        Application.DoEvents();

                        // 给目标表新增记录
                        DataAccessParameterCollection dpc = new DataAccessParameterCollection();
                        foreach (ColumnInfo field in entity.OringTableSchema.AllColumnInfo)
                        {
                            dpc.AddWithValue(field, entity.GetData(field.ColumnName));
                        }
                        broker.ExecuteSQL(sqlString, dpc);
                    }
                }
            }
            catch (Exception ex)
            {
                // 写日志
                string description = "运行中异常，目前执行到'" + curProcess + "',单一事务，事务回滚.";

                // 继续抛出异常
                throw new InvalidOperationException(description + ex.Message);
            }

        }

        public static void Update<T>(DataAccessBroker broker, List<T> list) where T : EntityBase
        {

            // 目标为数据表的情况
            string curProcess = "";
            try
            {
                // 保存数据
                curProcess = "修改数据";
                if (list.Count > 0)
                {
                    string sqlString = "UPDATE " + list[0].OringTableSchema.TableName + " SET " + ParseSQL(list[0].OringTableSchema.ValueColumnInfo, ",") + " WHERE " + ParseSQL(list[0].OringTableSchema.KeyColumnInfo, " AND ");
                    foreach (T entity in list)
                    {
                        Application.DoEvents();

                        // 给目标表新增记录
                        DataAccessParameterCollection dpc = new DataAccessParameterCollection();
                        foreach (ColumnInfo field in entity.OringTableSchema.AllColumnInfo)
                        {
                            dpc.AddWithValue(field, entity.GetData(field.ColumnName));
                        }
                        broker.ExecuteSQL(sqlString, dpc);
                    }
                }
            }
            catch (Exception ex)
            {
                // 写日志
                string description = "运行中异常，目前执行到'" + curProcess + "',单一事务，事务回滚.";

                // 继续抛出异常
                throw new InvalidOperationException(description + ex.Message);
            }

        }

        public const string ParameterPrefix = "@";

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

        private static string ParseSQL(List<ColumnInfo> fields, string sep)
        {
            string result = "";
            foreach (ColumnInfo field in fields)
            {
                result += "\"" + field.ColumnName + "\"=" + ParameterPrefix + field.ColumnName + sep;
            }
            if (result.Length > 2)
                result = result.Substring(0, result.Length - sep.Length);
            return result;
        }
     
    }
}
