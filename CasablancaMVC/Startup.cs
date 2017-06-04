using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CasablancaMVC.Startup))]
namespace CasablancaMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
