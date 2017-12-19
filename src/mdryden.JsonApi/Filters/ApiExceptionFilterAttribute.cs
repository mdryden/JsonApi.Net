using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace mdryden.JsonApi.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {

        private ILogger logger;

        public ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> logger)
        {
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            OnExceptionAsync(context).Wait();
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var responseWriter = new JsonApiResponseWriter();

            var apiError = context.Exception as JsonApiException;

            context.ExceptionHandled = true;

            logger.LogError($"Request for '{context.HttpContext.Request.Path}' threw an exxeption: '{context.Exception}");

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
