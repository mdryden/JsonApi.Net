using mdryden.JsonApi.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace mdryden.JsonApi.Tests
{
    public class ApiKeyFilterAttributeTests
    {
        private ApiKeyFilterAttribute GetTarget(Guid key, bool revoked = false)
        {
            var apiKeys = new KeyCollection
            {
                { new ApiKey { Key = key, Revoked = revoked, Details = "Unit test key" } }
            };

            var logger = new Mock<ILogger<ApiKeyFilterAttribute>>();
            return new ApiKeyFilterAttribute(logger.Object, apiKeys);
        }

        private ActionExecutingContext CreateActionExecutingContext(QueryString queryString)
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.QueryString = queryString;

            var context = new ActionExecutingContext(
                new ActionContext
                {
                    HttpContext = httpContext,
                    RouteData = new RouteData(),
                    ActionDescriptor = new ActionDescriptor(),
                },
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                new Mock<Controller>().Object);

            return context;
        }
		        
        [Fact]
        public void MissingKeyTest()
        {
            var target = GetTarget(Guid.Empty);
            var context = CreateActionExecutingContext(QueryString.Empty);

            target.OnActionExecuting(context);

            var expected = HttpStatusCode.Forbidden;
			var actual = ((context.Result as ObjectResult)?.Value as ApiResponse)?.ResponseCode;

			Assert.Equal(expected, actual);
        }

        [Fact]
        public void InvalidKeyTest()
        {
            var validKey = Guid.NewGuid();
            var target = GetTarget(validKey);
            var context = CreateActionExecutingContext(new QueryString($"?key={Guid.NewGuid().ToString()}"));

            target.OnActionExecuting(context);

            var expected = HttpStatusCode.Forbidden;
			var actual = ((context.Result as ObjectResult)?.Value as ApiResponse)?.ResponseCode;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void NotAGuidTest()
        {
            var validKey = Guid.NewGuid();
            var target = GetTarget(validKey);
            var context = CreateActionExecutingContext(new QueryString($"?key=123456"));

            target.OnActionExecuting(context);

            var expected = HttpStatusCode.Forbidden;
			var actual = ((context.Result as ObjectResult)?.Value as ApiResponse)?.ResponseCode;

			Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidKeyTest()
        {
            var validKey = Guid.NewGuid();
            var target = GetTarget(validKey);
            var context = CreateActionExecutingContext(new QueryString($"?key={validKey.ToString()}"));

            target.OnActionExecuting(context);

            var expected = false;
			var actual = ((context.Result as ObjectResult)?.Value as ApiResponse)?.HasErrors() == true;

			Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidKeyNoBracesTest()
        {
            var validKey = Guid.NewGuid();
            var target = GetTarget(validKey);
            var context = CreateActionExecutingContext(new QueryString($"?key={validKey.ToString()}"));

            target.OnActionExecuting(context);

            var expected = (int)HttpStatusCode.OK;
            var actual = context.HttpContext.Response.StatusCode;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void QueryStringKeyNotCaseSensitiveTest()
        {
            var validKey = Guid.NewGuid();
            var target = GetTarget(validKey);
            var context = CreateActionExecutingContext(new QueryString($"?KEY={validKey.ToString()}"));

            target.OnActionExecuting(context);

            var expected = (int)HttpStatusCode.OK;
            var actual = context.HttpContext.Response.StatusCode;

            Assert.Equal(expected, actual);
        }
    }
}
