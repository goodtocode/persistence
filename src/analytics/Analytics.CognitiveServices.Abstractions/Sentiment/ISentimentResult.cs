namespace GoodToCode.Shared.Analytics.Abstractions
{
    public interface ISentimentResult : IConfidence
    {
        string Text { get; }
        string LanguageIso { get; }        
        int Sentiment { get; }
    }
}