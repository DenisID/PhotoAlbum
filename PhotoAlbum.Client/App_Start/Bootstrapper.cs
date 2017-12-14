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
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}