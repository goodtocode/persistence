using System.Collections.Generic;

namespace GoodToCode.Shared.Blob.Abstractions
{
    public interface ISheetData : ISheetMetadata
    {
        IEnumerable<ICellData> Cells { get; }
        IEnumerable<IRowData> Rows { get; }
        IEnumerable<ICellData> GetColumn(int columnIndex);
        IRowData GetRow(int rowIndex);
        ICellData GetCell(int columnIndex, int rowIndex);
        IEnumerable<Dictionary<string, object>> ToDictionary();
    }
}
