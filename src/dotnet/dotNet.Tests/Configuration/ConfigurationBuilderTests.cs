using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace GoodToCode.Shared.dotNet.Tests.Identity
{
    [Binding]
    public class ConfigurationBuilderTests
    {
        private readonly IConfiguration configuration = new AppConfigurationFactory().Create();
        public HttpRequest SutHttpRequest { get; private set; }
        public ConfigurationBuilder SutConfigurationBuilder { get; private set; }

        private readonly string sentinelKey = "Gtc:Shared:Sentinel";
        private string sentinelValue;

        public ConfigurationBuilderTests()
        { }

        [Given(@"I have a ConfigurationBuilder to a valid AzureAppConfiguration connection")]
        public void GivenIHaveAConfigurationBuilderToAValidAzureAppConfigurationConnection()
        {
            Assert.IsTrue(configuration != null);
        }

        [When(@"a AzureAppConfiguration value is requested")]
        public void WhenAAzureAppConfigurationValueIsRequested()
        {
            sentinelValue = configuration[sentinelKey];
        }

        [Then(@"the AzureAppConfiguration value can be evaluated")]
        public void ThenTheAzureAppConfigurationValueCanBeEvaluated()
        {
            Assert.IsTrue(!string.IsNullOrEmpty(sentinelValue));
        }
    }
}
