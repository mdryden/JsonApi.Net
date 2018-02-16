using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace mdryden.JsonApi.Filters
{
	public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
	{

		ILogger logger;
		IDefaultMetaDataRetriever defaultMetaDataRetriever;

		public ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> logger, IDefaultMetaDataRetriever defaultMetaDataRetriever)
		{
			this.logger = logger;
			this.defaultMetaDataRetriever = defaultMetaDataRetriever;
		}

		public override void OnException(ExceptionContext context)
		{
			var apiError = context.Exception as JsonApiException;

			var responseCode = apiError?.Status ?? HttpStatusCode.InternalServerError;
			var detail = apiError?.Message ?? context.Exception.Message;

			context.ExceptionHandled = true;

			logger.LogError($"Request for '{context.HttpContext.Request.Path}' threw an exception: '{context.Exception}");

			var response = ApiResponse.WithStatus(responseCode).WithError(error =>
			{
				error.WithStatus(responseCode).WithDetail(detail);
			});

			response.AddMeta(defaultMetaDataRetriever.GetDefaultMetaData());

			context.Result = new ObjectResult(response.AsResponse());
		}
		
	}
}
