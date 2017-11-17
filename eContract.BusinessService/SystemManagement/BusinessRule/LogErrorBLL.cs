using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;
using eContract.BusinessService.Common;
using eContract.Common.Entity;
using eContract.BusinessService.SystemManagement.Domain;
using eContract.Common.WebUtils;
using System.Net;
using eContract.BusinessService.SystemManagement.Service;
using Suzsoft.Smart.Data;
using eContract.Common;
using eContract.BusinessService.SystemManagement.CommonQuery;
using eContract.Common.MVC;

namespace eContract.BusinessService.SystemManagement.BusinessRule
{
    public class LogErrorBLL : BusinessBase
    {
        /// <summary>
        /// 创建领域对象
        /// </summary>
        /// <returns></returns>
        public virtual LogErrorDomain LogErrorDomainCreate()
        {
            SecLogErrorEntity errorEty = new SecLogErrorEntity();
            errorEty.LogErrorId = Guid.NewGuid().ToString();
            errorEty.Message = string.Empty;
            errorEty.StackTrace = string.Empty;
            errorEty.MachineName = string.Empty;
            errorEty.Ip = string.Empty;
            //errorEty.LogTime = SystemService.ParameterService.GetSqlDateTimeNow();
            errorEty.LastModifiedBy = string.Empty;
            //errorEty.LastModifiedTime = SystemService.ParameterService.GetSqlDateTimeNow();
            return new LogErrorDomain(errorEty);
        }

        /// <summary>
        /// 新增错误日志
        /// </summary>
        /// <param name="logErrorDomain"></param>
        /// <returns></returns>
        public virtual bool InsertLogErrorDomain(LogErrorDomain logErrorDomain)
        {
            return Insert(logErrorDomain.SecLogErrorEntity);
        }

        /// <summary>
        /// 获取错误日志Domain
        /// </summary>
        /// <param name="logErrorId"></param>
        /// <returns></returns>
        public virtual LogErrorDomain GetLogErrorDomain(string logErrorId)
        {
            SecLogErrorEntity errorEty = GetById<SecLogErrorEntity>(logErrorId);
            if (errorEty == null)
            {
                return null;
            }
            return new LogErrorDomain(errorEty);
        }

        /// <summary>
        /// 记录用户自定义日志
        /// </summary>
        /// <param name="ex"></param>
        public virtual void InsertCustomLog(Exception ex)
        {
            InsertLog(ex, "Custom");
        }


        /// <summary>
        /// 记录异常日志
        /// </summary>
        /// <param name="ex"></param>
        public virtual void InsertLog(Exception ex)
        {
            InsertLog(ex, "System");
        }

        /// <summary>
        /// 记录异常日志
        /// </summary>
        /// <param name="ex"></param>
        public virtual void InsertLog(Exception ex, string Errorclass)
        {
            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
            string fileName = trace.GetFrame(trace.FrameCount - 1).GetFileName();
            string methodName=trace.GetFrame(trace.FrameCount - 1).GetMethod().Name;
            int lineNumber=trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber(); 
            SecLogErrorEntity err = new SecLogErrorEntity();
            err.LogTime = DateTime.Now;

            err.LogErrorId = Guid.NewGuid().ToString();
            UserDomain user = WebCaching.CurrentUser as UserDomain; 
            err.CreatedBy = user != null ? user.CasUserEntity.UserAccount : "1";
            err.CreateTime = DateTime.Now;
            err.LastModifiedBy = user != null ? user.CasUserEntity.UserAccount : "1";
            err.Ip = WebCaching.CurrentIP;
            err.MachineName = Dns.GetHostName();
            err.Message = ex.Message;
            err.StackTrace = ex.StackTrace; //+relationData;
            err.LastModifiedTime = DateTime.Now;
            if (WebCaching.CurrentContext == null || WebCaching.CurrentContext.Request == null)
            {
            }
            else
            {
                int i = WebCaching.CurrentContext.Request.Path.LastIndexOf('/');
                if (i != -1)
                { 
                    err.Pagename = WebCaching.CurrentContext.Request.Path.Substring(i + 1);
                }
            }
            DataAccess.Insert(err);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strError"></param>
        /// <param name="relationData"></param>
        public virtual void InsertLog(string strError, string relationData)
        {
            SecLogErrorEntity err = new SecLogErrorEntity();
            err.LogTime = DateTime.Now;

            // SystemService.ParameterService.GetSqlDateTimeNow();
            UserDomain user = WebCaching.CurrentUser as UserDomain;
            err.CreatedBy = user != null ? user.CasUserEntity.UserAccount : "1";
            err.CreateTime = DateTime.Now;
            err.LogErrorId = Guid.NewGuid().ToString();
            err.LastModifiedBy = CurrentUser.CasUserEntity.UserAccount;
            err.Ip = WebCaching.CurrentIP;
            err.MachineName = Dns.GetHostName();
            err.Message = strError;
            err.StackTrace = strError + relationData;
            DataAccess.Insert(err); 
        }

        public DataTable GetLogErrorBySearch(string dateFrom, string dateTo, string category, string pageName, string filename, string modified, string methodname, string message)
        {
            string sql = @"SELECT  [LOG_ERROR_ID]
      ,[MESSAGE]
      ,[STACK_TRACE]
      ,[MACHINE_NAME]
      ,[IP]
      ,[LOG_TIME]
      ,[LAST_MODIFIED_BY]
      ,[LAST_MODIFIED_TIME]
  FROM [SEC_LOG_ERROR]
  where 1=1  ";
            if (!string.IsNullOrEmpty(pageName))
            {
                sql += "  and PAGENAME  like '%" + pageName + "%'";
            }
            if (!string.IsNullOrEmpty(modified))
            {
                sql += "  and LAST_MODIFIED_BY like '%" + modified + "%'";
            }
            if (!string.IsNullOrEmpty(message))
            {
                sql += " and MESSAGE like '%" + message + "%'";
            }
           
            if (!string.IsNullOrEmpty(dateFrom))
            {
                sql += " and LOG_TIME>'" + dateFrom + "'";
            }
            if (!string.IsNullOrEmpty(dateTo))
            {
                sql += " and LOG_TIME<=DATEADD(DAY,1,'" + dateTo + "')";
            }
            sql += " order by LOG_TIME desc";
            return DataAccess.SelectDataSet(sql).Tables[0];

        }

        public JqGrid ForGrid(string systemName, JqGrid grid)
        {
            QueryLogError query = new QueryLogError();
            //query.ligerGrid = grid;
            query.systemName = systemName;
            grid = QueryTableHelper.QueryGrid<SecLogErrorEntity>(query, grid);
            return grid;
        }

        public void InsertLog(string strError, string relationData, string systemName, string errorCategory)
        {
            SecLogErrorEntity err = new SecLogErrorEntity();
            err.LogTime = DateTime.Now;

            // SystemService.ParameterService.GetSqlDateTimeNow();
            err.LogErrorId = Guid.NewGuid().ToString();

            UserDomain user = WebCaching.CurrentUser as UserDomain;
            err.CreatedBy = user != null ? user.CasUserEntity.UserAccount : "1";
            err.CreateTime = DateTime.Now;
            err.LastModifiedBy = user != null ? user.CasUserEntity.UserAccount : "1";
            err.LastModifiedTime = DateTime.Now;
            err.Ip = WebCaching.CurrentIP;
            err.MachineName = Dns.GetHostName();
            err.Message = strError;
            err.StackTrace = relationData;
            DataAccess.Insert(err);
        }
    }
}