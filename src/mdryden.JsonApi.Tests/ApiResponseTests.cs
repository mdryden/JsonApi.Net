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
			var target = ApiResponse.Create();

			target.WithResource("mock", "mock value", "0");

			Assert.Throws<NotSupportedException>(() => target.AddError(new Error()));

		}

		[Fact]
		public void NoDataWithErrorsTest()
		{
			var target = ApiResponse.Create();

			target.AddError(new Error());

			Assert.Throws<NotSupportedException>(() => target.Data = "mock value");
		}


    }
}
