using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyMusicPlus.Startup))]
namespace MyMusicPlus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
