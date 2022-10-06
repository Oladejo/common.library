using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace common.library.Model
{
    public class Payer
    {
        [JsonProperty("customer_ref")]
        [JsonPropertyName("customer_ref")]
        public string? Customerref { get; set; }

        [JsonProperty("firstname")]
        [JsonPropertyName("firstname")]
        public string? Firstname { get; set; }

        [JsonProperty("surname")]
        [JsonPropertyName("surname")]
        public string? Surname { get; set; }

        [JsonProperty("email")]
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonProperty("mobile_no")]
        [JsonPropertyName("mobile_no")]
        public string? Mobileno { get; set; }
    }
}
