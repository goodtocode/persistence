namespace GoodToCode.Shared.Analytics.CognitiveServices
{
    public interface IConfidence
    {
        double Negative { get; }
        double Neutral { get; }
        double Positive { get; }
    }
}