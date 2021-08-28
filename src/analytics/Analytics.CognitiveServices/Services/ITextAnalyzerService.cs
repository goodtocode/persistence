using Azure.AI.TextAnalytics;
using GoodToCode.Shared.Analytics.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Analytics.CognitiveServices
{
    public interface ITextAnalyzerService
    {
        Task<ISentimentResult> AnalyzeSentimentAsync(string text);
        Task<ISentimentResult> AnalyzeSentimentAsync(string text, string languageIso);
        Task<IList<ISentimentResult>> AnalyzeSentimentBatchAsync(string text);
        Task<string> DetectLanguageAsync(string text);
        Task<IEnumerable<EntityResult>> ExtractEntitiesAsync(string text);
        Task<LinkedResult> ExtractEntityLinksAsync(string text);
        Task<IEnumerable<IAnalyticsResult>> ExtractHealthcareEntitiesAsync(string text);
        Task<KeyPhrases> ExtractKeyPhrasesAsync(string text);
        Task<IEnumerable<OpinionResult>> ExtractOpinionAsync(string text);
        string[] SplitParagraph(string paragraph);
        List<ISentimentResult> ToSentimentResult(AnalyzeSentimentResultCollection results, string languageIso);
        ISentimentResult ToSentimentResult(DocumentSentiment result);
    }
}