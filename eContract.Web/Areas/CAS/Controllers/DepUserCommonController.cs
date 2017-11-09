using System.Web;
using System.Web.Mvc;
using eContract.BusinessService.BusinessData.Service;
using eContract.BusinessService.SystemManagement.BusinessRule;
using eContract.BusinessService.SystemManagement.Service;
using eContract.Common;
using eContract.Common.Entity;
using eContract.Common.MVC;
using eContract.Common.WebUtils;
using System.Collections.Generic;
using System;
using System.Linq;

namespace eContract.Web.Areas.CAS.Controllers
{
    public class DepUserCommonController : AuthBaseController
    {
        /// <summary>
        /// 申请部门
        /// </summary>
        /// <param name="q"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Department(string q, string page)
        {
            var department = BusinessDataService.DepUserCommonService.GetAllDepartment();
            var lstRes = new List<Department>();
            for (var i = 0; i < department.Rows.Count; i++)
            {
                var oProvince = new Department();
                oProvince.id = department.Rows[i]["DEPT_ID"].ToString();
                oProvince.name = department.Rows[i]["DEPT_ALIAS"].ToString();
                lstRes.Add(oProvince);
            }
            if (q != null && q.Trim().Length > 0)
            {
                lstRes = (from r in lstRes where r.name.ToLower().Contains(q.ToLower()) select r).ToList();
            }
            var lstCurPageRes = string.IsNullOrEmpty(page) ? lstRes.Take(10) : lstRes.Skip(Convert.ToInt32(page) * 10 - 10).Take(10);
            return Json(new { items = lstCurPageRes, total_count = lstRes.Count }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 审批部门
        /// </summary>
        /// <param name="q"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult ApprovalDepartment(string q, string page)
        {
            var department = BusinessDataService.DepUserCommonService.GetAllApprovalDepartment();
            var lstRes = new List<Department>();
            for (var i = 0; i < department.Rows.Count; i++)
            {
                var oProvince = new Department();
                oProvince.id = department.Rows[i]["DEPT_ID"].ToString();
                oProvince.name = department.Rows[i]["DEPT_ALIAS"].ToString();
                lstRes.Add(oProvince);
            }
            if (q != null && q.Trim().Length > 0)
            {
                lstRes = (from r in lstRes where r.name.ToLower().Contains(q.ToLower()) select r).ToList();
            }
            var lstCurPageRes = string.IsNullOrEmpty(page) ? lstRes.Take(10) : lstRes.Skip(Convert.ToInt32(page) * 10 - 10).Take(10);
            return Json(new { items = lstCurPageRes, total_count = lstRes.Count }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserList(string q, string page)
        {
            var department = BusinessDataService.DepUserCommonService.GetAllUser();
            var lstRes = new List<UserList>();
            for (var i = 0; i < department.Rows.Count; i++)
            {
                var oProvince = new UserList();
                oProvince.id = department.Rows[i]["USER_ID"].ToString();
                oProvince.name = department.Rows[i]["NAME"].ToString();
                lstRes.Add(oProvince);
            }
            if (q != null && q.Trim().Length > 0)
            {
                lstRes = (from r in lstRes where r.name.ToLower().Contains(q.ToLower()) select r).ToList();
            }
            var lstCurPageRes = string.IsNullOrEmpty(page) ? lstRes.Take(10) : lstRes.Skip(Convert.ToInt32(page) * 10 - 10).Take(10);
            return Json(new { items = lstCurPageRes, total_count = lstRes.Count }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserListWithEmpty(string q, string page)
        {
            var department = BusinessDataService.DepUserCommonService.GetAllUser();
            var lstRes = new List<UserList>();
            lstRes.Add(new UserList() { id = "0", name = "Select" });
            for (var i = 0; i < department.Rows.Count; i++)
            {
                var oProvince = new UserList();
                oProvince.id = department.Rows[i]["USER_ID"].ToString();
                oProvince.name = department.Rows[i]["NAME"].ToString();
                lstRes.Add(oProvince);
            }
            if (q != null && q.Trim().Length > 0)
            {
                lstRes = (from r in lstRes where r.name.ToLower().Contains(q.ToLower()) select r).ToList();
            }
            var lstCurPageRes = string.IsNullOrEmpty(page) ? lstRes.Take(10) : lstRes.Skip(Convert.ToInt32(page) * 10 - 10).Take(10);
            return Json(new { items = lstCurPageRes, total_count = lstRes.Count }, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult Confirm(string confirmKeys)
        //{
        //    //string strError = "";
        //    //if (SystemService.DepartmentService.DeleteDepartmentDomainByIds(deleteKeys, ref strError))
        //    //{
        //    //    return Json(AjaxResult.Success());
        //    //}
        //    return Json(AjaxResult.Error("123"));

        //}
    }
    public class Department
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class UserList
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}