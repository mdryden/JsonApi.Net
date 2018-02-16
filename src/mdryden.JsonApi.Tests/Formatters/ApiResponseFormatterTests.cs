using mdryden.JsonApi.Formatters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Xunit;

namespace mdryden.JsonApi.Tests
{
    public class ApiResponseFormatterTests
    {
		[Fact]
		public void ContentTypeIsSet()
		{
			var target = new ApiResponseFormatter();

			var responseObject = ApiResponse.OK().WithResource("mock", "mock data", "0").AsItemResponse();

			var httpContext = new DefaultHttpContext();

			var formatterContext = new OutputFormatterWriteContext(
				httpContext,
				(stream, encoding) => new StreamWriter(stream, encoding),
				typeof(ApiResponse),
				responseObject);


			target.WriteAsync(formatterContext);

			var expected = JsonApiConstants.ContentType;
			var actual = httpContext.Response.ContentType;

			Assert.Equal(expected, actual);
		}
		
		[Theory]
		[InlineData(HttpStatusCode.Accepted, (int)HttpStatusCode.Accepted)]
		[InlineData(HttpStatusCode.OK, (int)HttpStatusCode.OK)]
		[InlineData(HttpStatusCode.Forbidden, (int)HttpStatusCode.Forbidden)]
		public void HttpStatusCodeIsSet(HttpStatusCode responseCode, int expected)
		{
			var target = new ApiResponseFormatter();

			var responseObject = ApiResponse.WithStatus(responseCode).AsResponse();

			var httpContext = new DefaultHttpContext();

			var formatterContext = new OutputFormatterWriteContext(
				httpContext,
				(stream, encoding) => new StreamWriter(stream, encoding),
				typeof(ApiResponse),
				responseObject);


			target.WriteAsync(formatterContext);

			var actual = httpContext.Response.StatusCode;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void NoJsonWhenEmptyIsSet()
		{
			var target = new ApiResponseFormatter();

			var responseObject = ApiResponse.OK().WithMeta("mock", "meta data").AsResponse(); ;

			var httpContext = new DefaultHttpContext();
			httpContext.Response.Body = new MemoryStream();

			var formatterContext = new OutputFormatterWriteContext(
				httpContext,
				(stream, encoding) => new StreamWriter(stream, encoding),
				typeof(ApiResponse),
				responseObject);


			target.WriteAsync(formatterContext);
			
			var expected = 0;
			var actual = httpContext.Response.Body.Length;

			Assert.Equal(expected, actual);
		}


		[Fact]
		public void JsonWhenContentExists()
		{
			var target = new ApiResponseFormatter();

			var responseObject = ApiResponse.OK().WithResource<object>("person", null, "1").AsItemResponse(); ;

			var httpContext = new DefaultHttpContext();
			httpContext.Response.Body = new MemoryStream();

			var formatterContext = new OutputFormatterWriteContext(
				httpContext,
				(stream, encoding) => new StreamWriter(stream, encoding),
				typeof(ApiResponse),
				responseObject);


			target.WriteAsync(formatterContext);

			var expected = true;
			var actual = httpContext.Response.Body.Length > 0;

			Assert.Equal(expected, actual);
		}

	}
}
