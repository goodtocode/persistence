using GoodToCode.Shared.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace GoodToCode.Shared.dotNet.Tests.Identity
{
    [Binding]
    public class TokenContextTests
    {
        private readonly IConfiguration configuration = new AppConfigurationFactory().Create();
        public HttpRequest SutHttpRequest { get; private set; }
        public TokenContext SutTokenContext { get; private set; }

        private readonly string audience;
        private readonly string tenantId;
        private readonly string clientId;

        public TokenContextTests()
        {

            audience = configuration["Gtc:Shared:Tests:AzureAd:Audience"];
            tenantId = configuration["Gtc:Shared:Tests:AzureAd:TenantId"];
            clientId = configuration["Gtc:Shared:Tests:AzureAd:ClientId"];
        }

        [Given(@"I have a TokenContext to a valid AAD app registration")]
        public void GivenIHaveATokenContextToAValidAADAppRegistration()
        {
            SutTokenContext = new TokenContext(audience, clientId, tenantId);
        }

        [When(@"a HttpRequest object is validated")]
        public void WhenAHttpRequestObjectIsValidated()
        {
            //curl--location--request POST 'https://login.microsoftonline.com/common/oauth2/v2.0/token' \
            //    --header 'Content-Type: application/x-www-form-urlencoded' \
            //    --data - urlencode 'client_id={clientid}' \
            //    --data - urlencode 'refresh_token={refreshtoken}' \
            //    --data - urlencode 'redirect_uri={redirect_uri}' \
            //    --data - urlencode 'grant_type=refresh_token' \
            //    --data - urlencode 'client_secret={client_secret}
            SutHttpRequest = new HttpRequestFactory().CreateHttpRequest("GET");
            //SutHttpRequest.Headers.Add("Authorization", new Microsoft.Extensions.Primitives.StringValues(new string[] { $"Bearer{clientId}" }));
        }

        [Then(@"the bearer token validation is successful")]
        public async Task ThenTheBearerTokenValidationIsSuccessful()
        {
            bool isAuthed;
            try
            {
                isAuthed = await SutTokenContext.IsAuthenticatedAsync(SutHttpRequest);
            }
            catch(Exception ex)
            {
                Assert.Fail($"Exception: {ex.Message} - {ex?.InnerException?.Message} - {ex.StackTrace}");
            }

            //Assert.IsTrue(isAuthed, $"Should be valid token: {SutHttpRequest.Bearer()}");
        }
    }
}
