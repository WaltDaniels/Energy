using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CentralHub.Startup))]
namespace CentralHub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
