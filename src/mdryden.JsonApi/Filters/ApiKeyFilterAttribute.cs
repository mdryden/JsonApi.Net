using mdryden.JsonApi.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace mdryden.JsonApi.Filters
{
    public class ApiKeyFilterAttribute : ActionFilterAttribute
    {

        private IOptions<JsonApiKeyCollection> keyCollection;

        public ApiKeyFilterAttribute(IOptions<JsonApiKeyCollection> keyCollection)
        {
            this.keyCollection = keyCollection;
        }

        private bool IsKeyValid(HttpContext httpContext)
        {
            if (!httpContext.Request.Query.ContainsKey("key"))
            {
                return false;
            }

            if (!Guid.TryParse(httpContext.Request.Query["key"], out var key))
            {
                return false;
            }
            
            var matchedKey = keyCollection.Value.FirstOrDefault(k => k.Key == key);

            if (matchedKey == null || matchedKey.Revoked)
            {
                return false;
            }

            return true;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!IsKeyValid(context.HttpContext))
            {
                var responseWriter = new JsonApiResponseWriter();
                responseWriter.WriteErrorAsync(HttpStatusCode.Forbidden, "API key is missing or invalid.", context.HttpContext).Wait();
            }
            else
            {
                base.OnActionExecuting(context);
            }
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!IsKeyValid(context.HttpContext))
            {
                var responseWriter = new JsonApiResponseWriter();
                return responseWriter.WriteErrorAsync(HttpStatusCode.Forbidden, "API key is missing or invalid.", context.HttpContext);
            }
            return base.OnActionExecutionAsync(context, next);
        }
        
    }
}
