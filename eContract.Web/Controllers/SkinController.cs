using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eContract.Web.Controllers
{
    public class SkinController : BaseController
    {
        // GET: Skin
        public ActionResult OtherSkin(string viewName= "MDSkin")
        {
            return View(viewName);
        }

        public ActionResult SkinConfig()
        {
            return View();
        }
    }
}