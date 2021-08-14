using GoodToCode.Shared.Analytics.CognitiveServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Analytics.Tests.TextAnalytics
{
    [TestClass]
    public class TextAnalyticsTests
    {
        private TextAnalyzer analyzer;
        private string sutText;

        public TextAnalyticsTests() { }

        [TestInitialize]
        public void Initialize()
        {
            analyzer = new TextAnalyzer("", new Uri(""));
        }

        [TestMethod]
        public async Task TextAnalytics_Language()
        {
            var sutResult = await analyzer.DetectLanguageAsync(sutText);
        }

        [TestMethod]
        public async Task TextAnalytics_KeyPhrase()
        {
            var sutResult = await analyzer.ExtractKeyPhrasesAsync(sutText);
        }

        [TestMethod]
        public async Task TextAnalytics_Healthcare()
        {
            var sutResult = await analyzer.ExtractHealthcareEntitiesAsync(sutText);
        }

        [TestMethod]
        public async Task TextAnalytics_Entities()
        {
            var sutResult = await analyzer.ExtractEntitiesAsync(sutText);
        }

        [TestMethod]
        public async Task TextAnalytics_Links()
        {
            var sutResult = await analyzer.ExtractEntityLinksAsync(sutText);
        }

        [TestMethod]
        public async Task TextAnalytics_Opinion()
        {
            var sutResult = await analyzer.ExtractOpinionAsync(sutText);
        }

        [TestMethod]
        public async Task TextAnalytics_Sentiment()
        {
            var sutResult = await analyzer.AnalyzeSentimentAsync(sutText);
        }

    }
}
