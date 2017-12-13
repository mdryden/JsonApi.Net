using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mbsoft.JsonApi.Models
{
    public class JsonApiResponse<T>
    {
        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("errors")]
        public JsonApiError[] Errors { get; set; }


        [JsonProperty("meta")]
        public IDictionary<string, object> Meta { get; set; } 
    }
}
