using GoodToCode.Shared.Blob.Abstractions;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GoodToCode.Shared.Blob.Excel
{
    public class ExcelService : IExcelService
    {       
        public ISheetData GetSheet(Stream fileStream, int sheetIndex)
        {
            var currSheet = WorkbookFactory.Create(fileStream).GetSheetAt(sheetIndex);
            if (currSheet == null)
                throw new ArgumentOutOfRangeException("Sheet not found.");
            var rows = new List<IRowData>();
            for(int count = currSheet.FirstRowNum; count < currSheet.LastRowNum; count++)
            {
                var row = currSheet.GetRow(count);
                var cells = row.Cells.GetRange(0, row.Cells.Count - 1).Select(c => new CellData() { CellValue = c.StringCellValue, ColumnIndex = c.ColumnIndex, RowIndex = count, SheetKey = currSheet.SheetName });
                rows.Add(new RowData(count, "", cells ));
            }               
            return new SheetData(sheetIndex, currSheet.SheetName, rows);
        }

        public IColumnData GetColumn(Stream fileStream, int sheet, int column)
        {
            ISheetData currSheet = GetSheet(fileStream, sheet);
            if (currSheet == null)
                throw new ArgumentOutOfRangeException("Sheet not found.");
            return currSheet.GetColumn(column);
        }

        public IRowData GetRow(Stream fileStream, int sheet, int row)
        {            
            ISheetData currSheet = GetSheet(fileStream, sheet);
            if (currSheet == null)
                throw new ArgumentOutOfRangeException("Sheet not found.");

            return currSheet.GetRow(row);
        }

        public ICellData GetCell(Stream fileStream, int sheet, int column, int row)
        {
            ISheetData currSheet = GetSheet(fileStream, sheet);
            if (currSheet == null)
                throw new ArgumentOutOfRangeException("Sheet not found.");
            
            return currSheet.GetCell(column, row);
        }
    }
}
