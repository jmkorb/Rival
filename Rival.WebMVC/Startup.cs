using Microsoft.Owin;
using Owin;
using Rival.Services.RoleServices;

[assembly: OwinStartupAttribute(typeof(Rival.WebMVC.Startup))]
namespace Rival.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            var service = new RoleService();
            service.CreateAdmin();
            service.MakeMyUserAdmin();
        }

    }
}
