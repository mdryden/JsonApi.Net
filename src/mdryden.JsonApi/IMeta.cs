using Newtonsoft.Json;

namespace mdryden.JsonApi
{
	public interface IMeta
	{
		[JsonProperty("meta", NullValueHandling = NullValueHandling.Ignore)]
		MetaCollection Meta { get; set; }
	}
}
