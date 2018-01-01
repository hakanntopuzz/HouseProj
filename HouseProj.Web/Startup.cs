using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HouseProj.Web.Startup))]
namespace HouseProj.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            UnityConfig.RegisterComponents();
            ConfigureAuth(app);
        }
    }
}
