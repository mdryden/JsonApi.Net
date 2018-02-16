using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace mdryden.JsonApi
{
	public static class ApiResponseBuilderExtensions
	{
		public static ApiResponseBuilder WithError(this ApiResponseBuilder responseBuilder, Action<Error> errorConfig)
		{
			var error = new Error();
			errorConfig.Invoke(error);
			responseBuilder.AddError(error);
			return responseBuilder;
		}

		public static ApiResponseBuilder WithErrors(this ApiResponseBuilder responseBuilder, ModelStateDictionary modelStateErrors)
		{
			foreach (var validationErrorKvp in modelStateErrors)
			{
				var key = validationErrorKvp.Key;
				var validationErrors = validationErrorKvp.Value.Errors;
				foreach (var validationError in validationErrors)
				{
					responseBuilder.WithError(error => error.WithCode(key).WithDetail(validationError.ErrorMessage));
				}
			}

			return responseBuilder;
		}

		public static ApiResponseBuilder WithLink(this ApiResponseBuilder response, string key, string href)
		{
			response.AddLink(key, href);
			return response;
		}

		public static ApiResponseBuilder WithLink(this ApiResponseBuilder response, string key, string href, Action<Link> linkConfig)
		{
			var link = new Link { Href = href };
			linkConfig.Invoke(link);
			response.AddLink(key, link);
			return response;
		}
		public static ApiResponseBuilder WithMeta(this ApiResponseBuilder response, string key, object value)
		{
			response.AddMeta(key, value);
			return response;
		}
	}
}
