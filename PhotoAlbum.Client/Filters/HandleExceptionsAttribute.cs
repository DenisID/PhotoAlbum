using log4net;
using PhotoAlbum.Common.Exceptions;
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
            // Log section
            ILog log = log4net.LogManager.GetLogger("UnhandledException");
            log.Error("Error", filterContext.Exception);
            // -----------
            
            ActionResult errorResult = null;

            ///////////////////
            var photoAlbumException = filterContext.Exception as PhotoAlbumException;
            if (photoAlbumException != null)
            {

            }

            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();

            // GetPhotos action from Photo controller must return json (not view)
            if(controllerName == "Photo" && actionName == "GetPhotos")
            {
                filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                errorResult = new JsonResult()
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
                errorResult = new ViewResult()
                {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary(model)
                };
            }

            filterContext.ExceptionHandled = true;

            filterContext.Result = errorResult;
        }
    }
}