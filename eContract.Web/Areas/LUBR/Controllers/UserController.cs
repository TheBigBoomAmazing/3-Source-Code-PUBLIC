using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eContract.Common.Entity;
using eContract.BusinessService.BusinessData.Service;   

namespace eContract.Web.Areas.LUBR.Controllers
{
    public class UserController : BaseController
    {
        // GET: LUBR/ProductsShow
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 用户的安全中心
        /// </summary>
        /// <returns></returns>
        public ActionResult SecurityCenter(string userid)
        {
            var aa = userid;
            return View();
        }
    }
}