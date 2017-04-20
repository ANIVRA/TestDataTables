using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestDataTables.Startup))]
namespace TestDataTables
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
