using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Web.Mvc;
using eContract.BusinessService.BusinessData.Service;
using eContract.Common;
using eContract.Common.MVC;

namespace eContract.Web.Areas.CAS.Controllers
{
    public class ContractFieldController : AuthBaseController
    {
        // GET: CAS/ContractField
        public ActionResult Index(JqGrid grid, FormCollection data)
        {
            grid.ConvertParams(data);
            if (IsPost)
            {
                return Json(AjaxResult.Success(BusinessDataService.ContractFieldService.ForGrid(grid)));
            }
            return View();
        }
    }
}