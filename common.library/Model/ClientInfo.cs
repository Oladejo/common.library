using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace common.library.Model
{
    public class ClientInfo
    {

        [JsonProperty("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonProperty("options")]
        [JsonPropertyName("options")]
        public List<string> Options { get; set; }

        [JsonProperty("bank_cbn_code")]
        [JsonPropertyName("bank_cbn_code")]
        public string BankCbnCode { get; set; }

        [JsonProperty("bank_name")]
        [JsonPropertyName("bank_name")]
        public string BankName { get; set; }

        [JsonProperty("console_url")]
        [JsonPropertyName("console_url")]
        public string ConsoleUrl { get; set; }

        [JsonProperty("js_background_image")]
        [JsonPropertyName("js_background_image")]
        public string JsBackgroundImage { get; set; }

        [JsonProperty("css_url")]
        [JsonPropertyName("css_url")]
        public string CssUrl { get; set; }

        [JsonProperty("logo_url")]
        [JsonPropertyName("logo_url")]
        public string LogoURL { get; set; }

        [JsonProperty("footer_text")]
        [JsonPropertyName("footer_text")]
        public string FooterText { get; set; }

        [JsonPropertyName("colors")]
        public Colors Colors { get; set; }
    }

    public class Colors
    {
        public string primary { get; set; }
        public string secondary { get; set; }
        public string primary_button { get; set; }
        public string modal_background { get; set; }
        public string payment_option { get; set; }
        public string payment_description { get; set; }
        public string payment_option_active { get; set; }
        public string app { get; set; }
        public string list_arrow { get; set; }
        public string list_arrow_hover { get; set; }
        public string list_hover_background { get; set; }
        public string list_hover_item { get; set; }
        public string loading_spinner { get; set; }
    }
}
