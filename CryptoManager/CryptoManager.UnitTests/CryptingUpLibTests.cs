using CryptoManager.Lib.Exchanges.CryptingUp;
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
    }
}