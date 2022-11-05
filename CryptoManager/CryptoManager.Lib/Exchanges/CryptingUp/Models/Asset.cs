using Newtonsoft.Json;

namespace CryptoManager.Lib.Exchanges.CryptingUp.Models
{
    /// <summary>
    /// Cryptographic coin representation 
    /// </summary>
    public class Asset : QuoteContainer
    {
        /// <summary>
        /// Id of the coin/asset
        /// </summary>
        [JsonProperty("asset_id")]
        public string AssetId { get; set; } = "";

        /// <summary>
        /// Full name of the coin/asset
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = "";

        /// <summary>
        /// Description for the coin/asset
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; } = "";

        /// <summary>
        /// Website of the coin/asset
        /// </summary>
        [JsonProperty("website")]
        public string Website { get; set; } = "";

        /// <summary>
        /// Ethereum contract address of the coin/asset
        /// </summary>
        [JsonProperty("ethereum_contract_address")]
        public string EthereumContractAddress { get; set; } = "";

        /// <summary>
        /// A bank-issued cryptocurrency, to which value of this coin/asset is linked
        /// </summary>
        [JsonProperty("pegged")]
        public string Pegged { get; set; } = "";

        /// <summary>
        /// Price of the coin/asset in USD
        /// </summary>
        [JsonProperty("price")]
        public double Price { get; set; }

        /// <summary>
        /// Total value of all cryptocurrency transactions within a 24-hour period
        /// </summary>
        [JsonProperty("volume_24h")]
        public double Volume24H { get; set; }

        /// <summary>
        /// The difference between the current price of this coin/asset and the price 1 hour ago.
        /// </summary>
        [JsonProperty("change_1h")]
        public double Change1H { get; set; }

        /// <summary>
        /// The difference between the current price of this coin/asset and the price 24 hours ago.
        /// </summary>
        [JsonProperty("change_24h")]
        public double Change24H { get; set; }

        /// <summary>
        /// The difference between the current price of this coin/asset and the price 7 days ago.
        /// </summary>
        [JsonProperty("change_7d")]
        public double Change7d { get; set; }

        /// <summary>
        /// Total amount of coins or tokens of a specific cryptocurrency that have been created
        /// or mined, that are in circulation, including those that are locked or reserved.
        /// </summary>
        [JsonProperty("total_supply")]
        public ulong TotalSupply { get; set; }

        /// <summary>
        /// Number of coins or tokens of a specific cryptocurrency that are publicly availableto buy or sell.
        /// </summary>
        [JsonProperty("circulating_supply")]
        public double CirculatingSupply { get; set; }

        /// <summary>
        /// Maximum number of coins or tokens that will be ever created.
        /// </summary>
        [JsonProperty("max_supply")]
        public ulong MaxSupply { get; set; }

        /// <summary>
        /// Total value of all the coins that have been mined
        /// </summary>
        [JsonProperty("market_cap")]
        public double MarketCap { get; set; }

        /// <summary>
        /// Market cap of a project once all its tokens have been released into circulation
        /// </summary>
        [JsonProperty("fully_diluted_market_cap")]
        public double FullyDilutedMarketCap { get; set; }

        /// <summary>
        /// Status of the coin/asset
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; } = "";


        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
