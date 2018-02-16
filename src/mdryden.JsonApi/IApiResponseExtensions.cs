using System.Net;

namespace mdryden.JsonApi
{
	public static class IApiResponseExtensions
	{
		public static bool IsResource(this IApiResponse response)
		{
			return response is IApiItemResponse || response is IApiCollectionResponse;
		}

		public static HttpStatusCode? ResponseCode(this IApiResponse response)
		{
			try
			{
				if (response.Meta.TryGetValue(JsonApiConstants.StatusCodeMetaKey, out object value))
				{
					return (HttpStatusCode)value;
				}
			}
			catch { }

			return null;
		}
	}
}
