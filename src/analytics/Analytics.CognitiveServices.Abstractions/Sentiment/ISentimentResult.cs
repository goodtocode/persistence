namespace GoodToCode.Shared.Analytics.CognitiveServices
{
    public interface ISentimentResult
    {
        string Text { get; set; }
        string LanguageIso { get; set; }        
        int Sentiment { get; set; }
        ISentimentConfidence Confidence { get; set; }
    }
}