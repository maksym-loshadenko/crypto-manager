using Newtonsoft.Json;

namespace CryptoManager.Lib.Exchanges.CryptingUp.Models
{
    public class AssetOverview
    {
        [JsonProperty("asset_id")]
        public string AssetId { get; set; } = "";

        [JsonProperty("name")]
        public string Name { get; set; } = "";
    }
}
