using Azure.AI.TextAnalytics;
using GoodToCode.Shared.Analytics.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Analytics.CognitiveServices
{
    public interface ICognitiveServicesService : ITextAnalyzerService
    { 
        Task<IEnumerable<IAnalyticsResult>> ExtractHealthcareEntitiesAsync(string text, string languageIso);
    }
}