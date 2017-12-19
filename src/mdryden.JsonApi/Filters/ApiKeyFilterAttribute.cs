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
using Microsoft.Extensions.Logging;

namespace mdryden.JsonApi.Filters
{
    public class ApiKeyFilterAttribute : ActionFilterAttribute
    {

        private IOptions<JsonApiKeyCollection> keyCollection;
        private ILogger logger;

        public ApiKeyFilterAttribute(IOptions<JsonApiKeyCollection> keyCollection, ILogger<ApiKeyFilterAttribute> logger)
        {
            this.keyCollection = keyCollection;
            this.logger = logger;
        }

        private void LogInvalidKey(HttpContext httpContext, string reason)
        {
            logger.LogWarning($"Request for {httpContext.Request.Path} was denied: {reason}");
        }

        private bool IsKeyValid(HttpContext httpContext)
        {
            if (!httpContext.Request.Query.ContainsKey("key"))
            {
                LogInvalidKey(httpContext, "no key was given.");
                return false;
            }

            var keyValue = httpContext.Request.Query["key"];
            if (!Guid.TryParse(keyValue, out var key))
            {
                LogInvalidKey(httpContext, $"provided key '{keyValue}' is not a guid.");
                return false;
            }
            
            var matchedKey = keyCollection.Value.FirstOrDefault(k => k.Key == key);

            if (matchedKey == null)
            {
                LogInvalidKey(httpContext, $"provided key '{key}' is not valid.");
                return false;
            }

            if (matchedKey.Revoked)
            {
                LogInvalidKey(httpContext, $"provided key '{key}' has been revoked.");
            }

            logger.LogInformation($"Request for '{httpContext.Request.Path}' was accepted with key '{key}'.");
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
