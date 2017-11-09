using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using Suzsoft.Smart.EntityCore;
using System.Diagnostics;
using Suzsoft.Smart.Data;
using eContract.DDP.Common;
using System.Reflection;
using System.IO;

namespace eContract.DDP.Common
{
    /*
				<TableMap >
					<Source TableName="Item" PrimaryKeys="ItemNumber">
						<WhereSQL> where DownloadFlag='N' </WhereSQL>
						<OrderSQL> order by ReceiveDate </OrderSQL>
						<FieldNames RefSection="ItemFields">
						</FieldNames>
					</Source>
					<Target	
						FileName="%LocalFileName%.txt" 
						FieldSplitOperator="|" />
				</TableMap>
     */
    public class TableMap
    {
        public TableItem SourceTable = null;
        public TableItem TargetTable = null;

        // 字段映射
        public  List<FieldMap> FieldMapList = new List<FieldMap>();

        /// <summary>
        /// 缺省认为全量数据
        /// </summary>
        private bool _isFullData = false;

        // Header/Detail
        public string Name = "";

        /// <summary>
        /// 构造函数
        /// </summary>
        public TableMap()
        {
 
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="node"></param>
        public TableMap(XmlNode node)
        {
            Debug.Assert(node != null, "node参数不能为null");

            // 清空field list
            this.FieldMapList.Clear();

            // 来源信息
            XmlNode nodeSourceTable = node.SelectSingleNode("Source");
            if (nodeSourceTable != null)
                this.SourceTable = new TableItem(nodeSourceTable);

            // 目标信息
            XmlNode nodeTargetTable = node.SelectSingleNode("Target");
            if (nodeTargetTable != null)
                this.TargetTable = new TableItem(nodeTargetTable);

            // 判断是否是全量数据
            string isFullData = XmlUtil.GetAttrValue(node, "IsFullData").Trim();
            if (String.Compare(isFullData, "false", true) == 0)
                this._isFullData = false;

            // Name Jane 2009.6.5 加 用于区分Header和Detail
            this.Name = XmlUtil.GetAttrValue(node, "Name").Trim();

            // 初始化FieldMap列表，
            // 如果FieldMap不存在，则认为同步全部字段，且服务器表与客户端表结构完成一致
            XmlNodeList FieldMapList = node.SelectNodes("FieldMap");
            if (FieldMapList.Count > 0)
            {
                for (int i = 0; i < FieldMapList.Count; i++)
                {
                    XmlNode nodeFieldMap = FieldMapList[i];
                    FieldMap fieldMap = new FieldMap(nodeFieldMap);
                    this.FieldMapList.Add(fieldMap);
                }
            }
        }


        /// <summary>
        /// 根据DataTable初始化字段列表
        /// </summary>
        /// <param name="table"></param>
        public void InitialFieldMapByDataTable(DataTable table)
        {
            if (FieldMapList.Count == 0)
            {
                foreach (DataColumn column in table.Columns)
                {
                    FieldMapList.Add(new FieldMap(column.ColumnName, column.ColumnName));
                }
            }
        }
        public void InitialFieldMapByDataTable(DataRow row)
        {
            if (FieldMapList.Count == 0)
            {
                foreach (DataColumn column in row.Table.Columns)
                {
                    FieldMapList.Add(new FieldMap(column.ColumnName, column.ColumnName));
                }
            }
        }


        /// <summary>
        /// 是否是全量数据
        /// </summary>
        public bool IsFullData
        {
            get { return _isFullData; }
            set { _isFullData = value; }
        }


        #region Source端函数


        /// <summary>
        /// 得到客户端对应的Insert语句 Only call once
        /// </summary>
        /// <returns></returns>
        public string GetSourceInsertSQL()
        {
            if (this.FieldMapList == null || this.FieldMapList.Count == 0)
                return "";

            string strFields = "";
            string strValues = "";
            foreach (FieldMap field in FieldMapList)
            {
                if (strFields != "")
                    strFields += ",";
                strFields += field.SourceField;

                if (strValues != "")
                    strValues += ",";
                strValues += DataAccess.ParameterPrefix + field.SourceField;
            }

            string strCommand = "INSERT INTO " + this.SourceTable.TableName
                + " ( " + strFields + " ) "
                + " VALUES (" + strValues + ") ";

            return strCommand;
        }

        /// <summary>
        /// 得到客户端对应的左边Update语句，不包括where条件
        /// </summary>
        /// <returns></returns>
        public string GetSourceLeftUpdateSQL()
        {
            if (this.FieldMapList == null || this.FieldMapList.Count == 0)
                return "";

            string strFieldValues = "";
            foreach (FieldMap field in FieldMapList)
            {
                if (strFieldValues != "")
                    strFieldValues += ",";
                strFieldValues += field.SourceField + "=" + DataAccess.ParameterPrefix + field.SourceField;
            }

            string commandString = "UPDATE " + this.SourceTable.TableName
                + " SET " + strFieldValues;

            return commandString;
        }

        ///// <summary>
        ///// 组合Update语句，根据id值加where条件
        ///// </summary>
        ///// <param name="strLeftSql"></param>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public string GetSourceUpdateSQL(string strLeftSql, string id)
        //{
        //    return strLeftSql + " where " + this.SourceTable.PrimaryKey + "='" + id + "'";
        //}

        /// <summary>
        /// 得到客户端语句的参数值
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public DataAccessParameterCollection GetSourceParameters(DataRow row)
        {
            DataAccessParameterCollection dpc = new DataAccessParameterCollection();
            foreach (FieldMap field in FieldMapList)
            {
                ColumnInfo columnInfo = new ColumnInfo(field.SourceField, field.SourceField, false, row[field.TargetField].GetType());


                dpc.AddWithValue(columnInfo, row[field.TargetField]);
            }
            return dpc;
        }


        /// <summary>
        /// 得到服务器端的查询语句
        /// </summary>
        /// <returns></returns>
        public string GetSourceSelectSQL(int maxCount)
        {
            string sqlString = "";

            // 优先使用配置的SQL命令 2009/08/31 jane
            if (this.SourceTable.SelectSQL != "")
                return this.SourceTable.SelectSQL;

            string fields = "";            
            if (this.SourceTable.FieldNames != "") // 优先使用定义的FieldNames
                fields = this.SourceTable.FieldNames;
            else
                fields = this.GetSourceFields();

            // 把主键值加到字段里，因为可能配置的输出字段里没有主键字段
            if (this.SourceTable.PrimaryKeys != "")
                fields += " , " + this.SourceTable.PrimaryKeys;

            // 加最大记录数限制
            if (maxCount == -1)
                sqlString = "Select " + fields+ " from " + this.SourceTable.TableName;
            else
                sqlString = "Select top " + maxCount.ToString() + " " + fields + " from " + this.SourceTable.TableName;

            // where条件
            if (SourceTable.WhereSQL != "")
                sqlString += " " +SourceTable.WhereSQL;

            // order by
            if (this.SourceTable.OrderSQL != "")
                sqlString += " " + this.SourceTable.OrderSQL;
            
            return sqlString;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="dataID"></param>
        ///// <param name="dtReadDataID"></param>
        ///// <returns></returns>
        //public bool IsInSourceReadTable(string dataID, DataTable dtReadDataID)
        //{
        //    for (int i = 0; i < dtReadDataID.Rows.Count; i++)
        //    {
        //        DataRow row = dtReadDataID.Rows[i];
        //        string keyid = row[this.SourceTable.PrimaryKey].ToString().Trim();

        //        if (String.Compare(dataID, keyid, true) == 0)
        //            return true;
        //    }

        //    return false;
        //}

        /// <summary>
        /// 得到源字段，以逗号分隔
        /// </summary>
        /// <returns></returns>
        public string GetSourceFields()
        {
            string resultFields = "";


            foreach (FieldMap field in FieldMapList)
            {
                if (resultFields != "")
                    resultFields += ",";
                resultFields += field.SourceField;
            }

            if (resultFields == "")
                resultFields = "*";

            return resultFields;
        }

        /// <summary>
        /// 根据目标表字段类型，创建源表结构
        /// </summary>
        /// <returns></returns>
        public DataTable CreateSourceTableByTargetFieldType(DataTable dtTarget)
        {
            DataTable dtSource = new DataTable();

            // 确定存在字段关系
            this.InitialFieldMapByDataTable(dtTarget);

            // 创建表结构
            for (int i = 0; i < this.FieldMapList.Count; i++)
            {
                FieldMap fieldMap = this.FieldMapList[i];

                DataColumn col = new DataColumn(fieldMap.SourceField, dtTarget.Columns[fieldMap.TargetField].DataType);
                dtSource.Columns.Add(col);                
            }

            return dtSource;
        }

        #endregion


        #region Target端函数

        /// <summary>
        /// 得到源字段，以逗号分隔
        /// </summary>
        /// <returns></returns>
        public string GetTargetFields()
        {
            string resultFields = "";
            foreach (FieldMap field in FieldMapList)
            {
                if (resultFields != "")
                    resultFields += ",";
                resultFields += field.TargetField;
            }

            if (resultFields == "")
                resultFields = "*";

            return resultFields;
        }

        /// <summary>
        /// 得到目标端的查询语句
        /// </summary>
        /// <returns></returns>
        public string GetTargetSelectSQL()
        {
            //~~~~~~~~
            string field = this.GetTargetFields();
            string sqlString = "Select " + field + " from " + this.TargetTable.TableName;

            return sqlString;
        }

        /// <summary>
        /// 得到目标端的查询语句
        /// </summary>
        /// <returns></returns>
        public string GetTargetOneRecordSelectSQL()
        {
            //~~~~~~~~
            string field = this.GetTargetFields();
            string sqlString = "Select " + field + " from " + this.TargetTable.TableName
                + " where rownum = 1";
            return sqlString;
        }

        /// <summary>
        /// 得到客户端删除全部数据的SQL命令
        /// </summary>
        /// <returns></returns>
        public string GetTargetDeleteAllSQL()
        {
            return "delete from " + this.TargetTable.TableName;
        }

        ///// <summary>
        ///// 得到客户端删除某条记录的SQL命令
        ///// </summary>
        ///// <returns></returns>
        //public string GetTargetDeleteSQL(string id)
        //{
        //    string deleteSql = "Delete from " + this.TargetTable.TableName
        //        + " where " + this.TargetTable.PrimaryKey + "='" + id + "' ";
        //    return deleteSql;
        //}

        /// <summary>
        /// 得到客户端对应的Insert语句 Only call once
        /// </summary>
        /// <returns></returns>
        public string GetTargetInsertSQL(string parameterPrefix)
        {
            if (this.FieldMapList == null || this.FieldMapList.Count == 0)
                return "";

            string strFields = "";
            string strValues = "";
            foreach (FieldMap field in FieldMapList)
            {
                if (strFields != "")
                    strFields += ",";
                strFields += field.TargetField;

                if (strValues != "")
                    strValues += ",";
                strValues += parameterPrefix + field.TargetField;  
            }

            //// 加入guid主键
            //if (TargetTable.GUIDPrimaryKey != "")
            //{
            //    if (strFields != "")
            //        strFields += ",";
            //    strFields += TargetTable.GUIDPrimaryKey;

            //    if (strValues != "")
            //        strValues += ",";
            //    strValues += parameterPrefix + TargetTable.GUIDPrimaryKey; 
            //}

            string strCommand = "INSERT INTO " + this.TargetTable.TableName 
                + " ( " + strFields + " ) "
                + " VALUES (" + strValues + ") ";

            return strCommand;
        }

        /// <summary>
        /// 得到客户端对应的左边Update语句，不包括where条件
        /// </summary>
        /// <returns></returns>
        public string GetTargetLeftUpdateSQL(string parameterPrefix)
        {
            if (this.FieldMapList == null || this.FieldMapList.Count == 0)
                return "";

            string strFieldValues = "";
            foreach (FieldMap field in FieldMapList)
            {
                if (strFieldValues != "")
                    strFieldValues += ",";
                strFieldValues += field.TargetField + "=" + parameterPrefix + field.TargetField;
            }

            string commandString = "UPDATE " + this.TargetTable.TableName
                + " SET " + strFieldValues;

            return commandString;
        }

        ///// <summary>
        ///// 组合Update语句，根据id值加where条件
        ///// </summary>
        ///// <param name="strLeftSql"></param>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public string GetTargetUpdateSQL(string strLeftSql, string id)
        //{
        //    return strLeftSql + " where " + this.TargetTable.PrimaryKey + "='" + id + "'";
        //}

        /// <summary>
        /// 得到客户端语句的参数值
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public DataAccessParameterCollection GetTargetParameters(DataRow row,List<string> nullFieldList)
        {
            DataAccessParameterCollection dpc = new DataAccessParameterCollection();
            foreach (FieldMap field in FieldMapList)
            {
                object value = row[field.SourceField];

                string fieldName = field.TargetField;
                
                if (nullFieldList != null && nullFieldList.IndexOf(fieldName) != -1)
                {
                    if (value != null && value.ToString() == "")
                        value = null;                    
                }
                ColumnInfo columnInfo = new ColumnInfo(fieldName, fieldName,false, value.GetType());
                dpc.AddWithValue(columnInfo, value);//field.TargetField, row[field.SourceField]);
            }

            //// 加入Guid主键
            //if (TargetTable.GUIDPrimaryKey != "")
            //    dpc.AddWithValue(TargetTable.GUIDPrimaryKey, Guid.NewGuid().ToString());

            return dpc;
        }



        /// <summary>
        ///// 从服务器返回的DataTable里找到主键中为指定值的记录
        ///// </summary>
        ///// <param name="dtData"></param>
        ///// <param name="dataID"></param>
        ///// <returns></returns>
        //public DataRow GetRowFromSourceTable(DataTable dtData, string dataID)
        //{
        //    if (dtData == null || dtData.Rows.Count == 0)
        //        return null;

        //    foreach (DataRow row in dtData.Rows)
        //    {
        //        string id = row[this.SourceTable.PrimaryKey].ToString().Trim();
        //        if (id == dataID)
        //            return row;
        //    }

        //    return null;
        //}


        ///// <summary>
        ///// 得到PDA端指定ID数组对应的记录
        ///// </summary>
        ///// <param name="dataIDList"></param>
        ///// <returns></returns>
        //public string GetTargetSelectSQL(List<string> dataIDList)
        //{
        //    if (dataIDList.Count == 0)
        //        return "";

        //    //~~~~~~~~
        //    string fields = this.GetTargetFields();
        //    string sqlString = "Select " + fields + " from " + this.TargetTable.TableName;

        //    string strIDCondition = "";
        //    foreach (string id in dataIDList)
        //    {
        //        if (strIDCondition != "")
        //            strIDCondition += " or ";
        //        strIDCondition += this.TargetTable.PrimaryKey + " ='" + id + "' ";
        //    }

        //    // 加ID条件
        //    sqlString += " where " + strIDCondition;

        //    return sqlString;
        //}

        #endregion        


        // 比较两行是行的数据是否相等
        public bool CompareRow(DataRow rowSource,
            DataRow rowTarget)
        {
            Debug.Assert(rowSource != null, "rowSource参数不能为null");
            Debug.Assert(rowTarget != null, "rowTarget参数不能为null");

            for (int i = 0; i < this.FieldMapList.Count; i++)
            {
                FieldMap fieldMap = this.FieldMapList[i];

                if (rowSource[fieldMap.SourceField].Equals(rowTarget[fieldMap.TargetField]) == false)
                    return false;
            }

            return true;
        }

        #region 静态函数

        /// <summary>
        /// 比较两个表
        /// </summary>
        /// <param name="dtSource"></param>
        /// <param name="dtTarget"></param>
        /// <param name="tabMap"></param>
        /// <param name="insertRows"></param>
        /// <param name="updateRows"></param>
        /// <param name="deleteRows"></param>
        /// <param name="noChangeRows"></param>
        public static void CompareData(DataTable dtSource,
            DataTable dtTarget,
            TableMap tabMap,
            out List<DataRow> insertRows,
            out List<DataRow> updateRows,
            out List<DataRow> deleteRows,
            out List<DataRow> noChangeRows)
        {
            List<DataRow> sourceRowList = new List<DataRow>();
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                sourceRowList.Add(dtSource.Rows[i]);
            }

            CompareData(sourceRowList,
                dtTarget,
                tabMap,
                out insertRows,
                out updateRows,
                out deleteRows,
                out noChangeRows);
        }


        /// <summary>
        /// 比较数据，得到新增的行，修改的行，删除的行，未变化的行
        /// 此算法必须先排序
        /// </summary>
        /// <param name="dtSource"></param>
        /// <param name="dtTarget"></param>
        /// <param name="insertRows"></param>
        /// <param name="updateRows"></param>
        /// <param name="deleteRows"></param>
        /// <param name="noChangeRows"></param>
        public static void CompareData(List<DataRow> dtSource,
            DataTable dtTarget,
            TableMap tabMap,
            out List<DataRow> insertRows,
            out List<DataRow> updateRows,
            out List<DataRow> deleteRows,
            out List<DataRow> noChangeRows)
        {
            insertRows = new List<DataRow>();
            updateRows = new List<DataRow>();
            deleteRows = new List<DataRow>();
            noChangeRows = new List<DataRow>();

            // 源表没有记录，则认为全部是删除的
            if (dtSource.Count == 0)
            {
                for (int x = 0; x < dtTarget.Rows.Count; x++)
                {
                    DataRow row = dtTarget.Rows[x];
                    deleteRows.Add(row);
                }
                return;
            }

            // 如果目标没有记录，则认为全部是新增的
            if (dtTarget.Rows.Count == 0)
            {
                for (int x = 0; x < dtSource.Count; x++)
                {
                    DataRow row = dtSource[x];
                    insertRows.Add(row);
                }
                return;
            }


            int i = 0;    //i,j等于-1时表示对应的集合结束
            int j = 0;
            int ret;
            //无条件循环，当有一个集合结束（下标变为-1）跳出循环
            while (true)
            {
                if (i >= dtSource.Count)
                {
                    i = -1; // 源结束
                }

                if (j >= dtTarget.Rows.Count)
                {
                    j = -1;// 目标结束
                }

                //两个集合都没有结束时，执行比较，否则跳出循环（至少一个集合结束）
                if (i != -1 && j != -1)
                {
                    DataRow rowSource = dtSource[i];
                    DataRow rowTarget = dtTarget.Rows[j];

                    // 注意这里，1）定义主键；2）排序条件是按主键排序；3）数据表只有一个主键
                    string strKeySourceValue = rowSource[tabMap.SourceTable.PrimaryKeyList[0]].ToString().ToLower();//08/03/27改
                    string strKeyTargetValue = rowTarget[tabMap.TargetTable.PrimaryKeyList[0]].ToString().ToLower();//08/03/27改

                    // 比较主键，以便确定是新增，还是修改/不变，还是删除
                    ret = string.Compare(strKeySourceValue, strKeyTargetValue,StringComparison.Ordinal);//, true);// 不能忽略大小写 2008/3/18
                    if (strKeySourceValue == "b01d046c91504a4fa3ab9d34ae005483")
                    {
                        int ntemp = 0;
                        ntemp++;
                    }

                    // 当等于0时,主键相同，表示修改/不变的情况
                    if (ret == 0)
                    {
                        // 比较是否完全一致，从而确定是修改还是不变
                        if (tabMap.CompareRow(rowSource, rowTarget) == true)
                            noChangeRows.Add(rowSource);
                        else
                            updateRows.Add(rowSource);

                        i++;
                        j++;
                    }

                    // 小于0，表示源在目标中不存在，新增
                    if (ret < 0)
                    {
                        insertRows.Add(rowSource);
                        i++;
                    }

                    // 大于0，表示目标中多余的项，删除
                    if (ret > 0)
                    {
                        deleteRows.Add(rowTarget);
                        j++;
                    }

                }
                else
                {
                    break;
                }
            }

            // 处理剩余的末尾记录
            if (i != -1 && i < dtSource.Count)
            {
                for (; i < dtSource.Count; i++)
                {
                    insertRows.Add(dtSource[i]);
                }
            }

            if (j != -1 && j < dtTarget.Rows.Count)
            {
                for (; j < dtTarget.Rows.Count; j++)
                {
                    deleteRows.Add(dtTarget.Rows[j]);
                }
            }

            return;
        }

        #endregion

    }

    /// <summary>
    /// 字段映射
    /// </summary>
    public class FieldMap
    {
        /// <summary>
        ///  源字段
        /// </summary>
        private string _sourceField = "";

        /// <summary>
        /// 目标字段
        /// </summary>
        private string _targetField = "";


        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public FieldMap(XmlNode node)
        {
            //~~~~~~
            this._sourceField = XmlUtil.GetAttrValue(node, "SourceField").Trim();
            if (this._sourceField == "")
            {
                throw new Exception("配置文件不合法，<FieldMap>元素未定义SourceField属性或SourceField属性未赋值。");
            }

            //~~~~~~~~
            this._targetField = XmlUtil.GetAttrValue(node, "TargetField").Trim();
            if (this._targetField == "")
            {
                throw new Exception("配置文件不合法，<FieldMap>元素未定义TargetField属性或TargetField属性未赋值。");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SourceField"></param>
        /// <param name="targetField"></param>
        public FieldMap(string sourceField, string targetField)
        {
            this._sourceField = sourceField;
            this._targetField = targetField;
        }

        /// <summary>
        /// 
        /// </summary>
        public string SourceField
        {
            get { return _sourceField; }
            set { _sourceField = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string TargetField
        {
            get { return _targetField; }
            set { _targetField = value; }
        }

        /// <summary>
        /// 得到对应的xml
        /// </summary>
        /// <returns></returns>
        public string GetXml()
        {
            return "<FieldMap SourceField='" + this._sourceField+ "' TargetField='" + this._targetField + "'/>";
        }
    }

    /// <summary>
    /// 表信息
    /// </summary>
    public class TableItem
    {
        /// <summary>
        /// 表相关属性
        /// </summary>
        public string TableName = "";
        public string PrimaryKeys = "";
        public string[] PrimaryKeyList = null;

        // 排序sql
        public string OrderSQL = "";

        /// <summary>
        /// 文件相关属性
        /// </summary>
        public string FileName = "";      // 可能有多个文件 2008.01.28改
        public char FieldSplitOperator = new char();
        public string FieldNames = "";
        public List<string> FieldNameList = new List<string>();

        // 条件sql ,要带where关键字
        public string WhereSQL = "";

        // 关联字段,一般当配制Header/Detail命令时，放在Detail配置里
        public string RefFields = "";

        //public string NullFieldNames = "";
        public List<string> NullFieldNameList = new List<string>();
        public List<string> UTCTimeFields = new List<string>();
        public List<string> IntFields = new List<string>();
        public List<string> ArticleFields = new List<string>();


        public bool OneRowRecordsNum = false;
        public string Encoding = "";
        

        // 检索SQL
        public string SelectSQL = "";

        /// <summary>
        /// 自动产生Guid主键
        /// </summary>
        public string GUIDPrimaryKey = "";

        public TableItem()
        { }

        public TableItem(XmlNode node)
        {
            // 排序sql
            XmlNode nodeSelectSQL = node.SelectSingleNode("SelectSQL");
            if (nodeSelectSQL != null)
                this.SelectSQL = XmlUtil.GetNodeText(nodeSelectSQL).Trim();

            this.TableName = XmlUtil.GetAttrValue(node, "TableName").Trim();
            this.PrimaryKeys = XmlUtil.GetAttrValue(node, "PrimaryKeys").Trim();
            if (this.PrimaryKeys != "")
                this.PrimaryKeyList = this.PrimaryKeys.Split(new char[] {','});

            this.FileName = XmlUtil.GetAttrValue(node, "FileName").Trim();

            string tempSplit = XmlUtil.GetAttrValue(node, "FieldSplitOperator");
            if (tempSplit != "")
            {
                try
                {
                    if (char.IsNumber(tempSplit, 0) == true)
                        this.FieldSplitOperator = (char)(Convert.ToInt32(tempSplit));//.Trim(); 20090820 这里不去trim
                    else
                        this.FieldSplitOperator = tempSplit[0];
                }
                catch(Exception ex)
                {
                    throw new Exception("将分隔符'" + tempSplit+ "'转换为char出错:" + ex.Message);
                }
            }

            XmlNode nodeFieldNames = node.SelectSingleNode("FieldNames");
            if (nodeFieldNames != null)
            {
                this.FieldNames = XmlUtil.GetNodeText(nodeFieldNames).Trim();
                if (this.FieldNames != "")
                {
                    string[] fieldNameArray = this.FieldNames.Split(new char[] { ',' });
                    for (int i = 0; i < fieldNameArray.Length; i++)
                    {
                        string fieldName = fieldNameArray[i].Trim();
                        this.FieldNameList.Add(fieldName);
                    }
                }
            }

            NullFieldNameList.Clear();
            XmlNode nodeNullFieldNames = node.SelectSingleNode("NullFieldNames");
            if (nodeNullFieldNames != null)
            {
                string nullFieldNames = XmlUtil.GetNodeText(nodeNullFieldNames).Trim();
                string[] nullFieldNameArray = nullFieldNames.Split(new char[] {','});
                for (int i = 0; i < nullFieldNameArray.Length; i++)
                {
                    string field = nullFieldNameArray[i].Trim();
                    if (field != "")
                        this.NullFieldNameList.Add(field);
                }
            }

            UTCTimeFields.Clear();
            XmlNode nodeUTCTimeFieldNames = node.SelectSingleNode("UTCTimeFieldNames");
            if (nodeUTCTimeFieldNames != null)
            {
                string utcTimeFieldNames = XmlUtil.GetNodeText(nodeUTCTimeFieldNames).Trim();
                string[] utcTimeFieldNameArray = utcTimeFieldNames.Split(new char[] { ',' });
                for (int i = 0; i < utcTimeFieldNameArray.Length; i++)
                {
                    string field = utcTimeFieldNameArray[i].Trim();
                    if (field != "")
                        this.UTCTimeFields.Add(field);
                }
            }

            IntFields.Clear();
            XmlNode nodeIntFieldNames = node.SelectSingleNode("IntFieldNames");
            if (nodeIntFieldNames != null)
            {
                string IntFieldNames = XmlUtil.GetNodeText(nodeIntFieldNames).Trim();
                string[] IntFieldNameArray = IntFieldNames.Split(new char[] { ',' });
                for (int i = 0; i < IntFieldNameArray.Length; i++)
                {
                    string field = IntFieldNameArray[i].Trim();
                    if (field != "")
                        this.IntFields.Add(field);
                }
            }

            ArticleFields.Clear();
            XmlNode nodeArticleFieldNames = node.SelectSingleNode("ArticleFieldNames");
            if (nodeArticleFieldNames != null)
            {
                string ArticleFieldNames = XmlUtil.GetNodeText(nodeArticleFieldNames).Trim();
                string[] ArticleFieldNameArray = ArticleFieldNames.Split(new char[] { ',' });
                for (int i = 0; i < ArticleFieldNameArray.Length; i++)
                {
                    string field = ArticleFieldNameArray[i].Trim();
                    if (field != "")
                        this.ArticleFields.Add(field);
                }
            }

       



            if (this.TableName == "" && this.FileName == "" && this.SelectSQL=="")
                throw new Exception("配置文件不合法，<TableMap>元素未定义TableName属性或者FileName属性,或者SelectSQL内容为空。");

            XmlNode nodeWhereSQL = node.SelectSingleNode("WhereSQL");
            if (nodeWhereSQL != null)
                this.WhereSQL = XmlUtil.GetNodeText(nodeWhereSQL).Trim();

            // 关联字段
            this.RefFields = XmlUtil.GetAttrValue(node, "RefFields").Trim();

            // 排序sql
            XmlNode nodeOrderSQL = node.SelectSingleNode("OrderSQL");
            if (nodeOrderSQL != null)
                this.OrderSQL = XmlUtil.GetNodeText(nodeOrderSQL).Trim();

            // 第一行是否输出records num 格式为 #NNNNNN
            string recordsNum = XmlUtil.GetAttrValue(node, "OneRowRecordsNum").Trim();
            if (String.Compare(recordsNum, "True", true) == 0)
                this.OneRowRecordsNum = true;

            // 输出文件编码
            this.Encoding = XmlUtil.GetAttrValue(node, "Encoding").Trim();

            // 是否存在自动产生的Guid主键
            this.GUIDPrimaryKey = XmlUtil.GetAttrValue(node, "GUIDPrimaryKey").Trim();

        }

        /// <summary>
        /// 真实文件名
        /// </summary>
        private string _realFileName = "";

        /// <summary>
        /// 真实文件名
        /// </summary>
        public string RealFileName
        {
            get
            {
                if (this._realFileName == "")
                {
                    this._realFileName = TableItem.RegularFilePath(this.FileName);
                }

                return this._realFileName;
            }
        }

        /// <summary>
        /// 规范化文件路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string RegularFilePath(string filePath)
        {
            // 替换宏
            filePath = TableItem.ReplaceMacro(filePath);

            // 转换为物理路径
            int nTempIndex = filePath.IndexOf("\\");
            if (nTempIndex > 0)
            {
                string leftFilePath = filePath.Substring(0, nTempIndex);
                if (leftFilePath == ".")
                {
                    string strFullyQualifiedName = Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName;
                    string appDir = Path.GetDirectoryName(strFullyQualifiedName);
                    filePath = appDir + filePath.Substring(nTempIndex);
                }
            }
            return filePath;
        }

        /// <summary>
        /// 替换字符串中的宏
        /// </summary>
        /// <param name="text"></param>
        public static string ReplaceMacro(string text)
        {
            text = text.Replace("%CUR_TIME%", DateTime.Now.ToString("yyyyMMddHHmmss"));
            text = text.Replace("%CUR_DAY%", DateTime.Now.ToString("yyyyMMdd"));
            text = text.Replace("%CUR_MONTH%", DateTime.Now.ToString("yyyyMM"));

            return text;
        }


    }
}
