using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjektPortale.Startup))]
namespace ProjektPortale
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
