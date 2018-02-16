using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mdryden.JsonApi
{
	public static class ApiResponseResourceExtensions
	{

		public static T GetResourceAs<T>(this ApiResponse response)
		{
			var resource = response.Data as Resource;

			return (T)resource?.Attributes;
		}

		public static IEnumerable<T> GetResourceAsEnumerableOf<T>(this ApiResponse response)
		{
			return (response.Data as IEnumerable<Resource>)?.Select(item => (T)item.Attributes);
		}
		
		public static ApiResponse WithResource<T>(this ApiResponse response, T data, string id)
		{
			var type = typeof(T).Name.ToLower();
			return response.WithResource(type, data, id);
		}

		public static ApiResponse WithResource<T>(this ApiResponse response, string type, T data, string id)
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
		public static ApiResponse WithResources<T>(this ApiResponse response, IEnumerable<T> data, Func<T, string> getId)
		{
			var type = typeof(T).Name.ToLower();
			return response.WithResources(type, data, getId);
		}

		public static ApiResponse WithResources<T>(this ApiResponse response, string type, IEnumerable<T> data, Func<T, string> getId)
		{
			var resources = data?.Select(item => new Resource
			{
				Type = type,
				Id = getId(item),
				Attributes = item,
			});

			response.Data = resources;

			return response;
		}

	}
}
