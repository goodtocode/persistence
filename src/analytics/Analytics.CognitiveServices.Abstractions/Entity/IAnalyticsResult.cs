
namespace GoodToCode.Shared.Analytics.Abstractions
{
    public interface IAnalyticsResult: IAnalyticsText
    {
        string Category { get; }
        string SubCategory { get; }
        double Confidence { get; }
    }
}
