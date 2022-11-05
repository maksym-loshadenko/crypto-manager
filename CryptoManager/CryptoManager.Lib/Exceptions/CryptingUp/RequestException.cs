namespace CryptoManager.Lib.Exceptions.CryptingUp
{
    /// <summary>
    /// Custom CryptingUp web request to the API exception
    /// </summary>
    public class RequestException : Exception
    {
        public string Response { get; }

        public RequestException(string message, string response) : base(message)
        {
            Response = response;
        }
    }
}
