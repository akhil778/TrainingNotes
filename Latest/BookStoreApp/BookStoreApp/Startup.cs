using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookStoreApp.Startup))]
namespace BookStoreApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
