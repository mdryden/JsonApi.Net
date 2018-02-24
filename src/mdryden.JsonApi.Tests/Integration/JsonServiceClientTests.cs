using mdryden.JsonApi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using SampleWebApi;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace mdryden.JsonApi.Tests.Integration
{
    public class JsonServiceClientTests
    {

		private readonly TestServer _server;
		private readonly JsonServiceClient _client;

		public JsonServiceClientTests()
		{
			_server = new TestServer(new WebHostBuilder()
				.UseStartup<Startup>());

			_client = new JsonServiceClient(_server.CreateClient());

		}

		[Fact]
		public async void ItemArrayTest()
		{
			var response = await _client.GetResourceCollectionAsync("test/item");
			var items = response.GetResourcesAs<dynamic>();

			Assert.NotEmpty(items);
		}

		[Fact]
		public async void ItemTest()
		{
			var response = await _client.GetResourceAsync("test/item/1");
			var item = response.GetResourceAs<dynamic>();

			Assert.NotNull(item);
		}

		[Fact]
		public async void NoItemsTest()
		{
			var response = await _client.GetResourceCollectionAsync("test/noitems");

			var responseCode = response.ResponseCode();
			var expected = HttpStatusCode.OK;

			Assert.Equal(responseCode, expected);
		}

		[Fact]
		public async void DeleteGood()
		{
			var response = await _client.DeleteResourceAsync("test/delete/good");

			var expected = HttpStatusCode.OK;

			Assert.Equal(expected, response.ResponseCode());
		}


		[Fact]
		public async void DeleteDeleted()
		{
			var response = await _client.DeleteResourceAsync("test/delete/deleted");

			var expected = HttpStatusCode.NoContent;

			Assert.Equal(expected, response.ResponseCode());
		}


		[Fact]
		public async void DeleteInvalid()
		{
			var response = await _client.DeleteResourceAsync("test/delete/other");

			var expected = HttpStatusCode.Forbidden;

			Assert.Equal(expected, response.ResponseCode());
		}

		[Fact]
		public async void Post()
		{
			var response = await _client.PostResourceAsync("test/post", "value");

			var expected = HttpStatusCode.Forbidden;
			Assert.Equal(expected, response.ResponseCode());
		}
		
		[Fact]
		public async void PostAndReturnItem()
		{
			var response = await _client.PostResourceWithItemResponseAsync("test/post/andreturn/item", "value");

			var item = response.GetResourceAs<dynamic>();

			Assert.NotNull(item);
		}

		[Fact]
		public async void PostAndReturnCollection()
		{
			var response = await _client.PostResourceWithCollectionResponseAsync("test/post/andreturn/collection", "value");

			var item = response.GetResourcesAs<dynamic>();

			Assert.NotNull(item);
		}

		[Fact]
		public async void Put()
		{
			var response = await _client.PutResourceAsync("test/put", "value");

			var expected = HttpStatusCode.Forbidden;
			Assert.Equal(expected, response.ResponseCode());
		}

		[Fact]
		public async void PutAndReturnItem()
		{
			var response = await _client.PutResourceWithItemResponseAsync("test/put/andreturn/item", "value");

			var item = response.GetResourceAs<dynamic>();

			Assert.NotNull(item);
		}

		[Fact]
		public async void PutAndReturnCollection()
		{
			var response = await _client.PutResourceWithCollectionResponseAsync("test/put/andreturn/collection", "value");

			var item = response.GetResourcesAs<dynamic>();

			Assert.NotNull(item);
		}

		[Fact]
		public async void GetError()
		{
			var response = await _client.GetResourceAsync("test/error");

			var expected = HttpStatusCode.BadRequest;

			Assert.Equal(expected, response.ResponseCode());
		}

		[Fact]
		public async void GetException()
		{
			var response = await _client.GetResourceAsync("test/exception");

			var expected = HttpStatusCode.InternalServerError;

			Assert.Equal(expected, response.ResponseCode());
		}
	}
}
