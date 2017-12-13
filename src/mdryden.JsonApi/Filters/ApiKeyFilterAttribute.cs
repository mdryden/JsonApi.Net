using mbsoft.JsonApi.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace mbsoft.JsonApi.Filters
{
    public class ApiKeyFilterAttribute : ActionFilterAttribute
    {

        private IOptions<JsonApiKeyCollection> keyCollection;

        public ApiKeyFilterAttribute(IOptions<JsonApiKeyCollection> keyCollection)
        {
            this.keyCollection = keyCollection;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            if (!context.HttpContext.Request.Query.ContainsKey("key"))
            {
                return InvalidKey(context.HttpContext);
            }

            if (!Guid.TryParse(context.HttpContext.Request.Query["key"], out var key))
            {
                return InvalidKey(context.HttpContext);
            }
            

            //var keys = context.HttpContext.RequestServices.GetService<JsonApiKeyCollection>();

            var matchedKey = keyCollection.Value.FirstOrDefault(k => k.Key == key);

            if (matchedKey == null || matchedKey.Revoked)
            {
                return InvalidKey(context.HttpContext);
            }

            return base.OnActionExecutionAsync(context, next);
        }

        private Task InvalidKey(HttpContext context)
        {
            var responseWriter = new JsonApiResponseWriter();
            return responseWriter.WriteErrorAsync(HttpStatusCode.Forbidden, "API key is missing or invalid.", context);
        }
    }
}
