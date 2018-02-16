using mdryden.JsonApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SampleWebApi.Controllers
{
	[Route("test")]
	public class TestController : Controller
	{
		private class TestObject
		{
			public int Id { get; set; }

			public string Value { get; set; }
		}

		private readonly TestObject[] items = new[]
		{
			new TestObject { Id = 1, Value = "One" },
			new TestObject { Id = 2, Value = "Two" },
			new TestObject { Id = 3, Value = "Three" }
		};

		[HttpGet("item")]
		public ApiResponse ItemArrayTest()
		{
			//return ApiResponse2.WithData(values).WithMeta("test", "some meta value");
			var response = ApiResponse.Create().WithResources("testObject", items, (item) => item.Id.ToString()).WithMeta("test", "some meta value").WithLink("related", "/test/[value]", (config) =>
			{
				config.Meta = new Dictionary<string, object>
				{
					{ "count", items.Count() }
				};
			});

			return response;
		}

		[HttpGet("item/{id}")]
		public ApiResponse ItemTest(int id)
		{
			var item = items.FirstOrDefault(i => i.Id == id);

			return ApiResponse.Create().WithResource(item, item.Id.ToString());
		}

		[HttpGet("noitems")]
		public ApiResponse NoItemsTest()
		{
			var items = new List<TestObject>();

			return ApiResponse.Create().WithResources(items, (input) => input.Id.ToString());
		}

		[HttpDelete("delete/{value}")]
		public ApiResponse Delete(string value)
		{
			if (value == "good")
			{
				return ApiResponse.Create(HttpStatusCode.OK);
			}

			if (value == "deleted")
			{
				return ApiResponse.Create(HttpStatusCode.NoContent);
			}

			var response = ApiResponse.Create(HttpStatusCode.Forbidden).WithError(error =>
			{
				error.WithCode("not-allowed");
				error.WithDetail("This magic value can't be deleted");
			});

			return response;
		}

		[HttpPost("{value}")]
		public ApiResponse Post(string value)
		{
			return ApiResponse.Create(HttpStatusCode.Forbidden).WithError(error =>
			{
				error.WithDetail("Can't do that");
			});
		}


	}
}
