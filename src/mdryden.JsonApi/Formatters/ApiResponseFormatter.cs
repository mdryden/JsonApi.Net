using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mdryden.JsonApi.Formatters
{
	public class ApiResponseFormatter : IOutputFormatter
	{
		public bool CanWriteResult(OutputFormatterCanWriteContext context)
		{
			return context.Object != null && context.Object is ApiResponse;
		}

		public Task WriteAsync(OutputFormatterWriteContext context)
		{
			var response = context.Object as IApiResponse;

			if (response != null)
			{
				context.HttpContext.Response.ContentType = JsonApiConstants.ContentType;
				if (response.Meta.TryGetValue(JsonApiConstants.StatusCodeMetaKey, out object value))
				{
					context.HttpContext.Response.StatusCode = (int)value;
				}
			}

			if (response.HasErrors() || response.IsResource())
			{
				var json = JsonConvert.SerializeObject(response);
				return context.HttpContext.Response.WriteAsync(json);
			}
			else
			{
				return Task.CompletedTask;	
			}
		}
	}
}
