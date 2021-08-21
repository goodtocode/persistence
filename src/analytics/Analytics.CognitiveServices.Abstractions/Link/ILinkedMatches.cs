namespace GoodToCode.Shared.Analytics.Abstractions
{
    public interface ILinkedMatches
    {
        string Matches { get; set; }
        string Name { get; set; }
        string Url { get; set; }
    }
}