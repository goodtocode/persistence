using System;

namespace GoodToCode.Shared.Analytics.Abstractions
{
    public interface ISentimentResult : IAnalyzedText, IConfidence, ILanguageIso
    {
        Enum Sentiment { get; }
    }
}