using System.Net;

namespace CryptoManager.Lib
{
    public class WebClient
    {
        public static async Task<HttpResponseMessage> WebGetRequest(string url, Dictionary<string, string>? parameters)
        {
            using var client = new HttpClient();
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            using var content = new FormUrlEncodedContent(parameters ?? new Dictionary<string, string>());
            var query = content.ReadAsStringAsync().Result;

            url += parameters == null ? "" : parameters.Count == 0 ? "" : $"?{query}";

            var response = await client.GetAsync(url);

            return response;
        }
    }
}
