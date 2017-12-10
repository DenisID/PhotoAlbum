using PhotoAlbum.Client.BusinessServices.Services;
using PhotoAlbum.Client.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PhotoAlbum.Client
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "User",
                url: "{username}",
                defaults: new { controller = "Photo", action = "User" },
                constraints: new { username = new UserNameConstraint(new UserService()) }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Photo", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
