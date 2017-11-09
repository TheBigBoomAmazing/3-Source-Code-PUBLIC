using System;
using System.Collections.Generic;
using System.Text;

namespace eContract.Log
{
    public class LogEntity
    {
        /// <summary>
        /// 成员
        /// </summary>
        public string Time = "";
        public int Category = -1;
        public string JobCode = "";
        public string Description = "";
        public string ExceptionType = "";
        public string ExceptionMessage = "";
        public string ThreadName = "";

        /// <summary>
        /// 构造函数
        /// </summary>
        public LogEntity()
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        public LogEntity(string line)
        {
            string[] fields = line.Split('\t');

            this.Time = fields[0];

            if (fields.Length > 1)
            {
                string logCategory = fields[1].Trim();
                if (logCategory != "")
                    this.Category = Convert.ToInt32(logCategory);
            }

            if (fields.Length > 2)
                this.JobCode = fields[2];

            if (fields.Length > 3)
                this.Description = fields[3];

            if (fields.Length > 4)
                this.ExceptionType = fields[4];

            if (fields.Length > 5)
                this.ExceptionMessage = fields[5];

            if (fields.Length > 6)
                this.ThreadName = fields[6];
        }

        /// <summary>
        /// 日志分类字符串
        /// </summary>
        public string CategoryString
        {
            get
            {
                if (this.Category == LogConst.LOG_CATEGORY_COMMON)
                    return LogConst.LOG_CATEGORY_COMMON_STRING;
                else if (this.Category == LogConst.LOG_CATEGORY_ERROR)
                    return LogConst.LOG_CATEGORY_ERROR_STRING;
                else if (this.Category == LogConst.LOG_CATEGORY_EXCEPTION)
                    return LogConst.LOG_CATEGORY_EXCEPTION_STRING;

                return " ";
            }
        }

        /// <summary>
        /// 日志分类字符串转换成int
        /// </summary>
        /// <param name="categoryString"></param>
        /// <returns></returns>
        public static int CategoryString2Int(string categoryString)
        {
            if (categoryString == LogConst.LOG_CATEGORY_COMMON_STRING)
                return LogConst.LOG_CATEGORY_COMMON;

            if (categoryString == LogConst.LOG_CATEGORY_ERROR_STRING)
                return LogConst.LOG_CATEGORY_ERROR;

            if (categoryString == LogConst.LOG_CATEGORY_EXCEPTION_STRING)
                return LogConst.LOG_CATEGORY_EXCEPTION;

            return -1;
        }
    }
}
