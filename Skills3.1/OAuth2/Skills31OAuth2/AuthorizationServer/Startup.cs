using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AuthorizeWebApplication.Startup))]
namespace AuthorizeWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigureWebApi(app);
        }
    }
}
