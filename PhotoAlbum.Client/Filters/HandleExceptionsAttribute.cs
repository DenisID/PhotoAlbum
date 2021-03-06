﻿using log4net;
using PhotoAlbum.Client.Helpers;
using PhotoAlbum.Common.Exceptions;
using Resources;
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
            
            // Get inner exception and error message
            string errorMessage = null;
            var error = filterContext.Exception;
            while (error.InnerException != null)
            {
                error = error.InnerException;
            }
            errorMessage = error.Message;
            
            var resultException = new Exception(ErrorMsg.AnErrorHasOccurred, filterContext.Exception);

            // Photo album exceptions
            var photoAlbumException = error as PhotoAlbumException;
            if (photoAlbumException != null)
            {
                resultException = new Exception(photoAlbumException.Message.GetLocalExString(), filterContext.Exception);
            }

            // Server exceptions
            var serverException = error as System.Net.Sockets.SocketException;
            if (serverException != null)
            {
                resultException = new Exception(ErrorMsg.ServerIsNotAvailable, filterContext.Exception);
            }

            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();

            ActionResult errorResult = null;

            // GetPhotos action from Photo controller must return json (not view)
            if (controllerName == "Photo" && actionName == "GetPhotos")
            {
                filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                errorResult = new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        ErrorMessage = resultException.Message
                    }
                };
            }
            else
            {
                var model = new HandleErrorInfo(resultException,
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