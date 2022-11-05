using Newtonsoft.Json;

namespace CryptoManager.Lib.Exchanges.CryptingUp.Models
{
    /// <summary>
    /// An exchange that allows users to trade cryptocurrency or electronic
    /// money for real bank currencies or other electronic currencies.
    /// </summary>
    public class Exchange : QuoteContainer
    {
        /// <summary>
        /// Id of the exchange in the CryptingUp API
        /// </summary>
        [JsonProperty("exchange_id")]
        public string ExchangeId { get; set; } = "";

        /// <summary>
        /// Name of the exchange in the CryptingUp API
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = "";

        /// <summary>
        /// Link to the website of the exchange
        /// </summary>
        [JsonProperty("website")]
        public string Website { get; set; } = "";
    }
}
