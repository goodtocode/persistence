using GoodToCode.Persistence.Abstractions;
using System.Collections.Generic;
using System.IO;

namespace GoodToCode.Persistence.Blob.Csv
{
    public interface ICsvService
    {
        ICellData GetCell(Stream fileStream, int row, int cell);
        IEnumerable<ICellData> GetColumn(Stream fileStream, int column);
        IRowData GetRow(Stream fileStream, int row);
        ISheetData GetSheet(Stream fileStream);
    }
}