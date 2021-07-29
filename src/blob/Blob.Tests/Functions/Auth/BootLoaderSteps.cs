using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace GoodToCode.Shared.Blob.Excel
{
    [Binding]
    public class BootLoaderSteps
    {
        private readonly ILogger<Healthcheck> logger = LoggerFactory.CreateLogger<Healthcheck>();
        private readonly IConfiguration config = new AppConfigurationFactory().Create();

        public string Sut { get; private set; }
        public bool SutResponse { get; private set; }

        public BootLoaderSteps()
        {            
        }

        [Given(@"I have an Azure Function to check basic health")]
        public void GivenIHaveAnAzureFunctionToCheckBasicHealth()
        {
            Sut = $"{config["Reflections:Shared:FunctionsUrl"]}/api/HealthCheck?code={config["Reflections:Shared:FunctionsCode"]}";
            Assert.IsTrue(Sut.Length > 0);
        }

        [When(@"Basic health of the Azure Function is checked")]
        public async Task WhenBasicHealthOfTheAzureFunctionIsChecked()
        {
            var request = new HttpRequestFactory("GET").CreateHttpRequest("code", config["Reflections:Shared:FunctionsCode"]);
            var response = (OkObjectResult)await new Healthcheck(logger, config).Run(request);
            var returnedItem = (FunctionHealth)response.Value;
            SutResponse = returnedItem.Connected;
            Assert.IsTrue(response.StatusCode == StatusCodes.Status200OK);
        }

        [Then(@"the Basic health of the Azure Function is returned")]
        public void ThenTheBasicHealthOfTheAzureFunctionIsReturned()
        {
            Assert.IsTrue(SutResponse);
        }
    }
}
