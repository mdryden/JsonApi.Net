using System;
using System.Collections.Generic;
using System.Text;

namespace mdryden.JsonApi
{
	public class DefaultMetaDataRetriever : IDefaultMetaDataRetriever
	{
		private Action<MetaCollection> action;

		public DefaultMetaDataRetriever(Action<MetaCollection> action)
		{
			this.action = action;
		}

		public void AppendDefaultMetaData(MetaCollection metaData)
		{
			action?.Invoke(metaData);
		}

		public MetaCollection GetDefaultMetaData()
		{
			if (action == null)
			{
				return null;
			}

			var meta = new MetaCollection();
			action.Invoke(meta);
			return meta;
		}
	}
}
