using System;
using System.Collections.Generic;
using System.Text;

namespace Suzsoft.Smart.Data
{
    /// <summary>
    /// 数据访问工厂//TODO:连接池
    /// </summary>
    public class DataAccessFactory
    {
        // Jane.ren 2007/10/18
        public const string DBTYPE_ORACLE = "ORACLE";
        public const string DBTYPE_SQLSERVER = "SQLSERVER";
        public const string DBTYPE_ODBC = "ODBC";
        /// <summary>
        /// 获取默认数据访问
        /// </summary>
        /// <returns></returns>
        public static DataAccessBroker Instance()
        {
            return Instance("");
        }

        /// <summary>
        /// 根据名称获取数据访问
        /// </summary>
        /// <param name="instanceName"></param>
        /// <returns></returns>
        public static DataAccessBroker Instance(string instanceName)
        {
            DataAccessConfiguration config = DataAccessConfigurationMangement.GetDataAccessConfiguration(instanceName.Trim());
            return Instance(config);
        }

        /// <summary>
        /// 根据配置获取数据访问
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static DataAccessBroker Instance(DataAccessConfiguration config)
        {
            DataAccessBroker _broker = null;
            if (string.Compare(config.DBType, DBTYPE_SQLSERVER, false) == 0)
            {
                _broker = new SQLDataAccessBroker();
                _broker.Configuration = config;
            }
            //else if (string.Compare(config.DBType, DBTYPE_ODBC, false) == 0)
            //{
            //    _broker = new ODBCDataAccessBroker();
            //    _broker.Configuration = config;
            //}
            else
            {
                _broker = new OracelDataAccessBroker();
                _broker.Configuration = config;
            }
            _broker.Create();//创建并打开数据库连接
            return _broker;
        }
    }
}
