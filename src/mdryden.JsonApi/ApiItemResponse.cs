using System.Collections.Generic;
using Newtonsoft.Json;

namespace mdryden.JsonApi
{
	public class ApiItemResponse : ApiResponse, IApiItemResponse
	{
		[JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
		public Resource Data { get; set; }
	}
}
