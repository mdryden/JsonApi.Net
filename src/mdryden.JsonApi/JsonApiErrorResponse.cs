using Newtonsoft.Json;

namespace mdryden.JsonApi.Models
{
    public class JsonApiErrorResponse : JsonApiResponse
    {
        [JsonProperty("errors")]
        public JsonApiError[] Errors { get; set; }
    }
}
