using CryptoManager.Lib.Exceptions.CryptingUp;
using CryptoManager.Lib.Exchanges.CryptingUp;
using CryptoManager.Lib.Exchanges.CryptingUp.Models;
using Newtonsoft.Json;

namespace CryptoManager.UnitTests
{
    [TestClass]
    public class CryptingUpLibTests
    {
        public TestContext TestContext { get; set; }

        private static TestContext _testContext;

        [ClassInitialize]
        public static void SetupTests(TestContext testContext)
        {
            _testContext = testContext;
        }

        [TestMethod]
        public void TestGetAllExchanges()
        {
            var client = new CryptingUpClient();

            var result = client.GetAllExchanges();

            TestContext.WriteLine(
                JsonConvert.SerializeObject(result, Formatting.Indented)
            );

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetSpecificExchangeCorrect()
        {
            var client = new CryptingUpClient();
            const string correctExchangeName = "BINANCE";

            var result = client.GetSpecificExchange(correctExchangeName);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetSpecificExchangeIncorrect()
        {
            var client = new CryptingUpClient();
            const string incorrectExchangeName = "BNNC";

            Exception? result = null;

            try
            {
                client.GetSpecificExchange(incorrectExchangeName);
            }
            catch (Exception e)
            {
                result = e;
            }

            Assert.IsNotNull(result);
            Assert.IsTrue(result is RequestException);
        }
    }
}