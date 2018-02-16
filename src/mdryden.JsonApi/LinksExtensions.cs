namespace mdryden.JsonApi
{
	public static class IObjectWithLinksExtensions
	{
		public static void AddLink(this ILinks target, string key, object link)
		{
			if (target.Links == null)
			{
				target.Links = new LinkCollection();
			}
			target.Links.Add(key, link);
		}
	}
}
