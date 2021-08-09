namespace GoodToCode.Shared.Blob.Abstractions
{
    public interface IColumnMetadata : ISheetMetadata
    {
        int ColumnIndex { get; }
    }
}
