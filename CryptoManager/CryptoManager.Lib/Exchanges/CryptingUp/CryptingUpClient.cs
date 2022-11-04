using System.Net;
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
            var response =  MakeHttpRequest($"{BaseUrl}/exchanges").Result;

            var responseObject = JObject.Parse(response);

            return responseObject["exchanges"] switch
            {
                null => throw new Exception($"{ErrorString} Exchanges list is null."),
                JObject => throw new Exception($"{ErrorString} Exchanges list is not an array."),
                _ => responseObject["exchanges"]?.ToObject<List<Exchange>>() ?? throw new Exception($"{ErrorString}")
            };
        }

        public async Task<string> MakeHttpRequest(string url, Dictionary<string, string>? parameters = null)
        {
            try
            {
                var result = WebClient.WebGetRequest(url, parameters).Result;

                if (result.IsSuccessStatusCode)
                {
                    return await result.Content.ReadAsStringAsync();
                }

                throw new Exception(ParseStatusCode(result.StatusCode));
            }
            catch (Exception e)
            {
                throw new Exception(e.GetBaseException().Message);
            }
        }

        private static string ParseStatusCode(HttpStatusCode statusCode)
        {
            if (!Enum.IsDefined(typeof(CryptingUpError), statusCode))
                return "Unexpected error for CryptingUp.";

            switch ((CryptingUpError)statusCode)
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