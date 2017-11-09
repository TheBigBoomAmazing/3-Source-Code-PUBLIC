using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;
using eContract.BusinessService.Common;
using System.Data;
using eContract.DataAccessLayer.SystemManagement;
using eContract.BusinessService.SystemManagement.Service;
using eContract.Common.Entity;
using eContract.BusinessService.SystemManagement.Domain;
using Suzsoft.Smart.Data;
using eContract.Common;
using eContract.Common.WebUtils;
using eContract.BusinessService.SystemManagement.CommonQuery;
using eContract.Common.MVC;
using System.Web.UI.WebControls;
using eContract.Common.Const;
using ComixSDK.EDI.Utils;

namespace eContract.BusinessService.SystemManagement.BusinessRule
{
    public class FunctionRoleBLL : BusinessBase
    {
        /// <summary>
        /// 创建领域对象
        /// </summary>
        /// <returns></returns>
        public virtual FunctionRoleDomain CreateFunctionRoleDomain(string systemName)
        {
            SecRoleEntity roleEty = new SecRoleEntity();
            //roleEty.RoleId = Guid.NewGuid().ToString();
            roleEty.RoleName = string.Empty;
            roleEty.SystemName = systemName;
            roleEty.Remark = string.Empty;
            roleEty.LastModifiedBy = string.Empty;
            roleEty.LastModifiedTime = DateTime.Now;
            return new FunctionRoleDomain(roleEty);
        }

        /// <summary>
        /// 获取用户Domain
        /// </summary>
        /// <param name="delUserId"></param>
        /// <returns></returns>
        public virtual FunctionRoleDomain GetFunctionRoleDomainByRoleID(string roleId)
        {
            SecRoleEntity roleEty = GetById<SecRoleEntity>(roleId);
            if (roleEty == null)
                return null;
            return new FunctionRoleDomain(roleEty);
        }



        /// <summary>
        /// 获取functionRoleDomain
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        protected virtual List<SecUserRoleEntity> GetFunctionRoleUserDomainByRoleID(string roleID)
        {
            eContract.Common.Utils.QueryCondition qCondition = eContract.Common.Utils.QueryCondition.Create().Equals(SecUserRoleTable.C_ROLE_ID, roleID);
            return SelectByCondition<SecUserRoleEntity>(qCondition);
        }

        /// <summary>
        /// 获取functionRolePageDomain
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        protected virtual List<SecRolePageEntity> GetFunctionRolePageDomainByRoleID(string roleID)
        {
            eContract.Common.Utils.QueryCondition qCondition = eContract.Common.Utils.QueryCondition.Create().Equals(SecRolePageTable.C_ROLE_ID, roleID);
            return SelectByCondition<SecRolePageEntity>(qCondition);

        }



        /// <summary>
        /// 验证角色名称
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public virtual bool CheckSystemRoleNameUpdate(string roleName, string roleID)
        {

            eContract.Common.Utils.QueryCondition qCondition = eContract.Common.Utils.QueryCondition.Create().NotEquals(SecRoleTable.C_ROLE_ID, roleID).Equals(SecRoleTable.C_ROLE_NAME, roleName);
            List<SecRoleEntity> list = SelectByCondition<SecRoleEntity>(qCondition);
            if (list.Count != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 获取所有的角色
        /// </summary>
        /// <returns></returns>
        public virtual List<SecRoleEntity> GetAllRoles()
        {
            return DataAccess.SelectAll<SecRoleEntity>(SecRoleTable.C_ROLE_NAME);
        }

        /// <summary>
        /// 验证角色名称Insert
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public virtual bool CheckSystemRoleNameInsert(string roleName)
        {

            eContract.Common.Utils.QueryCondition qCondition = eContract.Common.Utils.QueryCondition.Create().Equals(SecRoleTable.C_ROLE_NAME, roleName);
            List<SecRoleEntity> list = SelectByCondition<SecRoleEntity>(qCondition);
            if (list.Count != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// 判断roleID,userID关联状态
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public virtual bool CheckRoleIdUseruID(string roleID, string userID)
        {
            eContract.Common.Utils.QueryCondition qCondition = eContract.Common.Utils.QueryCondition.Create().Equals(SecUserRoleTable.C_ROLE_ID, roleID).Equals(SecUserRoleTable.C_USER_ID, userID);
            List<SecUserRoleEntity> list = SelectByCondition<SecUserRoleEntity>(qCondition);
            if (list.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="userDomain"></param>
        /// <returns></returns>
        public virtual bool InsertFunctionRoleDomain(FunctionRoleDomain functionRoleDomain)
        {
            return Insert(functionRoleDomain.SecRoleEntity);
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="SecRoleEntity"></param>
        /// <returns></returns>
        public virtual bool UpdateFunctionRoleDomain(FunctionRoleDomain functionRoleDomain)
        {
            return Update(functionRoleDomain.SecRoleEntity);
        }

        /// <summary>
        /// 操作RoleUser提交功能,先删除,后插入
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public virtual bool SaveFunctionRoleUserDomain(List<SecUserRoleEntity> DeleteDic, List<SecUserRoleEntity> InsertDic)
        {
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                try
                {
                    broker.BeginTransaction();
                    DataAccess.Delete<SecUserRoleEntity>(DeleteDic, broker);
                    DataAccess.Insert<SecUserRoleEntity>(InsertDic, broker);
                    broker.Commit();
                    return true;
                }
                catch
                {
                    broker.RollBack();
                    return false;
                }
            }
        }

        public virtual bool Save(FunctionRoleDomain functionRoleDomain, ref string strError, string SystemName = "BSS")
        {
            functionRoleDomain.SecRoleEntity.LastModifiedTime = DateTime.Now;
            functionRoleDomain.SecRoleEntity.LastModifiedBy = WebCaching.UserId;
            try
            {
                //var entity = GetById<SecRoleEntity>(functionRoleDomain.SecRoleEntity.RoleId);
                if (!string.IsNullOrEmpty(functionRoleDomain.SecRoleEntity.RoleId))
                {
                    string sql = $@"SELECT * 
                                  FROM SEC_ROLE 
                                 WHERE ROLE_NAME = N'{functionRoleDomain.SecRoleEntity.RoleName}'
                                   AND ROLE_ID <> '{functionRoleDomain.SecRoleEntity.RoleId}'";
                    if (DataAccess.Select<SecRoleEntity>(sql).Count > 0)
                    {
                        strError = "角色名称已存在";
                        return false;
                    }

                    functionRoleDomain.SecRoleEntity.SystemName = SystemName;
                    if (Update(functionRoleDomain.SecRoleEntity))
                    {
                        return true;
                    }
                }
                else
                {
                    string sql = $@"SELECT * 
                                  FROM SEC_ROLE 
                                 WHERE ROLE_NAME = N'{functionRoleDomain.SecRoleEntity.RoleName}'";
                    if (DataAccess.Select<SecRoleEntity>(sql).Count > 0)
                    {
                        strError = "角色名称已存在";
                        return false;
                    }

                    functionRoleDomain.SecRoleEntity.RoleId = Guid.NewGuid().ToString();
                    if (Insert(functionRoleDomain.SecRoleEntity))
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                //strError = ex.Message;
                strError = "Update failed";
            }
            return false;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="parameterIds"></param>
        /// <returns></returns>
        public virtual bool DeleteRoleDomain(List<string> RoleIds)
        {
            using (DataAccessBroker broker = DataAccessFactory.Instance())
            {
                try
                {
                    List<SecUserRoleEntity> lUserRole;
                    List<SecRolePageEntity> IRoleMunu;
                    //List<SecRolePermissionEntity> IRolePermis;
                    List<SecRoleEntity> IRoleEntity = new List<SecRoleEntity>();
                    IRoleMunu = new List<SecRolePageEntity>();
                    //IRolePermis = new List<SecRolePermissionEntity>();
                    lUserRole = new List<SecUserRoleEntity>();
                    foreach (string id in RoleIds)
                    {
                        lUserRole.AddRange(GetFunctionRoleUserDomainByRoleID(id));
                        IRoleMunu.AddRange(GetFunctionRolePageDomainByRoleID(id));
                        //IRolePermis.AddRange(GetFunctionRolePermissionDomainByRoleID(id));
                        IRoleEntity.Add(new SecRoleEntity { RoleId = id });
                    }
                    broker.BeginTransaction();
                    DataAccess.Delete<SecUserRoleEntity>(lUserRole, broker);
                    DataAccess.Delete<SecRolePageEntity>(IRoleMunu, broker);
                    //DataAccess.Delete<SecRolePermissionEntity>(IRolePermis, broker);
                    DataAccess.Delete<SecRoleEntity>(IRoleEntity, broker);
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

        public virtual bool DeleteRoleDomainByIds(string deletekeys, ref string strError)
        {
            List<string> list = deletekeys.Split(new char[] { ';', ',' }).ToList<string>();
            try
            {
                return DeleteRoleDomain(list);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            return false;
        }

        /// <summary>
        /// 根据用户ID获取该用户所有功能角色列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual List<FunctionRoleDomain> GetFunctionRoleDomainListByUserId(string systemName, string userId)
        {
            List<FunctionRoleDomain> result = new List<FunctionRoleDomain>();
            List<SecRoleEntity> list = UserDA.GetFunctionRoleEntityListByUserId(systemName, userId);
            foreach (SecRoleEntity item in list)
            {
                result.Add(new FunctionRoleDomain(item));
            }
            return result;
        }

        #region 根据userId获取该用户拥有的权限树
        public virtual List<MenuDataItem> GetMenuDataItemByUserId(string systemName, string userId)
        {
            List<MenuDataItem> menuDataItemList = new List<MenuDataItem>();
            SecPageEntity SecPageEntity = new SecPageEntity();
            SecPageEntity.PageId = eContract.Common.Const.SysConst.C_Root_Parent_ID;
            SecPageEntity.SystemName = systemName;
            PageDomain baseDomain = new PageDomain(SecPageEntity);
            Dictionary<string, List<SecPageControlEntity>> allPageControls =
                SystemService.PageService.GetPageControls(0);
            foreach (PageDomain pageDomain in baseDomain.ChildsDomain)
            {
                if (GetPageIds(userId, systemName).Contains(pageDomain.SecPageEntity.PageId))
                {
                    MenuDataItem rootMenuDataItem = new MenuDataItem(pageDomain.SecPageEntity, null, null);
                    Bind(systemName, rootMenuDataItem, pageDomain, userId, allPageControls);
                    menuDataItemList.Add(rootMenuDataItem);
                }
            }
            return menuDataItemList;
        }

        public virtual List<MenuDataItem> GetFavoriteMenuByUserId(string userId, int sysType)
        {
            List<SecPageEntity> favoritePage = SystemService.UserService.GetFavoritePage(userId, sysType);
            List<MenuDataItem> menuDataItemList = new List<MenuDataItem>();
            Dictionary<string, List<SecPageControlEntity>> allPageControls =
                SystemService.PageService.GetPageControls(0);
            foreach (SecPageEntity pageEntity in favoritePage)
            {
                List<SecPageControlEntity> controls = null;
                if (allPageControls.ContainsKey(pageEntity.PageId))
                {
                    controls = allPageControls[pageEntity.PageId];
                }
                menuDataItemList.Add(new MenuDataItem(pageEntity, null, controls));
            }

            return menuDataItemList;
        }

        private void Bind(string systemName, MenuDataItem rootMenuDataItem, PageDomain pageDomain, string userId, Dictionary<string, List<SecPageControlEntity>> allPageControls)
        {
            foreach (PageDomain page in pageDomain.ChildsDomain)
            {
                if (GetPageIds(userId, systemName).Contains(page.SecPageEntity.PageId))
                {
                    List<SecPageControlEntity> controls = null;
                    if (allPageControls.ContainsKey(page.SecPageEntity.PageId))
                    {
                        controls = allPageControls[page.SecPageEntity.PageId];
                    }
                    MenuDataItem childMenuDataItem = new MenuDataItem(page.SecPageEntity, rootMenuDataItem, controls);
                    //foreach (PermissionDomain permissionDomain in page.PermissionDomainList)
                    //{
                    //    if (GetPermissionIds(userId).Contains(permissionDomain.SecPermissionEntity.PermissionId))
                    //    {
                    //        childMenuDataItem.ValidControls.Add(permissionDomain.SecPermissionEntity);
                    //    }
                    //}
                    Bind(systemName, childMenuDataItem, page, userId, allPageControls);
                    rootMenuDataItem.SubMenu.Add(childMenuDataItem);
                }
            }
        }

        private List<string> GetPageIds(string userId, string systemName)
        {
            List<SecRolePageEntity> roleMenuEntityList = new List<SecRolePageEntity>();
            roleMenuEntityList = GetRoleMenuEntityListByUserId(systemName, userId);
            //普通员工绑定默认页面
            if (roleMenuEntityList.Count == 0)
            {
                roleMenuEntityList = GetFunctionRolePageDomainByRoleID("cd3c9135-4446-45c5-b768-550abac4368d");
            }
            List<string> pageIds = new List<string>();
            foreach (SecRolePageEntity SecRolePageEntity in roleMenuEntityList)
            {
                pageIds.Add(SecRolePageEntity.PageId);
            }
            return pageIds;
        }

        //private List<string> GetPermissionIds(string userId)
        //{
        //    List<SecRolePermissionEntity> roleSecPermissionEntityList = new List<SecRolePermissionEntity>();
        //    roleSecPermissionEntityList = GetRoleSecPermissionEntityListByUserId(userId);
        //    List<string> permissionIds = new List<string>();
        //    foreach (SecRolePermissionEntity roleSecPermissionEntity in roleSecPermissionEntityList)
        //    {
        //        permissionIds.Add(roleSecPermissionEntity.PermissionId);
        //    }
        //    return permissionIds;
        //}

        public virtual List<SecRolePageEntity> GetRoleMenuEntityListByUserId(string systemName, string userId)
        {
            List<SecRolePageEntity> roleMenuEntityList = new List<SecRolePageEntity>();
            roleMenuEntityList = UserDA.GetRoleMenuEntityByUserId(systemName, userId);
            return roleMenuEntityList;
        }

        //public static void BindRoleType(DropDownList ddl, string first)
        //{
        //    if (!string.IsNullOrEmpty(first))
        //        ddl.Items.Add(new ListItem(first, ""));
        //    ddl.Items.Add(new ListItem("总部", SysConst.C_role_zongbu));
        //    ddl.Items.Add(new ListItem("门店", SysConst.C_role_zhongduan));
        //    ddl.Items.Add(new ListItem("系统管理", SysConst.C_role_admin));
        //}
        #endregion

        #region 根据userId获取角色列表
        public virtual DataTable QueryRoleByUserId(string userId, string userType)
        {
            string sql = @"select a.role_id,a.role_name,a.role_type,case a.role_type when 0 then N'系统管理' when 1 then N'总部' else N'门店' end as role_type_name,b.USER_ID,a.remark from SEC_ROLE a 
                            left join (select * from SEC_USER_ROLE where USER_ID='" + userId + @"') b on a.ROLE_ID=b.ROLE_ID where 1=1 ";
            if (userType != "0")
            {
                sql += " and a.ROLE_TYPE='" + userType + "' ";
            }
            return DataAccess.SelectDataSet(sql).Tables[0];
        }
        #endregion
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="grid">列表</param>
        /// <returns></returns>
        public JqGrid ForGrid(string systemName, JqGrid grid)
        {
            SecRoleQuery query = new SecRoleQuery();
            //query.ligerGrid = grid;
            query.systemName = systemName;
            grid = QueryTableHelper.QueryGrid<SecRoleEntity>(query, grid);
            return grid;
        }

        public LigerGrid RoleAddGrid(string systemName, LigerGrid grid)
        {
            SecRoleQuery query = new SecRoleQuery();
            query.ligerGrid = grid;
            query.systemName = systemName;
            grid = QueryTableHelper.QueryTable<SecRoleEntity>(query.RoleAddSQL(), grid);
            return grid;
        }

        public virtual bool ExportExcel(LigerGrid grid, ref ExcelConvertHelper.ExcelContext context, ref string Error)
        {
            SecRoleQuery query = new SecRoleQuery();
            query.ligerGrid = grid;
            eContract.Common.ExcelConvertHelper.ColumnList columns = new eContract.Common.ExcelConvertHelper.ColumnList();
            columns.Add(SecRoleTable.C_ROLE_ID, "角色编号");
            columns.Add("role_type_name", "角色类型");
            columns.Add(SecRoleTable.C_ROLE_NAME, "角色名称");
            columns.Add(SecRoleTable.C_REMARK, "角色描述");
            context = new ExcelConvertHelper.ExcelContext();
            context.FileName = "RoleList" + ".xls";
            context.Title = "角色列表";
            WhereBuilder where = query.ParseSQL();
            context.Data = DataAccess.Select(where.SQLString, where.Parameters).Tables[0];
            if (columns != null)
            {
                context.Columns.Add(columns);
            }
            CacheHelper.Instance.Set(WebCaching.UserId + "_" + ExcelHelper.EXPORT_EXCEL_CONTEXT, context);
            return true;
        }
    }
}
