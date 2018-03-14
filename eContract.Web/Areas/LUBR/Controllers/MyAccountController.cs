using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eContract.Web.Areas.LUBR.Controllers
{
    public class MyAccountController : BaseController
    {
        // GET: LUBR/MyAccount我的账户中心首页
        public ActionResult Index()
        {
            return View();
        }
    }
}