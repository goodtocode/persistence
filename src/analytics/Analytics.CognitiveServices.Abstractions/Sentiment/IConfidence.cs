namespace GoodToCode.Shared.Analytics.Abstractions
{
    public interface IConfidence
    {
        double Negative { get; }
        double Neutral { get; }
        double Positive { get; }
    }
}