using System.Web.Http;

namespace PhotoAlbum.Server.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            AutofacConfig.Initialize(GlobalConfiguration.Configuration);
            AutoMapperConfig.Configure();
        }
    }
}