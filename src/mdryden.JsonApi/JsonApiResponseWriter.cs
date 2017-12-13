using mbsoft.JsonApi.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace mbsoft.JsonApi
{
    public class JsonApiResponseWriter
    {
        private Task WriteAsync(object apiResponse, HttpContext context)
        {
            var json = JsonConvert.SerializeObject(apiResponse, new JsonApiSerializerSettings());

            var httpResponse = context.Response;
            httpResponse.ContentType = JsonApiConstants.ContentType;

            return httpResponse.WriteAsync(json);
        }

        public Task WriteContentAsync(object content, HttpContext context)
        {
            var apiResponse = JsonApiResponseCreator.CreateDataResponse(content);
            return WriteAsync(apiResponse, context);
        }

        public Task WriteErrorAsync(HttpStatusCode code, string message, HttpContext context)
        {
            var apiResponse = JsonApiResponseCreator.CreateErrorResponse(code, message);
            return WriteAsync(apiResponse, context);
        }
    }
}
