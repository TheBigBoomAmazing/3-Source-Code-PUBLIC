using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace eContract.Log
{
    public class FileLog : BaseLog
    {
        /// <summary>
        /// 日志文件
        /// </summary>
        private string _logFileName = "";

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="appDir"></param>
        public override void Initialize(string appDir)
        {
            base.Initialize(appDir);

            //
            this._logFileName = appDir + "\\" + "log.txt";
        }

        /// <summary>
        /// 底层函数，写日志
        /// </summary>
        /// <param name="jobCode"></param>
        /// <param name="description"></param>
        /// <param name="ex"></param>
        /// <param name="jobCategory"></param>
        public override void WriteLogInternal(string jobCode, string description, Exception ex,
            int logCategory, string threadName)
        {
            // 拼字符串
            string strCurTime = DateTime.Now.ToString();
            string text = strCurTime + "\t"
                + logCategory + "\t"
                + jobCode + "\t"
                + description + "\t";
            if (ex != null)
            {
                text += ex.GetType().ToString() + "\t"
                    + ex.Message + "\t";
            }
            else
            {
                text += " " + "\t"
                    + " " + "\t";
            }
            text += " \t" + " \t" + threadName;
            text += "\r\n";

            

            // 将日志写到文件
            StreamWriter sw = new StreamWriter(this._logFileName,
                true,
                Encoding.UTF8);
            sw.Write(text);
            sw.Close();
        }

        ///// <summary>
        ///// 获取日志
        ///// </summary>
        ///// <param name="logCategory"></param>
        ///// <returns></returns>
        //public override List<LogEntity> GetLogs(int logCategory)
        //{
        //    List<LogEntity> entityList = new List<LogEntity>();

        //    if (File.Exists(this._logFileName) == false)
        //        return entityList;

        //    StreamReader sr = new StreamReader(this._logFileName);
        //    try
        //    {
        //        while (sr.EndOfStream == false)
        //        {
        //            string line = sr.ReadLine().Trim();
        //            if (line == "")
        //                continue;
        //            LogEntity entity = new LogEntity(line);
        //            if (logCategory == -1)
        //                entityList.Add(entity);
        //            else if (logCategory == entity.Category)
        //                entityList.Add(entity);
        //        }
        //    }
        //    finally
        //    {
        //        sr.Close();
        //    }

        //    return entityList;
        //}
        /// <summary>
        /// 获取日志
        /// </summary>
        /// <param name="logCategory"></param>
        /// <returns></returns>
        public override List<LogEntity> GetLogs(int logCategory,string jobCode)
        {
            List<LogEntity> entityList = new List<LogEntity>();

            if (File.Exists(this._logFileName) == false)
                return entityList;

            StreamReader sr = new StreamReader(this._logFileName);
            try
            {
                while (sr.EndOfStream == false)
                {
                    string line = sr.ReadLine().Trim();
                    if (line == "")
                        continue;
                    LogEntity entity = new LogEntity(line);
                    if (logCategory == -1)
                    {
                        if (jobCode == "")
                            entityList.Add(entity);
                        else if (entity.JobCode == jobCode)
                            entityList.Add(entity);
                    }
                    else if (logCategory == entity.Category)
                    {
                        if (jobCode == "")
                            entityList.Add(entity);
                        else if (entity.JobCode == jobCode)
                            entityList.Add(entity);
                    }
                }
            }
            finally
            {
                sr.Close();
            }

            return entityList;
        }
    }



}
