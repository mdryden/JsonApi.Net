using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mbsoft.JsonApi.Models
{
    public class JsonApiError
    {
        [JsonProperty("code")]
        public int? Code { get; set; }

        [JsonProperty("detail")]
        public string Detail { get; set; }

        [JsonProperty("source")]
        public object Source { get; set; }
    }
}
