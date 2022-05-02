using GoodToCode.Persistence.Abstractions;
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
        ISheetData GetSheet(Stream fileStream, int sheet, string name);
        IWorkbookData GetWorkbook(Stream fileStream);
        IWorkbookData GetWorkbook(Stream fileStream, string name);
    }
}