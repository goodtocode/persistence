using System;
using System.Collections.Generic;
using System.Linq;

namespace GoodToCode.Shared.Blob.Abstractions
{
    public class SheetData : ISheetData
    {
        public string SheetKey { get; set; }

        public IEnumerable<IRowData> Rows { get; set; }

        public string WorkbookKey { get; set; }

        public SheetData(string sheetKey, IEnumerable<IRowData> rows)
        {
            SheetKey = sheetKey;
            Rows = rows;
        }

        public IColumnData GetColumn(int columnIndex)
        {
            var cells = Rows.SelectMany(r => r.Cells.Where(c => c.ColumnIndex == columnIndex));
            return new ColumnData() { Cells = cells, ColumnIndex = columnIndex };
        }

        public IRowData GetRow(int rowIndex)
        {
            return Rows.Where(r => r.RowIndex == rowIndex).FirstOrDefault();
        }

        public ICellData GetCell(int rowIndex, int columnIndex)
        {
            var row = Rows.Where(r => r.RowIndex == rowIndex).FirstOrDefault();
            return row.Cells.Where(y => y.ColumnIndex == columnIndex).FirstOrDefault();
        }
    }
}
