namespace GoodToCode.Shared.Blob.Excel
{
    public interface ICellData : ICellMetadata
    {
        string CellValue { get; }
    }
}
