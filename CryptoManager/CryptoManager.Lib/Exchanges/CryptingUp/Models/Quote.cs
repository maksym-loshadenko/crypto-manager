using Newtonsoft.Json;

namespace CryptoManager.Lib.Exchanges.CryptingUp.Models
{
    public class Quote
    {
        public Quote(double? price, double? volume24H, double? marketCap, double? fullyDilutedMarketCap)
        {
            Price = price;
            Volume24H = volume24H;
            MarketCap = marketCap;
            FullyDilutedMarketCap = fullyDilutedMarketCap;
        }

        /// <summary>
        /// Price of the asset/coin.
        /// </summary>
        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public double? Price { get; set; }

        /// <summary>
        /// Total value of all cryptocurrency transactions within a 24-hour period
        /// </summary>
        [JsonProperty("volume_24h", NullValueHandling = NullValueHandling.Ignore)]
        public double? Volume24H { get; set; }

        /// <summary>
        /// Total value of all the coins that have been mined
        /// </summary>
        [JsonProperty("market_cap", NullValueHandling = NullValueHandling.Ignore)]
        public double? MarketCap { get; set; }

        /// <summary>
        /// Market cap of a project once all its tokens have been released into circulation
        /// </summary>
        [JsonProperty("fully_diluted_market_cap", NullValueHandling = NullValueHandling.Ignore)]
        public double? FullyDilutedMarketCap { get; set; }
    }
}
