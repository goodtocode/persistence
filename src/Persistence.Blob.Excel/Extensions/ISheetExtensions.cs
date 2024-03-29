﻿using GoodToCode.Persistence.Abstractions;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoodToCode.Persistence.Blob.Excel
{
    public static class ISheetExtensions
    {
        public static ISheetData ToSheetData(this ISheet item, int sheetIndex = 0, bool hasHeaderRow = true)
        {
            return ToSheetData(item, sheetIndex, DateTime.UtcNow.ToString("s"), hasHeaderRow);
        }

        public static ISheetData ToSheetData(this ISheet item, int sheetIndex, string workbookName, bool hasHeaderRow = true)
        {
            IRow header = null;
            var rows = new List<IRowData>();
            int firstRow = item.FirstRowNum;
            if (hasHeaderRow && firstRow == 0)
            {
                header = item.GetRow(firstRow);
                firstRow = 1;
            }
            for (int count = firstRow; count <= item.LastRowNum; count++)
            {
                var row = item.GetRow(count);
                if (row != null)
                {
                    var rowIndex = count;
                    var rowCells = row.Cells.GetRange(0, row.Cells.Count).Select(c =>
                                new CellData()
                                {
                                    CellValue = c.ToString(),
                                    ColumnName = header != null ? header.GetCell(c.ColumnIndex).ToString() : c.ColumnIndex.ToString(),
                                    ColumnIndex = c.ColumnIndex,
                                    RowIndex = rowIndex,
                                    SheetName = item.SheetName ?? string.Empty,
                                    SheetIndex = sheetIndex,
                                    WorkbookName = workbookName ?? string.Empty
                                });
                    rows.Add(new RowData(count, rowCells));
                }
            }
            return new SheetData(sheetIndex, item.SheetName, rows);
        }
    }
}
