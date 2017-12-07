using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PhotoAlbum.Client.Startup))]
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)]
namespace PhotoAlbum.Client
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
