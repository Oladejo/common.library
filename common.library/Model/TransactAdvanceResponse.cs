using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace common.library.Model
{
    public class TransactAdvanceResponse
    {
        [JsonProperty("status")]
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonProperty("message")]
        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonProperty("data")]
        [JsonPropertyName("data")]
        public ResponseData? Data { get; set; }

        [JsonProperty("errors")]
        [JsonPropertyName("errors")]
        public List<Error>? Errors { get; set; }

        [JsonProperty("error")]
        [JsonPropertyName("error")]
        public Error? Error { get; set; }
    }
}
