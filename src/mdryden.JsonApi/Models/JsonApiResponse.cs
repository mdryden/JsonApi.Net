using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mdryden.JsonApi.Models
{
    public abstract class JsonApiResponse
    {
        [JsonProperty("meta")]
        public IDictionary<string, object> Meta { get; set; }
    }
}
