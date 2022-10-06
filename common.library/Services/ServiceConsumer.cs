using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using common.library.Exception;
using System.Net.Http.Headers;
using System.Text;

namespace common.library.Services
{
    public class ServiceConsumer
    {
        private readonly IHttpClientFactory _clientFactory;
        private ILogger<ServiceConsumer> _logger;
        private string _clientName;

        public ServiceConsumer(IHttpClientFactory clientFactory, ILogger<ServiceConsumer> logger, string clientName)
        {
            _clientFactory = clientFactory;
            _logger = logger;
            _clientName = clientName;
        }


        public async Task<Tuple<T, HttpResponseMessage>> Get<T>(string endpoint, Dictionary<string, string> headers, Dictionary<string, string> parameters) where T : new()
        {
            var httpClient = _clientFactory.CreateClient(_clientName);

            httpClient.Timeout = new TimeSpan(0, 2, 0);
            httpClient.MaxResponseContentBufferSize = 2147483647L;
            var _parameters = new List<string>();
            if (parameters != null && parameters.Count > 0)
            {
                endpoint += "?";

                foreach (var param in parameters)
                {
                    _parameters.Add(string.Concat(param.Key + "=", param.Value));
                }

                endpoint += string.Join("&", _parameters);
            }


            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);

            try
            {
                if (headers != null)
                    foreach (var (key, value) in headers)
                    {
                        request.Headers.Add(key, value);
                    }

                var response = httpClient.SendAsync(request).Result;
                if (!response.IsSuccessStatusCode)
                {
                    var r = response.Content.ReadAsStringAsync().Result;
                    r = XMLToJsonExtension.ConvertXMLToJson(r);
                    return new Tuple<T, HttpResponseMessage>(JsonConvert.DeserializeObject<T>(r), response);
                }
                var json = response.Content.ReadAsStringAsync().Result;
                json = XMLToJsonExtension.ConvertXMLToJson(json);
                return new Tuple<T, HttpResponseMessage>(JsonConvert.DeserializeObject<T>(json), response);
            }
            catch (System.Exception exception)
            {
                _logger.LogError(exception, "Error occurred calling this endpoint " + endpoint);
                throw new RestException(exception.Message + "-" + endpoint);
            }
        }

        public async Task<Tuple<T, HttpResponseMessage>> Post<T>(string endpoint, Dictionary<string, string> headers, Dictionary<string, string> parameters, object body, bool asJson = true) where T : new()
        {
            var serviceUrl = endpoint;
            var httpClient = _clientFactory.CreateClient(_clientName);

            try
            {
                var _parameters = new List<string>();
                if (parameters != null && parameters.Count > 0)
                {
                    endpoint += "?";
                    _parameters.AddRange(parameters.Select(param => string.Concat(param.Key + "=", param.Value)));
                    endpoint += string.Join("&", _parameters);
                }
                var request = new HttpRequestMessage(HttpMethod.Post, endpoint);

                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }
                }

                request.Content = new JsonContent(body);

                string json = "";
                var resp = new HttpResponseMessage();
                if (asJson)
                {
                    resp = httpClient.SendAsync(request).Result;
                    json = resp.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    var result = httpClient.PostAsync(endpoint, new StringContent(
                        body.ToString(),
                        Encoding.UTF8,
                        "application/json")).Result;
                    json = result.Content.ReadAsStringAsync().Result;
                }

                json = XMLToJsonExtension.ConvertXMLToJson(json);
                return new Tuple<T, HttpResponseMessage>(JsonConvert.DeserializeObject<T>(json), resp);
            }
            catch (System.Exception exception)
            {
                _logger.LogError(exception, "Error occurred calling this endpoint " + endpoint);
                throw new RestException(exception.Message + "-" + endpoint);
            }
        }


        public async Task<Tuple<T, HttpResponseMessage>> PostOverload<T>(string endpoint, Dictionary<string, string> headers, Dictionary<string, string> parameters, object body, bool asJson) where T : new()
        {
            var serviceUrl = endpoint;
            var httpClient = _clientFactory.CreateClient(_clientName);
            try
            {
                if (headers != null)
                    foreach (var header in headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }

                string json = "";
                var resp = new HttpResponseMessage();
                if (asJson)
                {
                    resp = httpClient.PostAsJsonAsync(serviceUrl, body).Result;
                    json = resp.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    var result = httpClient.PostAsync(serviceUrl, new StringContent(
                        body.ToString(),
                        Encoding.UTF8,
                        "application/json")).Result;
                    json = result.Content.ReadAsStringAsync().Result;
                }

                json = XMLToJsonExtension.ConvertXMLToJson(json);
                return new Tuple<T, HttpResponseMessage>(JsonConvert.DeserializeObject<T>(json), resp);
            }
            catch (System.Exception exception)
            {
                _logger.LogError(exception, "Error occurred calling this endpoint " + endpoint);
                throw new RestException(exception.Message + "-" + endpoint);
            }
        }

    }

    public static class ServiceConsumerUtil
    {
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(
            this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return httpClient.PostAsync(url, content);
        }
    }

    public class JsonContent : StringContent
    {
        public JsonContent(object obj) :
            base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
        { }
    }


}
