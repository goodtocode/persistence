using System.Collections.Generic;

namespace GoodToCode.Shared.Blob.Abstractions
{
    public interface ISheetData : ISheetMetadata
    {
        IEnumerable<IRowData> Rows { get; }
        IColumnData GetColumn(int columnIndex);
        IRowData GetRow(int rowIndex);
        ICellData GetCell(int columnIndex, int rowIndex);
    }
}
