namespace GoodToCode.Shared.CognitiveServices
{
    public interface ISentimentConfidence
    {
        double Negative { get; set; }
        double Neutral { get; set; }
        double Positive { get; set; }
    }
}