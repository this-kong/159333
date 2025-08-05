using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AlumniNetworkingPlatform.Startup))]
namespace AlumniNetworkingPlatform
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
