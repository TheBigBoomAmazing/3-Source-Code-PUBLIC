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

namespace eContract.Web.Areas.CAS.Controllers
{
    public class HolidayController : AuthBaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllHoliday()
        {
            List<CasHolidayEntity> holidays = SystemService.HolidayService.GetAllHoliday();
            var result = from h in holidays select new
            {
                startDate = h.HolidayDate.ToString("yyyy-MM-dd"),
                endDate = h.HolidayDate.ToString("yyyy-MM-dd"),
                holidayType = 0
            };
            return Json(result.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddHoliday(string startDate, string endDate)
        {
            string strError = "";
            if (SystemService.HolidayService.Add(startDate, endDate, ref strError))
            {
                return Json(AjaxResult.Success(), JsonRequestBehavior.AllowGet);
            }
            return Json(AjaxResult.Error(strError), JsonRequestBehavior.AllowGet);

        }

        public JsonResult DeleteHoliday(string startDate, string endDate)
        {
            string strError = "";
            if (SystemService.HolidayService.Delete(startDate, endDate, ref strError))
            {
                return Json(AjaxResult.Success(), JsonRequestBehavior.AllowGet);
            }
            return Json(AjaxResult.Error(strError), JsonRequestBehavior.AllowGet);
        }

        public JsonResult InitHoliday(string year)
        {
            string strError = "";
            if (SystemService.HolidayService.InitHoliday(year, ref strError))
            {
                return Json(AjaxResult.Success(), JsonRequestBehavior.AllowGet);
            }
            return Json(AjaxResult.Error(strError), JsonRequestBehavior.AllowGet);
        }
    }
}
