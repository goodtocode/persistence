using dotNet.Identity.Cryptography;
using GoodToCode.Shared.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace GoodToCode.Shared.Unit
{
    [Binding]
    public class TokenContextTests
    {
        private readonly IConfiguration configuration = new AppConfigurationFactory().Configuration;
        public HttpRequest SutHttpRequest { get; private set; }
        public TokenContext SutTokenContext { get; private set; }

        private readonly string audience;
        private readonly string tenantId;
        private readonly string clientId;

        public TokenContextTests()
        {

            audience = configuration["Gtc: Shared:Tests: AzureAd:Audience"];
            tenantId = configuration["Gtc: Shared:Tests: AzureAd:TenantId"];
            clientId = configuration["Gtc: Shared:Tests: AzureAd:ClientId"];
        }

        [Given(@"I have a TokenContext to a valid AAD app registration")]
        public void GivenIHaveATokenContextToAValidAADAppRegistration()
        {
            SutTokenContext = new TokenContext(audience, clientId, tenantId);
        }

        [When(@"a HttpRequest object is validated")]
        public void WhenAHttpRequestObjectIsValidated()
        {
            SutHttpRequest = new HttpRequestFactory().CreateHttpRequest("GET");

        }

        [Then(@"the bearer token validation is successful")]
        public async Task ThenTheBearerTokenValidationIsSuccessful()
        {
            Assert.IsFalse(await SutTokenContext.IsAuthenticatedAsync(SutHttpRequest));
        }
    }
}
