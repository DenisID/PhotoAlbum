using Autofac;
using Autofac.Integration.WebApi;
using PhotoAlbum.Server.Model.Interfaces;
using PhotoAlbum.Server.Model.Services;
using System.Reflection;
using System.Web.Http;

namespace PhotoAlbum.Server.App_Start
{
    public class AutofacConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }
        
        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<PhotoAlbumService>()
                   .As<IPhotoAlbumService>()
                   .InstancePerRequest();

            Container = builder.Build();

            return Container;
        }
    }
}