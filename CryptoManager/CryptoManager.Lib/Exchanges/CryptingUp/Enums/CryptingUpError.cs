namespace CryptoManager.Lib.Exchanges.CryptingUp.Enums
{
    public enum CryptingUpError
    {
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        TooManyRequests = 429,
        InternalServerError = 500
    }
}
