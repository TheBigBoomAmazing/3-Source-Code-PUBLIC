using System;
using System.Collections.Generic;
using System.Text;

namespace eContract.Log
{
    public class LogConst
    {
        // log type
        public const string LOG_TYPE_FILE = "file";
        public const string LOG_TYPE_SqlServer = "SqlServer";
        public const string LOG_TYPE_ORACLE = "oracle";
        public const string LOG_TYPE_SqlLite = "SqlLite";

        // LogCategory
        public const int LOG_CATEGORY_COMMON =0;
        public const int LOG_CATEGORY_ERROR = 1;
        public const int LOG_CATEGORY_EXCEPTION = 2;

        // log category string
        public const string LOG_CATEGORY_COMMON_STRING = "common";       // 普通日志
        public const string LOG_CATEGORY_ERROR_STRING = "error";            // 普通错误日志
        public const string LOG_CATEGORY_EXCEPTION_STRING = "exception";       // Exception错误日志

    }
}
