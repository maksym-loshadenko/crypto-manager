using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CryptoManager.Lib.Exchanges.CryptingUp.Models
{
    public class Market
    {
        [JsonIgnore]
        public JObject Quote { get; set; }

        [JsonProperty("quote")]
        public JObject QuoteSetter
        {
            set => Quote = value;
        }

        [JsonProperty("exchange_id")]
        public string ExchangeId { get; set; } = "";

        [JsonProperty("symbol")]
        public string Symbol { get; set; } = "";

        [JsonProperty("base_asset")]
        public string BaseAsset { get; set; } = "";

        [JsonProperty("quote_asset")]
        public string QuoteAsset { get; set; } = "";

        [JsonProperty("price_unconverted")]
        public double PriceUnconverted { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("change_24h")]
        public double Change24H { get; set; }

        [JsonProperty("spread")]
        public double Spread { get; set; }

        [JsonProperty("volume_24h")]
        public double Volume24H { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; } = "";

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("price_cad")]
        public double? PriceCad => Quote["CAD"]?["price"]?.ToObject<double>();

        [JsonProperty("price_usd")]
        public double? PriceUsd => Quote["CAD"]?["price"]?.ToObject<double>();

        [JsonProperty("price_eur")]
        public double? PriceEur => Quote["CAD"]?["price"]?.ToObject<double>();

        [JsonProperty("price_gbp")]
        public double? PriceGbp => Quote["CAD"]?["price"]?.ToObject<double>();

        [JsonProperty("price_jpy")]
        public double? PriceJpy => Quote["CAD"]?["price"]?.ToObject<double>();

        [JsonProperty("price_aud")]
        public double? PriceAud => Quote["CAD"]?["price"]?.ToObject<double>();

        [JsonProperty("price_nzd")]
        public double? PriceNzd => Quote["CAD"]?["price"]?.ToObject<double>();

        [JsonProperty("volume_24h_cad")]
        public double? Volume24Cad => Quote["CAD"]?["volume_24h"]?.ToObject<double>();

        [JsonProperty("volume_24h_usd")]
        public double? Volume24Usd => Quote["USD"]?["volume_24h"]?.ToObject<double>();

        [JsonProperty("volume_24h_eur")]
        public double? Volume24Eur => Quote["EUR"]?["volume_24h"]?.ToObject<double>();

        [JsonProperty("volume_24h_gbp")]
        public double? Volume24Gbp => Quote["GBP"]?["volume_24h"]?.ToObject<double>();

        [JsonProperty("volume_24h_jpy")]
        public double? Volume24Jpy => Quote["JPY"]?["volume_24h"]?.ToObject<double>();

        [JsonProperty("volume_24h_aud")]
        public double? Volume24Aud => Quote["AUD"]?["volume_24h"]?.ToObject<double>();

        [JsonProperty("volume_24h_nzd")]
        public double? Volume24Nzd => Quote["NZD"]?["volume_24h"]?.ToObject<double>();
    }
}
