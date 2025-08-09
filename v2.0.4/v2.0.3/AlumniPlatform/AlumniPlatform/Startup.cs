using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AlumniPlatform.Startup))]
namespace AlumniPlatform
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
