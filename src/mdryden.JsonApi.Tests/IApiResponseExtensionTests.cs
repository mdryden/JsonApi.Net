using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace mdryden.JsonApi.Tests
{
    public class IApiResponseExtensionTests
    {
		[Fact]
		public void GetResponseCodeTest()
		{
			var expected = HttpStatusCode.OK;
			var response = ApiResponse.WithStatus(expected).AsResponse();

			var actual = response.ResponseCode();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void GetResponseCodeAfterSerializationTest()
		{
			var expected = HttpStatusCode.OK;
			var response = ApiResponse.WithStatus(expected).AsResponse();

			var serialized = JsonConvert.SerializeObject(response);
			var deserialized = JsonConvert.DeserializeObject<ApiResponse>(serialized);

			var actual = deserialized.ResponseCode();

			Assert.Equal(expected, actual);
		}
	}
}
