using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Spending.Web.Startup))]
namespace Spending.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            
        }
    }
}
