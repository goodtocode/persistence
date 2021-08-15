
namespace GoodToCode.Shared.Analytics.Abstractions
{
    public interface IAnalyticsEntityResult
    {
        string Text { get; }
        string Category { get; }
        string SubCategory { get; }
        double Confidence { get; }
    }
}
