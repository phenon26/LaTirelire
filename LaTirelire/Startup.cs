using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LaTirelire.Startup))]
namespace LaTirelire
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
