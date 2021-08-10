using GoodToCode.Shared.Blob.Abstractions;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.Linq;

namespace GoodToCode.Shared.Blob.Excel
{
    public static class ISheetExtensions
    {
        public static SheetData ToSheetData(this ISheet item)
        {
            var rows = new List<IRowData>();
            for (int count = item.FirstRowNum; count < item.LastRowNum; count++)
            {
                var row = item.GetRow(count);
                var cells = row.Cells.GetRange(0, row.Cells.Count - 1).Select(c => 
                            new CellData() { CellValue = c.ToString(), ColumnIndex = c.ColumnIndex, RowIndex = count, SheetName = item.SheetName });
                rows.Add(new RowData(count, cells));
            }
            return new SheetData(item.SheetName, rows);
        }
    }
}
