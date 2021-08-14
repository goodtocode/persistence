namespace GoodToCode.Shared.Analytics.CognitiveServices
{
    /// <summary>
    /// Abstraction of Azure.AI.TextAnalytics.TextSentiment
    /// </summary>
    public class SentimentConfidence : ISentimentConfidence
    {
        public double Positive { get; set; } = 0.0;
        public double Neutral { get; set; } = 0.0;
        public double Negative { get; set; } = 0.0;
    }
}
