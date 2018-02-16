using mdryden.JsonApi.Filters;
using mdryden.JsonApi.Formatters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace mdryden.JsonApi.Tests
{
    public class DefaultMetaDataFilterAttributeTests
    {

		private ResultExecutingContext CreateResultExecutingContext(object resultObject)
		{
			var httpContext = new DefaultHttpContext();

			var context = new ResultExecutingContext(
				new ActionContext
				{
					HttpContext = httpContext,
					RouteData = new RouteData(),
					ActionDescriptor = new ActionDescriptor(),
				},
				new List<IFilterMetadata>(),
				new ObjectResult(resultObject),
				new Mock<Controller>().Object);

			return context;
		}

		[Fact]
		public void MetaDataIncludedWithDataTest()
		{
			var logger = new Mock<ILogger<DefaultMetaDataFilterAttribute>>();
			var defaultMetaDataRetriever = new Mock<IDefaultMetaDataRetriever>();
			defaultMetaDataRetriever.Setup(r => r.GetDefaultMetaData()).Returns(new MetaCollection { { "mock", "meta" } });
			var target = new DefaultMetaDataFilterAttribute(logger.Object, defaultMetaDataRetriever.Object);

			var responseObject = ApiResponse.OK().WithResource("mock", "Mock result data", "0").AsItemResponse();

			var context = CreateResultExecutingContext(responseObject);

			target.OnResultExecuting(context);

			var result = (context.Result as ObjectResult).Value as IApiResponse;

			var expected = 1;
			var actual = result.Meta?.Count(m => m.Key == "mock");

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void MetaDataIncludedWithErrorTest()
		{
			var logger = new Mock<ILogger<DefaultMetaDataFilterAttribute>>();
			var defaultMetaDataRetriever = new Mock<IDefaultMetaDataRetriever>();
			defaultMetaDataRetriever.Setup(r => r.GetDefaultMetaData()).Returns(new MetaCollection { { "mock", "meta" } });
			var target = new DefaultMetaDataFilterAttribute(logger.Object, defaultMetaDataRetriever.Object);

			var responseObject = ApiResponse.OK().WithError(error => error.Status = System.Net.HttpStatusCode.Forbidden).AsItemResponse();

			var context = CreateResultExecutingContext(responseObject);

			target.OnResultExecuting(context);

			var result = (context.Result as ObjectResult).Value as IApiResponse;

			var expected = 1;
			var actual = result.Meta?.Count(m => m.Key == "mock");

			Assert.Equal(expected, actual);
		}


		[Fact]
		public void MetaDataNotIncludedOnEmptyTest()
		{
			var logger = new Mock<ILogger<DefaultMetaDataFilterAttribute>>();
			var defaultMetaDataRetriever = new Mock<IDefaultMetaDataRetriever>();
			defaultMetaDataRetriever.Setup(r => r.GetDefaultMetaData()).Returns(new MetaCollection { { "mock", "meta" } });
			var target = new DefaultMetaDataFilterAttribute(logger.Object, defaultMetaDataRetriever.Object);

			var responseObject = ApiResponse.OK().AsResponse();

			var context = CreateResultExecutingContext(responseObject);

			target.OnResultExecuting(context);

			var result = (context.Result as ObjectResult).Value as IApiResponse;

			var expected = 0;
			var actual = result.Meta?.Count(m => m.Key == "mock") ?? 0;

			Assert.Equal(expected, actual);
		}

	}
}
