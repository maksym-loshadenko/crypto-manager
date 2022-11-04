using System.Net;
using CryptoManager.Lib.Exceptions.CryptingUp;
using CryptoManager.Lib.Exchanges.CryptingUp.Enums;
using CryptoManager.Lib.Exchanges.CryptingUp.Models;
using Newtonsoft.Json.Linq;

namespace CryptoManager.Lib.Exchanges.CryptingUp
{
    public class CryptingUpClient
    {
        private const string BaseUrl = "https://cryptingup.com/api";

        private const string ErrorString = "Unable to load exchanges list from the CryptingUp.";

        public IEnumerable<Exchange> GetAllExchanges()
        {
            try
            {
                var response = MakeHttpRequest($"{BaseUrl}/exchanges").Result;

                var responseObject = JObject.Parse(response);

                return responseObject["exchanges"] switch
                {
                    null => throw new Exception($"{ErrorString} Exchanges list is null."),
                    JObject => throw new Exception($"{ErrorString} Exchanges list is not an array."),
                    _ => responseObject["exchanges"]?.ToObject<List<Exchange>>() ??
                         throw new Exception($"{ErrorString}")
                };
            }
            catch (AggregateException e)
            {
                throw e.InnerException ?? new RequestException(ErrorString, "");
            }
        }

        public Exchange GetSpecificExchange(string exchangeId)
        {
            try
            {
                var response = MakeHttpRequest($"{BaseUrl}/exchanges/{exchangeId}").Result;

                var responseObject = JObject.Parse(response);

                return responseObject["exchange"] switch
                {
                    null => throw new RequestException($"{ErrorString} Exchanges list is null.", response),
                    _ => responseObject["exchange"]?.ToObject<Exchange>() ?? throw new RequestException($"{ErrorString}", response)
                };
            }
            catch (AggregateException e)
            {
                throw e.InnerException ?? new RequestException(ErrorString, "");
            }
        }

        public async Task<string> MakeHttpRequest(string url, Dictionary<string, string>? parameters = null)
        {
            var result = WebClient.WebGetRequest(url, parameters).Result;

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsStringAsync();
            }

            throw new RequestException(ParseStatusCode(result.StatusCode), result.Content.ReadAsStringAsync().Result);
        }

        private static string ParseStatusCode(HttpStatusCode statusCode)
        {
            if (!Enum.IsDefined(typeof(CryptingUpError), (int)statusCode))
                return "Unexpected error for CryptingUp.";

            switch ((CryptingUpError)(int)statusCode)
            {
                case CryptingUpError.BadRequest:
                    return "Bad request - Request is invalid";
                case CryptingUpError.Unauthorized:
                    return "Unauthorized -- Your API key is wrong.";
                case CryptingUpError.Forbidden:
                    return "Forbidden -- Authenticated users only";
                case CryptingUpError.NotFound:
                    return "Not Found -- Item not found";
                case CryptingUpError.TooManyRequests:
                    return "Too Many Requests -- Rate limit reached";
                case CryptingUpError.InternalServerError:
                    return "Internal Server Error -- Something went wrong on our side";
                default:
                    throw new ArgumentOutOfRangeException(nameof(statusCode), statusCode, "Unknown response code.");
            }
        }
    }
}