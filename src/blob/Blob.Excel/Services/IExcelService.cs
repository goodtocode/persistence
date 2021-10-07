using GoodToCode.Shared.Blob.Abstractions;
using System.Collections.Generic;
using System.IO;

namespace GoodToCode.Shared.Blob.Excel
{
    public interface IExcelService
    {
        ICellData GetCell(Stream fileStream, int sheet, int row, int cell);
        IEnumerable<ICellData> GetColumn(Stream fileStream, int sheet, int column);
        IRowData GetRow(Stream fileStream, int sheet, int row);
        ISheetData GetSheet(Stream fileStream, int sheet);
        IWorkbookData GetWorkbook(Stream fileStream);
    }
}