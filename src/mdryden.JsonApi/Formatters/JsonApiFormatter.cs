using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mbsoft.JsonApi.Formatters
{
    public class JsonApiFormatter : IOutputFormatter
    {
        public bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            return true;
        }

        public Task WriteAsync(OutputFormatterWriteContext context)
        {
            var responseWriter = new JsonApiResponseWriter();
            return responseWriter.WriteContentAsync(context.Object, context.HttpContext);
        }
    }
}
