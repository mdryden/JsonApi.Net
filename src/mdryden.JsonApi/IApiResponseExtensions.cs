namespace mdryden.JsonApi
{
	public static class IApiResponseExtensions
	{
		public static bool IsResource(this IApiResponse response)
		{
			return response is IApiItemResponse || response is IApiCollectionResponse;
		}
	}
}
