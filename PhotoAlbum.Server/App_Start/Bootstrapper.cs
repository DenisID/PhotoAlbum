using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
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