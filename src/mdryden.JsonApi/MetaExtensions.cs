namespace mdryden.JsonApi
{
	public static class MetaExtensions
	{
		public static void AddMeta(this IMeta target, string key, object value)
		{
			if (target.Meta == null)
			{
				target.Meta = new MetaCollection();
			}

			target.Meta.Add(key, value);
		}

		public static void AddMeta(this IMeta target, MetaCollection meta)
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
