using System;
using System.Collections.Generic;
using System.Text;

namespace eContract.Log
{
    public interface ILog
    {
        string TableName { get;set;}

        void Initialize(string appDir);

        void WriteErrorLog(string jobCode, string error,string threadName);

        void WriteCommonLog(string jobCode, string description, string threadName);

        void WriteExceptionLog(string jobCode, string description, Exception ex, string threadName);

        //List<LogEntity> GetLogs(int logCategory);

        List<LogEntity> GetLogs(int logCategory,string jobCode);

        List<LogEntity> GetExceptionLog();
    }

}
