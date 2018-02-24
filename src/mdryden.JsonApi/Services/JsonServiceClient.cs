using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace mdryden.JsonApi.Services
{
	public class JsonServiceClient : IDisposable
	{

		private readonly HttpClient _client;
		
		public JsonServiceClient()
		{
			_client = new HttpClient();
		}

		public JsonServiceClient(Uri baseAddress)
			: this()
		{
			_client.BaseAddress = baseAddress;
		}

		public JsonServiceClient(HttpClient client)
		{
			_client = client;
		}

		private StringContent CreateContent(string jsonObject)
		{
			return new StringContent(jsonObject.ToString(), Encoding.UTF8, JsonApiConstants.ContentType);
		}

		private async Task<IApiResponse> TryReadResponseAsync(HttpResponseMessage responseMessage)
		{
			var json = await responseMessage.Content?.ReadAsStringAsync();

			if (!string.IsNullOrEmpty(json))
			{
				var response = JsonConvert.DeserializeObject<ApiResponse>(json);
				return response;
			}
			else
			{
				return ApiResponse.WithStatus(responseMessage.StatusCode).AsResponse();
			}
		}

		protected async Task<IApiItemResponse> TryReadItemResponseAsync(HttpResponseMessage responseMessage)
		{

			var json = await responseMessage.Content?.ReadAsStringAsync();

			if (json != null)
			{
				var response = JsonConvert.DeserializeObject<ApiItemResponse>(json);
				return response;
			}
			else
			{
				return ApiResponse.WithStatus(responseMessage.StatusCode).AsItemResponse();
			}
		}

		protected async Task<IApiCollectionResponse> TryReadCollectionResponseAsync(HttpResponseMessage responseMessage)
		{
			var json = await responseMessage.Content?.ReadAsStringAsync();

			if (json != null)
			{
				var response = JsonConvert.DeserializeObject<ApiCollectionResponse>(json);
				return response;
			}
			else
			{
				return ApiResponse.WithStatus(responseMessage.StatusCode).AsCollectionResponse();
			}
		}

		public async Task<IApiResponse> DeleteResourceAsync(string requestUri)
		{
			var response = await _client.DeleteAsync(requestUri);
			return await TryReadResponseAsync(response);
		}

		public async Task<IApiItemResponse> GetResourceAsync(string requestUri)
		{
			var response = await _client.GetAsync(requestUri);
			return await TryReadItemResponseAsync(response);

		}

		public async Task<IApiCollectionResponse> GetResourceCollectionAsync(string requestUri)
		{
			var response = await _client.GetAsync(requestUri);
			return await TryReadCollectionResponseAsync(response);
		}

		public async Task<IApiResponse> PostResourceAsync<T>(string requestUri, T content)
		{
			var jsonObject = JsonConvert.SerializeObject(content);
			var stringContent = CreateContent(jsonObject);

			var response = await _client.PostAsync(requestUri, stringContent);
			return await TryReadResponseAsync(response);
		}

		public async Task<IApiItemResponse> PostResourceWithItemResponseAsync<T>(string requestUri, T content)
		{
			var jsonObject = JsonConvert.SerializeObject(content);
			var stringContent = CreateContent(jsonObject);

			var response = await _client.PostAsync(requestUri, stringContent);
			return await TryReadItemResponseAsync(response);
		}

		public async Task<IApiCollectionResponse> PostResourceWithCollectionResponseAsync<T>(string requestUri, T content)
		{
			var jsonObject = JsonConvert.SerializeObject(content);
			var stringContent = CreateContent(jsonObject);

			var response = await _client.PostAsync(requestUri, stringContent);
			return await TryReadCollectionResponseAsync(response);
		}

		public async Task<IApiResponse> PutResourceAsync<T>(string requestUri, T content)
		{
			var jsonObject = JsonConvert.SerializeObject(content);
			var stringContent = CreateContent(jsonObject);

			var response = await _client.PutAsync(requestUri, stringContent);
			return await TryReadResponseAsync(response);
		}

		public async Task<IApiItemResponse> PutResourceWithItemResponseAsync<T>(string requestUri, T content)
		{
			var jsonObject = JsonConvert.SerializeObject(content);
			var stringContent = CreateContent(jsonObject);

			var response = await _client.PutAsync(requestUri, stringContent);
			return await TryReadItemResponseAsync(response);
		}

		public async Task<IApiCollectionResponse> PutResourceWithCollectionResponseAsync<T>(string requestUri, T content)
		{
			var jsonObject = JsonConvert.SerializeObject(content);
			var stringContent = CreateContent(jsonObject);

			var response = await _client.PutAsync(requestUri, stringContent);
			return await TryReadCollectionResponseAsync(response);
		}

		public void Dispose()
		{
			_client.Dispose();
		}
	}
}
