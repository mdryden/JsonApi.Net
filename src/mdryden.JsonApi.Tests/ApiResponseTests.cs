using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace mdryden.JsonApi.Tests
{
    public class ApiResponseTests
    {

		[Fact]
		public void NoErrorsWithDataTest()
		{
			var target = ApiResponse.OK();

			target.WithResource("mock", "mock value", "0");
			target.AddError(new Error());

			Assert.Throws<NotSupportedException>(() => target.AsItemResponse());

		}

		[Fact]
		public void NoDataWithErrorsTest()
		{
			var target = ApiResponse.OK();

			target.AddError(new Error());
			target.WithResource("mock value", "1");

			Assert.Throws<NotSupportedException>(() => target.AsItemResponse());
		}


    }
}
