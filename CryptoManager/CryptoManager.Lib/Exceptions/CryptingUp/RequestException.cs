namespace CryptoManager.Lib.Exceptions.CryptingUp
{
    public class RequestException : Exception
    {
        public string Response { get; }

        public RequestException(string message, string response) : base(message)
        {
            Response = response;
        }
    }
}
