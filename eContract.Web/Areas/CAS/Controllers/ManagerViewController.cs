using eContract.BusinessService.SystemManagement.Service;
using eContract.Common;
using eContract.Common.Entity;
using eContract.Common.MVC;
using System.Web.Mvc;

namespace eContract.Web.Areas.CAS.Controllers
{
    public class ManagerViewController : AuthBaseController
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

        public ActionResult ManagerViewList(JqGrid grid, FormCollection data, string id)
        {
            CasUserEntity user = SystemService.UserService.GetUserEntity(id);
            ViewBag.UserId = id;
            ViewBag.UserCode = user.UserAccount;
            ViewBag.UserName = user.EnglishName;
            grid.ConvertParams(data);
            if (IsPost)
            {
                return Json(AjaxResult.Success(SystemService.UserService.GetAllLineManagers(id, grid)));
            }
            return View();
        }

    }
}
