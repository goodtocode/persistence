using GoodToCode.Shared.dotNet.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace GoodToCode.Shared.dotNet.Tests.Identity
{
    [Binding]
    public class HttpRequestExtensionsTests
    {
        private readonly IConfiguration configuration = new AppConfigurationFactory().Create();
        public HttpRequest SutHttpRequest { get; private set; }

        public HttpRequestExtensionsTests()
        {
            SutHttpRequest = new HttpRequestFactory().CreateHttpRequest("GET");
        }

        [Given(@"I have a HttpRequestExtensions to a valid HttpRequest object")]
        public void GivenIHaveAHttpRequestExtensionsToAValidHttpRequestObject()
        {
            Assert.IsTrue(SutHttpRequest != null);
        }

        [When(@"a ToUri method is requested")]
        public void WhenAToUriMethodIsRequested()
        {
            SutHttpRequest.ToUri();
        }

        [Then(@"the ToUri Uri value can be evaluated")]
        public void ThenTheToUriUriValueCanBeEvaluated()
        {
            Assert.IsTrue(SutHttpRequest.ToUri().IsWellFormedOriginalString());
        }
    }
}
