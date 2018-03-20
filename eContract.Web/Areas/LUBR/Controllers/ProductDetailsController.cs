using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eContract.Common.Entity;
using eContract.BusinessService.BusinessData.Service;

namespace eContract.Web.Areas.LUBR.Controllers
{
    public class ProductDetailsController : BaseController
    {
        // GET: LUBR/ProductDetails
        public ActionResult Index(string id)
        {
            LubrFinancialGoodsEntity productDetail = new LubrFinancialGoodsEntity();
            if (id!=""&&id!=null)
            {
                productDetail = BusinessDataService.LubrProductsShowBLLService.GetProductsDetailInfo(id);
            }
            return View(productDetail);
        }
    }
}