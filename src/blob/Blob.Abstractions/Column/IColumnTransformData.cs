namespace GoodToCode.Shared.Blob.Abstractions
{
    public interface IColumnTransformData : IColumnMetadata
    {
        string ColumnValue { get; }
        string TransformedColumnName { get; }
        string TransformedColumnValue { get; }
    }
}
