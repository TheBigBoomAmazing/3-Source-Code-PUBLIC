using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eContract.Common;
using eContract.BusinessService.SystemManagement.Service;
using eContract.Common.Entity;
using eContract.Common.MVC;

namespace eContract.Web.Areas.Admin.Controllers
{
    public class LogErrorManagementController : AuthBaseController
    {
        public ActionResult Index(JqGrid grid, FormCollection data)
        {
            grid.ConvertParams(data);
            if (IsPost)
            {
                return Json(AjaxResult.Success(SystemService.LogErrorService.ForGrid("MDM",grid)));
            }
            return View();
        }

        public ActionResult Edit(SecLogErrorEntity entity, string id)
        {
            if (!IsPost)
            {
                entity = SystemService.LogErrorService.LogErrorDomainCreate().SecLogErrorEntity;
                if (!string.IsNullOrEmpty(id))
                {
                    entity = SystemService.LogErrorService.GetById<SecLogErrorEntity>(id);
                }
            }
            return View(entity);
        }
    }
}
