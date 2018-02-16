using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mdryden.JsonApi
{
	public static class ApiResponseResourceExtensions
	{

		public static T GetResourceAs<T>(this IApiItemResponse response)
		{
			return (T)response.Data?.Attributes;
		}

		public static IEnumerable<T> GetResourcesAs<T>(this IApiCollectionResponse response)
		{
			return response.DataCollection?.Select(item => (T)item.Attributes);
		}
		
		public static ApiResponseBuilder WithResource<T>(this ApiResponseBuilder response, T data, string id)
		{
			var type = typeof(T).Name.ToLower();
			return response.WithResource(type, data, id);
		}

		public static ApiResponseBuilder WithResource<T>(this ApiResponseBuilder response, string type, T data, string id)
		{
			if (data == null)
			{
				response.Data = new NullResource();
			}
			else
			{
				var resource = new Resource
				{
					Type = type,
					Id = id,
					Attributes = data
				};

				response.Data = resource;
			}
			return response;
		}
		public static ApiResponseBuilder WithResources<T>(this ApiResponseBuilder response, IEnumerable<T> data, Func<T, string> getId)
		{
			var type = typeof(T).Name.ToLower();
			return response.WithResources(type, data, getId);
		}

		public static ApiResponseBuilder WithResources<T>(this ApiResponseBuilder response, string type, IEnumerable<T> data, Func<T, string> getId)
		{
			var resources = data?.Select(item => new Resource
			{
				Type = type,
				Id = getId(item),
				Attributes = item,
			});

			response.DataCollection = resources.ToList();

			return response;
		}

	}
}
