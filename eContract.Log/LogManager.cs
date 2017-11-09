using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;
using System.IO;

namespace eContract.Log
{
    public class LogManager : List<ILog>
    {
        /// <summary>
        /// 当前实例
        /// </summary>
        private static LogManager _current = null;
        public static LogManager Current
        {
            get
            {
                if (_current == null)
                    _current = new LogManager();

                return _current;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public LogManager()
        {
            // 设置日志文件名称
            string strFullyQualifiedName = Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName;
            string appDir = Path.GetDirectoryName(strFullyQualifiedName);

            string cfgFile = appDir + "\\" + "cfg.xml";
            if (File.Exists(cfgFile) == true)
            {
                XmlDocument dom = new XmlDocument();
                dom.Load(cfgFile);

                XmlNodeList logList = dom.DocumentElement.SelectNodes("logs/log");
                for (int i = 0; i < logList.Count; i++)
                {
                    XmlNode logNode = logList[i];
                    string logType = logNode.Attributes["type"].Value;
                    ILog log = LogFactory.CreateLog(appDir, logType);
                    string tableName = "";
                    if (logNode.Attributes["TableName"] != null)
                        tableName = logNode.Attributes["TableName"].Value.ToString();
                    log.TableName = tableName;
                    this.Add(log);
                }
            }
        }


        /// <summary>
        /// 写普通信息
        /// </summary>
        /// <param name="jobCode"></param>
        /// <param name="error"></param>
        public void WriteCommonLog(string jobCode, string description,string threadName)
        {
            for (int i = 0; i < this.Count; i++)
            {
                ILog log = this[i];
                log.WriteCommonLog(jobCode, description,threadName);
            }
        }


     

        /// <summary>
        /// 写出错信息
        /// </summary>
        /// <param name="jobCode"></param>
        /// <param name="error"></param>
        public void WriteErrorLog(string jobCode, string description, string threadName)
        {
            for (int i = 0; i < this.Count; i++)
            {
                ILog log = this[i];
                log.WriteErrorLog(jobCode, description,threadName);
            }
        }

        /// <summary>
        /// 写异常信息
        /// </summary>
        /// <param name="jobCode"></param>
        /// <param name="error"></param>
        public void WriteExceptionLog(string jobCode, string description, Exception ex, string threadName)
        {
            for (int i = 0; i < this.Count; i++)
            {
                ILog log = this[i];
                log.WriteExceptionLog(jobCode, description, ex,threadName);
            }
        }

        /// <summary>
        /// 获取日志信息
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public List<LogEntity> GetLogs(int category,string jobCode)
        {
            List<LogEntity> entityList = new List<LogEntity>();

            if (this.Count > 0)
            {
                //取第一类型的日志就可以了，因为各个日志都是一样的
                ILog log = this[0];

                entityList = log.GetLogs(category,jobCode);
            }
            return entityList;
        }


        /// <summary>
        /// 获取日志信息
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public List<LogEntity> GetExceptionLog()
        {
            List<LogEntity> entityList = new List<LogEntity>();

            if (this.Count > 0)
            {
                //取第一类型的日志就可以了，因为各个日志都是一样的
                ILog log = this[0];

                entityList = log.GetExceptionLog();
            }
            return entityList;
        }
    }
}
