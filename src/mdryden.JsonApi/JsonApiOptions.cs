using System;
using System.Collections.Generic;
using System.Text;

namespace mdryden.JsonApi
{
    public class JsonApiOptions
    {
		public KeyCollection ApiKeys { get; private set; } = new KeyCollection();

		public Action<KeyCollection> ConfigureKeys
		{
			set
			{
				var keys = new KeyCollection();
				value?.Invoke(keys);
				ApiKeys = keys;
			}
		}

		public bool AddSelfLinks { get; set; } = true;
		
		public Action<MetaCollection> AddDefaultMetaData { get; set; }

	}
}
