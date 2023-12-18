using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AIA.Startup))]
namespace AIA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
         //   ConfigureAuth(app);
        }
    }
}
