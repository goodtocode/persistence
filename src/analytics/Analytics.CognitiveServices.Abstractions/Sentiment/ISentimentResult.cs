namespace GoodToCode.Shared.Analytics.Abstractions
{
    public interface ISentimentResult : IConfidence, ILanguageIso
    {
        string Text { get; }
        int Sentiment { get; }
    }
}