using System.Web.Mvc;

namespace eContract.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
               new { controller = "UserManagement", action = "Index", id = UrlParameter.Optional }, // 参数默认值
                 namespaces: new[] { "eContract.Web.Areas.Admin.Controllers" }
                
            );
        }
    }
}
