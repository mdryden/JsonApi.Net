using mdryden.JsonApi.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace mdryden.JsonApi.Tests
{
    public class ApiExceptionFilterAttributeTests
    {
        
        private ApiExceptionFilterAttribute GetTarget()
        {
            var logger = new Mock<ILogger<ApiExceptionFilterAttribute>>();
			var defaultMetaDataRetriever = new Mock<IDefaultMetaDataRetriever>();

			return new ApiExceptionFilterAttribute(logger.Object, defaultMetaDataRetriever.Object);
        }

        private ExceptionContext GetContext(Exception ex)
        {
            var httpContext = new DefaultHttpContext();
            var context = new ExceptionContext(
                 new ActionContext
                 {
                     HttpContext = httpContext,
                     RouteData = new RouteData(),
                     ActionDescriptor = new ActionDescriptor(),
                 },
                 new List<IFilterMetadata>()
            )
            {
                Exception = ex
            };

            return context;
        }

        [Fact]
        public void GenericServerExceptionTest()
        {
            var exceptionHandlingAttribute = GetTarget();
            var context = GetContext(new Exception());

            exceptionHandlingAttribute.OnException(context);

            var expected = HttpStatusCode.InternalServerError;
			var actual = ((context.Result as ObjectResult)?.Value as IApiResponse)?.ResponseCode();

			Assert.Equal(expected, actual);
        }

        [Fact]
        public void HandledApiExceptionTest()
        {
            var apiException = new JsonApiException(HttpStatusCode.Forbidden);
       
            var exceptionHandlingAttribute = GetTarget();
            var context = GetContext(apiException);

            exceptionHandlingAttribute.OnException(context);

			var expected = apiException.Status;
			var actual = ((context.Result as ObjectResult)?.Value as IApiResponse)?.ResponseCode();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void DefaultMetaAddedToExceptions()
		{
			var logger = new Mock<ILogger<ApiExceptionFilterAttribute>>();
			var defaultMetaDataRetriever = new Mock<IDefaultMetaDataRetriever>();
			defaultMetaDataRetriever.Setup(r => r.GetDefaultMetaData()).Returns(new MetaCollection { { "mock", "meta" } });

			var target = new ApiExceptionFilterAttribute(logger.Object, defaultMetaDataRetriever.Object);

			var testException = new NotImplementedException();

			var context = GetContext(testException);

			target.OnException(context);

			var response = (context.Result as ObjectResult)?.Value as IApiResponse;

			Assert.Contains(response.Meta, m => m.Key == "mock");
			
		}
    }    
}
