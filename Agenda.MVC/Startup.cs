using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Agenda.MVC.Startup))]
namespace Agenda.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
