using GoodToCode.Shared.Blob.Abstractions;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoodToCode.Shared.Blob.Excel
{
    public static class ISheetExtensions
    {
        public static SheetData ToSheetData(this ISheet item, int sheetIndex = 0, bool hasHeaderRow = true)
        {
            return ToSheetData(item, DateTime.UtcNow.ToString("s"), sheetIndex, hasHeaderRow);
        }

        public static SheetData ToSheetData(this ISheet item, string workbookName, int sheetIndex = 0, bool hasHeaderRow = true)
        {
            IRow header = null;
            var rows = new List<IRowData>();
            int firstRow = item.FirstRowNum;
            if (hasHeaderRow && firstRow == 0)
            {
                header = item.GetRow(firstRow);
                firstRow = 1;
            }
            for (int count = firstRow; count < item.LastRowNum; count++)
            {
                var row = item.GetRow(count);
                var rowIndex = count;
                var cells = row.Cells.GetRange(0, row.Cells.Count - 1).Select(c =>
                            new CellData()
                            {
                                CellValue = c.ToString(),
                                ColumnName = (header != null ? header.GetCell(c.ColumnIndex).ToString() : c.ColumnIndex.ToString()),
                                ColumnIndex = c.ColumnIndex,
                                RowIndex = rowIndex,
                                SheetName = item.SheetName,
                                SheetIndex = sheetIndex,
                                WorkbookName = workbookName
                            });
                rows.Add(new RowData(count, cells));
            }
            return new SheetData(item.SheetName, rows);
        }
    }
}
