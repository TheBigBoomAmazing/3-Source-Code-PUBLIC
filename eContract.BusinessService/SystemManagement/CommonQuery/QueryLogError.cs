using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;
using eContract.Common.Entity;
using eContract.Common;

namespace eContract.BusinessService.SystemManagement.CommonQuery
{
    public class QueryLogError : SecLogErrorEntity, IQuery
    {
        #region 属性

        public string systemName
        {
            get;
            set;
        }
        #endregion

        #region IQuery Members

        public LigerGrid ligerGrid;
        public WhereBuilder ParseSQL()
        {
            StringBuilder sb = new StringBuilder("select a.* from " + SecLogErrorTable.C_TableName + " a where 1=1 ");
//            sb.Append(@"Select 	log_error_id,
//	                    log_error_category,
//	                    message, 
//	                    machine_name,
//	                    ip,
//	                    log_time,LAST_MODIFIED_BY from ").Append(SecLogErrorTable.C_TableName).Append(" e ");
            //WhereBuilder wb = new WhereBuilder(sb.ToString());
            //wb.FixFirstCondition = true;
            //wb.AddAndCondition("1", "1");
            //string IP = "";
            //if (ligerGrid.HasKey("IP", ref IP))
            //{
            //    //wb.AddAndCondition("e", SecLogErrorTable.C_IP, SQLOperator.Like, Ip);
            //    sb.Append("and a.ip='" + Utils.ToSQLStr(IP)+"'");
            //}
            //string LogErrorCategory = "";
            //if (ligerGrid.HasKey("LogErrorCategory", ref LogErrorCategory))
            //{
            //    //wb.AddAndCondition("e", SecLogErrorTable.C_LOG_ERROR_CATEGORY, SQLOperator.Like, LogErrorCategory);
            //    sb.Append("and a.LOG_ERROR_CATEGORY='" + Utils.ToSQLStr(LogErrorCategory)+"'");
            //}
            //string MachineName = "";
            //if (ligerGrid.HasKey("MachineName", ref MachineName))
            //{
            //    //wb.AddAndCondition("e", SecLogErrorTable.C_MACHINE_NAME, SQLOperator.Like, MachineName);
            //    sb.Append("and a.MACHINE_NAME like'%" + Utils.ToSQLStr(MachineName) + "%'");
            //}
            //string DateFrom = "";
            //if (ligerGrid.HasKey("DateFrom", ref DateFrom) && Validator.IsDateTime(DateFrom))
            //{
            //    //wb.AddAndCondition("e", SecLogErrorTable.C_LOG_TIME, SQLOperator.GreaterEquals, DateFrom);
            //    sb.Append(" and convert(varchar(10),a.LOG_TIME,120)>='" + Utils.ToSQLStr(DateFrom) + "'");
            //}
            //string DateTo = "";
            //if (ligerGrid.HasKey("DateTo", ref DateTo) && Validator.IsDateTime(DateTo))
            //{
            //    //wb.AddAndCondition("e", SecLogErrorTable.C_LOG_TIME, SQLOperator.LessEquals, DateTo);
            //    sb.Append(" and convert(varchar(10),a.LOG_TIME,120)<='" + Utils.ToSQLStr(DateTo) + "'");
            //}
            ////根据LogTime来排序
            //string sqlExtends = " order by e." + SecLogErrorTable.C_LOG_TIME + " desc";
            //wb.AddCondition(sqlExtends);
            //sb.Append("order by a.LOG_TIME desc");
            WhereBuilder wb = new WhereBuilder(sb.ToString());
            return wb;
        }

        #endregion
    }
}
