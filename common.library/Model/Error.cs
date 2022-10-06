using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace common.library.Model
{
    public class Error
    {
        [JsonProperty("code")]
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }

}
