using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace PhotoAlbum.Server.Controllers
{
    public class BaseController : ApiController
    {
        [NonAction]
        public HttpResponseMessage Success(object data = null)
        {
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [NonAction]
        public HttpResponseMessage Error(object result)
        {
            return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
        }
    }
}
