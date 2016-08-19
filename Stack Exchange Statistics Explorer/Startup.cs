using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Stack_Exchange_Statistics_Explorer.Startup))]
namespace Stack_Exchange_Statistics_Explorer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
