using mdryden.JsonApi.Filters;
using mdryden.JsonApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace mdryden.JsonApi.xUnit
{
    public class ApiKeyFilterAttributeTests
    {
        private ApiKeyFilterAttribute GetTarget(Guid key, bool revoked = false)
        {
            var apiKeys = new JsonApiKeyCollection
            {
                { new JsonApiKey { Key = key, Revoked = revoked, Details = "Unit test key" } }
            };

            return new ApiKeyFilterAttribute(Options.Create(apiKeys));
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
        public void MissingKeyAsyncTest()
        {
            var target = GetTarget(Guid.Empty);
            var context = CreateActionExecutingContext(QueryString.Empty);
            
            target.OnActionExecutionAsync(context, null).Wait();

            var expected = (int)HttpStatusCode.Forbidden;
            var actual = context.HttpContext.Response.StatusCode;

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void MissingKeyTest()
        {
            var target = GetTarget(Guid.Empty);
            var context = CreateActionExecutingContext(QueryString.Empty);

            target.OnActionExecuting(context);

            var expected = (int)HttpStatusCode.Forbidden;
            var actual = context.HttpContext.Response.StatusCode;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InvalidKeyAsyncTest()
        {
            var validKey = Guid.NewGuid();
            var target = GetTarget(validKey);
            var context = CreateActionExecutingContext(new QueryString($"?key={Guid.NewGuid().ToString()}"));
            
            target.OnActionExecutionAsync(context, null).Wait();

            var expected = (int)HttpStatusCode.Forbidden;
            var actual = context.HttpContext.Response.StatusCode;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InvalidKeyTest()
        {
            var validKey = Guid.NewGuid();
            var target = GetTarget(validKey);
            var context = CreateActionExecutingContext(new QueryString($"?key={Guid.NewGuid().ToString()}"));

            target.OnActionExecuting(context);

            var expected = (int)HttpStatusCode.Forbidden;
            var actual = context.HttpContext.Response.StatusCode;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void NotAGuidAsyncTest()
        {
            var validKey = Guid.NewGuid();
            var target = GetTarget(validKey);
            var context = CreateActionExecutingContext(new QueryString($"?key=12345"));

            target.OnActionExecutionAsync(context, null).Wait();

            var expected = (int)HttpStatusCode.Forbidden;
            var actual = context.HttpContext.Response.StatusCode;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void NotAGuidTest()
        {
            var validKey = Guid.NewGuid();
            var target = GetTarget(validKey);
            var context = CreateActionExecutingContext(new QueryString($"?key=123456"));

            target.OnActionExecuting(context);

            var expected = (int)HttpStatusCode.Forbidden;
            var actual = context.HttpContext.Response.StatusCode;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidKeyTest()
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
