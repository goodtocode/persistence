namespace GoodToCode.Shared.Blob.Excel
{
    public interface IColumnMetada : ISheetMetadata, IRowMetadata
    {
        int ColumnIndex { get; }
        string ColumnKey { get; }
    }
}
