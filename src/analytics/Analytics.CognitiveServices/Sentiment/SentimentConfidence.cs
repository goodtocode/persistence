namespace GoodToCode.Shared.Analytics.CognitiveServices
{
    /// <summary>
    /// Abstraction of Azure.AI.TextAnalytics.TextSentiment
    /// </summary>
    public class SentimentConfidence : IConfidence
    {
        public double Positive { get; set; } = 0.0;
        public double Neutral { get; set; } = 0.0;
        public double Negative { get; set; } = 0.0;

        public SentimentConfidence(double positive, double neutral, double negative)
        {
            Positive = positive;
            Neutral = neutral;
            Negative = negative;
        }
    }
}
