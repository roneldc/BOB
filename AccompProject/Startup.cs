using Microsoft.Owin;
using Owin;
using System.Web.Routing;

[assembly: OwinStartupAttribute(typeof(AccompProject.Startup))]
namespace AccompProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
