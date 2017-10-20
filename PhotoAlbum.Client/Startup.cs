using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PhotoAlbum.Client.Startup))]
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
