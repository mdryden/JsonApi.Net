using mdryden.JsonApi;
using mdryden.JsonApi.Services;
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
			var url = "http://localhost:59553/test/item/1";
			using (var client = new JsonServiceClient())
			{
				var response = await client.GetResourceAsync(url);
				var item = response.GetResourceAs<TestObject>();
				return item;
			}
		}

		[HttpGet("error")]
		public async Task<TestObject> GetErrorAsync()
		{
			var url = "http://localhost:59553/test/error";
			using (var client = new JsonServiceClient())
			{
				var response = await client.GetResourceAsync(url);
				var item = response.GetResourceAs<TestObject>();
				return item;
			}
		}

	}
}
