using System.Web.Mvc;

namespace eContract.Web.Areas.LUBR
{
    public class LUBRAreaRegistration: AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "LUBR";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "LUBR_default",
                "LUBR/{controller}/{action}/{id}",
               new { controller = "ProductsShow", action = "Index", id = UrlParameter.Optional }, // 参数默认值
                 namespaces: new[] { "eContract.Web.Areas.LUBR.Controllers" }

            );
        }
    }
}