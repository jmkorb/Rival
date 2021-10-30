using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Rival.WebMVC.Startup))]
namespace Rival.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
