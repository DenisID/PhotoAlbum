using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoAlbum.Client.Filters
{
    public class HandleExceptionsAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            //var exception = filterContext.Exception;

            ILog log = log4net.LogManager.GetLogger("UnhandledException");
            log.Error("Error", filterContext.Exception);

            filterContext.ExceptionHandled = true;

            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();
            if(controllerName == "Photo" && actionName == "GetPhotos")
            {
                filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                filterContext.Result = new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        ErrorMessage = filterContext.Exception.Message
                    }
                };
            }
            else
            {
                var model = new HandleErrorInfo(filterContext.Exception,
                                            filterContext.RouteData.Values["controller"].ToString(),
                                            filterContext.RouteData.Values["action"].ToString());
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary(model)
                };
            }

            
        }
    }
}