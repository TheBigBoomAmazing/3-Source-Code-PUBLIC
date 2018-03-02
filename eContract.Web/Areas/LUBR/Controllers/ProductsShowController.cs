using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eContract.Web.Areas.LUBR.Controllers
{
    public class ProductsShowController : BaseController
    {
        // GET: LUBR/ProductsShow
        public ActionResult Index()
        {
            return View();
        }
    }
}