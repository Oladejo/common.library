using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace common.library.Model
{
    public class AppInfo
    {
        [JsonProperty("name")]
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonProperty("code")]
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonProperty("id")]
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonProperty("beneficiary_account_no")]
        [JsonPropertyName("beneficiary_account_no")]
        public string? BeneficiaryAccountNo { get; set; }

        [JsonProperty("extras")]
        [JsonPropertyName("extras")]
        public Dictionary<string, string>? Extras { get; set; }
    }
}
