using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Suzsoft.Smart.Data;
using Suzsoft.Smart.EntityCore;
using eContract.BusinessService.Common;
using eContract.Common;
using eContract.Common.Entity;
using eContract.Common.MVC;
using eContract.Common.WebUtils;
using eContract.BusinessService.BusinessData.CommonQuery;
using System.Web.Script.Serialization;

namespace eContract.BusinessService.BusinessData.BusinessRule
{
    public class POApprovalSetBLL : BusinessBase
    {
        public JqGrid ForGrid(JqGrid grid)
        {
            var query = new POApprovalSetQuery();
            query.keyWord = grid.keyWord;
            grid = QueryTableHelper.QueryGrid<CasPoApprovalSettingsEntity>(query, grid);
            return grid;
        }

        public virtual CasPoApprovalSettingsEntity CreatePoApprovalSettEntity(string systemName = "MDM")
        {
            return new CasPoApprovalSettingsEntity();
        }
        public string GetPOApprovalSelectUser(string POSetId, string contractTypeId,string company)
        {
            var strsql = new StringBuilder();
            strsql.AppendFormat(" SELECT USER_ID FROM CAS_PO_APPROVAL_SETTINGS WHERE COMPANY=N'{0}' AND CONTRACT_TYPE_ID='{1}'", company, contractTypeId);
            DataSet userDataSet = DataAccess.SelectDataSet(strsql.ToString());
            //string[] userAry = Array.ConvertAll<DataRow, string>(userDataSet.Tables[0].Rows.Cast<DataRow>().ToArray(), r => r["USER_ID"].ToString());
            if (userDataSet.Tables[0].Rows.Count == 0)
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                System.Collections.ArrayList dic1 = new System.Collections.ArrayList();
                return jss.Serialize(dic1);
            }
            else
            {
                //string[] userAry = userDataSet.Tables[0].Rows[0]["USER_ID"].ToString().Split(',');

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("id", typeof(String));
                dataTable.Columns.Add("name", typeof(String));
                //for (int i = 0; i < userAry.Length; i++)
                for (int i = 0; i < userDataSet.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dataTable.NewRow();
                    var usersql = new StringBuilder();
                    usersql.AppendFormat(" SELECT USER_ID as id,(ENGLISH_NAME+'-'+ CHINESE_NAME) as name FROM CAS_USER WHERE USER_ID={0}", Utils.ToSQLStr(userDataSet.Tables[0].Rows[i]["USER_ID"].ToString()).Trim());
                    var valueString = DataAccess.SelectDataSet(usersql.ToString());
                    dr["id"] = valueString.Tables[0].Rows[0]["id"];
                    dr["name"] = valueString.Tables[0].Rows[0]["name"];
                    dataTable.Rows.Add(dr);
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

        }


        public virtual bool SavePOApprovalSetEntity(CasPoApprovalSettingsEntity poApprovalSetEntity, ref string strError)
        {
            poApprovalSetEntity.LastModifiedBy = WebCaching.UserAccount;
            poApprovalSetEntity.LastModifiedTime = DateTime.Now;
            try
            {
                var isExist = IsExist(poApprovalSetEntity, ref strError);
                if (isExist)
                {
                    poApprovalSetEntity.UserId = poApprovalSetEntity.ApprovalUserValue;
                    //更新
                    if (UpdateApprovalSet(poApprovalSetEntity))
                    {
                        return true;
                    }
                }
                else
                {
                    //新增
                    poApprovalSetEntity.PoApprovalId = Guid.NewGuid().ToString();
                    poApprovalSetEntity.CreatedBy = WebCaching.UserAccount;
                    poApprovalSetEntity.CreateTime = DateTime.Now;
                    //poApprovalSetEntity.UserId = poApprovalSetEntity.ApprovalUserValue;
                    if (InsertApprovalSet(poApprovalSetEntity))
                    {
                        return true;
                    }
                }
            }
            catch (Exception exception)
            {
                strError = exception.Message;
            }
            return false;
        }
        /// <summary>
        /// 跟新PO审批人
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateApprovalSet(CasPoApprovalSettingsEntity entity)
        {
            var delValue =  DeletePoApprovalSet(entity.ContractTypeId, entity.Company);
            bool insValue = false;
            if (delValue)
            {
                insValue = InsertApprovalSet(entity);
            }
            return insValue;
        }

        /// <summary>
        /// 新增POApprovalSet
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool InsertApprovalSet(CasPoApprovalSettingsEntity entity)
        {
            var poApprovalSetEntities =new List<CasPoApprovalSettingsEntity>();
            string[] userId = entity.ApprovalUserValue.ToString().Split(',');
            for (int i = 0; i < userId.Length; i++)
            {
                var poApprovalSetEntity = new CasPoApprovalSettingsEntity();
                poApprovalSetEntity.PoApprovalId = Guid.NewGuid().ToString();
                poApprovalSetEntity.Company = entity.Company;
                poApprovalSetEntity.ContractTypeId = entity.ContractTypeId;
                poApprovalSetEntity.UserId = userId[i];
                poApprovalSetEntity.CreatedBy = WebCaching.UserId;
                poApprovalSetEntity.CreateTime = DateTime.Now;
                poApprovalSetEntity.LastModifiedBy = WebCaching.UserId;
                poApprovalSetEntity.LastModifiedTime = DateTime.Now;
                poApprovalSetEntities.Add(poApprovalSetEntity);
            }
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                try
                {
                    broker.BeginTransaction();
                    DataAccess.Insert<CasPoApprovalSettingsEntity>(poApprovalSetEntities, broker);
                    broker.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    broker.RollBack();
                    return false;
                }
            }
            //return true;
        }

        public virtual bool IsExist(CasPoApprovalSettingsEntity poApprovalSetEntity, ref string strError)
        {
            if (poApprovalSetEntity.PoApprovalId == null) return false;
            var strsql = new StringBuilder();
            strsql.AppendFormat("SELECT 1 FROM CAS_PO_APPROVAL_SETTINGS WHERE PO_APPROVAL_ID = {0}",
                Utils.ToSQLStr(poApprovalSetEntity.PoApprovalId).Trim());
            var val = DataAccess.SelectScalar(strsql.ToString());
            if (string.IsNullOrEmpty(val) || val != "1") return false;
            strError = "该PO审批设置已经存在";
            return true;
        }

        public virtual bool DeletePoApprovalSet(string id ,string company)
        {
            if (string.IsNullOrWhiteSpace(id)) return false;
            var strsql = new StringBuilder();
            strsql.AppendFormat("DELETE FROM CAS_PO_APPROVAL_SETTINGS WHERE CONTRACT_TYPE_ID= {0} AND COMPANY={1}",
                Utils.ToSQLStr(id).Trim(), Utils.ToSQLStr(company).Trim());
            var val = DataAccess.SelectScalar(strsql.ToString());
            return true;
        }
    }
}
