using System;

namespace GoodToCode.Shared.Analytics.Abstractions
{
    public interface ISentimentResult : IAnalyticsText, IConfidence, ILanguageIso
    {
        Enum Sentiment { get; }
    }
}