using System.Net;
using CryptoManager.Lib.Exceptions.CryptingUp;
using CryptoManager.Lib.Exchanges.CryptingUp.Enums;
using CryptoManager.Lib.Exchanges.CryptingUp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CryptoManager.Lib.Exchanges.CryptingUp
{
    /// <summary>
    /// A <see href="https://cryptingup.com">CryptingUp</see> client, that makes requests
    /// to the <see href="https://cryptingup.com/apidoc/">CryptingUp public API</see>.
    /// </summary>
    public class CryptingUpClient
    {
        /// <summary>
        /// Base CryptingUp URL for all the rest API requests.
        /// </summary>
        private const string BaseUrl = "https://cryptingup.com/api";

        /// <summary>
        /// Basic error string.
        /// </summary>
        private const string ErrorString = "Unable to load exchanges list from the CryptingUp.";

        /// <summary>
        /// This is the endpoint of a <seealso href="https://cryptingup.com/apidoc/">CryptingUp API</seealso>,
        /// that will return the list of all the available exchanges.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="Exchange"/> - all the available exchanges</returns>
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

        /// <summary>
        /// This is the endpoint of a <seealso href="https://cryptingup.com/apidoc/">CryptingUp API</seealso>,
        /// that will return a data of the specific exchanges. If exchange id received is invalid - an
        /// <see cref="RequestException"/> is thrown.
        /// </summary>
        /// <param name="exchangeId">Id of the specific exchange</param>
        /// <returns>A specific <see cref="Exchange"/> with the received id.</returns>
        /// <exception cref="RequestException"/>
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

        /// <summary>
        /// This is the endpoint of a <seealso href="https://cryptingup.com/apidoc/">CryptingUp API</seealso>,
        /// that will return a list of all available markets.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="Market"/> - all the available markets</returns>
        /// <exception cref="Exception"/>
        /// <exception cref="RequestException"/>
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

        /// <summary>
        /// This is the endpoint of a <seealso href="https://cryptingup.com/apidoc/">CryptingUp API</seealso>,
        /// that will return a list of all available markets for the received exchange. If exchange id is invalid -
        /// an empty list is returned.
        /// </summary>
        /// <param name="exchangeId">Exchange ids of markets needed.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="Market"/> - all the exchange's market.</returns>
        /// <exception cref="Exception"/>
        /// <exception cref="RequestException"/>
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

        /// <summary>
        /// This is the endpoint of a <seealso href="https://cryptingup.com/apidoc/">CryptingUp API</seealso>,
        /// that will return a list of all available markets for the received asset. If exchange id is invalid -
        /// an empty list is returned.
        /// </summary>
        /// <param name="assetId">Assets ids of markets needed.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="Market"/> - all the asset's market.</returns>
        /// <exception cref="Exception"/>
        /// <exception cref="RequestException"/>
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

        /// <summary>
        /// This is the endpoint of a <seealso href="https://cryptingup.com/apidoc/">CryptingUp API</seealso>,
        /// that will return the list of all the available assets.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="Asset"/> - all the asset's market.</returns>
        /// <exception cref="Exception"/>
        /// <exception cref="RequestException"/>
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

        /// <summary>
        /// This is the endpoint of a <seealso href="https://cryptingup.com/apidoc/">CryptingUp API</seealso>,
        /// that will return a data of the specific assets. If asset id received is invalid - an
        /// <see cref="RequestException"/> is thrown.
        /// </summary>
        /// <param name="assetId">Id of the specific asset</param>
        /// <returns>A specific <see cref="Asset"/> with the received id.</returns>
        /// <exception cref="RequestException"/>
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

        /// <summary>
        /// This is the endpoint of a <seealso href="https://cryptingup.com/apidoc/">CryptingUp API</seealso>,
        /// that will return a list of all available asset overviews.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> of all available <see cref="Asset"/>.</returns>
        /// <exception cref="Exception"/>
        public IEnumerable<AssetOverview> GetAssetsOverview()
        {
            try
            {
                var response = MakeHttpRequest($"{BaseUrl}/assetsoverview").Result;

                var responseObject = JObject.Parse(response);

                return responseObject["assets"] switch
                {
                    null => throw new Exception($"{ErrorString} Assets overview list is null."),
                    JObject => throw new Exception($"{ErrorString} Assets overview list is not an array."),
                    _ => responseObject["assets"]?.ToObject<List<AssetOverview>>() ??
                         throw new Exception($"{ErrorString}")
                };
            }
            catch (AggregateException e)
            {
                throw e.InnerException ?? new RequestException(ErrorString, "");
            }
        }

        /// <summary>
        /// A method for paginated requests of the data from the CryptingUp API.
        /// </summary>
        /// <param name="url">Url of the request.</param>
        /// <param name="startIndex">
        /// Start index, that is needed for the request of the
        /// next page in the pagination.
        /// </param>
        /// <returns>A <see cref="JObject"/> with the received JSON response.</returns>
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

        /// <summary>
        /// A universal method, that makes http requests to the received url
        /// with the received parameters. If something is wrong and status code
        /// of the request is not success - method parses an error and throws
        /// a <see cref="RequestException"/> with problem details.
        /// </summary>
        /// <param name="url">Url of the request.</param>
        /// <param name="parameters">Parameters of the request.</param>
        /// <returns>A JSON string, received from the API after request.</returns>
        /// <exception cref="RequestException"></exception>
        public static async Task<string> MakeHttpRequest(string url, Dictionary<string, string>? parameters = null)
        {
            var result = WebClient.WebGetRequest(url, parameters).Result;

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsStringAsync();
            }

            throw new RequestException(ParseStatusCode(result.StatusCode), result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// A method, that parses an error code, received after the web API request.
        /// </summary>
        /// <param name="statusCode">Status code of the error response.</param>
        /// <returns>A text of the exception for the request response.</returns>
        /// <exception cref="ArgumentOutOfRangeException"/>
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