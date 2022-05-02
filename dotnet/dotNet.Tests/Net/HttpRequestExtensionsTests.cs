using GoodToCode.Shared.dotNet.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoodToCode.Shared.dotNet.Tests.Identity
{
    [TestClass]
    public class HttpRequestExtensionsTests
    {
        private readonly IConfiguration configuration = new AppConfigurationFactory().Create();
        public HttpRequest SutHttpRequest { get; private set; }

        public HttpRequestExtensionsTests()
        {
            SutHttpRequest = new HttpRequestFactory().CreateHttpRequest("GET");
        }

        [TestMethod]
        public void HttpRequest_ToUri()
        {
            Assert.IsTrue(SutHttpRequest != null);
            SutHttpRequest.ToUri();
            Assert.IsTrue(SutHttpRequest.ToUri().IsWellFormedOriginalString());
        }
    }
}
