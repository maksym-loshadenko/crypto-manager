using System.Net;
using System.Reflection;
using System.Security;
using CryptoManager.Lib.Exceptions.CryptingUp;
using CryptoManager.Lib.Exchanges.CryptingUp.Enums;
using CryptoManager.Lib.Exchanges.CryptingUp.Models;
using Newtonsoft.Json;
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

        public IEnumerable<Market> GetAllMarkets()
        {
            try
            {
                var markets = new List<Market>();

                for (string? start = null; start != "";)
                {
                    var response = GetQuery($"{BaseUrl}/markets", start ?? "");

                    markets.AddRange(response["markets"] switch
                    {
                        null => throw new Exception($"{ErrorString} Markets list is null."),
                        JObject => throw new Exception($"{ErrorString} Markets list is not an array."),
                        _ => response["markets"]?.ToObject<List<Market>>() ??
                             throw new Exception($"{ErrorString}")
                    });

                    start = response["next"] == null
                        ? throw new RequestException(
                            "Unable to load query for markets.",
                            response.ToString(Formatting.Indented
                            ))
                        : response["next"]?.ToObject<string>();
                }

                return markets;

            }
            catch (AggregateException e)
            {
                throw e.InnerException ?? new RequestException(ErrorString, "");
            }
        }

        public IEnumerable<Market> GetExchangeMarkets(string exchangeId)
        {
            try
            {
                var markets = new List<Market>();

                for (string? start = null; start != "";)
                {
                    var response = GetQuery($"{BaseUrl}/exchanges/{exchangeId}/markets", start ?? "");

                    markets.AddRange(response["markets"] switch
                    {
                        null => throw new Exception($"{ErrorString} Markets list is null."),
                        JObject => throw new Exception($"{ErrorString} Markets list is not an array."),
                        _ => response["markets"]?.ToObject<List<Market>>() ??
                             throw new Exception($"{ErrorString}")
                    });

                    start = response["next"] == null
                        ? throw new RequestException(
                            "Unable to load query for markets.",
                            response.ToString(Formatting.Indented
                            ))
                        : response["next"]?.ToObject<string>();
                }

                return markets;

            }
            catch (AggregateException e)
            {
                throw e.InnerException ?? new RequestException(ErrorString, "");
            }
        }

        public IEnumerable<Market> GetAssetMarkets(string assetId)
        {
            try
            {
                var markets = new List<Market>();

                for (string? start = null; start != "";)
                {
                    var response = GetQuery($"{BaseUrl}/assets/{assetId}/markets", start ?? "0");

                    markets.AddRange(response["markets"] switch
                    {
                        null => throw new Exception($"{ErrorString} Markets list is null."),
                        JObject => throw new Exception($"{ErrorString} Markets list is not an array."),
                        _ => response["markets"]?.ToObject<List<Market>>() ??
                             throw new Exception($"{ErrorString}")
                    });

                    start = response["next"] == null
                        ? throw new RequestException(
                            "Unable to load query for markets.",
                            response.ToString(Formatting.Indented
                            ))
                        : response["next"]?.ToObject<string>();
                }

                return markets;
            }
            catch (AggregateException e)
            {
                throw e.InnerException ?? new RequestException(ErrorString, "");
            }
        }

        public IList<Asset> GetAllAssets()
        {
            try
            {
                var assets = new List<Asset>();

                for (string? start = null; start != "";)
                {
                    var response = GetQuery($"{BaseUrl}/assets", start ?? "");

                    assets.AddRange(response["assets"] switch
                    {
                        null => throw new Exception($"{ErrorString} Assets list is null."),
                        JObject => throw new Exception($"{ErrorString} Assets list is not an array."),
                        _ => response["assets"]?.ToObject<List<Asset>>() ??
                             throw new Exception($"{ErrorString}")
                    });

                    start = response["next"] == null
                        ? throw new RequestException(
                            "Unable to load query for assets.",
                            response.ToString(Formatting.Indented
                            ))
                        : response["next"]?.ToObject<string>();
                }

                return assets;

            }
            catch (AggregateException e)
            {
                throw e.InnerException ?? new RequestException(ErrorString, "");
            }
        }

        public Asset GetSpecificAsset(string assetId)
        {
            try
            {
                var response = MakeHttpRequest($"{BaseUrl}/assets/{assetId}").Result;

                var responseObject = JObject.Parse(response);

                return responseObject["asset"] switch
                {
                    null => throw new RequestException($"{ErrorString} Asset is null.", response),
                    _ => responseObject["asset"]?.ToObject<Asset>() ?? throw new RequestException($"{ErrorString}", response)
                };
            }
            catch (AggregateException e)
            {
                throw e.InnerException ?? new RequestException(ErrorString, "");
            }
        }

        public JObject GetQuery(string url, string startIndex)
        {
            try
            {
                var parameters = new Dictionary<string, string>()
                {
                    { "start", startIndex }
                };

                var response = MakeHttpRequest(url, parameters).Result;

                return JObject.Parse(response);
            }
            catch (AggregateException e)
            {
                throw e.InnerException ?? new RequestException(ErrorString, "");
            }
        }

        public static async Task<string> MakeHttpRequest(string url, Dictionary<string, string>? parameters = null)
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