using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace mdryden.JsonApi
{
	public class Link
	{
		[JsonProperty("href")]
		public string Href { get; set; }

		[JsonProperty("meta")]
		public Dictionary<string, object> Meta { get; set; }
    }
}
