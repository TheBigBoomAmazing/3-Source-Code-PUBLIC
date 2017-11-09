using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using Suzsoft.Smart.EntityCore;
using System.Data.SqlClient;

namespace Suzsoft.Smart.Data
{
    /// <summary>
    /// Sql的数据访问broker
    /// </summary>
    public class SQLDataAccessBroker:DataAccessBroker
    {
        public const string ParameterPrefixConst = "@";
        internal override void Create()
        {
            _connection = new SqlConnection(Configuration.ConnectionString);
            _connection.Open();//TODO:
        }

        /// <summary>
        /// 参数前缀()
        /// </summary>
        public override string ParameterPrefix
        {
            get { return "@"; }
        }

        /// <summary>
        /// 创建Command
        /// </summary>
        /// <param name="commandString"></param>
        /// <returns></returns>
        protected override DbCommand CreateCommand(string commandString)
        {
            SqlCommand command = new SqlCommand(commandString, (SqlConnection)_connection);
            return command;
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameter"></param>
        protected override void AddParameter(DbCommand command, DataAccessParameter parameter)
        {
            SqlCommand comm = command as SqlCommand;
            if (comm != null)
            {
                comm.Parameters.AddWithValue(parameter.ParameterName, parameter.ParameterValue);
                comm.Parameters[parameter.ParameterName].Direction = parameter.Direction;
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
            SqlCommand comm = command as SqlCommand;
            if (comm != null)
            {
                SqlDataAdapter adapter = new SqlDataAdapter(comm);
                if (mapping != null)
                {
                    adapter.TableMappings.Add(mapping);
                }
                //设置主键
                //adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
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
            SqlCommand cmd = (SqlCommand)this.CreateCommand("SELECT * FROM " + tableName);
            IDataReader dr = cmd.ExecuteReader(CommandBehavior.SchemaOnly);
            DataTable dt = dr.GetSchemaTable();
            dr.Close();
            return dt;
        }

        public DataTable GetDataByProcedure(SqlCommand oracleCommand)
        {
            DataTable dtData = new DataTable();

            try
            {
                DataAccessConfiguration config = new DataAccessConfiguration();
                string sCon = config.ConnectionString;
                _connection = new SqlConnection(sCon);
                _connection.Open();
                oracleCommand.Connection = (SqlConnection)_connection;
                SqlDataAdapter oda = new SqlDataAdapter();
                oda.SelectCommand = oracleCommand;
                oda.Fill(dtData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _connection.Close();
            }

            return dtData;
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="dt">DataTable名称</param>
        /// <param name="tableName">表名称</param>
        public override void BulkInsert(DataTable dt, string tableName)
        {
            try
            {
                SqlBulkCopy sbc = new SqlBulkCopy((SqlConnection)_connection);
                sbc.DestinationTableName = tableName;
                sbc.BulkCopyTimeout = 300;
                sbc.WriteToServer(dt);
                sbc.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}
