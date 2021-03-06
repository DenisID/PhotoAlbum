﻿using Newtonsoft.Json;
using PhotoAlbum.Common.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace PhotoAlbum.Server.Attributes
{
    public class EntityTagContentHashAttribute : ActionFilterAttribute
    {
        private IEnumerable<string> _receivedEntityTags;

        private readonly HttpMethod[] _supportedRequestMethods = {
        HttpMethod.Get,
        HttpMethod.Head
    };

        public override void OnActionExecuting(HttpActionContext context)
        {
            if (!_supportedRequestMethods.Contains(context.Request.Method))
                throw new HttpResponseException(context.Request.CreateErrorResponse(HttpStatusCode.PreconditionFailed,
                    "This request method is not supported in combination with ETag."));

            var conditions = context.Request.Headers.IfNoneMatch;

            if (conditions != null)
            {
                _receivedEntityTags = conditions.Select(t => t.Tag.Trim('"'));
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            var objectContent = context.Response.Content as ObjectContent;

            if (objectContent == null) return;

            var unifiedData = JsonConvert.SerializeObject(objectContent.Value);
            var computedEntityTag = ETagHashCreator.ComputeHash(unifiedData);

            if (_receivedEntityTags.Contains(computedEntityTag))
            {
                context.Response.StatusCode = HttpStatusCode.NotModified;
                context.Response.Content = null;
            }

            context.Response.Headers.ETag = new EntityTagHeaderValue("\"" + computedEntityTag + "\"", true);
        }
    }
}