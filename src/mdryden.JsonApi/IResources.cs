using Newtonsoft.Json;
using System.Collections.Generic;

namespace mdryden.JsonApi
{
	public interface IResources
	{
		[JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
		List<Resource> DataCollection
		{
			get; set;
		}
	}
}