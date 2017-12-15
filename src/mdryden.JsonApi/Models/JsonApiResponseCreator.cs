using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace mdryden.JsonApi.Models
{
    public static class JsonApiResponseCreator
    {
        public static Func<IDictionary<string, object>> DefaultMeta;

        public static JsonApiDataResponse<T> CreateDataResponse<T>(T data)
        {
            var response = new JsonApiDataResponse<T>
            {
                Data = data,
                Meta = DefaultMeta?.Invoke(),
            };

            return response;

        }

        public static JsonApiErrorResponse CreateErrorResponse(Exception e)
        {
            if (e is JsonApiException)
            {
                var httpCodeException = e as JsonApiException;
                return CreateErrorResponse(httpCodeException);
            }
            else
            {
                return CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        public static JsonApiErrorResponse CreateErrorResponse(JsonApiException e)
        {
            return CreateErrorResponse(e.Code, e);
        }

        public static JsonApiErrorResponse CreateErrorResponse(HttpStatusCode code, Exception e)
        {
            var error = new JsonApiError { Code = (int)code, Detail = e.Message, Source = e.StackTrace };
            return new JsonApiErrorResponse
            {
                Errors = new[] { error },
                Meta = DefaultMeta?.Invoke(),
            };
        }

        public static JsonApiErrorResponse CreateErrorResponse(HttpStatusCode code, string message)
        {
            var error = new JsonApiError { Code = (int)code, Detail = message };
            return new JsonApiErrorResponse
            {
                Errors = new[] { error },
                Meta = DefaultMeta?.Invoke(),
            };
        }
    }
}
