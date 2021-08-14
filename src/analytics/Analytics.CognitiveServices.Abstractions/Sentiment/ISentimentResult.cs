namespace GoodToCode.Shared.Analytics.CognitiveServices
{
    public interface ISentimentResult
    {
        string Text { get; set; }
        string LanguageIso { get; set; }        
        int Sentiment { get; set; }
        IConfidence Confidence { get; set; }
    }
}