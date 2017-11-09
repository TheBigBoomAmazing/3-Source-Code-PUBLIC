using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using System.Data;
using Suzsoft.Smart.Data;
using eContract.DDP.Common.CommonJob;
using System.IO;
using System.Windows.Forms;
using Suzsoft.Smart.EntityCore;
using System.Diagnostics;
using eContract.Log;

namespace eContract.DDP.Common.Command
{
    public class DBHeaderDetailCommand : DatabaseCommand
    {
        public TableMap HeaderTableMap = null;
        public TableMap DetailTableMap = null;


        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="appDir"></param>
        /// <param name="node"></param>
        public override void Initialize(Hashtable parameters, XmlNode node)
        {
            base.Initialize(parameters, node);


            // 分出Header和Detail表
            for (int i = 0; i < this.TableMapList.Count; i++)
            {
                TableMap tableMap = this.TableMapList[i];
                if (tableMap.Name == "Header")
                    this.HeaderTableMap = tableMap;
                else if (tableMap.Name == "Detail")
                    this.DetailTableMap = tableMap;
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

            // 替换文件名
            string dataSplitOperator = DDPConst.C_ValueSplitOperator;

            List<string> dataIDList = null;

            DataTable dtHeader = null;

            bool hasDataIDList = false;
            if (parameters.ContainsKey(DDPConst.Param_HasDataIDList) == true)
                hasDataIDList = (bool)parameters[DDPConst.Param_HasDataIDList];
            if (hasDataIDList == true)
            {
                // 获取来源数据
                dataIDList = (List<string>)parameters[DDPConst.Param_DataIDList];
                // 写日志
                LogManager.Current.WriteCommonLog(this.JobCode, "程序传入header来源数据，记录数为" + dataIDList.Count.ToString() + "", this.ThreadName);

                dataSplitOperator = (string)parameters[DDPConst.Param_SplitOperator];
                dtHeader = this.GetSourceByIDList(this.HeaderTableMap,
                    this.HeaderTableMap.SourceTable.PrimaryKeys,
                    dataIDList,
                    dataSplitOperator);

                // 写日志
                LogManager.Current.WriteCommonLog(this.JobCode, "根据ID获得header的实际记录数为" + dtHeader.Rows.Count.ToString(), this.ThreadName);


            }
            else
            {
                // 得到Header表
                dtHeader = this.GetSourceData(parameters, this.HeaderTableMap);
            }

            
            // 如果没有header数据，则不进行后续的处理（后面的命令也将不再执行）
            if (dtHeader == null || dtHeader.Rows.Count == 0)
                return ResultCode.Break;
            

            // 取header表的主键值，存放到list，以便后续的更新标志的任务使用
            if (this.HeaderTableMap.SourceTable.PrimaryKeyList != null && dtHeader != null)
            {
                // 加入参数里，传给后面的命令
                parameters[DDPConst.Param_PrimaryKeys] = this.HeaderTableMap.SourceTable.PrimaryKeys;
                if (hasDataIDList == false)
                {
                    dataIDList = GetDataIDList(dtHeader, this.HeaderTableMap.SourceTable.PrimaryKeys, DDPConst.C_ValueSplitOperator);
                    parameters[DDPConst.Param_DataIDList] = dataIDList;
                }

                if (this.SourceDataAccessCfg != null)
                    parameters[DDPConst.Param_DataAccessCfg] = this.SourceDataAccessCfg;
                else
                    parameters[DDPConst.Param_DataAccessCfg] = this.TargetDataAccessCfg;
            }

            // 得到Detail表，可以无记录
            DataTable dtDetail = this.GetDetailData(parameters, dtHeader, dataIDList, dataSplitOperator);

            // 写日志
            LogManager.Current.WriteCommonLog(this.JobCode, "根据header获得的detail记录数为" + dtDetail.Rows.Count.ToString(), this.ThreadName);


            if (this.TargetConnEntity.Type == DDPConst.Conn_TYPE_TEXT)
            {
                // 保存到目标
                this.SaveToTargetFile(this.HeaderTableMap, dtHeader, true);
                this.SaveToTargetFile(this.DetailTableMap, dtDetail, false);
            }
            else
            {
                this.SaveHeaderDetailToTarget(ref parameters, dtHeader, dtDetail, dataSplitOperator);
            }

            return ResultCode.Success;
        }

        /// <summary>
        /// 保存header和detail到数据,一笔header与对应的几笔detail作为一个事务
        /// </summary>
        /// <param name="dtHeader"></param>
        /// <param name="dtDetail"></param>
        private void SaveHeaderDetailToTarget(ref Hashtable parameters, DataTable dtHeader, DataTable dtDetail, string dataSplitOperator)
        {
            List<string> dataIDList = (List<string>)parameters[DDPConst.Param_DataIDList];
            int nError = 0;
            int nSuccess = 0;

            DataAccessBroker brokerTarget = DataAccessFactory.Instance(this.TargetDataAccessCfg);
            try
            {
                // header sql
                this.HeaderTableMap.InitialFieldMapByDataTable(dtHeader);
                string insertSqlHeader = this.HeaderTableMap.GetTargetInsertSQL(brokerTarget.ParameterPrefix.ToString());

                // detail sql
                if (dtDetail.Rows.Count > 0)
                    this.DetailTableMap.InitialFieldMapByDataTable(dtDetail.Rows[0]);
                string insertSqlDetail = this.DetailTableMap.GetTargetInsertSQL(brokerTarget.ParameterPrefix.ToString());

                for (int i = 0; i < dtHeader.Rows.Count; i++)
                {
                    Application.DoEvents();

                    string recordsCondition = "";
                    // 一条header
                    DataRow rowHeader = dtHeader.Rows[i];

                    // 一笔完整的记录一个事务，包括header和detail
                    brokerTarget.BeginTransaction();
                    try
                    {

                        // 并出关联字段及值，便出检索detail
                        string refFieldSql = "";
                        string refValueSql = "";
                        string[] refFieldList = this.DetailTableMap.SourceTable.RefFields.Split(new char[] { ',' });
                        for (int x = 0; x < refFieldList.Length; x++)
                        {
                            string refField = refFieldList[x];
                            if (refFieldSql != "")
                                refFieldSql += "+'|'+";
                            refFieldSql += refField;

                            if (refValueSql != "")
                                refValueSql += "|";
                            refValueSql += rowHeader[refField].ToString();
                        }
                        recordsCondition = refFieldSql + "='" + refValueSql + "'";

                        // 找到Detail数据
                        DataRow[] detailRowList = dtDetail.Select(recordsCondition);
                        LogManager.Current.WriteCommonLog(this.JobCode, refValueSql + "对应" + detailRowList.Length.ToString() + "条detail记录。", this.ThreadName);


                        // 将header插入到数据库
                        DataAccessParameterCollection dpcHeader = this.HeaderTableMap.GetTargetParameters(rowHeader, this.HeaderTableMap.TargetTable.NullFieldNameList);
                        brokerTarget.ExecuteSQL(insertSqlHeader, dpcHeader);

                        // 将detail插入到数据库    

                        for (int j = 0; j < detailRowList.Length; j++)
                        {
                            // detail
                            DataRow rowDetail = detailRowList[j];
                            DataAccessParameterCollection dpcDetail = this.DetailTableMap.GetTargetParameters(rowDetail, this.DetailTableMap.TargetTable.NullFieldNameList);
                            brokerTarget.ExecuteSQL(insertSqlDetail, dpcDetail);
                        }

                        // 事务提交
                        brokerTarget.Commit();

                        LogManager.Current.WriteCommonLog(this.JobCode, recordsCondition + "记录保存数据库成功。", this.ThreadName);
                        nSuccess++;
                    }
                    catch (Exception ex)
                    {
                        // 事务回滚
                        brokerTarget.RollBack();

                        LogManager.Current.WriteErrorLog(this.JobCode, recordsCondition + "记录保存数据库出错:" + ex.Message, this.ThreadName);

                        // 从DataIDList中移出此笔记录，这样就不会更新最新时间了
                        string dataID = this.GetFieldValue(rowHeader, this.HeaderTableMap.SourceTable.PrimaryKeys, dataSplitOperator);
                        dataIDList.Remove(dataID);

                        nError++;

                        // 继续下面的记录
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                brokerTarget.Close();
            }

            // 写日志
            string errorInfo = "来源共" + dtHeader.Rows.Count.ToString() + "笔记录，" + nSuccess.ToString() + "笔保存成功，" + nError.ToString() + "笔保存失败。";
            LogManager.Current.WriteCommonLog(this.JobCode, errorInfo, this.ThreadName);

            // 保存到参数里，传给外面
            parameters[DDPConst.Param_Info] = errorInfo;

            // 将错误的记录数保存在参数里
            if (nError > 0)
                parameters[DDPConst.Param_ErrorCount] = nError;

        }

        /// <summary>
        /// 从数据库得到来源数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetDetailData(Hashtable parameters, DataTable dtHeader, List<string> dataIDList, string dataSplitOperator)
        {
            if (this.SourceConnEntity.Type == DDPConst.Conn_TYPE_ORACLE || this.SourceConnEntity.Type == DDPConst.Conn_TYPE_SQLSERVER)
            {
                // 得到对应的ID列表
                if (dataIDList == null)
                    dataIDList = GetDataIDList(dtHeader, this.DetailTableMap.SourceTable.RefFields, dataSplitOperator);
                if (dataIDList.Count == 0) // 没有对应的关联数据
                    return null;
                return this.GetSourceByIDList(this.DetailTableMap, this.DetailTableMap.SourceTable.RefFields, dataIDList, dataSplitOperator);
            }
            else
            {
                return this.GetDetailDataFromFile(parameters,this.DetailTableMap, dtHeader);
            }            
        }

        /// <summary>
        /// 从文件得到detail
        /// </summary>
        /// <param name="tableMap"></param>
        /// <param name="dtHeader"></param>
        /// <returns></returns>
        public DataTable GetDetailDataFromFile(Hashtable parameters,TableMap tableMap, DataTable dtHeader)
        {
            string fileName = BaseCommand.ReplaceParameters(parameters, tableMap.SourceTable.FileName);


            DataTable dtDetail = new DataTable(); ;

            if (dtHeader == null)
                return null;

            string[] refFieldList = tableMap.SourceTable.RefFields.Split(new char[] { ',' });

            // 拼出关联条件
            List<string> refDataIDList = GetDataIDList(dtHeader, tableMap.SourceTable.RefFields, DDPConst.C_ValueSplitOperator);
            if (refDataIDList.Count == 0) // 没有对应的关联数据
                return null;

            // 创建表结构
            string[] fields = tableMap.SourceTable.FieldNames.Split(new char[] { ',' });
            for (int i = 0; i < fields.Length; i++)
            {
                string field = fields[i].Trim();
                DataColumn col = new DataColumn(field);
                dtDetail.Columns.Add(col);
            }

            // 读取文件中的数据            
            StreamReader sr = new StreamReader(fileName, Encoding.Default);  //this.JobEntity.TableMap.SourceFile
            try
            {
                int nCount = 0;
                while (sr.EndOfStream == false)
                {
                    Application.DoEvents();

                    // 读取一行
                    string line = sr.ReadLine().Trim();
                    if (line == "")
                        continue;

                    // 按分隔符拆分字段
                    string[] fieldValues = line.Split(new char[] {tableMap.SourceTable.FieldSplitOperator });

                    // 创建行，并给各字段赋值
                    DataRow row = dtDetail.NewRow();
                    for (int i = 0; i < fieldValues.Length && i < dtDetail.Columns.Count; i++)
                    {
                        string fieldName = fields[i].Trim();
                        string fieldValue = fieldValues[i];
                        row[fieldName] = fieldValue;
                    }

                    // 支持多个主键
                    string keyValues = "";
                    for (int x = 0; x < refFieldList.Length; x++)
                    {
                        string key = refFieldList[x];
                        string value = row[key].ToString();
                        if (keyValues != "")
                            keyValues += DDPConst.C_ValueSplitOperator;
                        keyValues += value;
                    }

                    if (refDataIDList.IndexOf(keyValues) == -1)
                    {
                        LogManager.Current.WriteCommonLog(this.JobCode, keyValues + "未找到匹配的的header记录。",this.ThreadName);
                    }
                    else
                    {
                        dtDetail.Rows.Add(row);
                    }
                }
            }
            finally
            {
                sr.Close();
            }

            return dtDetail;
        }

    }
}
