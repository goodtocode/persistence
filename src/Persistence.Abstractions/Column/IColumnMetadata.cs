namespace GoodToCode.Persistence.Abstractions
{
    public interface IColumnMetadata
    {
        int ColumnIndex { get; }
        string ColumnName { get; }
    }
}
