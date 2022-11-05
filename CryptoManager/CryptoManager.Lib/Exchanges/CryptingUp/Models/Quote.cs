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

        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public double? Price { get; set; }

        [JsonProperty("volume_24h", NullValueHandling = NullValueHandling.Ignore)]
        public double? Volume24H { get; set; }

        [JsonProperty("market_cap", NullValueHandling = NullValueHandling.Ignore)]
        public double? MarketCap { get; set; }

        [JsonProperty("fully_diluted_market_cap", NullValueHandling = NullValueHandling.Ignore)]
        public double? FullyDilutedMarketCap { get; set; }
    }
}
