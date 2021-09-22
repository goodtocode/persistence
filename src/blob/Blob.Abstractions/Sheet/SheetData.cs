using System.Collections.Generic;
using System.Linq;

namespace GoodToCode.Shared.Blob.Abstractions
{
    public class SheetData : ISheetData
    {
        public int SheetIndex { get; set; }
        public string SheetName { get; set; }

        public IEnumerable<IRowData> Rows { get; set; }

        public string WorkbookName { get; set; }

        public SheetData(string sheetName, IEnumerable<IRowData> rows)
        {
            SheetName = sheetName;
            Rows = rows;
        }

        public SheetData(int sheetIndex, string sheetName, IEnumerable<IRowData> rows) : this(sheetName, rows)
        {
            SheetIndex = sheetIndex;
        }

        public IEnumerable<ICellData> GetColumn(int columnIndex)
        {            
            return Rows.SelectMany(r => r.Cells.Where(c => c.ColumnIndex == columnIndex));
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
