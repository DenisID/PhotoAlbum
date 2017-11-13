using Autofac;
using Autofac.Integration.Mvc;
using PhotoAlbum.Client.BusinessServices.Interfaces;
using PhotoAlbum.Client.BusinessServices.Services;
using PhotoAlbum.Client.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace PhotoAlbum.Client.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            AutofacConfig.Initialize();
            AutoMapperConfig.Configure();
        }

        //private static void SetAutofacContainer()
        //{
        //    var builder = new ContainerBuilder();
        //    builder.RegisterControllers(Assembly.GetExecutingAssembly());
        //    builder.RegisterType<PhotoAlbumService>().As<IPhotoAlbumService>().InstancePerRequest();
        //    //builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

        //    //// Repositories
        //    //builder.RegisterAssemblyTypes(typeof(GadgetRepository).Assembly)
        //    //    .Where(t => t.Name.EndsWith("Repository"))
        //    //    .AsImplementedInterfaces().InstancePerRequest();
        //    //// Services
        //    //builder.RegisterAssemblyTypes(typeof(GadgetService).Assembly)
        //    //   .Where(t => t.Name.EndsWith("Service"))
        //    //   .AsImplementedInterfaces().InstancePerRequest();

        //    IContainer container = builder.Build();
        //    DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        //}
    }
}