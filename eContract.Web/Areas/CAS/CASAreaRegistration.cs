using System.Web.Mvc;

namespace eContract.Web.Areas.CAS
{
    public class CASAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CAS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CAS_default",
                "CAS/{controller}/{action}/{id}",
               new { controller = "ApplayContract", action = "Index", id = UrlParameter.Optional }, // 参数默认值
                 namespaces: new[] { "eContract.Web.Areas.CAS.Controllers" }

            );
        }
    }
}
