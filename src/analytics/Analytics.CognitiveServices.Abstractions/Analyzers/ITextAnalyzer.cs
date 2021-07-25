using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodToCode.Shared.CognitiveServices
{
    public interface ITextAnalyzer
    {
        Task<string> DetectLanguageAsync(string text);
        Task<IList<string>> ExtractKeyPhrasesAsync(string text);
        Task<ISentimentResult> AnalyzeSentimentAsync(string text);
        Task<IList<ISentimentResult>> AnalyzeSentimentBatchAsync(string text);
        string[] SplitParagraph(string paragraph);
    }
}
