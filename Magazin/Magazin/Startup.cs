using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Magazin.Startup))]
namespace Magazin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            
        }
    }
}
