using mdryden.JsonApi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SampleWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SampleWebApi.Controllers
{
	[Route("client")]
    public class TestClientController : Controller
    {

		[HttpGet("")]
		public async Task<TestObject> GetAsync()
		{
			var url = "http://localhost:9456/test/item/1";
			using (var client = new HttpClient())
			{
				var result = await client.GetStringAsync(url);
				var response = JsonConvert.DeserializeObject<ApiItemResponse>(result);
				var item = response.GetResourceAs<TestObject>();
				return item;
			}
		}

    }
}
