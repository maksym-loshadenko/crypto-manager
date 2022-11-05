using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace CryptoManager.Lib.Exchanges.CryptingUp.Models
{
    /// <summary>
    /// Base class of all the CryptingUp classes
    /// </summary>
    public class QuoteContainer
    {
        [JsonProperty("quote_data")]
        public IList<Quote> Quote { get; set; } = new List<Quote>();

        [JsonProperty("quote")]
        public JObject QuoteSetter
        {
            set => AddQuotes(value);
        }

        private void AddQuotes(JObject quote)
        {
            AddQuote(quote, "NZD");
            AddQuote(quote, "AUD");
            AddQuote(quote, "CAD");
            AddQuote(quote, "GBP");
            AddQuote(quote, "JPY");
            AddQuote(quote, "EUR");
            AddQuote(quote, "USD");
        }

        private void AddQuote(JObject quote, string currencyCode)
        {
            var price = GetQuoteData(quote, "price", currencyCode);
            var volume24h = GetQuoteData(quote, "volume_24h", currencyCode);
            var marketCap = GetQuoteData(quote, "market_cap", currencyCode);
            var fullyDilutedMarketCap = GetQuoteData(quote, "fully_diluted_market_cap", currencyCode);

            Quote.Add(new Quote(price, volume24h, marketCap, fullyDilutedMarketCap));
        }

        private static double? GetQuoteData(JObject quote, string fieldName, string currencyCode)
        {
            return quote[currencyCode] == null ? null :
                quote[currencyCode]?[fieldName] == null ? null :
                quote[currencyCode]?[fieldName]?.ToObject<double>();
        }
    }
}
