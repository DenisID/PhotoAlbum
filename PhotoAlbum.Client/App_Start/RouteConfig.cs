using PhotoAlbum.Client.AppParameters;
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
                name: "UserManage",
                url: "{username}/manage",
                defaults: new { controller = "Photo", action = "UserPageManage" }/*,
                constraints: new { username = new UserNameConstraint(new UserService(new UriConstantsService())) }*/
            );

            routes.MapRoute(
                name: "UserProfile",
                url: "{username}/profile",
                defaults: new { controller = "Account", action = "EditUserProfile" }/*,
                constraints: new { username = new UserNameConstraint(new UserService(new UriConstantsService())) }*/
            );

            routes.MapRoute(
                name: "User",
                url: "{username}",
                defaults: new { controller = "Photo", action = "UserPage" }/*,
                constraints: new { username = new UserNameConstraint(new UserService(new UriConstantsService())) }*/
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Photo", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
