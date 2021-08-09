namespace GoodToCode.Shared.Blob.Abstractions
{
    public interface ICellData : ICellMetadata
    {
        string CellValue { get; }
    }
}
