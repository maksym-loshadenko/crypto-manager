using Newtonsoft.Json;

namespace CryptoManager.Lib.Exchanges.CryptingUp.Models
{
    public class Exchange : QuoteContainer
    {
        [JsonProperty("exchange_id")]
        public string ExchangeId { get; set; } = "";

        [JsonProperty("name")]
        public string Name { get; set; } = "";

        [JsonProperty("website")]
        public string Website { get; set; } = "";
    }
}
