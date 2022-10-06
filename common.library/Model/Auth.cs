using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace common.library.Model
{
    public class Auth
    {
        [JsonProperty("type")]
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonProperty("secure")]
        [JsonPropertyName("secure")]
        public string? Secure { get; set; }


        [JsonProperty("auth_provider")]
        [JsonPropertyName("auth_provider")]
        public string? Authprovider { get; set; }

        [JsonProperty("route_mode")]
        [JsonPropertyName("route_mode")]
        public string? RouteMode { get; set; }
    }
}
