using Azure.AI.TextAnalytics;
using GoodToCode.Shared.CognitiveServices;
using GoodToCode.Shared.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace MySundial.Reflections.Services
{
    [Binding]
    public class SentimentAnalyzeByParagraphSteps
    {
        private readonly IConfiguration _config;
        private readonly TextAnalyzer _sentiment;

        public string Sut { get; private set; }
        public IList<string> Suts { get; private set; } = new List<string>();        
        public IList<ISentimentResult> Result { get; private set; }

        public SentimentAnalyzeByParagraphSteps()
        {
            var builder = new ConfigurationBuilder();
            builder.AddAzureAppConfigurationWithSentinel(Environment.GetEnvironmentVariable("AppSettingsConnection"), "Reflections:Shared:Sentinel");
            _config = builder.Build();
            _sentiment = new TextAnalyzer(_config["Reflections:Shared:CognitiveKey"], new Uri(_config["Reflections:Shared:CognitiveUrl"]));
        }

        [Given(@"An positive engine Paragraph exists")]
        public void GivenAnPositiveEngineParagraphExists()
        {
            Sut = "I feel great today! What about you? Me too.";
            Assert.IsTrue(string.IsNullOrWhiteSpace(Sut) == false);
        }

        [When(@"the positive engine Paragraph is analyzed for sentiment")]
        public async Task WhenThePositiveEngineParagraphIsAnalyzedForSentiment()
        {
            Result = await _sentiment.AnalyzeSentimentBatchAsync(Sut);
            Assert.IsTrue(Result != null);
        }

        [Then(@"the result of the Paragraph analysys returns a positive sentiment score")]
        public void ThenTheResultOfTheAnalysysReturnsAPositiveSentimentScore()
        {
            Assert.IsTrue(Result.Count > 0);
        }

        [TestCleanup]
        public void Cleanup()
        {
        }
    }
}
