using mdryden.JsonApi.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mdryden.JsonApi.Formatters
{
	public class DefaultMetaDataFilterAttribute : ActionFilterAttribute
	{
		ILogger logger;
		IDefaultMetaDataRetriever defaultMetaDataRetriever;

		public DefaultMetaDataFilterAttribute(ILogger<DefaultMetaDataFilterAttribute> logger, IDefaultMetaDataRetriever defaultMetaDataRetriever)
		{
			this.logger = logger;
			this.defaultMetaDataRetriever = defaultMetaDataRetriever;
		}

		public override void OnResultExecuting(ResultExecutingContext context)
		{
			var objectResult = context.Result as ObjectResult;

			if (objectResult?.Value is ApiResponse response && response.HasContent() == true)
			{
				response.AddMeta(defaultMetaDataRetriever.GetDefaultMetaData());
			}
		}
		
	}
}
