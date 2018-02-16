using Newtonsoft.Json;

namespace mdryden.JsonApi
{
	public interface IResource
	{
		[JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
		Resource Data { get; set; }
	}
}