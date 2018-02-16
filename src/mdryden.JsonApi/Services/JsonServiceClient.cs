using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace mdryden.JsonApi.Services
{
    public class JsonServiceClient
	{
		private StringContent CreateContent(string jsonObject)
		{
			return new StringContent(jsonObject.ToString(), Encoding.UTF8, JsonApiConstants.ContentType);
		}

		private async Task<IApiResponse> TryReadResponseAsync(HttpResponseMessage responseMessage)
		{
			var json = await responseMessage.Content?.ReadAsStringAsync();

			if (json != null)
			{
				var response = JsonConvert.DeserializeObject<ApiResponse>(json);
				return response;
			}
			else
			{
				return ApiResponse.WithStatus(responseMessage.StatusCode).AsResponse();
			}
		}

		private async Task<IApiItemResponse> TryReadItemResponseAsync(HttpResponseMessage responseMessage)
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

		private async Task<IApiCollectionResponse> TryReadCollectionResponseAsync(HttpResponseMessage responseMessage)
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

		public async Task<IApiResponse> DeleteResourceAsync(Uri uri)
		{
			using (var client = new HttpClient())
			{
				var response = await client.DeleteAsync(uri);
				return await TryReadResponseAsync(response);
			}
		}

		public async Task<IApiItemResponse> GetResourceAsync(Uri uri)
		{
			using (var client = new HttpClient())
			{
				var result = await client.GetStringAsync(uri);
				return JsonConvert.DeserializeObject<ApiItemResponse>(result);
			}
		}

		public async Task<IApiCollectionResponse> GetResourceCollectionAsync(Uri uri)
		{
			using (var client = new HttpClient())
			{
				var result = await client.GetStringAsync(uri);
				return JsonConvert.DeserializeObject<ApiCollectionResponse>(result);
			}
		}

		public async Task<IApiItemResponse> PostResourceAsync<T>(Uri uri, T content)
		{
			var jsonObject = JsonConvert.SerializeObject(content);
			var stringContent = CreateContent(jsonObject);

			using (var client = new HttpClient())
			{
				var response = await client.PostAsync(uri, stringContent);
				return await TryReadItemResponseAsync(response);
			}
		}

		public async Task<IApiItemResponse> PutResourceAsync<T>(Uri uri, T content)
		{
			var jsonObject = JsonConvert.SerializeObject(content);
			var stringContent = CreateContent(jsonObject);

			using (var client = new HttpClient())
			{
				var response = await client.PutAsync(uri, stringContent);
				return await TryReadItemResponseAsync(response);
			}
		}
	}
}
