using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using PhotoAlbum.Server.Handlers;
using System.Web.Http.ExceptionHandling;
using PhotoAlbum.Server.ExLogger;

namespace PhotoAlbum.Server
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.MessageHandlers.Add(new ResponseHandler());

            config.Services.Add(typeof(IExceptionLogger), new ExceptionManagerApi());
        }
    }
}
