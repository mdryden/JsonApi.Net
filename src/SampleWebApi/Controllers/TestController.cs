using mdryden.JsonApi;
using Microsoft.AspNetCore.Mvc;
using SampleWebApi.Models;
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
		private readonly TestObject[] items = new[]
		{
			new TestObject { Id = 1, Value = "One" },
			new TestObject { Id = 2, Value = "Two" },
			new TestObject { Id = 3, Value = "Three" }
		};

		[HttpGet("item")]
		public IApiCollectionResponse ItemArrayTest()
		{
			//return ApiResponse2.WithData(values).WithMeta("test", "some meta value");
			var response = ApiResponse.OK().WithResources("testObject", items, (item) => item.Id.ToString()).WithMeta("test", "some meta value").WithLink("related", "/test/[value]", (config) =>
			{
				config.Meta = new Dictionary<string, object>
				{
					{ "count", items.Count() }
				};
			});

			return response.AsCollectionResponse(); ;
		}

		[HttpGet("item/{id}")]
		public IApiItemResponse ItemTest(int id)
		{
			var item = items.FirstOrDefault(i => i.Id == id);

			return ApiResponse.OK().WithResource(item, item.Id.ToString()).AsItemResponse();
		}

		[HttpGet("noitems")]
		public IApiCollectionResponse NoItemsTest()
		{
			var items = new List<TestObject>();

			return ApiResponse.OK().WithResources(items, (input) => input.Id.ToString()).AsCollectionResponse();
		}

		[HttpDelete("delete/{value}")]
		public IApiResponse Delete(string value)
		{
			if (value == "good")
			{
				return ApiResponse.OK().AsResponse();
			}

			if (value == "deleted")
			{
				return ApiResponse.NoContent().AsResponse(); ;
			}

			var response = ApiResponse.Forbidden().WithError(error =>
			{
				error.WithCode("not-allowed");
				error.WithDetail("This magic value can't be deleted");
			});

			return response.AsResponse(); ;
		}

		[HttpPost("post")]
		public IApiResponse Post(string value)
		{
			return ApiResponse.Forbidden().WithError(error =>
			{
				error.WithDetail("Can't do that");
			}).AsResponse(); ;
		}

		[HttpPost("post/andreturn/item")]
		public IApiItemResponse PostAndReturn([FromBody]object value)
		{
			return ApiResponse.Created().WithResource(value, "1").AsItemResponse();
		}

		[HttpPost("post/andreturn/collection")]
		public IApiCollectionResponse PostAndReturnCollection([FromBody]object value)
		{
			return ApiResponse.Created().WithResources(new[] { value }, (item) => "1").AsCollectionResponse();
		}


		[HttpPut("put")]
		public IApiResponse Put(string value)
		{
			return ApiResponse.Forbidden().WithError(error =>
			{
				error.WithDetail("Can't do that");
			}).AsResponse(); ;
		}

		[HttpPut("put/andreturn/item")]
		public IApiItemResponse PutAndReturn([FromBody]object value)
		{
			return ApiResponse.Created().WithResource(value, "1").AsItemResponse();
		}

		[HttpPut("put/andreturn/collection")]
		public IApiCollectionResponse PutAndReturnCollection([FromBody]object value)
		{
			return ApiResponse.Created().WithResources(new[] { value }, (item) => "1").AsCollectionResponse();
		}

		[HttpGet("error")]
		public IApiItemResponse GetError()
		{
			ModelState.AddModelError("", "general error");
			return ApiResponse.BadRequest().WithErrors(ModelState).AsItemResponse();

		}
		[HttpGet("exception")]
		public ApiResponse Get()
		{
			throw new NotImplementedException();
		}

	}
}
