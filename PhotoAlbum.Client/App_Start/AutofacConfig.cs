using Autofac;
using Autofac.Integration.Mvc;
using PhotoAlbum.Client.AppParameters;
using PhotoAlbum.Client.BusinessServices.Interfaces;
using PhotoAlbum.Client.BusinessServices.Services;
using PhotoAlbum.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<PhotoAlbumService>()
                   .As<IPhotoAlbumService>()
                   .InstancePerRequest();

            builder.RegisterType<UserService>()
                   .As<IUserService>()
                   .InstancePerRequest();

            builder.RegisterInstance( (UriConstantsService)ConfigurationManager.GetSection(nameof(UriConstantsService)) )
                   .As<IUriConstantsService>();

            builder.RegisterInstance((ValidateFileConstantsService)ConfigurationManager.GetSection(nameof(ValidateFileConstantsService)))
                   .As<IValidateFileConstantsService>();

            Container = builder.Build();
            
            return Container;
        }
    }
}