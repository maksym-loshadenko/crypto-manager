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

        [JsonProperty("volume_24h")]
        public double Volume24H { get; set; }

        [JsonProperty("quote")] public JObject Quote { get; set; } = new();

        [JsonIgnore]
        public double? Volume24Cad => Quote["CAD"]?["volume_24h"]?.ToObject<double>();

        [JsonIgnore]
        public double? Volume24Usd => Quote["USD"]?["volume_24h"]?.ToObject<double>();

        [JsonIgnore]
        public double? Volume24Eur => Quote["EUR"]?["volume_24h"]?.ToObject<double>();

        [JsonIgnore]
        public double? Volume24Gbp => Quote["GBP"]?["volume_24h"]?.ToObject<double>();

        [JsonIgnore]
        public double? Volume24Jpy => Quote["JPY"]?["volume_24h"]?.ToObject<double>();

        [JsonIgnore]
        public double? Volume24Aud => Quote["AUD"]?["volume_24h"]?.ToObject<double>();

        [JsonIgnore]
        public double? Volume24Nzd => Quote["NZD"]?["volume_24h"]?.ToObject<double>();
    }
}
