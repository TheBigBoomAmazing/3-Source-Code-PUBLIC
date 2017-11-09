using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common;
using eContract.Common.Entity;
using eContract.Common.WebUtils;
using eContract.BusinessService.SystemManagement.Domain;

namespace eContract.BusinessService.BusinessData.CommonQuery
{
    public class ContractApplayListQuery : IQuery
    {
        //public Dictionary<string, string> queryFieldValue=new Dictionary<string, string>();
        public string FERRERO_ENTITY;
        public string CONTRACT_TYPE_NAME;
        public string CONTRACT_NAME;
        public string CONTRACT_INITIATOR;
        public string CONTRACT_SERIAL_NO;
        //public string CREATE_TIME;
        public WhereBuilder ParseSQL()
        {
            UserDomain userDomain = (UserDomain)WebCaching.CurrentUserDomain;
            //var sql = new StringBuilder();
            //sql.AppendLine($@" SELECT * from (
            //                SELECT t1.*,t2.STATUS as APPROVE_STATUS,t2.CONTRACT_APPROVER_ID  
            //                FROM dbo.CAS_CONTRACT t1
            //                JOIN dbo.CAS_CONTRACT_APPROVER t2 ON t1.CONTRACT_ID = t2.CONTRACT_ID
            //                WHERE 1 = 1 AND t2.APPROVER_TYPE != 1 AND t2.STATUS IN (2,4) AND t1.STATUS IN (2,8)
            //                AND (t2.APPROVER_ID = '{userDomain.CasUserEntity.UserId}' 
            //                OR t2.APPROVER_ID IN (SELECT t3.AUTHORIZED_USER_ID FROM dbo.CAS_PROXY_APPROVAL t3 
            //                WHERE t3.AGENT_USER_ID = '{userDomain.CasUserEntity.UserId}' 
            //                AND GETDATE() BETWEEN t3.BEGIN_TIME AND t3.END_TIME))
            //                UNION ALL
            //                SELECT t1.*,t2.STATUS as APPROVE_STATUS,t2.CONTRACT_APPROVER_ID  
            //                FROM dbo.CAS_CONTRACT t1
            //                JOIN dbo.CAS_CONTRACT_APPROVER t2 ON t1.CONTRACT_ID = t2.CONTRACT_ID
            //                JOIN dbo.CAS_DEPARTMENT t4 ON t4.DEPT_ID = t2.APPROVER_ID
            //                JOIN dbo.CAS_DEPT_USER t5 ON t5.DEPT_ID = t4.DEPT_ID
            //                WHERE 1 = 1 AND t2.APPROVER_TYPE != 1 AND t2.STATUS IN (2,4) AND t1.STATUS IN (2,8)
            //                AND (t5.USER_ID = '{userDomain.CasUserEntity.UserId}' 
            //                OR t5.USER_ID IN (SELECT t3.AUTHORIZED_USER_ID FROM dbo.CAS_PROXY_APPROVAL t3 
            //                WHERE t3.AGENT_USER_ID = '{userDomain.CasUserEntity.UserId}' 
            //                AND GETDATE() BETWEEN t3.BEGIN_TIME AND t3.END_TIME))) temp ");
            string sql = $@"SELECT * from (
                            SELECT t1.*,t2.STATUS as APPROVE_STATUS,t2.APPROVER_TYPE,t2.CONTRACT_APPROVER_ID,t6.STEP   
                            FROM dbo.CAS_CONTRACT t1
                            JOIN dbo.CAS_CONTRACT_APPROVER t2 ON t1.CONTRACT_ID = t2.CONTRACT_ID
                            LEFT JOIN dbo.CAS_CONTRACT_APPROVAL_STEP t6 ON t2.CONTRACT_APPROVAL_STEP_ID=t6.CONTRACT_APPROVAL_STEP_ID
                            WHERE 1 = 1 AND t2.APPROVER_TYPE != 1 AND t2.STATUS IN (2,4) AND t1.STATUS IN (2,8)
                            AND (t2.APPROVER_ID = '{userDomain.CasUserEntity.UserId}' 
                            OR t2.APPROVER_ID IN (SELECT t3.AUTHORIZED_USER_ID FROM dbo.CAS_PROXY_APPROVAL t3 
                            WHERE t3.AGENT_USER_ID = '{userDomain.CasUserEntity.UserId}' 
                            AND GETDATE() BETWEEN t3.BEGIN_TIME AND t3.END_TIME AND t3.IS_DELETED=0))
                            UNION ALL
                            SELECT t1.*,t2.STATUS as APPROVE_STATUS,t2.APPROVER_TYPE,t2.CONTRACT_APPROVER_ID,t6.STEP   
                            FROM dbo.CAS_CONTRACT t1
                            JOIN dbo.CAS_CONTRACT_APPROVER t2 ON t1.CONTRACT_ID = t2.CONTRACT_ID
                            JOIN dbo.CAS_DEPARTMENT t4 ON t4.DEPT_ID = t2.APPROVER_ID
                            JOIN dbo.CAS_DEPT_USER t5 ON t5.DEPT_ID = t4.DEPT_ID
                            LEFT  JOIN dbo.CAS_CONTRACT_APPROVAL_STEP t6 ON t2.CONTRACT_APPROVAL_STEP_ID=t6.CONTRACT_APPROVAL_STEP_ID
                            WHERE 1 = 1 AND t2.APPROVER_TYPE != 1 AND t2.STATUS IN (2,4) AND t1.STATUS IN (2,8)
                            AND (t5.USER_ID = '{userDomain.CasUserEntity.UserId}' 
                            OR t5.USER_ID IN (SELECT t3.AUTHORIZED_USER_ID FROM dbo.CAS_PROXY_APPROVAL t3 
                            WHERE t3.AGENT_USER_ID = '{userDomain.CasUserEntity.UserId}' 
                            AND GETDATE() BETWEEN t3.BEGIN_TIME AND t3.END_TIME AND t3.IS_DELETED=0))
							UNION ALL 
							SELECT t1.*,t2.STATUS as APPROVE_STATUS,t2.APPROVER_TYPE,t2.CONTRACT_APPROVER_ID,t6.STEP  FROM dbo.CAS_CONTRACT t1
                            JOIN dbo.CAS_CONTRACT_APPROVER t2 ON t1.CONTRACT_ID = t2.CONTRACT_ID
                            LEFT JOIN dbo.CAS_CONTRACT_APPROVAL_STEP t6 ON t2.CONTRACT_APPROVAL_STEP_ID=t6.CONTRACT_APPROVAL_STEP_ID
                            WHERE 1 = 1 AND t2.APPROVER_TYPE = 1 AND t2.STATUS = 2 AND t1.STATUS IN (2,8) 
							AND t2.APPROVER_ID = '{userDomain.CasUserEntity.UserId}') temp";
            if (CONTRACT_NAME != "")
            {
                var para = $@" 1 = 1 AND t1.CONTRACT_NAME LIKE N'%{CONTRACT_NAME}%' ";
                sql = sql.Replace("1 = 1", para);
            }
            if (CONTRACT_SERIAL_NO != "")
            {
                var para = $@" 1 = 1 AND t1.CONTRACT_SERIAL_NO LIKE N'%{CONTRACT_SERIAL_NO}%' ";
                sql = sql.Replace("1 = 1", para);
            }
            if (CONTRACT_TYPE_NAME!="")
            {
                var para = $@" 1 = 1 AND t1.CONTRACT_TYPE_NAME = '{CONTRACT_TYPE_NAME}' ";
                sql = sql.Replace("1 = 1", para);
            }
            if (FERRERO_ENTITY != "")
            {
                var para = $@" 1 = 1 AND t1.FERRERO_ENTITY = '{FERRERO_ENTITY}' ";
                sql = sql.Replace("1 = 1", para);
            }
            if (CONTRACT_INITIATOR!="")
            {
                var para = $@" 1 = 1 AND t1.CONTRACT_INITIATOR LIKE N'%{CONTRACT_INITIATOR}%' ";
                sql = sql.Replace("1 = 1", para);
            }
            //if (CREATE_TIME!="")
            //{
            //    var star = CREATE_TIME + " 00:00:00";
            //    var end = CREATE_TIME + " 23:59:59"; 
            //    var para = $@" 1 = 1 AND t1.CREATE_TIME BETWEEN '{star}' AND '{end}' ";
            //    sql = sql.Replace("1 = 1", para);
            //}
            var wb = new WhereBuilder(sql.ToString());
            return wb;
        }
    }
}
