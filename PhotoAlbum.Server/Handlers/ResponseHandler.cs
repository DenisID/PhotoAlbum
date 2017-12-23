using PhotoAlbum.Common.ErrorCodes;
using PhotoAlbum.Common.Exceptions;
using PhotoAlbum.Server.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PhotoAlbum.Server.Handlers
{
    public class ResponseHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            return BuildApiResponse(request, response);
        }

        private static HttpResponseMessage BuildApiResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
            object content;
            string errorMessage = null;
            HttpStatusCode responseStatusCode = response.StatusCode;

            if (response.TryGetContentValue(out content) && !response.IsSuccessStatusCode)
            {
                HttpError error = content as HttpError;

                if (error != null)
                {
                    content = null;
                    while(error.InnerException != null)
                    {
                        error = error.InnerException;
                    }
                    errorMessage = error.ExceptionMessage;

                    if (errorMessage.Contains(ErrorCodes.ErrorsMark))
                    {
                        responseStatusCode = HttpStatusCode.OK;
                    }
                }
            }

            var newResponse = request.CreateResponse(responseStatusCode, new ApiResponse(responseStatusCode, content, errorMessage));

            foreach (var header in response.Headers)
            {
                newResponse.Headers.Add(header.Key, header.Value);
            }

            return newResponse;
        }
    }
}