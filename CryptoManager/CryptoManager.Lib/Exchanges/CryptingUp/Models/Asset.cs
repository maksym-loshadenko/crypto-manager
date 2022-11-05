using Newtonsoft.Json;

namespace CryptoManager.Lib.Exchanges.CryptingUp.Models
{
    public class Asset : QuoteContainer
    {
        [JsonProperty("asset_id")]
        public string AssetId { get; set; } = "";

        [JsonProperty("name")]
        public string Name { get; set; } = "";

        [JsonProperty("description")]
        public string Description { get; set; } = "";

        [JsonProperty("website")]
        public string Website { get; set; } = "";

        [JsonProperty("ethereum_contract_address")]
        public string EthereumContractAddress { get; set; } = "";

        [JsonProperty("pegged")]
        public string Pegged { get; set; } = "";

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("volume_24h")]
        public double Volume24H { get; set; }

        [JsonProperty("change_1h")]
        public double Change1H { get; set; }

        [JsonProperty("change_24h")]
        public double Change24H { get; set; }

        [JsonProperty("change_7d")]
        public double Change7d { get; set; }

        [JsonProperty("total_supply")]
        public ulong TotalSupply { get; set; }

        [JsonProperty("circulating_supply")]
        public double CirculatingSupply { get; set; }

        [JsonProperty("max_supply")]
        public ulong MaxSupply { get; set; }

        [JsonProperty("market_cap")]
        public double MarketCap { get; set; }

        [JsonProperty("fully_diluted_market_cap")]
        public double FullyDilutedMarketCap { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; } = "";

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
