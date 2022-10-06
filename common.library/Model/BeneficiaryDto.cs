using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace common.library.Model
{
    public class BeneficiaryDto
    {
        [JsonProperty("firstname")]
        [JsonPropertyName("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("surname")]
        [JsonPropertyName("surname")]
        public string Surname { get; set; }

        [JsonProperty("email")]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonProperty("mobile_no")]
        [JsonPropertyName("mobile_no")]
        public string MobileNo { get; set; }

        [JsonProperty("beneficiary_id")]
        [JsonPropertyName("beneficiary_id")]
        public string Beneficiaryid { get; set; }

        [JsonProperty("account_bank_code")]
        [JsonPropertyName("account_bank_code")]
        public string AccountBankCode { get; set; }

        [JsonProperty("account_bank_name")]
        [JsonPropertyName("account_bank_name")]
        public string AccountBankName { get; set; }

        [JsonProperty("account_number")]
        [JsonPropertyName("account_number")]
        public string AccountNumber { get; set; }

        [JsonProperty("account_type")]
        [JsonPropertyName("account_type")]
        public string AccountType { get; set; }

        [JsonProperty("account_name")]
        [JsonPropertyName("account_name")]
        public string AccountName { get; set; }
    }
}
