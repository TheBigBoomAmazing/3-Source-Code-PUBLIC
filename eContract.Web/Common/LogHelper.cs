using System;
using System.Web.Helpers;
using log4net;
using eContract.Common.WebUtils;

namespace eContract.Web.Common
{
    public class LogHelper
    {
        #region 日志 方式一 ： log4net

        public static void WriteErrorLog(Exception ex)
        {
            ILog logger = LogManager.GetLogger("MyLogger");

            log4net.ThreadContext.Properties["userID"] = WebCaching.UserAccount;
            log4net.ThreadContext.Properties["userIP"] = WebCaching.CurrentIP;

            string ErrorMessage = ex.Message;

            if (ex.InnerException != null)
            {
                ErrorMessage = ex.InnerException.Message;
            }
            logger.Error(ErrorMessage);
        }

        public static void WriteErrorLog(string message)
        {
            ILog logger = LogManager.GetLogger("MyLogger");

            log4net.ThreadContext.Properties["userID"] = WebCaching.UserAccount;
            log4net.ThreadContext.Properties["userIP"] = WebCaching.CurrentIP;

            logger.Error(message);
        }

        public static void WriteErrorLog(string strMsg, string userID, string userIP)
        {
            ILog logger = LogManager.GetLogger("MyLogger");

            log4net.ThreadContext.Properties["userID"] = userID;
            log4net.ThreadContext.Properties["userIP"] = userIP;

            logger.Error(strMsg);
        }

        //监控日志记录
        public static void WriteMonitorLog(string strMsg, string userID, string userIP, int businessID)
        {
            ILog logger = LogManager.GetLogger("MyLogger");
            log4net.ThreadContext.Properties["userID"] = userID;
            log4net.ThreadContext.Properties["userIP"] = userIP;
            log4net.ThreadContext.Properties["businessID"] = businessID;
            log4net.ThreadContext.Properties["userType"] = WebCaching.CurrentUserDomain.ToString();
            logger.Info(strMsg);
        }
        public static void WriteMonitorLog(string strMsg, string userID, string userIP, int businessID, int userType)
        {
            ILog logger = LogManager.GetLogger("MyLogger");
            log4net.ThreadContext.Properties["userID"] = userID;
            log4net.ThreadContext.Properties["userIP"] = userIP;
            log4net.ThreadContext.Properties["businessID"] = businessID;
            log4net.ThreadContext.Properties["userType"] = userType;
            logger.Info(strMsg);
        }

        //异常数据写入日志文件  文件夹 \WebApp\log
        public static void WriteLogToFile(string strMsg)
        {
            ILog logger = LogManager.GetLogger("MyLoggerFile");
            logger.Error(strMsg);
        }

        public static void WriteLogToFile(Exception ex)
        {
            ILog logger = LogManager.GetLogger("MyLoggerFile");

            string ErrorMessage = ex.Message;

            if (ex.InnerException != null)
            {
                ErrorMessage = ex.InnerException.Message;
            }
            logger.Error(ErrorMessage);
        }
        #endregion

    }
}