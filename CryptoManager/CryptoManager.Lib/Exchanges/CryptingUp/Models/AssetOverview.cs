using Newtonsoft.Json;

namespace CryptoManager.Lib.Exchanges.CryptingUp.Models
{
    /// <summary>
    /// Relationship between the id of an Asset and its full name
    /// </summary>
    public class AssetOverview
    {
        /// <summary>
        /// Id of the coin/asset.
        /// </summary>
        [JsonProperty("asset_id")]
        public string AssetId { get; set; } = "";

        /// <summary>
        /// Full name of the coin/asset.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = "";
    }
}
