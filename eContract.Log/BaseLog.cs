using System;
using System.Collections.Generic;
using System.Text;

namespace eContract.Log
{
    public class BaseLog : ILog
    {
        /// <summary>
        /// 程序目录
        /// </summary>
        public string AppDir = "";

        #region ILog Members

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="appDiir"></param>
        public virtual void Initialize(string appDiir)
        {
            this.AppDir = appDiir;
        }

        /// <summary>
        /// 写普通日志
        /// </summary>
        /// <param name="jobCode"></param>
        /// <param name="description"></param>
        public virtual void WriteCommonLog(string jobCode, string description, string threadName)
        {
            this.WriteLogInternal(jobCode,
                description,
                null,   //Exception
                LogConst.LOG_CATEGORY_COMMON,
                threadName);
        }

        /// <summary>
        /// 写错误日志
        /// </summary>
        /// <param name="jobCode"></param>
        /// <param name="error"></param>
        public virtual void WriteErrorLog(string jobCode, string description, string threadName)
        {
            if (description.Length > 1000)
                description = description.Substring(0, 999);

            this.WriteLogInternal(jobCode,
                description,
                null,   //Exception
                LogConst.LOG_CATEGORY_ERROR,
                threadName);
        }

        /// <summary>
        /// 写异常日志
        /// </summary>
        /// <param name="jobCode"></param>
        /// <param name="description"></param>
        /// <param name="ex"></param>
        public virtual void WriteExceptionLog(string jobCode, string description, Exception ex, string threadName)
        {
            this.WriteLogInternal(jobCode,
                description,
                ex,
                LogConst.LOG_CATEGORY_EXCEPTION,
                threadName);
        }

        /// <summary>
        /// 底层函数，写日志
        /// </summary>
        /// <param name="jobCode"></param>
        /// <param name="description"></param>
        /// <param name="ex"></param>
        /// <param name="jobCategory"></param>
        public virtual void WriteLogInternal(string jobCode, string description, Exception ex, int logCategory, string threadName)
        {

        }

        ///// <summary>
        ///// 获取日志信息
        ///// </summary>
        ///// <param name="logCategory"></param>
        ///// <returns></returns>
        //public virtual List<LogEntity> GetLogs(int logCategory)
        //{
        //    List<LogEntity> entityList = new List<LogEntity>();

        //    return entityList;
        //}
        /// <summary>
        /// 获取日志信息
        /// </summary>
        /// <param name="logCategory"></param>
        /// <returns></returns>
        public virtual List<LogEntity> GetLogs(int logCategory, string jobCode)
        {
            List<LogEntity> entityList = new List<LogEntity>();

            return entityList;
        }

        public virtual List<LogEntity> GetExceptionLog()
        {
            List<LogEntity> entityList = new List<LogEntity>();

            return entityList;
        }

        #endregion


        #region ILog Members

        private string _tableName = "";

        public string TableName
        {
            get
            {
                return this._tableName;
            }
            set
            {
                this._tableName = value;
            }
        }

        #endregion
    }
}
