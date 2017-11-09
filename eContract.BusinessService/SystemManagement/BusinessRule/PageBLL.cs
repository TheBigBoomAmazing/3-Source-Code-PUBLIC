using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Suzsoft.Smart.EntityCore;
using eContract.Common.Schema;
using eContract.Common.Entity;
using eContract.BusinessService.Common;
using Suzsoft.Smart.Data;
using eContract.Common.Const;
using eContract.Common;
using eContract.BusinessService.SystemManagement.Domain;
using eContract.Common.WebUtils;
using eContract.Common.MVC;
using eContract.BusinessService.SystemManagement.CommonQuery;
using eContract.BusinessService.SystemManagement.BusinessRule;

namespace eContract.BusinessService.SystemManagement
{
    public class PageBLL : BusinessBase
    {
        /// <summary>
        /// 创建页面领域对象
        /// </summary>
        /// <returns></returns>
        public virtual PageDomain CreatePageDomain(string systemName)
        {
            return new PageDomain(
                new SecPageEntity
                {
                    PageId = string.Empty,
                    PageName = string.Empty,
                    PageUrl = string.Empty,
                    ParentId = string.Empty,
                    Remark = string.Empty,
                    SystemName = systemName,
                    IsMenu = false,
                    LastModifiedBy = string.Empty,
                    LastModifiedTime = DateTime.Now,
                    MenuLevel = 0,
                    MenuOrder = 0 
                });
        }

        /// <summary>
        /// 根据Id获取Domain
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual PageDomain GetDomainById(string id, string systemName)
        {
            List<SecPageEntity> list = GetAllPage(systemName);
            foreach (SecPageEntity item in list)
            {
                if (item.PageId == id)
                {
                    return new PageDomain(item);
                }
            }
            return null;
        }
        /// <summary>
        /// 根据Id获取子页面
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        internal virtual List<SecPageEntity> GetChildsByParentId(string pId,string systemName)
        {
            List<SecPageEntity> list = GetAllPage(systemName);
            List<SecPageEntity> resultList = new List<SecPageEntity>();
            foreach (SecPageEntity item in list)
            {
                if (item.ParentId.ToLower() == pId.ToLower())
                {
                    resultList.Add(item);
                }
            }
            return resultList;
        }

        /// <summary>
        /// 根据Id,moduleCode获取子页面
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public List<PageDomain> GetDomainChildsByParentId(string pId,string systemName)
        {
            List<SecPageEntity> list = GetAllPage(systemName);
            List<PageDomain> resultList = new List<PageDomain>();
            foreach (SecPageEntity item in list)
            {
                if (item.ParentId == pId)
                {
                    resultList.Add(new PageDomain(item));
                }
            }
            return resultList;
        }

        /// <summary>
        /// 新增页面
        /// </summary>
        /// <returns></returns>
        public virtual bool InsertPageDomain(PageDomain pageDomain)
        {
            pageDomain.SecPageEntity.PageId = Guid.NewGuid().ToString();
            pageDomain.SecPageEntity.LastModifiedTime = DateTime.Now;

            if (Insert(pageDomain.SecPageEntity))
            {
                WebCaching.PageCaching = null;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <returns></returns>
        public virtual bool UpdatePageDomain(PageDomain pageDomain)
        {
            pageDomain.SecPageEntity.LastModifiedTime = DateTime.Now;
            if (Update(pageDomain.SecPageEntity))
            {
                WebCaching.PageCaching = null;
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual bool Save(PageDomain pageDomain,ref string strError)
        {
            pageDomain.SecPageEntity.LastModifiedTime = DateTime.Now;
            pageDomain.SecPageEntity.LastModifiedBy = WebCaching.UserId;
            pageDomain.SecPageEntity.CreatedBy= WebCaching.UserId;
            pageDomain.SecPageEntity.CreateTime = DateTime.Now;
            if (pageDomain.SecPageEntity.IsMenu == null)
            {
                pageDomain.SecPageEntity.IsMenu = false;
            }
            try
            {
                if (!string.IsNullOrEmpty(pageDomain.SecPageEntity.PageId))
                {
                    if (Update(pageDomain.SecPageEntity))
                    {
                        WebCaching.PageCaching = null;
                        return true;
                    }
                }
                else
                {
                    pageDomain.SecPageEntity.PageId = Guid.NewGuid().ToString();
                    if (Insert(pageDomain.SecPageEntity))
                    {
                        WebCaching.PageCaching = null;
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
        public virtual bool Save(SecPageEntity entity)
        {
            entity.LastModifiedTime = DateTime.Now;
            if (Update(entity))
            {
                WebCaching.PageCaching = null;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除单个实体
        /// </summary>
        /// <returns></returns>
        public virtual bool DeletePageDomain(PageDomain pageDomain)
        {
            if (Delete(pageDomain.SecPageEntity))
            {
                WebCaching.PageCaching = null;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除批量
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public virtual bool DeletePageDomainByIds(List<string> list)
        {
            if (Delete<SecPageEntity>(list))
            {
                WebCaching.PageCaching = null;
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除批量
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public virtual bool DeletePageDomainByIds(string deletekeys,ref string strError)
        {
            List<string> list = deletekeys.Split(new char[] { ';', ',' }).ToList<string>();
            try
            {
                return DeletePageDomainByIds(list);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            return false;
        }

        /// <summary>
        /// 从缓存中获取页 或 更新缓存中页
        /// 已使用 WebCaching.PageCaching
        /// </summary>
        /// <returns></returns>
        public virtual List<SecPageEntity> GetAllPage(string systemName)
        {
            if (null == WebCaching.PageCaching)
            {
                WebCaching.PageCaching = this.SelectByCondition<SecPageEntity>(eContract.Common.Utils.QueryCondition.Create().Equals(SecPageTable.C_SYSTEM_NAME,systemName).Order(SecPageTable.C_MENU_LEVEL, SecPageTable.C_MENU_ORDER, SecPageTable.C_PAGE_NAME));
            }
            return WebCaching.PageCaching as List<SecPageEntity>;
        }
        /// <summary>
        /// 从缓存中获取页 或 更新缓存中页
        /// 已使用 WebCaching.PageCaching
        /// </summary>
        /// <returns></returns>
        public virtual List<SecPageEntity> GetParentByPageList(string parentId)
        {
            return this.SelectByCondition<SecPageEntity>(eContract.Common.Utils.QueryCondition.Create().Equals(SecPageTable.C_PARENT_ID, parentId).Order(SecPageTable.C_MENU_LEVEL, SecPageTable.C_MENU_ORDER, SecPageTable.C_PAGE_NAME));
        }

        public virtual List<SecPageEntity> GetPageList(string systemName)
        {
            List<SecPageEntity> listPage = new List<SecPageEntity>();
            SecPageEntity entity = new SecPageEntity();
            entity.MenuLevel = 1;
            entity.SystemName = systemName;
            listPage.AddRange(DataAccess.Select(entity));
            entity.MenuLevel = 2;
            entity.SystemName = systemName;
            listPage.AddRange(DataAccess.Select(entity));
            listPage.OrderBy(e => e.MenuLevel);
            return listPage;
        }
        ///// <summary>
        ///// 根据navChartId获取导航图数据
        ///// </summary>
        ///// <param name="navChartId"></param>
        ///// <returns></returns>
        //public virtual List<SecNavChartDetailEntity> GetNavDetailsByChartId(string navChartId)
        //{
        //    SecNavChartDetailEntity ety = new SecNavChartDetailEntity();
        //    ety.NavChartId = navChartId;
        //    return DataAccess.Select<SecNavChartDetailEntity>(ety);
        //}


        public virtual List<SecPageControlEntity> GetAllPageControls()
        {
            return DataAccess.SelectAll<SecPageControlEntity>();
        }

        public virtual List<SecPageControlEntity> GetPageControls(string pageId, int pageType)
        {
            SecPageControlEntity ety = new SecPageControlEntity { PageId = pageId, PageType = pageType.ToString()};
            return DataAccess.Select<SecPageControlEntity>(ety);
        }

        public virtual Dictionary<string, List<SecPageControlEntity>> GetPageControls(int pageType)
        {
            List<SecPageControlEntity> all = GetAllPageControls();
            Dictionary<string, List<SecPageControlEntity>> dict = new Dictionary<string, List<SecPageControlEntity>>();
            foreach (SecPageControlEntity pc in all)
            {
                if (pc.PageType == pageType.ToString())
                {
                    if (!dict.ContainsKey(pc.PageId))
                    {
                        dict.Add(pc.PageId, new List<SecPageControlEntity>());
                    }
                    dict[pc.PageId].Add(pc);
                }
            }
            return dict;
        }

        /// <summary>
        /// 获取角色有权限的控件
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public virtual List<string> GetRoleControls(string roleId)
        {
            List<string> list = new List<string>();
            DataTable dt = DataAccess.SelectDataSet("select * from SEC_ROLE_CONTROL where Role_Id='"+roleId+"'").Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
               list.Add( dr["CONTROL_ID"].ToString());
            }
            return list;
        }

        public bool UpdatePageControls(string pageID, int pageType, List<SecPageControlEntity> list)
        {
           List<SecPageControlEntity> oldPageControls = GetPageControls(pageID, pageType);
           List<SecPageControlEntity> insertList = new List<SecPageControlEntity>();
           List<SecPageControlEntity> updateList = new List<SecPageControlEntity>();
           List<SecPageControlEntity> deleteList = new List<SecPageControlEntity>();
           foreach (var item1 in oldPageControls)
           {
               bool find =false;
               foreach (var item2 in list)
               {
                   if (item2.ControlId == item1.ControlId)
                   { 
                       find = true;
                       if (item1.ControlName != item2.ControlName || item1.ServerId != item2.ServerId)
                       {
                           updateList.Add(item2);
                       }
                   }
               }
               if(!find)
               {
                   deleteList.Add(item1);
               }
           }
           foreach (var item2 in list)
           {
               if (string.IsNullOrEmpty(item2.ControlId))
               {
                   item2.ControlId = Guid.NewGuid().ToString();
                   insertList.Add(item2); 
               }
           }
           using (DataAccessBroker broker = DataAccessFactory.Instance())
           {
               try
               {
                   broker.BeginTransaction();
                   DataAccess.Update(updateList, broker);
                   DataAccess.Insert(insertList, broker);
                   foreach (var item in deleteList)
                   {
                       broker.ExecuteSQL("DELETE FROM [SEC_ROLE_CONTROL] WHERE [CONTROL_ID]='"+item.ControlId+"' and [PAGE_TYPE]="+pageType);
                   }
                   DataAccess.Delete(deleteList, broker);
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

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="grid">列表</param>
        /// <returns></returns>
        public JqGrid ForGrid(string systemName, JqGrid grid)
        {
            SecPageQuery query = new SecPageQuery();
            //query.keyWord = grid.keyWord;
            query.systemName = systemName;
            grid = QueryTableHelper.QueryGrid<SecPageEntity>(query, grid);   
            return grid;
        }

        /// <summary>
        /// 获取菜单树
        /// 已使用 WebCaching.PageCaching
        /// </summary>
        /// <returns></returns>
        public virtual string GetAllPageTree(string roleid, string systemName)
        {
            List<SecPageEntity> lstPage = this.GetAllPage(systemName);
            List<Dictionary<string, object>> lstDic = new List<Dictionary<string, object>>();
            RolePageBLL rolepage = new RolePageBLL();
            List<SecRolePageEntity> lstRolePage= rolepage.GetPagesByRoleId(roleid);
            foreach (var item in lstPage)
            {
                bool flag = false;
                if (lstRolePage != null && lstRolePage.Count > 0)
                {
                    var entity = lstRolePage.Where(x => x.PageId == item.PageId).FirstOrDefault();
                    flag = (entity != null);
                }
                if (item.DataCollection.ContainsKey("ischecked"))
                    item.DataCollection["ischecked"]= flag;
                else
                item.DataCollection.Add("ischecked", flag);
                lstDic.Add(item.DataCollection);
            }
            return ComixSDK.EDI.Utils.JSONHelper.ToJson(lstDic) ;
        }

        /// <summary>
        /// 从缓存中获取页 或 更新缓存中页
        /// 已使用 WebCaching.PageCaching
        /// </summary>
        /// <returns></returns>
        public virtual List<SecPageEntity> GetAllPageV2(string systemName)
        {
            var obj=ComixSDK.EDI.Utils.CacheHelper.Instance.Get<List<SecPageEntity>>("SecPageEntity_"+systemName);
            if(obj==null)
            {
                obj = this.SelectByCondition<SecPageEntity>(eContract.Common.Utils.QueryCondition.Create().Equals(SecPageTable.C_SYSTEM_NAME, systemName).Order(SecPageTable.C_MENU_LEVEL, SecPageTable.C_MENU_ORDER, SecPageTable.C_PAGE_NAME));
                if(obj!=null)
                {
                    ComixSDK.EDI.Utils.CacheHelper.Instance.Set("SecPageEntity_"+systemName, obj);
                }
            }
            return obj;
        }

        public string GetPortalTree(string id, string systemName)
        {
            StringBuilder strTree = new StringBuilder();
            // List<SysPageModels> models = new List<SysPageModels>();
            List<SecPageEntity> lstPage = this.GetChildsByParentId(id,systemName);
            if ((lstPage != null && lstPage.Count > 0))
            {
                strTree.Append("<ul>");
                if (lstPage != null && lstPage.Count > 0)
                {
                    foreach (var item in lstPage)
                    {
                        string strHtml = GetPortalTree(item.PageId, systemName);
                        strTree.Append("<li id='" + item.PageId + "' data-children='" + (!string.IsNullOrEmpty(strHtml) ? "1" : "0") + "' data-jstree='{\"id\":\"" + item.PageId + "}'>");
                        strTree.Append(item.PageName);
                        strTree.Append(strHtml);
                        strTree.Append("</li>");
                    }
                }
                strTree.Append("</ul>");
            }

            return strTree.ToString();
        }
        public virtual List<string> GetPortalRoleTree(string roleid, string systemName)
        {
            List<string> lstResult = new List<string>();
            RolePageBLL rolepage = new RolePageBLL();
            List<SecRolePageEntity> lstRolePage = rolepage.GetPagesByRoleId(roleid);
            foreach (var item in lstRolePage)
            {
                lstResult.Add(item.PageId);
            }
            return lstResult;
        }
    }
}
