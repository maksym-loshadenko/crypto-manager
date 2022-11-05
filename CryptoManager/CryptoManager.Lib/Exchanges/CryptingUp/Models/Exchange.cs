using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CryptoManager.Lib.Exchanges.CryptingUp.Models
{
    public class Exchange
    {
        [JsonProperty("exchange_id")]
        public string ExchangeId { get; set; } = "";

        [JsonProperty("name")]
        public string Name { get; set; } = "";

        [JsonProperty("website")]
        public string Website { get; set; } = "";

        [JsonIgnore]
        public JObject Quote { get; set; } = new();

        [JsonProperty("quote")]
        public JObject QuoteSetter
        {
            set => Quote = value;
        }

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
