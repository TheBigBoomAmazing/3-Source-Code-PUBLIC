using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Suzsoft.Smart.Data;
using System.Collections;
using eContract.DDP.Common.CommonJob;
using System.Data;
using System.IO;
using eContract.Log;

namespace eContract.DDP.Common.Command
{
    public class DBtoFileCommand : BaseCommand
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
        /// 表映射关系
        /// </summary>
        TableMap _tableMap =null;

        /// <summary>
        /// 最大记录数
        /// </summary>
        public int MaxCount = -1;

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="node"></param>
        public override void Initialize(Hashtable parameters, XmlNode node)
        {
            // 先调基类函数
            base.Initialize(parameters, node);


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
                    this.SourceDataAccessCfg.DBType = "SQLSERVER"; //DataAccessFactory.DBTYPE_SQL;
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

            }

            // 表映射关系
            XmlNode tableMapNode = node.SelectSingleNode("TableMap");
            this._tableMap = new TableMap(tableMapNode);
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

            // 先将文件名中的参数替换掉
            string targetFileName = BaseCommand.ReplaceParameters(this.InitialParameters, this._tableMap.TargetTable.FileName);

            // 确保目录存在
            int nIndex = targetFileName.LastIndexOf("\\");
            if (nIndex != -1)
            {
                string dir = targetFileName.Substring(0, nIndex);
                if (Directory.Exists(dir) == false)
                    Directory.CreateDirectory(dir);
            }

            int count = 0;

            DataAccessBroker brokerSource = DataAccessFactory.Instance(this.SourceDataAccessCfg);

            // 将数据保存到目标文件
            StreamWriter sw = null;
            if (String.Compare(this._tableMap.TargetTable.Encoding, "UTF8", true) == 0)
                sw = new StreamWriter(targetFileName, false, Encoding.UTF8);  //C2-CI指定utf8
            else
                sw = new StreamWriter(targetFileName, false);
            try
            {
                string sqlString = "";
                sw.BaseStream.Seek(0, SeekOrigin.Begin);

                                
                // 判断是否第一行输出总记录数
                if (this._tableMap.TargetTable.OneRowRecordsNum == true)
                {
                    string header = "#";
                    header = header.PadRight(10, '#');
                    sw.WriteLine(header);
                }

                // 依次
                sqlString = this._tableMap.GetSourceSelectSQL(this.MaxCount);
                IDataReader dataReader = brokerSource.ExecuteSQLReader(sqlString);
                while (dataReader.Read())
                {
                    string line = "";
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        if (line != "")
                            line += this._tableMap.TargetTable.FieldSplitOperator;
                        line += dataReader[i].ToString();
                    }
                    sw.WriteLine(line);

                    count++;
                }
                sw.Flush();


                // 判断是否第一行输出总记录数
                if (this._tableMap.TargetTable.OneRowRecordsNum == true)
                {
                    sw.BaseStream.Seek(0, SeekOrigin.Begin);
                    string header = "#" + count.ToString();
                    sw.WriteLine(header.PadRight(10),' ');
                    sw.Flush();
                }


            }
            finally
            {
                if (sw != null)
                {
                    sw.Flush();
                    sw.Close();
                }

                brokerSource.Close();

            }

            // 写日志
            LogManager.Current.WriteCommonLog(this.JobCode, "从数据表" + this._tableMap.SourceTable.TableName + "获得" + count.ToString() + "笔记录。", this.ThreadName);


            return ResultCode.Success;
        }

    }
}
