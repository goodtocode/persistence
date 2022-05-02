using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoodToCode.Shared.dotNet.Tests.Identity
{
    [TestClass]
    public class ConfigurationBuilderTests
    {
        private readonly IConfiguration configuration = new AppConfigurationFactory().Create();
        public HttpRequest SutHttpRequest { get; private set; }
        public ConfigurationBuilder SutConfigurationBuilder { get; private set; }

        private string sentinelValue;

        public ConfigurationBuilderTests()
        { }

        [TestMethod]
        public void ConfigurationBuilder_GetKey()
        {
            Assert.IsTrue(configuration != null);
            sentinelValue = configuration[AppConfigurationKeys.SentinelKey];
            Assert.IsTrue(!string.IsNullOrEmpty(sentinelValue));
        }
    }
}
