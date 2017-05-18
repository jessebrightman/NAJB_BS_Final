using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NAJB_BS_Final.Startup))]
namespace NAJB_BS_Final
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
