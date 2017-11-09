using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eContract.Web.Startup))]
namespace eContract.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           // ConfigureAuth(app);
        }
    }
}
