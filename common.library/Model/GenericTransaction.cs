using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace common.library.Model
{
    public class GenericTransaction
    {
        [JsonProperty("customer")]
        [JsonPropertyName("customer")]
        public Payer? Payer { get; set; }

        public string? Amount { get; set; }

        [JsonProperty("mock_mode")]
        [JsonPropertyName("mock_mode")]
        public string? MockMode { get; set; }

        [JsonProperty("transaction_ref")]
        [JsonPropertyName("transaction_ref")]
        public string? Transactionref { get; set; }

        [JsonProperty("transaction_ref_parent")]
        [JsonPropertyName("transaction_ref_parent")]
        public string? TransactionRefParent { get; set; }

        [JsonProperty("transaction_desc")]
        [JsonPropertyName("transaction_desc")]
        public string? Transactiondesc { get; set; }

        public Dictionary<string, string>? Meta { get; set; }

        public Dictionary<string, object>? Details { get; set; }

        [JsonProperty("client_info")]
        [JsonPropertyName("client_info")]
        public ClientInfo? ClientInfo { get; set; }

        [JsonProperty("app_info")]
        [JsonPropertyName("app_info")]
        public AppInfo? AppInfo { get; set; }


    }
}
