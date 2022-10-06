using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace common.library.Model
{
    public class CommonRequest
    {
        [JsonProperty("data")]
        [JsonPropertyName("data")]
        public string Data { get; set; }
    }

}
