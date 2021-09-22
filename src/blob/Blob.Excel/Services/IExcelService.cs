using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.IO;

namespace GoodToCode.Shared.Blob.Excel
{
    public interface IExcelService
    {
        ICell GetCell(Stream fileStream, int sheet, int row, int cell);
        IEnumerable<ICell> GetColumn(Stream fileStream, int sheet, int column);
        IRow GetRow(Stream fileStream, int sheet, int row);
        ISheet GetSheet(Stream fileStream, int sheet);
        IWorkbook GetWorkbook(Stream fileStream);
    }
}