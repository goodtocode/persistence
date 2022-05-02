namespace GoodToCode.Persistence.Abstractions
{
    public interface ICellData : ICellMetadata
    {
        string CellValue { get; }
    }
}
