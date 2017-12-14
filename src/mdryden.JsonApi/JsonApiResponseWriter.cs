using mdryden.JsonApi.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace mdryden.JsonApi
{
    public class JsonApiResponseWriter
    {
        private Task WriteAsync(HttpStatusCode statusCode, JsonApiResponse apiResponse, HttpContext context)
        {
            var json = JsonConvert.SerializeObject(apiResponse, new JsonApiSerializerSettings());

            var httpResponse = context.Response;
            httpResponse.ContentType = JsonApiConstants.ContentType;
            httpResponse.StatusCode = (int)statusCode;

            return httpResponse.WriteAsync(json);
        }
        
        public Task WriteContentAsync(object content, HttpContext context)
        {
            var apiResponse = JsonApiResponseCreator.CreateDataResponse(content);
            return WriteAsync(HttpStatusCode.OK, apiResponse, context);
        }

        public Task WriteErrorAsync(HttpStatusCode code, string message, HttpContext context)
        {
            var apiResponse = JsonApiResponseCreator.CreateErrorResponse(code, message);
            return WriteAsync(code, apiResponse, context);
        }
    }
}
