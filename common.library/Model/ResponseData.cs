using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace common.library.Model
{
    public class ResponseData
    {
        [JsonProperty("provider_response_code")]
        [JsonPropertyName("provider_response_code")]
        public string? ProviderResponseCode { get; set; }


        [JsonProperty("status")]
        [JsonPropertyName("status")]
        public string? Status { get; set; }


        [JsonProperty("provider")]
        [JsonPropertyName("provider")]
        public string? Provider { get; set; }


        [JsonProperty("reference")]
        [JsonPropertyName("reference")]
        public string? Reference { get; set; }


        [JsonProperty("provider_response")]
        [JsonPropertyName("provider_response")]
        public Dictionary<string, object>? ProviderResponse { get; set; }


        [JsonProperty("errors")]
        [JsonPropertyName("errors")]
        public List<Error>? Errors { get; set; }


        [JsonProperty("error")]
        [JsonPropertyName("error")]
        public Error? Error { get; set; }


        [JsonProperty("charge_token")]
        [JsonPropertyName("charge_token")]
        public string? ChargeToken { get; set; }

    }

}
