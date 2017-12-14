using Newtonsoft.Json;

namespace mdryden.JsonApi.Models
{
    public class JsonApiDataResponse<T> : JsonApiResponse
    {
        [JsonProperty("data")]
        public T Data { get; set; }       
    }
}
