using System.Xml;
using Newtonsoft.Json;


namespace common.library
{
    public static class XMLToJsonExtension
    {
        public static string ConvertXMLToJson(string xml)
        {
            return CheckMediaType(xml);
        }

        private static string CheckMediaType(string json)
        {
            if (!IsValidJson(json))
            {
                json = ConvertXMLtoJson(json);
            }

            return json;
        }

        private static bool IsValidJson(string value)
        {
            try
            {
                Newtonsoft.Json.Linq.JToken.Parse(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static string ConvertXMLtoJson(string xml)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                string json = JsonConvert.SerializeXmlNode(doc);
                return json;
            }
            catch
            {
                var error = new
                {
                    message = xml
                };

                return JsonConvert.SerializeObject(error);
            }
        }

    }
}
