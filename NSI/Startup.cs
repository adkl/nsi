using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(NSI.Startup))]

namespace NSI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
