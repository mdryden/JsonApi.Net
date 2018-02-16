using Newtonsoft.Json;
using System.Collections.Generic;

namespace mdryden.JsonApi
{
	public class ApiCollectionResponse : ApiResponse, IApiCollectionResponse
	{
		[JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
		public List<Resource> DataCollection { get; set; }
	}
}
