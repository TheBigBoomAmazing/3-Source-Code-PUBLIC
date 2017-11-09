using eContract.BusinessService.BusinessData.CommonQuery;
using eContract.BusinessService.Common;
using eContract.Common;
using eContract.Common.Entity;
using eContract.Common.MVC;
using eContract.Common.WebUtils;
using Suzsoft.Smart.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace eContract.BusinessService.BusinessData.BusinessRule
{
    public class ProxyApprovalBLL : BusinessBase
    {
        public JqGrid ForGrid(JqGrid grid)
        {
            var query = new ProxyApprovalQuery();
            query.keyword = grid.keyWord;
            query.startdate = grid.QueryField.ContainsKey("BEGIN_TIME") ? grid.QueryField["BEGIN_TIME"] : "";
            grid.QueryField.Remove("BEGIN_TIME");
            grid = QueryTableHelper.QueryGrid<CasProxyApprovalEntity>(query, grid);
            return grid;
        }

        public bool DeleteProxyApprovalByIds(string deleteKeys, ref string strError)
        {
            List<string> listKeys = deleteKeys.Split(',').ToList();
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                try
                {
                    List<CasProxyApprovalEntity> listCasProxyApprovals = new List<CasProxyApprovalEntity>();
                    foreach (string id in listKeys)
                    {
                        listCasProxyApprovals.Add(GetById<CasProxyApprovalEntity>(id));
                    }
                    broker.BeginTransaction();
                    DataAccess.Delete<CasProxyApprovalEntity>(listCasProxyApprovals, broker);
                    broker.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    broker.RollBack();
                    return false;
                }
            }
        }

        public bool Save(CasProxyApprovalEntity entity, ref string strError)
        {
            if (!string.IsNullOrEmpty(entity.ProxyApprovalId))
            {
                List<string> agentUserIds = entity.AgentUserId.Split(',').ToList();
                using (DataAccessBroker broker = DataAccessFactory.Instance())
                {
                    try
                    {
                        broker.BeginTransaction();
                        Delete<CasProxyApprovalEntity>(new List<string> { entity.ProxyApprovalId }, broker);
                        foreach (string agentUserId in agentUserIds)
                        {
                            CasProxyApprovalEntity casProxyApprovalEntity = new CasProxyApprovalEntity();
                            casProxyApprovalEntity.ProxyApprovalId = Guid.NewGuid().ToString();
                            casProxyApprovalEntity.AgentUserId = agentUserId;
                            casProxyApprovalEntity.AuthorizedUserId = entity.AuthorizedUserId;
                            casProxyApprovalEntity.BeginTime = entity.BeginTime;
                            casProxyApprovalEntity.EndTime = entity.EndTime;
                            casProxyApprovalEntity.IsDeleted = false;
                            casProxyApprovalEntity.CreateTime = DateTime.Now;
                            casProxyApprovalEntity.CreatedBy = WebCaching.UserId;
                            casProxyApprovalEntity.LastModifiedTime = DateTime.Now;
                            casProxyApprovalEntity.LastModifiedBy = WebCaching.UserId;
                            casProxyApprovalEntity.TerminationTime = null;// entity.EndTime;
                            Insert(casProxyApprovalEntity, broker);
                        }
                        broker.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        broker.RollBack();
                        return false;
                    }
                }
            }
            else
            {
                List<string> agentUserIds = entity.AgentUserId.Split(',').ToList();
                using (DataAccessBroker broker = DataAccessFactory.Instance())
                {
                    try
                    {
                        broker.BeginTransaction();
                        foreach (string agentUserId in agentUserIds)
                        {
                            CasProxyApprovalEntity casProxyApprovalEntity = new CasProxyApprovalEntity();
                            casProxyApprovalEntity.ProxyApprovalId = Guid.NewGuid().ToString();
                            casProxyApprovalEntity.AgentUserId = agentUserId;
                            casProxyApprovalEntity.AuthorizedUserId = entity.AuthorizedUserId;
                            casProxyApprovalEntity.BeginTime = entity.BeginTime;
                            casProxyApprovalEntity.EndTime = entity.EndTime;
                            casProxyApprovalEntity.IsDeleted = false;
                            casProxyApprovalEntity.CreateTime = DateTime.Now;
                            casProxyApprovalEntity.CreatedBy = WebCaching.UserId;
                            casProxyApprovalEntity.LastModifiedTime = DateTime.Now;
                            casProxyApprovalEntity.LastModifiedBy = WebCaching.UserId;
                            casProxyApprovalEntity.TerminationTime = null;//entity.EndTime;
                            Insert(casProxyApprovalEntity, broker);
                        }
                        broker.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        broker.RollBack();
                        return false;
                    }
                }
            }
        }
        public string GetAgentUserList(string proxyApprovalId)
        {
            CasProxyApprovalEntity entity = GetById<CasProxyApprovalEntity>(proxyApprovalId);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(String));
            dataTable.Columns.Add("name", typeof(String));

            DataRow drUser = dataTable.NewRow();
            var usersql = new StringBuilder();
            usersql.AppendFormat(" SELECT USER_ID as id,(ENGLISH_NAME+'-'+ CHINESE_NAME) as name FROM CAS_USER WHERE USER_ID={0}", Utils.ToSQLStr(entity == null ? "" : entity.AgentUserId).Trim());
            var valueString = DataAccess.SelectDataSet(usersql.ToString());
            if (valueString.Tables[0].Rows.Count > 0)
            {
                drUser["id"] = valueString.Tables[0].Rows[0]["id"];
                drUser["name"] = valueString.Tables[0].Rows[0]["name"];
                dataTable.Rows.Add(drUser);
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            System.Collections.ArrayList dic = new System.Collections.ArrayList();
            foreach (DataRow dr in dataTable.Rows)
            {
                System.Collections.Generic.Dictionary<string, object> drow = new System.Collections.Generic.Dictionary<string, object>();
                foreach (DataColumn dc in dataTable.Columns)
                {
                    drow.Add(dc.ColumnName, dr[dc.ColumnName]);
                }
                dic.Add(drow);
            }
            //序列化  
            return jss.Serialize(dic);
        }

        /// <summary>
        /// 授权人信息查询
        /// </summary>
        /// <param name="proxyApprovalId"></param>
        /// <returns></returns>
        public string GetAuthorizedUserList(string proxyApprovalId)
        {
            CasProxyApprovalEntity entity = GetById<CasProxyApprovalEntity>(proxyApprovalId);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(String));
            dataTable.Columns.Add("name", typeof(String));

            DataRow drUser = dataTable.NewRow();
            var usersql = new StringBuilder();
            usersql.AppendFormat(" SELECT USER_ID as id,(ENGLISH_NAME+'-'+ CHINESE_NAME) as name FROM CAS_USER WHERE USER_ID={0}", Utils.ToSQLStr(entity == null ? "" : entity.AuthorizedUserId).Trim());
            var valueString = DataAccess.SelectDataSet(usersql.ToString());
            if (valueString.Tables[0].Rows.Count > 0)
            {
                drUser["id"] = valueString.Tables[0].Rows[0]["id"];
                drUser["name"] = valueString.Tables[0].Rows[0]["name"];
                dataTable.Rows.Add(drUser);
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            System.Collections.ArrayList dic = new System.Collections.ArrayList();
            foreach (DataRow dr in dataTable.Rows)
            {
                System.Collections.Generic.Dictionary<string, object> drow = new System.Collections.Generic.Dictionary<string, object>();
                foreach (DataColumn dc in dataTable.Columns)
                {
                    drow.Add(dc.ColumnName, dr[dc.ColumnName]);
                }
                dic.Add(drow);
            }
            //序列化  
            return jss.Serialize(dic);
        }


        public bool TerminationDelegationById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return false;
            var strsql = new StringBuilder();
            strsql.AppendFormat(" UPDATE CAS_PROXY_APPROVAL SET [IS_DELETED]=1,[TERMINATION_TIME]=getdate() WHERE PROXY_APPROVAL_ID={0} ",
                Utils.ToSQLStr(id).Trim());
            var val = DataAccess.ExecuteNoneQuery(strsql.ToString());
            return true;
        }

    }
}
