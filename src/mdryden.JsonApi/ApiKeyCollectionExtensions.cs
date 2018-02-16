using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using mdryden.JsonApi;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class KeyCollectionExtensions
	{
		public static void ConfigureFrom(this KeyCollection keys, IConfigurationSection section)
		{
			var newKeys = section.Get<KeyCollection>();
			if (newKeys != null)
			{
				keys.AddRange(newKeys);
			}
		}
	}
}
