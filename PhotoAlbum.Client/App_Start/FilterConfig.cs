using PhotoAlbum.Client.Filters;
using System.Web;
using System.Web.Mvc;

namespace PhotoAlbum.Client
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new HandleExceptionsAttribute());
        }
    }
}
