namespace mdryden.JsonApi
{
	public static class ResourceExtensions
	{
		public static Resource WithAttributes(this Resource resource, object attributesObject)
		{
			resource.Attributes = attributesObject;
			return resource;
		}
	}
}
