using Newtonsoft.Json;

namespace mdryden.JsonApi
{
	public interface ILinks
	{
		[JsonProperty("links", NullValueHandling = NullValueHandling.Ignore)]
		LinkCollection Links { get; set; }
	}
}
