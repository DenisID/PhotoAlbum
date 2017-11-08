using Autofac;
using Autofac.Integration.Mvc;
using PhotoAlbum.Client.BusinessServices.Interfaces;
using PhotoAlbum.Client.BusinessServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace PhotoAlbum.Client.App_Start
{
    public class AutofacConfig
    {
        public static IContainer Container;

        public static void Initialize()
        {
            Initialize(RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(IContainer container)
        {
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
        
        public static IContainer RegisterServices(ContainerBuilder builder)
        {
            //var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<PhotoAlbumService>().As<IPhotoAlbumService>().InstancePerRequest();
            //builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            //// Repositories
            //builder.RegisterAssemblyTypes(typeof(GadgetRepository).Assembly)
            //    .Where(t => t.Name.EndsWith("Repository"))
            //    .AsImplementedInterfaces().InstancePerRequest();
            //// Services
            //builder.RegisterAssemblyTypes(typeof(GadgetService).Assembly)
            //   .Where(t => t.Name.EndsWith("Service"))
            //   .AsImplementedInterfaces().InstancePerRequest();

            Container = builder.Build();

            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            return Container;
        }
    }
}