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
    public class SelfLinksFilterAttributeTests
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
		public void SelfLinkIncludedTest()
		{
			var logger = new Mock<ILogger<SelfLinksFilterAttribute>>();
			var target = new SelfLinksFilterAttribute(logger.Object);

			var responseObject = ApiResponse.Create().WithResource("mock", "Mock result data", "0");

			var context = CreateResultExecutingContext(responseObject);

			target.OnResultExecuting(context);

			var result = (context.Result as ObjectResult).Value as ApiResponse;

			var expected = 1;
			var actual = result.Links?.Count(l => l.Key == JsonApiConstants.SelfLinkKey);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void SelfLinkNotIncludedOnErrorTest()
		{
			var logger = new Mock<ILogger<SelfLinksFilterAttribute>>();
			var target = new SelfLinksFilterAttribute(logger.Object);

			var responseObject = ApiResponse.Create().WithError(error => error.Status = System.Net.HttpStatusCode.Forbidden);

			var context = CreateResultExecutingContext(responseObject);

			target.OnResultExecuting(context);

			var result = (context.Result as ObjectResult).Value as ApiResponse;

			var expected = 0;
			var actual = result.Links?.Count(l => l.Key == JsonApiConstants.SelfLinkKey) ?? 0;

			Assert.Equal(expected, actual);
		}


		[Fact]
		public void SelfLinkNotIncludedOnEmptyTest()
		{
			var logger = new Mock<ILogger<SelfLinksFilterAttribute>>();
			var target = new SelfLinksFilterAttribute(logger.Object);

			var responseObject = ApiResponse.Create();

			var context = CreateResultExecutingContext(responseObject);

			target.OnResultExecuting(context);

			var result = (context.Result as ObjectResult).Value as ApiResponse;

			var expected = 0;
			var actual = result.Links?.Count(l => l.Key == JsonApiConstants.SelfLinkKey) ?? 0;

			Assert.Equal(expected, actual);
		}

	}
}
