using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace mbsoft.JsonApi.Models
{
    public static class JsonApiResponseCreator
    { 
        public static Func<IDictionary<string, object>> DefaultMeta;
       
        public static JsonApiResponse<T> CreateDataResponse<T>(T data)
        {
            var response = new JsonApiResponse<T>
            {
                Data = data,
                Meta = DefaultMeta.Invoke(),
            };

            return response;

        }

        public static JsonApiResponse<object> CreateErrorResponse(Exception e)
        {
            if (e is JsonApiException)
            {
                var httpCodeException = e as JsonApiException;
                return CreateErrorResponse(httpCodeException);
            }
            else
            {
                return CreateErrorResponse( HttpStatusCode.InternalServerError, e);
            }
        }

        public static JsonApiResponse<object> CreateErrorResponse(JsonApiException e)
        {
            return CreateErrorResponse(e.Code, e);
        }

        public static JsonApiResponse<object> CreateErrorResponse(HttpStatusCode code, Exception e)
        { 
            var error = new JsonApiError { Code = (int)code, Detail = e.Message, Source = e.StackTrace };
            return new JsonApiResponse<object> { Errors = new[] { error } };
        }

        public static JsonApiResponse<object> CreateErrorResponse(HttpStatusCode code, string message)
        {
            var error = new JsonApiError { Code = (int)code, Detail = message };
            return new JsonApiResponse<object> { Errors = new[] { error } };
        }
    }
}
