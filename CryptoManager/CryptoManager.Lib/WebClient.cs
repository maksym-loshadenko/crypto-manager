using System.Net;

namespace CryptoManager.Lib
{
    public class WebClient
    {
        /// <summary>
        /// Static method, that allows to make an http request to the received url with received url
        /// parameters. It forms url with received url string and a dictionary of all the parameters
        /// and their values.
        /// </summary>
        /// <param name="url">Url of the request.</param>
        /// <param name="parameters">Parameters of the request.</param>
        /// <returns>An <see cref="HttpResponseMessage"/>, formed after web request.</returns>
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
