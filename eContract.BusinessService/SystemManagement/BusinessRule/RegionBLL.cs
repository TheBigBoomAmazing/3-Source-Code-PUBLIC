using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eContract.BusinessService.Common;
using eContract.BusinessService.SystemManagement.Domain;
using eContract.Common.Entity;
using Suzsoft.Smart.Data;
using eContract.DataAccessLayer;
using eContract.Common.Schema;
using System.Data;
using eContract.DataAccessLayer.SystemManagement;
using eContract.Common;
using Suzsoft.Smart.EntityCore;
using eContract.BusinessService.SystemManagement.Service;
using eContract.Common.MVC;
using eContract.BusinessService.SystemManagement.CommonQuery;
using eContract.Common.WebUtils;
using System.Web.Script.Serialization;

namespace eContract.BusinessService.SystemManagement.BusinessRule
{
    public class RegionBLL : BusinessBase
    {
        /// <summary>
        /// 创建领域对象
        /// </summary>
        /// <returns></returns>
        public virtual CasRegionDomain CreateRegionDomain()
        {
            CasRegionEntity RegionEntity = new CasRegionEntity();
            return new CasRegionDomain(RegionEntity);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="grid">列表</param>
        /// <returns></returns>
        public JqGrid ForGrid(JqGrid grid)
        {
            CasRegionQuery query = new CasRegionQuery();
            query.keyWord = grid.keyWord;
            grid = QueryTableHelper.QueryGrid<CasRegionEntity>(query, grid);
            return grid;
        }
        /// <summary>
        /// 获得区域总监信息
        /// </summary>
        /// <param name="deptManagerId"></param>
        /// <returns></returns>
        public string GetRegionManager(string deptManagerId)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(String));
            dataTable.Columns.Add("name", typeof(String));
            DataRow dr = dataTable.NewRow();
            var usersql = new StringBuilder();
            usersql.AppendFormat(" SELECT USER_ID as id, (ENGLISH_NAME+'-'+ CHINESE_NAME) as name FROM CAS_USER WHERE USER_ID={0}", Utils.ToSQLStr(deptManagerId).Trim());
            var valueString = DataAccess.SelectDataSet(usersql.ToString());
            if (valueString.Tables[0].Rows.Count == 0)
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                System.Collections.ArrayList dic1 = new System.Collections.ArrayList();
                return jss.Serialize(dic1);
            }
            else
            {
                dr["id"] = valueString.Tables[0].Rows[0]["id"];
                dr["name"] = valueString.Tables[0].Rows[0]["name"];
                dataTable.Rows.Add(dr);
                JavaScriptSerializer jss = new JavaScriptSerializer();
                System.Collections.ArrayList dic = new System.Collections.ArrayList();
                foreach (DataRow drV in dataTable.Rows)
                {
                    System.Collections.Generic.Dictionary<string, object> drow = new System.Collections.Generic.Dictionary<string, object>();
                    foreach (DataColumn dc in dataTable.Columns)
                    {
                        drow.Add(dc.ColumnName, drV[dc.ColumnName]);
                    }
                    dic.Add(drow);

                }
                //序列化  
                return jss.Serialize(dic);
            }

        }
        public virtual bool Save(CasRegionDomain casRegionDomain, ref string strError)
        {
            casRegionDomain.CasRegionEntity.LastModifiedTime = DateTime.Now;
            casRegionDomain.CasRegionEntity.LastModifiedBy = WebCaching.UserId;
            try
            {
                if (!string.IsNullOrEmpty(casRegionDomain.CasRegionEntity.RegionId))
                {
                    if (Update(casRegionDomain.CasRegionEntity))
                    {
                        return true;
                    }
                }
                else
                {
                    casRegionDomain.CasRegionEntity.RegionId = Guid.NewGuid().ToString();
                    if (Insert(casRegionDomain.CasRegionEntity))
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            return false;
        }

        /// <summary>
        /// 删除大区
        /// </summary>
        /// <param name="RegionIds"></param>
        /// <returns></returns>
        public virtual bool DeleteRegionDomain(List<string> RegionIds)
        {
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                try
                {
                    List<CasRegionEntity> lRegion = new List<CasRegionEntity>();
                    foreach (string id in RegionIds)
                    {
                        lRegion.Add(GetById<CasRegionEntity>(id));
                    }
                    broker.BeginTransaction();
                    DataAccess.Delete<CasRegionEntity>(lRegion, broker);
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

        public virtual bool DeleteRegionDomainByIds(string deletekeys, ref string strError)
        {
            List<string> list = deletekeys.Split(new char[] { ';', ',' }).ToList<string>();
            try
            {
                return DeleteRegionDomain(list);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            return false;
        }


        /// <summary>
        /// 获取functionRoleDomain
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public List<CasUserEntity> GetAllUsers()
        {
            eContract.Common.Utils.QueryCondition qCondition = eContract.Common.Utils.QueryCondition.Create();
            return SelectByCondition<CasUserEntity>(qCondition);
        }

    }
}
