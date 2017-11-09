using System;
using System.Collections.Generic;
using System.Text;

namespace eContract.Log
{
    public class LogFactory
    {
        /// <summary>
        /// 根据类型创建LOG对象
        /// </summary>
        /// <param name="logType"></param>
        /// <returns></returns>
        public static ILog CreateLog(string appDir, string logType)
        {
            ILog log = null;

            if (logType == LogConst.LOG_TYPE_FILE)
                log = new FileLog();
            else if (logType == LogConst.LOG_TYPE_SqlServer)
                log = new SqlServerLog();
            else if (logType == LogConst.LOG_TYPE_ORACLE)
                log = new OracleLog();
            //else if (logType == LogConst.LOG_TYPE_SqlLite)
            //    log = new SqlLiteLog();

            // 初始化
            log.Initialize(appDir);

            return log;
        }
    }

}
