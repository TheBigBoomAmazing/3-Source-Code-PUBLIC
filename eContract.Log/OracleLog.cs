using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
//using Suzsoft.Smart.Data;
using System.Data;
using Suzsoft.Smart.Data;
using Suzsoft.Smart.EntityCore;
//using Suzsoft.Smart.EntityCore;

namespace eContract.Log
{
    public class OracleLog : BaseLog
    {
        // log type
        public const int Log_Type_Schedule = 1; // schedule执行过程中产生的日志
        public const int Log_Type_Hand = 1; // 手工执行时产生的日志
        public const string _prefix = ":";

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="appDir"></param>
        public override void Initialize(string appDir)
        {
            base.Initialize(appDir);
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="jobCode"></param>
        /// <param name="logState"></param>
        /// <param name="description"></param>
        public override void WriteLogInternal(string jobCode, string description, Exception ex,
            int logCategory, string threadName)
        {
            using (DataAccessBroker broder = DataAccessFactory.Instance())
            {
                string sqlString = "insert into " + this.TableName
                    + " ("
                    + " sn"
                    + " ,LOG_DDP_ID"
                    + " ,JOB_NAME"
                    + " ,LOG_TIME"

                    + " ,LOG_TYPE"
                    + " ,LOG_STATE"
                    + " ,PHASE"

                    + " ,DESCRIPTION"
                    + " ,EXCEPTION_TYPE"
                    + " ,EXCEPTION_MESSAGE"

                    + " ,MACHINE_NAME"
                    + " ,PROCESS_NAME"
                    + " ,THREAD_NAME"

                    + " )"

                    + " values("
                    + " emp_sequence.nextval"
                    + " ," + _prefix + "LOG_DDP_ID"
                    + " ," + _prefix + "JOB_NAME"
                    + " ," + _prefix + "LOG_TIME"

                    + " ," + _prefix + "LOG_TYPE"
                    + " ," + _prefix + "LOG_STATE"
                    + " ," + _prefix + "PHASE"

                    + " ," + _prefix + "DESCRIPTION"
                    + " ," + _prefix + "EXCEPTION_TYPE"
                    + " ," + _prefix + "EXCEPTION_MESSAGE"

                    + " ," + _prefix + "MACHINE_NAME"
                    + " ," + _prefix + "PROCESS_NAME"
                    + " ," + _prefix + "THREAD_NAME"

                    + ")";

                DataAccessParameterCollection dpc = new DataAccessParameterCollection();
                ColumnInfo columnInfo = new ColumnInfo("LOG_DDP_ID", "LOG_DDP_ID",true,typeof(string));
                dpc.AddWithValue(columnInfo, Guid.NewGuid().ToString());
                if (jobCode == "")
                    jobCode = " ";
                 columnInfo = new ColumnInfo("JOB_NAME", "JOB_NAME", true, typeof(string));
                dpc.AddWithValue(columnInfo, jobCode);
                columnInfo = new ColumnInfo("LOG_TIME", "LOG_TIME", true, typeof(DateTime));
                dpc.AddWithValue(columnInfo, DateTime.Now);

                columnInfo = new ColumnInfo("LOG_TYPE", "LOG_TYPE", true, typeof(int));
                dpc.AddWithValue(columnInfo, Log_Type_Hand);

                columnInfo = new ColumnInfo("LOG_STATE", "LOG_STATE", true, typeof(int));
                dpc.AddWithValue(columnInfo, logCategory);

                columnInfo = new ColumnInfo("PHASE", "PHASE", true, typeof(string));
                dpc.AddWithValue(columnInfo, " ");

                columnInfo = new ColumnInfo("DESCRIPTION", "DESCRIPTION", true, typeof(string));
                dpc.AddWithValue(columnInfo, description);

                if (ex == null)
                {
                    columnInfo = new ColumnInfo("EXCEPTION_TYPE", "EXCEPTION_TYPE", true, typeof(string));
                    dpc.AddWithValue(columnInfo, " ");

                    columnInfo = new ColumnInfo("EXCEPTION_MESSAGE", "EXCEPTION_MESSAGE", true, typeof(string));
                    dpc.AddWithValue(columnInfo, " ");
                }
                else
                {
                    columnInfo = new ColumnInfo("EXCEPTION_TYPE", "EXCEPTION_TYPE", true, typeof(string));
                    dpc.AddWithValue(columnInfo, ex.GetType().ToString());

                    string tempMessage = ex.Message + ex.StackTrace;
                    if (tempMessage.Length > 1999)
                        tempMessage = tempMessage.Substring(0, 1999);

                    columnInfo = new ColumnInfo("EXCEPTION_MESSAGE", "EXCEPTION_MESSAGE", true, typeof(string));
                    dpc.AddWithValue(columnInfo, tempMessage);
                }
                columnInfo = new ColumnInfo("MACHINE_NAME", "MACHINE_NAME", true, typeof(string));
                dpc.AddWithValue(columnInfo, Environment.MachineName);
                columnInfo = new ColumnInfo("PROCESS_NAME", "PROCESS_NAME", true, typeof(string));
                dpc.AddWithValue(columnInfo, "eContract.DDP.WindowsService.exe");
                columnInfo = new ColumnInfo("THREAD_NAME", "THREAD_NAME", true, typeof(string));
                dpc.AddWithValue(columnInfo, threadName);

                broder.ExecuteSQL(sqlString, dpc);
            }
        }

        ///// <summary>
        ///// 获得日志
        ///// </summary>
        ///// <param name="logCategory"></param>
        ///// <returns></returns>
        //public override List<LogEntity> GetLogs(int logCategory)
        //{
        //    List<LogEntity> entityList = new List<LogEntity>();

        //    string sqlString = "";

        //    if (logCategory == -1)
        //    {
        //        sqlString = "select top 500 * from " + this.TableName + " where LOG_TIME > SYSDATE-3 order by LOG_TIME desc,SN desc";
        //    }
        //    else
        //    {
        //        sqlString = "select top 500 * from " + this.TableName
        //            + " where LOG_STATE=" + logCategory
        //            + " and LOG_TIME >SYSDATE-3 order by LOG_TIME desc,SN desc";
        //    }


        //    DataSet ds = DataAccess.SelectDataSet(sqlString);
        //    if (ds == null || ds.Tables.Count == 0)
        //        return entityList;

        //    DataTable dt = ds.Tables[0];
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        DataRow row = dt.Rows[i];

        //        LogEntity entity = new LogEntity();
        //        entity.Time = row["LOG_TIME"].ToString();
        //        entity.Category = Convert.ToInt32(row["LOG_STATE"]);
        //        entity.JobCode = row["JOB_NAME"].ToString();
        //        entity.Description = row["DESCRIPTION"].ToString();
        //        entity.ExceptionType = row["EXCEPTION_TYPE"].ToString();
        //        entity.ExceptionMessage = row["EXCEPTION_MESSAGE"].ToString();

        //        entityList.Add(entity);
        //    }

        //    return entityList;
        //}

        /// <summary>
        /// 获得日志
        /// </summary>
        /// <param name="logCategory"></param>
        /// <returns></returns>
        public override List<LogEntity> GetLogs(int logCategory,string jobCode)
        {
            List<LogEntity> entityList = new List<LogEntity>();

            string whereSql = "";
            if (logCategory != -1)
            {
                whereSql = " LOG_STATE = " + logCategory;
            }
            if (jobCode != "")
            {
                if (whereSql != "")
                    whereSql += " and ";
                whereSql += " JOB_NAME like '%" + jobCode + "%'";
            }
            if (whereSql != "")
                whereSql = " where " + whereSql;

            // 只取前3天的日志
            if (whereSql == "")
                whereSql += " where";
            whereSql += " log_time>SYSDATE-3";


            string sqlString = "select * from " + this.TableName
                    + whereSql
                    + " order by LOG_TIME desc,SN desc";

            DataSet ds = DataAccess.SelectDataSet(sqlString);
            if (ds == null || ds.Tables.Count == 0)
                return entityList;

            DataTable dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];

                LogEntity entity = new LogEntity();
                entity.Time = row["LOG_TIME"].ToString();
                entity.Category = Convert.ToInt32(row["LOG_STATE"]);
                entity.JobCode = row["JOB_NAME"].ToString();
                entity.Description = row["DESCRIPTION"].ToString();
                entity.ExceptionType = row["EXCEPTION_TYPE"].ToString();
                entity.ExceptionMessage = row["EXCEPTION_MESSAGE"].ToString();
                entity.ThreadName = row["THREAD_Name"].ToString();

                entityList.Add(entity);
            }

            return entityList;
        }
    }
}
