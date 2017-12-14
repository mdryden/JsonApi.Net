using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace mdryden.JsonApi.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            OnExceptionAsync(context).Wait();
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var responseWriter = new JsonApiResponseWriter();

            var apiError = context.Exception as JsonApiException;

            context.ExceptionHandled = true;

            if (apiError != null)
            {
                return responseWriter.WriteErrorAsync(apiError.Code, apiError.Message, context.HttpContext);
            }
            else
            {
                return responseWriter.WriteErrorAsync(HttpStatusCode.InternalServerError, context.Exception.Message, context.HttpContext);
            }
        }
    }
}
