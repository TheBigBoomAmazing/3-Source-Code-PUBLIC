using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eContract.Common;
using eContract.BusinessService;
using eContract.Common.Entity;
using eContract.BusinessService.SystemManagement.Service;
using eContract.Common.WebUtils;
using System.Text;
using eContract.Common.MVC;
using Suzsoft.Smart.EntityCore;
using Suzsoft.Smart.Data;
using Newtonsoft.Json;

namespace eContract.Web.Areas.CAS.Controllers
{
    public class DepartmentController : AuthBaseController
    {
        public ActionResult Index(JqGrid grid, FormCollection data)
        {
            grid.ConvertParams(data);
            if (IsPost)
            {
                return Json(AjaxResult.Success(SystemService.DepartmentService.ForGrid(grid)));
            }
            return View();
        }

        public ActionResult Edit(CasDepartmentEntity entity, string id)
        {
            string strError = "";
            if (!IsPost)
            {
                entity = SystemService.DepartmentService.CreateDepartmentDomain().CasDepartmentEntity;
                entity.DeptType = 2;
                if (!string.IsNullOrEmpty(id))
                {
                    entity = SystemService.DepartmentService.GetById<CasDepartmentEntity>(id);
                    ViewBag.EditType = "0";//编辑
                }
                else
                {
                    ViewBag.EditType = "1";//新增
                }
                //Dictionary<string, string> dicDeptManager = new Dictionary<string, string>();

                //List<CasUserEntity> listAllUsers = SystemService.DepartmentService.GetAllUsers();
                //if (listAllUsers.Count > 0)
                //{
                //    dicDeptManager = listAllUsers.ToDictionary(x => x.UserId, x => x.ChineseName);
                //}

                //ViewBag.DeptManagerDic = dicDeptManager;
            }
            else
            {
                entity.DeptManagerId = entity.DeptManagerId != "0" ? entity.DeptManagerId : "";
                var domian = SystemService.DepartmentService.CreateDepartmentDomain();
                domian.CasDepartmentEntity = entity;
                if (SystemService.DepartmentService.Save(domian, ref strError))
                {
                    return Json(AjaxResult.Success());
                }
                else
                {
                    return Json(AjaxResult.Error(strError));
                }
            }
            ViewBag.strError = strError;
            return View(entity);
        }
        /// <summary>
        /// 得到部门总监的信息
        /// </summary>
        /// <param name="deptManagerId"></param>
        /// <returns></returns>
        public JsonResult loadSelectManager(string deptManagerId)
        {
            var selectedValue = SystemService.DepartmentService.GetSelectManager(deptManagerId);
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var ret = JsonConvert.SerializeObject(selectedValue, setting);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获得部门成员
        /// </summary>
        /// <param name="depId"></param>
        /// <returns></returns>
        public JsonResult loadDeptUsers(string depId)
        {
            var selectedValue = SystemService.DepartmentService.GetSelectDepUser(depId);
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var ret = JsonConvert.SerializeObject(selectedValue, setting);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(string deleteKeys)
        {
            string strError = "";
            if (SystemService.DepartmentService.DeleteDepartmentDomainByIds(deleteKeys, ref strError))
            {
                return Json(AjaxResult.Success());
            }
            return Json(AjaxResult.Error(strError));

        }

        public ActionResult DeptUserEdit(JqGrid grid, FormCollection data, string id)
        {
            grid.ConvertParams(data);
            grid.QueryField.Remove("id");
            ViewBag.DeptId = id;
            ViewBag.SelectUserIds = SystemService.DepartmentService.GetDeptUsersByDeptId( id);
            if (IsPost)
            {
                return Json(AjaxResult.Success(SystemService.UserService.ForGrid(grid)));
            }
            return View();
        }

        public JsonResult DeptUserAddSave(string DeptId, string strUserIds)
        {
            string strError = "";
            if (SystemService.DepartmentService.SaveUserAdd(DeptId, strUserIds, ref strError))
            {
                return Json(AjaxResult.Success());
            }
            return Json(AjaxResult.Error(strError));

        }
    }
}
