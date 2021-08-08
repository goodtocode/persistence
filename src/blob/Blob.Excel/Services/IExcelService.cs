using GoodToCode.Shared.Blob.Abstractions;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.IO;

namespace GoodToCode.Shared.Blob.Excel
{
    public interface IExcelService
    {
        ICellData GetCell(Stream fileStream, int sheet, int row, int cell);
        IColumnData GetColumn(Stream fileStream, int sheet, int column);
        IRowData GetRow(Stream fileStream, int sheet, int row);
        ISheetData GetSheet(Stream fileStream, int sheet);
    }
}