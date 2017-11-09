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
    public class DepartmentBLL : BusinessBase
    {
        /// <summary>
        /// 创建领域对象
        /// </summary>
        /// <returns></returns>
        public virtual CasDepartmentDomain CreateDepartmentDomain()
        {
            CasDepartmentEntity departmentEntity = new CasDepartmentEntity();
            return new CasDepartmentDomain(departmentEntity);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="grid">列表</param>
        /// <returns></returns>
        public JqGrid ForGrid(JqGrid grid)
        {
            CasDepartmentQuery query = new CasDepartmentQuery();
            query.keyWord = grid.keyWord;
            if (grid.QueryField.ContainsKey("DEPT_TYPE"))
            {
                query.depType = grid.QueryField["DEPT_TYPE"].ToString();
            } ;
            grid = QueryTableHelper.QueryGrid<CasDepartmentEntity>(query, grid);
            return grid;
        }
        /// <summary>
        /// 获得部门总监
        /// </summary>
        /// <param name="deptManagerId"></param>
        /// <returns></returns>
        public string GetSelectManager(string deptManagerId)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(String));
            dataTable.Columns.Add("name", typeof(String));
            DataRow dr = dataTable.NewRow();
            var usersql = new StringBuilder();
            usersql.AppendFormat(" SELECT USER_ID as id,(ENGLISH_NAME+'-'+ CHINESE_NAME) as name FROM CAS_USER WHERE USER_ID={0}", Utils.ToSQLStr(deptManagerId).Trim());
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
        /// <summary>
        /// 获得部门成员
        /// </summary>
        /// <param name="depId"></param>
        /// <returns></returns>
        public string GetSelectDepUser(string depId)
        {
            var strsql = new StringBuilder();
            strsql.AppendFormat(" SELECT DISTINCT USER_ID FROM CAS_DEPT_USER WHERE DEPT_ID='{0}' ", depId);
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
                int len = userDataSet.Tables[0].Rows.Count;
                string[] userAry = new string[len];
                for (int i = 0; i < len; i++)
                {
                    userAry[i] = userDataSet.Tables[0].Rows[i]["USER_ID"].ToString();
                }
                //string[] userAry = userDataSet.Tables[0].Rows[0]["USER_ID"].ToString().Split(',');

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("id", typeof(String));
                dataTable.Columns.Add("name", typeof(String));
                for (int i = 0; i < userAry.Length; i++)
                {
                    DataRow dr = dataTable.NewRow();
                    var usersql = new StringBuilder();
                    usersql.AppendFormat(" SELECT USER_ID as id,(ENGLISH_NAME+'-'+ CHINESE_NAME) as name FROM CAS_USER WHERE USER_ID={0}", Utils.ToSQLStr(userAry[i]).Trim());
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

        public virtual bool Save(CasDepartmentDomain casDepartmentDomain, ref string strError)
        {
            casDepartmentDomain.CasDepartmentEntity.LastModifiedTime = DateTime.Now;
            casDepartmentDomain.CasDepartmentEntity.LastModifiedBy = WebCaching.UserId;
            bool isSuccess = true;
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                try
                {
                    broker.BeginTransaction();
                    if (!string.IsNullOrEmpty(casDepartmentDomain.CasDepartmentEntity.DeptId))
                    {
                        isSuccess = Update(casDepartmentDomain.CasDepartmentEntity, broker);
                        if (isSuccess&& casDepartmentDomain.CasDepartmentEntity.DeptType.HasValue &&
                            casDepartmentDomain.CasDepartmentEntity.DeptType.Value==2)
                        {
                            isSuccess = UpdateDepartmentInfo(casDepartmentDomain, broker);
                        }
                    }
                    else
                    {
                        casDepartmentDomain.CasDepartmentEntity.DeptId = Guid.NewGuid().ToString();
                        isSuccess = Insert(casDepartmentDomain.CasDepartmentEntity, broker);
                        if (isSuccess && casDepartmentDomain.CasDepartmentEntity.DeptType.HasValue &&
                            casDepartmentDomain.CasDepartmentEntity.DeptType.Value == 2)
                        {
                            isSuccess = UpdateDepartmentInfo(casDepartmentDomain, broker);
                        }
                    }
                    if (isSuccess)
                    {
                        broker.Commit();
                    }
                    else
                    {
                        strError = "Update failed"; 
                        broker.RollBack();
                    }
                }
                catch (Exception ex)
                {
                    broker.RollBack();
                    strError = ex.Message;
                }
            }
            return isSuccess;
        }
        /// <summary>
        /// 判断成员是否已经在其他组了 
        /// </summary>
        /// <param name="casDepartmentDomain"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public bool CheckUserIdExistInOtherDep(CasDepartmentDomain casDepartmentDomain, ref string strError)
        {

            string userIds = "'" + casDepartmentDomain.CasDepartmentEntity.DeptUserIds.Replace(",", "','") + "'";

            var strsql = new StringBuilder();
            strsql.AppendFormat(" SELECT CUSER.ENGLISH_NAME FROM CAS_DEPT_USER DU  INNER JOIN CAS_DEPARTMENT DEP ON DU.DEPT_ID = DEP.DEPT_ID INNER JOIN dbo.CAS_USER CUSER ON DU.USER_ID = CUSER.USER_ID WHERE DU.USER_ID IN({0}) AND DEP.DEPT_TYPE = '1' AND DEP.DEPT_ID <> {1}", userIds, Utils.ToSQLStr(casDepartmentDomain.CasDepartmentEntity.DeptId).Trim());
            DataSet val = DataAccess.SelectDataSet(strsql.ToString());
            if (val == null || val.Tables[0].Rows.Count == 0)
            {
                return true;
            }
            else
            {
                List<object> names = val.Tables[0].AsEnumerable().Select(r => r["ENGLISH_NAME"]).ToList();
                string str = string.Join(",", names);
                strError = "以下成员已经存在其他申请组：" + str;
                return false;
            }
            //return true;
        }

        public bool UpdateDepartmentInfo(CasDepartmentDomain casDepartmentDomain, DataAccessBroker broker)
        {
            try
            {
                #region 删除已经存在的用户
                var strsql = new StringBuilder();
                strsql.AppendFormat("  DELETE FROM CAS_DEPT_USER WHERE DEPT_ID= {0}", Utils.ToSQLStr(casDepartmentDomain.CasDepartmentEntity.DeptId).Trim());
                var val = DataAccess.SelectScalar(strsql.ToString(), broker);
                #endregion
                var depUserEnites = new List<CasDeptUserEntity>();
                string[] userIds;
                userIds = casDepartmentDomain.CasDepartmentEntity.DeptUserIds.ToString().Split(',');
                for (int i = 0; i < userIds.Length; i++)
                {
                    var depUserEnity = new CasDeptUserEntity();
                    depUserEnity.DeptId = casDepartmentDomain.CasDepartmentEntity.DeptId;
                    depUserEnity.UserId = userIds[i];
                    depUserEnity.IsDeleted = false;
                    depUserEnity.CreatedBy = WebCaching.UserId;
                    depUserEnity.CreateTime = DateTime.Now;
                    depUserEnity.LastModifiedBy = WebCaching.UserId;
                    depUserEnity.LastModifiedTime = DateTime.Now;
                    depUserEnites.Add(depUserEnity);
                }
                DataAccess.Insert<CasDeptUserEntity>(depUserEnites, broker);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="DeptIds"></param>
        /// <returns></returns>
        public virtual bool DeleteDepartmentDomain(List<string> DeptIds)
        {
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                try
                {
                    List<CasDeptUserEntity> lDeptUser = new List<CasDeptUserEntity>();
                    List<CasDepartmentEntity> lDepartment = new List<CasDepartmentEntity>();
                    foreach (string id in DeptIds)
                    {
                        lDepartment.Add(GetById<CasDepartmentEntity>(id));
                        lDeptUser.AddRange(GetDeptUserDomainByDeptID(id));
                    }
                    broker.BeginTransaction();
                    DataAccess.Delete<CasDepartmentEntity>(lDepartment, broker);
                    DataAccess.Delete<CasDeptUserEntity>(lDeptUser, broker);
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

        public virtual bool DeleteDepartmentDomainByIds(string deletekeys, ref string strError)
        {
            List<string> list = deletekeys.Split(new char[] { ';', ',' }).ToList<string>();
            try
            {
                return DeleteDepartmentDomain(list);
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

        /// <summary>
        /// 获取DeptUserDomain
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        protected virtual List<CasDeptUserEntity> GetDeptUserDomainByDeptID(string deptID)
        {
            eContract.Common.Utils.QueryCondition qCondition = eContract.Common.Utils.QueryCondition.Create().Equals(CasDeptUserTable.C_DEPT_ID, deptID);
            return SelectByCondition<CasDeptUserEntity>(qCondition);
        }


        public virtual string GetDeptUsersByDeptId(string deptId)
        {
            WhereBuilder wb = new WhereBuilder($"SELECT * FROM CAS_DEPT_USER WHERE DEPT_ID = '{deptId}' AND IS_DELETED = 0 ");
            List<CasDeptUserEntity> list = DataAccess.Select<CasDeptUserEntity>(wb);
            string deptUserId = list.Aggregate(string.Empty, (current, secDeptUserEntity) => current + (secDeptUserEntity.UserId + ","));
            if (!string.IsNullOrEmpty(deptUserId))
                deptUserId = deptUserId.Substring(0, deptUserId.Length - 1);
            return deptUserId;
        }
        public virtual bool SaveUserAdd(string DeptId, string strUserIds, ref string strError)
        {
            if (string.IsNullOrEmpty(DeptId))
            {
                return false;
            }
            string[] arr = strUserIds.Split(new char[] { ',' });
            arr = arr.Where(p => p.Length > 0).ToArray();
            List<CasDeptUserEntity> lst = new List<CasDeptUserEntity>();
            if (arr != null && arr.Length > 0)
            {
                foreach (var item in arr)
                {
                    lst.Add(new CasDeptUserEntity
                    {
                        UserId = item,
                        DeptId = DeptId,
                        LastModifiedBy = WebCaching.UserId,
                        LastModifiedTime = DateTime.Now
                    });
                }
            }
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                try
                {
                    DataAccess.ExcuteNoneQuery("delete CAS_DEPT_USER where Dept_ID='" + DeptId + "'", broker);
                    if (lst != null && lst.Count > 0)
                    {
                        DataAccess.Insert<CasDeptUserEntity>(lst, broker);
                    }
                    broker.Commit();
                }
                catch (Exception ex)
                {
                    SystemService.LogErrorService.InsertLog(ex);
                    broker.RollBack();
                    return false;
                }
            }
            return true;
        }

        public CasUserEntity GetDepartmentManagerByUserId(string userId)
        {
            string sql = $@"SELECT MANAGER.* 
                              FROM CAS_DEPARTMENT
                        INNER JOIN CAS_DEPT_USER
                                ON CAS_DEPARTMENT.DEPT_ID = CAS_DEPT_USER.DEPT_ID
                        INNER JOIN CAS_USER [USER]
                                ON CAS_DEPT_USER.USER_ID = [USER].USER_ID
                        INNER JOIN CAS_USER MANAGER
                                ON CAS_DEPARTMENT.DEPT_MANAGER_ID = MANAGER.USER_ID
                             WHERE CAS_DEPARTMENT.DEPT_TYPE = {DepartmentTypeEnum.ApplyDepartment.GetHashCode()}
                               AND [USER].USER_ID = '{userId}'";
            return DataAccess.Select<CasUserEntity>(sql).FirstOrDefault();
        }

        public List<CasUserEntity> GetAllDepartmentManagers()
        {
            string sql = $@"SELECT * 
                              FROM CAS_USER
                             WHERE USER_ID IN
                                          (
                                            SELECT DEPT_MANAGER_ID 
                                              FROM CAS_DEPARTMENT
                                          )";
            return DataAccess.Select<CasUserEntity>(sql);
        }

    }
}
