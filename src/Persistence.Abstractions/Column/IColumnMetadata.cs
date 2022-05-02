namespace GoodToCode.Shared.Blob.Abstractions
{
    public interface IColumnMetadata
    {
        int ColumnIndex { get; }
        string ColumnName { get; }
    }
}
