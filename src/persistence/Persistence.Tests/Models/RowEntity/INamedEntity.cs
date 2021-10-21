namespace GoodToCode.Shared.Persistence.Tests
{
    public interface IAnalyzedText
    {
        string AnalyzedText { get; }
    }

    public interface IAnalyticsResult : IAnalyzedText
    {
        string Category { get; }
        string SubCategory { get; }
        double Confidence { get; }
    }

    public interface INamedEntity : IRowEntity, IAnalyticsResult
    {
    }
}
