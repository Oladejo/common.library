using Newtonsoft.Json;

namespace common.library.GenericAPILogger
{
    public class ApiLogs
    {
        [JsonProperty("platform")]
        public string? Platform { get; set; }

        [JsonProperty("client")]
        public Client? Client { get; set; }

        [JsonProperty("transaction")]
        public Transaction? Transaction { get; set; }

        [JsonProperty("request")]
        public Request? Request { get; set; }

        [JsonProperty("response")]
        public Response? Response { get; set; }
    }


    public class Client
    {
        [JsonProperty("client_id")]
        public string? ClientId { get; set; }

        [JsonProperty("client_name")]
        public string? ClientName { get; set; }

        [JsonProperty("client_app_id")]
        public string? ClientAppId { get; set; }

        [JsonProperty("client_app_name")]
        public string? ClientAppName { get; set; }
    }

    public class Transaction
    {
        [JsonProperty("transaction_ref")]
        public string? TransactionRef { get; set; }

        [JsonProperty("transaction_timestamp")]
        public DateTime TransactionTimestamp { get; set; }
    }

    public class Request
    {
        [JsonProperty("source_name")]
        public string? SourceName { get; set; }

        [JsonProperty("destination_name")]
        public string? DestinationName { get; set; }

        [JsonProperty("destination_url")]
        public string? DestinationUrl { get; set; }

        [JsonProperty("request_type")]
        public string? RequestType { get; set; }

        [JsonProperty("request_description")]
        public string? RequestDescription { get; set; }

        [JsonProperty("request_timestamp")]
        public DateTime RequestTimestamp { get; set; }

        [JsonProperty("request_headers")]
        public Dictionary<string, string>? RequestHeaders { get; set; }

        [JsonProperty("request_body")]
        public object? RequestBody { get; set; }
    }


    public class Response
    {
        [JsonProperty("response_timestamp")]
        public DateTime ResponseTimestamp { get; set; }

        [JsonProperty("response_http_status")]
        public int ResponseHttpStatus { get; set; }

        [JsonProperty("response_code")]
        public string? ResponseCode { get; set; }

        [JsonProperty("response_headers")]
        public dynamic? ResponseHeaders { get; set; }

        [JsonProperty("response_body")]
        public object? ResponseBody { get; set; }
    }

}
