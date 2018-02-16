using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace mdryden.JsonApi.Tests
{
	public class ApiResponseResourceExtensionTests
	{
		private class MockObject
		{
			public string Id { get; set; }
			public string Value { get; set; }
		}

		private struct MockStruct
		{
			public string Id { get; set; }

			public string Value { get; set; }
		}

		[Fact]
		public void GetResourceAsClassTest()
		{
			var input = new MockObject { Id = "1", Value = "Mock" };

			var response = ApiResponse.OK().WithResource(input, input.Id).AsItemResponse();

			var output = response.GetResourceAs<MockObject>();

			Assert.Equal(input.Id, output.Id);
		}

		[Fact]
		public void GetResourceAsStructTest()
		{
			var input = new MockStruct { Id = "1", Value = "Mock" };

			var response = ApiResponse.OK().WithResource(input, input.Id).AsItemResponse();

			var output = response.GetResourceAs<MockStruct>();

			Assert.Equal(input.Id, output.Id);
		}

		[Fact]
		public void GetResourceAsStructArrayTest()
		{
			var input1 = new MockStruct { Id = "1", Value = "Mock" };
			var input2 = new MockStruct { Id = "2", Value = "Mock 2" };
			var input = new[] { input1, input2 };

			var response = ApiResponse.OK().WithResources(input, (item) => item.Id).AsCollectionResponse();

			var output = response.GetResourcesAs<MockStruct>();

			Assert.Equal(input.Length, output.Count());
		}

		[Fact]
		public void GetEmptyResourceAsStructArrayTest()
		{
			var input = new List<MockStruct>();

			var response = ApiResponse.OK().WithResources(input, (item) => item.Id).AsCollectionResponse();

			var output = response.GetResourcesAs<MockStruct>();

			Assert.Equal(input.Count, output.Count());
		}

	}
}
