using Newtonsoft.Json;

namespace CryptoManager.Lib.Exchanges.CryptingUp.Models
{
    /// <summary>
    /// Offers of cryptocurrency or e-money transactions available
    /// on the crypto exchanges provided.
    /// </summary>
    public class Market : QuoteContainer
    {
        /// <summary>
        /// Id of the exchange, which provides the offer
        /// </summary>
        [JsonProperty("exchange_id")]
        public string ExchangeId { get; set; } = "";

        /// <summary>
        /// String that represents the conversible coins/currencies
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = "";

        /// <summary>
        /// Asset/coin, that will be traded.
        /// </summary>
        [JsonProperty("base_asset")]
        public string BaseAsset { get; set; } = "";

        /// <summary>
        /// Asset/coin, that will be bought.
        /// </summary>
        [JsonProperty("quote_asset")]
        public string QuoteAsset { get; set; } = "";

        /// <summary>
        /// Unconverted price in USD
        /// </summary>
        [JsonProperty("price_unconverted")]
        public double PriceUnconverted { get; set; }

        /// <summary>
        /// Converted price in USD
        /// </summary>
        [JsonProperty("price")]
        public double Price { get; set; }

        /// <summary>
        /// The difference between the current price of this coin/asset and the price 24 hours ago.
        /// </summary>
        [JsonProperty("change_24h")]
        public double Change24H { get; set; }

        /// <summary>
        /// Difference between the current market price for that asset and the price you buy or sell that asset for
        /// </summary>
        [JsonProperty("spread")]
        public double Spread { get; set; }

        /// /// <summary>
        /// Total value of all cryptocurrency transactions within a 24-hour period
        /// </summary>
        [JsonProperty("volume_24h")]
        public double Volume24H { get; set; }

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
