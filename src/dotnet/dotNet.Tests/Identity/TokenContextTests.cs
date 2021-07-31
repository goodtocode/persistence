using dotNet.Identity.Cryptography;
using GoodToCode.Shared.Identity;
using GoodToCode.Shared.System;
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

            string original = "Here is some data to encrypt!";
            var enc = new Encryptor();

            using Aes myAes = Aes.Create();
            byte[] encrypted = enc.Encrypt(original, myAes.Key, myAes.IV);
            string roundtrip = enc.Decrypt(encrypted, myAes.Key, myAes.IV);
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
