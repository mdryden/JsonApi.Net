using mdryden.JsonApi.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace mdryden.JsonApi
{
    public class Error
    {
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }

		[JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
		[JsonConverter(typeof(HttpStatusConverter))]
		public HttpStatusCode? Status { get; set; }

        [JsonProperty("detail", NullValueHandling = NullValueHandling.Ignore)]
        public string Detail { get; set; }

        [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
        public object Source { get; set; }
    }
}
