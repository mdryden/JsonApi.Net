namespace mdryden.JsonApi
{
	public static class IObjectWithMetaExtensions
	{
		public static void AddMeta(this IObjectWithMeta target, string key, object value)
		{
			if (target.Meta == null)
			{
				target.Meta = new MetaCollection();
			}

			target.Meta.Add(key, value);
		}

		public static void AddMeta(this IObjectWithMeta target, MetaCollection meta)
		{
			if (meta == null)
			{
				return;
			}

			foreach (var item in meta)
			{
				target.AddMeta(item.Key, item.Value);

			}
		}
	}
}
