using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mdryden.JsonApi
{
	public static class ApiResponseExtensions
	{
		public static void AddError(this ApiResponse response, Error error)
		{
			if (response.Errors == null)
			{
				response.Errors = new ErrorCollection();
			}

			response.Errors.Add(error);			
		}

		public static T GetResourceAs<T>(this ApiResponse response) where T : class
		{
			return (response.Data as Resource)?.Attributes as T;
		}

		public static ApiResponse WithError(this ApiResponse response, Action<Error> errorConfig)
		{
			var error = new Error();
			errorConfig.Invoke(error);
			response.AddError(error);
			return response;
		}

		public static ApiResponse WithLink(this ApiResponse response, string key, string href)
		{
			response.AddLink(key, href);
			return response;
		}

		public static ApiResponse WithLink(this ApiResponse response, string key, string href, Action<Link> linkConfig)
		{
			var link = new Link { Href = href };
			linkConfig.Invoke(link);
			response.AddLink(key, link);
			return response;
		}


		public static ApiResponse WithMeta(this ApiResponse response, string key, object value)
		{
			response.AddMeta(key, value);
			return response;
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
