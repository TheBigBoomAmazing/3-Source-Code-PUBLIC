using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eContract.Common;
using eContract.BusinessService.SystemManagement.Service;
using eContract.Common.Entity;
using eContract.Common.WebUtils;
using eContract.Common.MVC;
using Newtonsoft.Json;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;
using Suzsoft.Smart.Data;

namespace eContract.Web.Areas.Admin.Controllers
{
    public class UserManagementController : AuthBaseController
    {
        public ActionResult Index(JqGrid grid, FormCollection data)
        {
            grid.ConvertParams(data);
            if (IsPost)
            {
                return Json(AjaxResult.Success(SystemService.UserService.ForGrid(grid)));
            }
            return View();
        }

        public ActionResult Edit(CasUserEntity entity, string id)
        {
            string strError = "";
            if (!IsPost)
            {
                entity = SystemService.UserService.CreateUserDomain("MDM").CasUserEntity;
                if (!string.IsNullOrEmpty(id))
                {
                    entity = SystemService.UserService.GetById<CasUserEntity>(id);
                    ViewBag.CityName = SystemService.UserService.GetCityName(entity);
                    ViewBag.RegionName = SystemService.UserService.GetRegionName(entity);
                    ViewBag.EditType = "0";
                }
                else
                {
                    ViewBag.EditType = "1";
                }
            }
            else
            {
                var domain = SystemService.UserService.CreateUserDomain("MDM");
                CasUserEntity oldEntity = SystemService.UserService.GetById<CasUserEntity>(id);
                domain.CasUserEntity = entity;
                if (SystemService.UserService.Save(domain, ref strError))
                {
                    WebCaching.IsAdmin = domain.CasUserEntity.IsAdmin.ToString();
                    WebCaching.UserCaching = null;
                    return Json(AjaxResult.Success());
                }
                else
                {
                    return Json(AjaxResult.Error("Update failed"));
                }
            }
            ViewBag.strError = strError;
            return View(entity);
        }

        public JsonResult Delete(string deleteKeys)
        {
            List<string> list = deleteKeys.Split(new char[] { ';', ',' }).ToList<string>();
            if (SystemService.UserService.DeleteUserById(list))
            {
                return Json(AjaxResult.Success());
            }
            return Json(AjaxResult.Error("删除用户失败"));
        }

        public JsonResult ChangePwd(string userId)
        {
            string strError = "";
            if (SystemService.UserService.ChangePassword(userId, ref strError))
            {
                WebCaching.UserCaching = null;
                return Json(AjaxResult.Success());
            }
            return Json(AjaxResult.Error(strError));
        }

        public ActionResult UserRoleEdit(JqGrid grid, FormCollection data, string id)
        {
            grid.ConvertParams(data);
            ViewBag.UserId = id;
            ViewBag.SelectUserRoleIds = SystemService.UserService.GetUserRolesByUser("MDM", id);
            if (IsPost)
            {
                return Json(AjaxResult.Success(SystemService.FunctionRoleService.ForGrid("MDM", grid)));
            }
            return View();
        }

        public ActionResult UserPermissionEdit(CasUserEntity entity, string id)
        {
            string strError = "";
            if (!IsPost)
            {
                entity = SystemService.UserService.GetById<CasUserEntity>(id); // SystemService.UserService.CreateUserDomain("MDM").CasUserEntity;
                if (!string.IsNullOrEmpty(id))
                {
                    ViewBag.EditType = "0";
                }
                else
                {
                    ViewBag.EditType = "1";
                }
            }
            else
            {
                
                if (SystemService.UserService.SaveUserPermissionAdd(entity.UserId, entity.OwnDepValue.Trim(), ref strError))
                {
                    return Json(AjaxResult.Success());
                }
                else
                {
                    return Json(AjaxResult.Error("Update failed"));
                }
            }
            ViewBag.strError = strError;
            return View(entity);
        }

        public ActionResult Export(LigerGrid grid, FormCollection data)
        {
            string strError = "";
            grid.ConvertParams(data);
            eContract.Common.ExcelConvertHelper.ExcelContext context = new ExcelConvertHelper.ExcelContext();
            if (SystemService.UserService.ExportExcel(grid, ref context, ref strError))
            {

                return Json(AjaxResult.Success());
            }
            return Json(AjaxResult.Error(strError));
        }

        public ActionResult RoleAdd(LigerGrid grid, FormCollection data, string id)
        {
            grid.ConvertParams(data);
            ViewBag.UserId = id;
            if (IsPost)
            {
                return Json(SystemService.FunctionRoleService.RoleAddGrid("MDM", grid));
            }
            return View();
        }

        public JsonResult RoleAddSave(string UserId, string strRoleIds)
        {
            string strError = "";
            if (SystemService.UserService.SaveRoleAdd(UserId, strRoleIds, "MDM", ref strError))
            {
                return Json(AjaxResult.Success());
            }
            return Json(AjaxResult.Error(strError));

        }

        public JsonResult loadSelectDepVale(string deptCode)
        {
            //var selectedValue = SystemService.DepartmentService.GetSelectManager(deptCode);
            var selectedDep = GetSelectDep(deptCode);
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var ret = JsonConvert.SerializeObject(selectedDep, setting);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        public string GetSelectDep(string deptCode)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(String));
            dataTable.Columns.Add("name", typeof(String));
            DataRow dr = dataTable.NewRow();
            var usersql = new StringBuilder();
            usersql.AppendFormat(" SELECT DEPT_ID AS id,DEPT_ALIAS AS name FROM dbo.CAS_DEPARTMENT WHERE DEPT_CODE={0}", Utils.ToSQLStr(deptCode).Trim());
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

        public JsonResult loadSelectUserDepPermission(string userid)
        {
            var selectedDep = GetSelectUserDepPermission(userid);
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var ret = JsonConvert.SerializeObject(selectedDep, setting);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        public string GetSelectUserDepPermission(string userid)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(String));
            dataTable.Columns.Add("name", typeof(String));
            //DataRow dr = dataTable.NewRow();
            var usersql = new StringBuilder();
            usersql.AppendFormat(" SELECT A.DEPT_ID AS id,A.DEPT_ALIAS AS name FROM dbo.CAS_DEPARTMENT A JOIN CAS_USER_PERMISSION B ON A.DEPT_ID=B.DEPT_ID WHERE B.USER_ID={0}", Utils.ToSQLStr(userid).Trim());
            var valueString = DataAccess.SelectDataSet(usersql.ToString());
            if (valueString.Tables[0].Rows.Count == 0)
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                System.Collections.ArrayList dic1 = new System.Collections.ArrayList();
                return jss.Serialize(dic1);
            }
            else
            {
                foreach (DataRow row in valueString.Tables[0].Rows)
                {
                    DataRow dr = dataTable.NewRow();
                    dr["id"] = row["id"];
                    dr["name"] = row["name"];
                    dataTable.Rows.Add(dr);
                }
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

    }
}
