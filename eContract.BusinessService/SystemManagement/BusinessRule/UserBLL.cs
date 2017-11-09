using ComixSDK.EDI.Utils;
using eContract.BusinessService.Common;
using eContract.BusinessService.SystemManagement.CommonQuery;
using eContract.BusinessService.SystemManagement.Domain;
using eContract.BusinessService.SystemManagement.Service;
using eContract.Common;
using eContract.Common.Entity;
using eContract.Common.MVC;
using eContract.Common.Schema;
using eContract.Common.WebUtils;
using eContract.DataAccessLayer.SystemManagement;
using Suzsoft.Smart.Data;
using Suzsoft.Smart.EntityCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace eContract.BusinessService.SystemManagement.BusinessRule
{
    public class UserBLL : BusinessBase
    {
        /// <summary>
        /// 创建领域对象
        /// </summary>
        /// <returns></returns>
        public virtual UserDomain CreateUserDomain(string systemName)
        {
            CasUserEntity userEty = new CasUserEntity();
            //userEty.UserId = Guid.NewGuid().ToString();
            return new UserDomain(systemName, userEty);
        }

        /// <summary>
        /// 当前登录系统的用户Domain
        /// </summary>
        public virtual UserDomain CurrentUser
        {
            get { return WebCaching.CurrentUser as UserDomain; }
        }

        /// <summary>
        /// 当前登录系统的用户Domain
        /// </summary>
        public virtual UserDomain CurrentUserDomain
        {
            get { return WebCaching.CurrentUserDomain as UserDomain; }
        }


        /// <summary>
        /// 从缓存中获取用户Domain
        /// </summary>
        /// <param name="delUserId"></param>
        /// <returns></returns>
        public virtual UserDomain GetUserDomain(string systemName, string userId)
        {
            Dictionary<string, CasUserEntity> user = GetAllUser();
            if (user.ContainsKey(userId))
            {
                return new UserDomain(systemName, user[userId]);
            }
            return null;
        }

        /// <summary>
        /// 从缓存中获取用户Domain
        /// </summary>
        /// <param name="delUserId"></param>
        /// <returns></returns>
        public virtual CasUserEntity GetUserEntity(string userId)
        {
            Dictionary<string, CasUserEntity> user = GetAllUser();
            if (user.ContainsKey(userId))
            {
                return user[userId];
            }
            return null;
        }

        public virtual string GetFullNameById(string userId)
        {
            Dictionary<string, CasUserEntity> user = GetAllUser();
            if (user.ContainsKey(userId))
            {
                return user[userId].UserAccount + "[" + user[userId].ChineseName + "]";
            }
            return null;
        }

        /// <summary>
        /// 从数据库中直接获取用户Domain
        /// </summary>
        /// <param name="delUserId"></param>
        /// <returns></returns>
        public virtual UserDomain GetUserDomainFromDB(string systemName, string userId)
        {
            CasUserEntity user = this.GetById<CasUserEntity>(userId);
            if (null != user)
            {
                return new UserDomain(systemName, user);
            }
            return null;
        }

        public virtual string GetRealAccount(string vAccount)
        {
            DataSet ds = DataAccess.SelectDataSet("select user_account from sec_user_spider where user_account='" + vAccount + "' or mobile_phone='" + vAccount + "' or email='" + vAccount + "' ");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0]["user_account"].ToString();
            else
                return "";
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<string, CasUserEntity> GetAllUser()
        {
            if (null == WebCaching.UserCaching)
            {
                List<CasUserEntity> users = DataAccess.SelectAll<CasUserEntity>(CasUserTable.C_CHINESE_NAME);
                users.Sort((x, y) => string.Compare(x.ChineseName, y.ChineseName));
                Dictionary<string, CasUserEntity> dicUser = new Dictionary<string, CasUserEntity>();
                foreach (CasUserEntity user in users)
                {
                    dicUser.Add(user.UserId, user);
                }
                WebCaching.UserCaching = dicUser;
            }
            return WebCaching.UserCaching as Dictionary<string, CasUserEntity>;
        }

        public virtual CasUserEntity GetMstUserEntityByUserAccount(string strUserAccount)
        {
            CasUserEntity entity = new CasUserEntity();
            entity.UserAccount = strUserAccount;
            return DataAccess.SelectSingle<CasUserEntity>(entity);
        }

        public virtual UserDomain GetDomainByLoginName(string systemName, string userAccount)
        {
            var sql =
                "SELECT * FROM CAS_USER WHERE USER_ACCOUNT = @UserAccount OR EMAIL = @UserAccount OR MOBILE_PHONE = @UserAccount";

            DataAccessParameterCollection param = new DataAccessParameterCollection();
            ColumnInfo columnInfo = new ColumnInfo("UserAccount", "UserAccount",false,typeof(string));
            param.AddWithValue(columnInfo, userAccount);

            var list = DataAccess.Select<CasUserEntity>(sql, param);

            if (list != null && list.Any())
            {
                var user = list[0];

                return new UserDomain(systemName, user);
            }

            return null;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="delUserId"></param>
        /// <returns></returns>
        public virtual bool InsertUserDomain(UserDomain userDomain)
        {
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                broker.BeginTransaction();
                try
                {
                    Insert(userDomain.CasUserEntity, broker);
                    broker.Commit();
                    WebCaching.UserCaching = null;
                    return true;
                }
                catch (Exception ex)
                {
                    broker.RollBack();

                    throw ex;
                }
            }
        }

        public virtual DataTable QueryUser(IQuery query)
        {
            DataTable dt = DataAccess.QueryDataSet(query);
            return dt;
        }

        public virtual bool DeleteUserByIdList(List<string> idList, string userid)
        {
            string sql = "";
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                broker.BeginTransaction();
                try
                {
                    foreach (string id in idList)
                    {
                        sql = "UPDATE " + CasUserTable.C_TableName;
                        //sql += " SET " + CasUserTable.C_IS_DELETE + "=1 ";
                        sql += " WHERE " + CasUserTable.C_USER_ID + "='" + id + "'";

                        broker.ExecuteSQL(sql);
                    }
                    broker.Commit();
                    WebCaching.UserCaching = null;
                    return true;
                }
                catch
                {
                    broker.RollBack();
                    return false;
                }
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public bool DeleteUserById(List<string> idList)
        {
            return Delete<CasUserEntity>(idList);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="userDomain"></param>
        /// <returns></returns>
        public virtual bool UpdateUserDomain(UserDomain userDomain)
        {
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                broker.BeginTransaction();
                try
                {
                    string user_id = userDomain.CasUserEntity.UserId;
                    Update(userDomain.CasUserEntity, broker);
                    broker.Commit();
                    WebCaching.UserCaching = null;
                    return true;
                }
                catch (Exception ex)
                {
                    broker.RollBack();
                    SystemService.LogErrorService.InsertLog(ex);
                    return false;
                }
            }
        }


        /// <summary>
        /// 根据用户账号获取用户
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns></returns>
        public virtual UserDomain GetUserDomainByUserAccount(string systemName, string userAccount)
        {
            //foreach (CasUserEntity user in GetAllUser().Values)
            //{
            //    if (user.UserAccount == userAccount)
            //    {
            //        return new UserDomain(systemName,user);
            //    }
            //}
            //return null;
            CasUserEntity user = GetMstUserEntityByUserAccount(userAccount);
            if (user != null && !string.IsNullOrEmpty(user.Password))//
            {
                return new UserDomain(systemName, user);
            }
            return null;
        }

        public virtual UserDomain GetDomainByUserAccount(string systemName, string userAccount)
        {
            CasUserEntity user = GetMstUserEntityByUserAccount(userAccount);
            if (user != null)
            {
                return new UserDomain(systemName, user);
            }
            return null;
        }

        /// <summary>
        /// 根据 RoleId 返回 UserDomain 列表 Add By Shakken Xie on 2010 - 4 - 1
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public virtual List<UserDomain> GetUserDomainListByRoleId(string systemName, string roleId)
        {
            List<CasUserEntity> list = UserDA.GetUserEntityListByRoleId(roleId);
            List<UserDomain> result = new List<UserDomain>();
            foreach (var item in list)
            {
                result.Add(new UserDomain(systemName, item));
            }
            return result;
        }

        /// <summary>
        /// 获取采购人员
        /// </summary>
        /// <returns></returns>
        public virtual List<CasUserEntity> GetAllPurchase()
        {
            Dictionary<string, CasUserEntity> dicUser = GetAllUser();
            List<CasUserEntity> list = new List<CasUserEntity>();
            foreach (KeyValuePair<string, CasUserEntity> ety in dicUser)
            {
                list.Add(ety.Value);
            }
            return list;
        }

        /// <summary>
        /// sysType:
        ///    1--门店 0---总部
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="sysType"></param>
        /// <returns></returns>
        public virtual List<SecPageEntity> GetFavoritePage(string userid, int sysType)
        {
            return DataAccess.Select<SecPageEntity>(@"
                select p.* from sec_page p 
                     join sec_user_favorite f on p.page_id=f.page_id and f.User_Id='" + userid + @"'
                        where f.sys_type='" + sysType + @"'
                order by p.MENU_ORDER ");
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userAccount"></param>
        /// <param name="password"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public virtual bool B2BLogin(string userAccount, string password, ref string strError)
        {
            System.Net.WebClient client = new System.Net.WebClient();
            client.Headers["Content-Type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string url = ComixSDK.EDI.Utils.ConfigHelper.GetConfigString("B2BLOGINURL") + "ACTION=UserLogin&account=" + userAccount + "&password=" + password;
            string responseJson = "";
            string callingJson = "";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("ACTION", "UserLogin");
            dic.Add("account", userAccount);
            dic.Add("password", password);
            callingJson = JsonDataService.ObjectToJSON(dic);
            try
            {
                responseJson = client.UploadString(url, callingJson);
                responseJson = responseJson.Trim("\0".ToCharArray());
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
            ResponseLoginInfoModel responseDomain = new ResponseLoginInfoModel();
            if (responseDomain.code == "0")
            {
                return true;
            }
            else if (responseDomain.code == "-1")
            {
                strError = "系统故障，请联系管理员";
            }
            else if (responseDomain.code == "8" || responseDomain.code == "4")
            {
                strError = "您输入的账户名/密码不匹配，请核对后重新输入!";
            }
            else if (responseDomain.code == "2")
            {
                strError = "对不起，该帐号已被冻结或未激活，请联系管理员！";
            }
            else if (responseDomain.code == "3")
            {
                strError = "对不起，管理员不能登录前台！";
            }
            else if (responseDomain.code == "5")
            {
                strError = "用户尚未激活，必须激活后才能登录！";
            }
            else if (responseDomain.code == "7")
            {
                strError = "账号异常，请联系管理员！";
            }
            else
            {
                strError = "系统账号异常，请联系管理员！";
            }
            return false;

        }
        public virtual bool IsExist(UserDomain userDomain, ref string strError)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("select 1 from " + CasUserTable.C_TableName);
            strsql.Append(" where " + CasUserTable.C_USER_ID + "<>'" + userDomain.CasUserEntity.UserId + "'");
            strsql.Append(" and " + CasUserTable.C_USER_ACCOUNT + "='" + userDomain.CasUserEntity.UserAccount + "'");
            //strsql.Append(" and " + CasUserTable.C_IS_DELETE + "=0");
            string val = DataAccess.SelectScalar(strsql.ToString());
            if (!string.IsNullOrEmpty(val) && val == "1")
            {
                strError = "用户已存在，请确认后在保存";
                return true;
            }
            return false;
        }
        public virtual bool Save(UserDomain userDomain, ref string strError)
        {
            userDomain.CasUserEntity.LastModifiedTime = DateTime.Now;
            userDomain.CasUserEntity.LastModifiedBy = WebCaching.UserId;
            if (userDomain.CasUserEntity.IsLock == null)
            {
                userDomain.CasUserEntity.IsLock = false;
            }
            try
            {
                if (IsExist(userDomain, ref strError))
                {
                    return false;
                }
                if (!string.IsNullOrEmpty(userDomain.CasUserEntity.UserId))
                {
                    if (!string.IsNullOrWhiteSpace(userDomain.CasUserEntity.OwnDepValue.ToString()))
                    {
                        DataTable depData = GetDepartmentInfo(userDomain.CasUserEntity.OwnDepValue.ToString());
                        userDomain.CasUserEntity.DeparmentCode = depData.Rows[0]["DEPT_CODE"].ToString();
                        userDomain.CasUserEntity.DeparmentName = depData.Rows[0]["DEPT_NAME"].ToString();
                    }
                    if (string.IsNullOrWhiteSpace(userDomain.CasUserEntity.UserCode))
                    {
                        userDomain.CasUserEntity.UserCode = userDomain.CasUserEntity.UserAccount;
                    }
                    if (Update(userDomain.CasUserEntity))
                    {
                        WebCaching.UserCaching = null;
                        return true;
                    }
                }
                else
                {
                    userDomain.CasUserEntity.UserId = Guid.NewGuid().ToString();
                    //userDomain.CasUserEntity.Gender = 1;
                    //userDomain.CasUserEntity.IsDelete = 0;
                    if (!string.IsNullOrWhiteSpace(userDomain.CasUserEntity.OwnDepValue.ToString()))
                    {
                        DataTable depData = GetDepartmentInfo(userDomain.CasUserEntity.OwnDepValue.ToString());
                        userDomain.CasUserEntity.DeparmentCode = depData.Rows[0]["DEPT_CODE"].ToString();
                        userDomain.CasUserEntity.DeparmentName = depData.Rows[0]["DEPT_NAME"].ToString();
                    }
                    userDomain.CasUserEntity.UserCode = userDomain.CasUserEntity.UserId;
                    userDomain.CasUserEntity.Password = Encryption.Encrypt(eContract.Common.ConfigHelper.GetSetString("DefaultPassword"));
                    if (Insert(userDomain.CasUserEntity))
                    {
                        WebCaching.UserCaching = null;
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
        /// 获得部门信息
        /// </summary>
        /// <param name="DepId"></param>
        /// <returns>返回部门信息的dataTable</returns>
        public DataTable GetDepartmentInfo(string DepId)
        {
            var strSql = new StringBuilder();
            strSql.AppendFormat("SELECT * FROM CAS_DEPARTMENT WHERE DEPT_ID='{0}'", DepId);
            DataTable depData = DataAccess.SelectDataSet(strSql.ToString()).Tables[0];
            return depData;
        }

        public virtual bool SaveUserPermissionAdd(string UserId, string strUserDeptIds, ref string strError)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                return false;
            }

            string[] arr = strUserDeptIds.Split(new char[] { ',' });
            List<CasUserPermissionEntity> lst = new List<CasUserPermissionEntity>();
            if (arr != null && arr.Length > 0)
            {
                foreach (var item in arr)
                {
                    lst.Add(new CasUserPermissionEntity
                    {
                        DeptId = item,
                        PermissionId = Guid.NewGuid().ToString(),
                        UserId = UserId,
                        IsDeleted = false,
                        CreatedBy = WebCaching.UserId,
                        CreateTime = DateTime.Now,
                        LastModifiedBy = WebCaching.UserId,
                        LastModifiedTime = DateTime.Now
                    });
                }
            }
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                try
                {
                    DataAccess.ExcuteNoneQuery("delete CAS_USER_PERMISSION where USER_ID='" + UserId + "' ", broker);
                    if (lst != null && lst.Count > 0)
                    {
                        DataAccess.Insert<CasUserPermissionEntity>(lst, broker);
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

        public virtual bool SaveRoleAdd(string UserId, string strRoleIds, string systemName, ref string strError)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                return false;
            }

            string[] arr = strRoleIds.Split(new char[] { ',' });
            List<SecUserRoleEntity> lst = new List<SecUserRoleEntity>();
            if (arr != null && arr.Length > 0)
            {
                foreach (var item in arr)
                {
                    lst.Add(new SecUserRoleEntity
                    {
                        RoleId = item,
                        UserRoleId = Guid.NewGuid().ToString(),
                        UserId = UserId,
                        SystemName = systemName,
                        LastModifiedBy = WebCaching.UserId,
                        LastModifiedTime = DateTime.Now
                    });
                }
            }
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                try
                {
                    DataAccess.ExcuteNoneQuery("delete SEC_USER_ROLE where USER_ID='" + UserId + "' and SYSTEM_NAME='" + systemName + "' ", broker);
                    if (lst != null && lst.Count > 0)
                    {
                        DataAccess.Insert<SecUserRoleEntity>(lst, broker);
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

        public JqGrid ForGrid(JqGrid grid)
        {
            QueryUser query = new QueryUser();
            query.name = grid.QueryField.ContainsKey("CHINESE_NAME") ? grid.QueryField["CHINESE_NAME"] : "";
            grid.QueryField.Remove("CHINESE_NAME");
            grid = QueryTableHelper.QueryGrid<CasUserEntity>(query, grid);
            return grid;
        }
        public JqGrid GetAllLineManagers(string userId, JqGrid grid)
        {
            QueryManager query = new QueryManager();
            query.userId = userId;
            DataTable dtResult = DataAccess.Select<CasUserEntity>(query.ParseSQL().SQLString).ToDataTable();

            //去掉所有Department Head

            //获取所有Department Head
            DepartmentBLL departmentBLL = new DepartmentBLL();
            List<CasUserEntity> listDepartmentManagers = departmentBLL.GetAllDepartmentManagers();

            //LineManager列表中去掉自己
            if (dtResult.Rows.Count > 0)
            {
                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    if (dtResult.Rows[i]["USER_ID"].ToString() == userId)
                    {
                        dtResult.Rows.RemoveAt(i);
                        break;
                    }
                }
            }
            //LineManager列表中去掉Department Head，即为最终的LineManager列表
            if (dtResult.Rows.Count > 0)
            {
                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    if (listDepartmentManagers.Where(p => p.UserId == dtResult.Rows[i]["USER_ID"].ToString()).Count() > 0)
                    {
                        dtResult.Rows.RemoveAt(i);
                        i--;
                    }
                }
            }

            grid.DataBind(dtResult, dtResult.Rows.Count);
            return grid;
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public bool ChangePassword(string userId, ref string strError)
        {
            if (string.IsNullOrEmpty(userId))
            {
                strError = "用户名为空，无法修改密码！";
                return false;
            }
            CasUserEntity entity = SystemService.UserService.GetUserEntity(userId);
            if (entity != null)
            {
                entity.Password = Encryption.Encrypt(eContract.Common.ConfigHelper.GetSetString("DefaultPassword"));
                SystemService.UserService.Update(entity);
            }
            return true;
        }

        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public LigerGrid UserRoleGrid(string systemName, LigerGrid grid)
        {
            QueryUser query = new QueryUser();
            query.ligerGrid = grid;
            query.systemName = systemName;
            grid = QueryTableHelper.QueryTable<CasUserEntity>(query.GetUserRoleSQL(), grid);
            return grid;
        }

        public string GetUserRolesByUser(string systemName, string userId)
        {
            WhereBuilder wb = new WhereBuilder($"SELECT * FROM SEC_USER_ROLE WHERE USER_ID = '{userId}' AND SYSTEM_NAME = '{systemName}'");
            List<SecUserRoleEntity> list = DataAccess.Select<SecUserRoleEntity>(wb);
            string userRoleId = list.Aggregate(string.Empty, (current, secUserRoleEntity) => current + (secUserRoleEntity.RoleId + ","));
            if (!string.IsNullOrEmpty(userRoleId))
                userRoleId = userRoleId.Substring(0, userRoleId.Length - 1);
            return userRoleId;
        }



        public virtual bool ExportExcel(LigerGrid grid, ref ExcelConvertHelper.ExcelContext context, ref string Error)
        {
            QueryUser query = new QueryUser();
            query.ligerGrid = grid;
            eContract.Common.ExcelConvertHelper.ColumnList columns = new eContract.Common.ExcelConvertHelper.ColumnList();
            columns.Add(CasUserTable.C_USER_ACCOUNT, "登录名");
            columns.Add(CasUserTable.C_ENGLISH_NAME, "姓名");
            context = new ExcelConvertHelper.ExcelContext();
            context.FileName = "UserList" + ".xls";
            context.Title = "用户列表";
            //context.Data = DataAccess.QueryDataSet(query); 
            WhereBuilder where = query.ParseSQL();
            context.Data = DataAccess.Select(where.SQLString, where.Parameters).Tables[0];
            if (columns != null)
            {
                context.Columns.Add(columns);
            }
            CacheHelper.Instance.Set(WebCaching.UserId + "_" + ExcelHelper.EXPORT_EXCEL_CONTEXT, context);
            return true;
        }

        public virtual void Logout()
        {

            CookieHelper.SetCookie("auth_comix_name", "", DateTime.Now.AddSeconds(-1));
            CookieHelper.SetCookie("auth_comix_auto", "0", DateTime.Now.AddSeconds(-1));
            CookieHelper.SetCookie("auth_comix_token", "", DateTime.Now.AddSeconds(-1));
            FormsAuthentication.SignOut();

        }

        public virtual int SetPassword(string userAccount, byte[] encPassword)
        {
            DataAccessParameterCollection param = new DataAccessParameterCollection();

            ColumnInfo columnInfo = new ColumnInfo("UserAccount", "UserAccount", false, typeof(string));
            param.AddWithValue(columnInfo, userAccount);

             columnInfo = new ColumnInfo("EncryptedPassword", "EncryptedPassword", false, typeof(string));
            param.AddWithValue(columnInfo, encPassword);

            var num = DataAccess.ExecuteStoreProcedureHasReturn("sp_Sec_User_SetPassword", param);

            return num;
        }

        /// <summary>
        /// 生成token
        /// </summary>
        /// <param name="sortParams"></param>
        /// <returns></returns>
        public static string GetToken(SortedDictionary<string, string> sortParams)
        {
            string strToken = "";
            foreach (var item in sortParams)
            {
                if (!string.IsNullOrEmpty(item.Value))
                {
                    if (!item.Key.ToLower().Equals("sign") && !item.Key.ToLower().Equals("token"))
                    {
                        if (!string.IsNullOrEmpty(strToken))
                        {
                            strToken += "&";
                        }
                        strToken += item.Key + "=" + item.Value;
                    }
                }

            }
            return GetMD5(strToken);
        }
        public static string GetMD5(string s)
        {
            byte[] buffer = new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding("utf-8").GetBytes(s + ComixSDK.EDI.Utils.ConfigHelper.GetConfigString("System_MD5Key")));
            StringBuilder builder = new StringBuilder(0x20);
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.Append(buffer[i].ToString("x").PadLeft(2, '0'));
            }
            return builder.ToString();
        }

        public string GetCityName(CasUserEntity userEntity)
        {
            DataAccessBroker broker = DataAccessFactory.Instance();
            string sql = "SELECT CITY_NAME FROM CAS_CITY WHERE CITY_CODE = '" + userEntity.CityCode + "'";
            object result = broker.ExecuteSQLScalar(sql);
            return result == null ? "" : result.ToString();
        }
        public string GetRegionName(CasUserEntity userEntity)
        {
            DataAccessBroker broker = DataAccessFactory.Instance();
            string sql = "SELECT CAS_REGION.REGION_NAME FROM CAS_CITY INNER JOIN CAS_REGION ON CAS_CITY.REGION_ID =  CAS_REGION.REGION_ID WHERE CAS_CITY.CITY_CODE = '" + userEntity.CityCode + "'";
            object result = broker.ExecuteSQLScalar(sql);
            return result == null ? "" : result.ToString();
        }

        public List<CasUserEntity> GetAllContractManagers()
        {
            string sql = $@"SELECT CAS_USER.* 
                              FROM SEC_ROLE 
                        INNER JOIN SEC_USER_ROLE
                                ON SEC_ROLE.ROLE_ID = SEC_USER_ROLE.ROLE_ID
                        INNER JOIN CAS_USER
                                ON SEC_USER_ROLE.USER_ID = CAS_USER.USER_ID
                             WHERE ROLE_NAME = N'合同管理员'";
            return DataAccess.Select<CasUserEntity>(sql);
        }
    }
    [Serializable]
    class ResponseLoginInfoModel
    {
        public string code { get; set; }
        public string msg { get; set; }
        public AppUsersModel userinfo { get; set; }
    }

    [Serializable]
    class AppUsersModel
    {
        public int UserID { set; get; }
        public string UserName { set; get; }
        public string NickName { set; get; }
        public string TrueName { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public int Points { set; get; }
        public int EnterpriseID { set; get; }
        public string EnterpriseName { set; get; }
        public int ParentUserID { get; set; }
        public string ParentUserName { get; set; }
        public ParentUserModel ParentUser { get; set; }
    }

    [Serializable]
    class ParentUserModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
    }
}
