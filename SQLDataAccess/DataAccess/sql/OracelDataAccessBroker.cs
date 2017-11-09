using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using Suzsoft.Smart.EntityCore;
using System.Data.OracleClient;

namespace Suzsoft.Smart.Data
{
    /// <summary>
    /// Sql的数据访问broker
    /// </summary>
    public class OracelDataAccessBroker : DataAccessBroker
    {
        public const string ParameterPrefixConst = ":";
        internal override void Create()
        {
            _connection = new OracleConnection(Configuration.ConnectionString);
            _connection.Open();//TODO:
        }

        /// <summary>
        /// 参数前缀()
        /// </summary>
        public override string ParameterPrefix
        {
            get { return ":"; }
        }

        /// <summary>
        /// 创建Command
        /// </summary>
        /// <param name="commandString"></param>
        /// <returns></returns>
        protected override DbCommand CreateCommand(string commandString)
        {
            OracleCommand command = new OracleCommand(commandString, (OracleConnection)_connection);
            return command;
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameter"></param>
        protected override void AddParameter(DbCommand command, DataAccessParameter parameter)
        {
            OracleCommand comm = command as OracleCommand;
            if (comm != null)
            {
         
                comm.Parameters.AddWithValue(parameter.ParameterName, parameter.ParameterValue);
                comm.Parameters[parameter.ParameterName].Direction = parameter.Direction;
                comm.Parameters[parameter.ParameterName].Size = parameter.Size; //Lucas 2011-01-04  执行oracle存储过程时参数必须要设置大小
                //comm.Parameters[parameter.ParameterName].DbType = parameter.DbType;
            }
        }

        /// <summary>
        /// 获取Dataset
        /// </summary>
        /// <param name="command"></param>
        /// <param name="mapping"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override int ExecuteDataSet(DbCommand command, DataTableMapping mapping, ref DataSet result)
        {
            int r = 0;
            OracleCommand comm = command as OracleCommand;
            if (comm != null)
            {
                comm.CommandText = comm.CommandText.Replace("[", "").Replace("]", "");
                OracleDataAdapter adapter = new OracleDataAdapter(comm);
                if (mapping != null)
                {
                    adapter.TableMappings.Add(mapping);
                }
                r = adapter.Fill(result);
            }
            comm.Dispose();
            return r;
        }

        /// <summary>
        /// 获取表结构
        /// Jane.ren 2007/11/5
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public override DataTable GetSchema(string tableName)
        {
            OracleCommand cmd = (OracleCommand)this.CreateCommand("SELECT * FROM " + tableName);
            IDataReader dr = cmd.ExecuteReader(CommandBehavior.SchemaOnly);
            DataTable dt = dr.GetSchemaTable();
            dr.Close();
            return dt;
        }


        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="dt">DataTable名称</param>
        /// <param name="tableName">表名称</param>
        public override void BulkInsert(DataTable dt, string tableName)
        {
            return;
        }
    }
}
