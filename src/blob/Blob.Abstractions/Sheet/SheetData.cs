using System.Collections.Generic;
using System.Linq;

namespace GoodToCode.Shared.Blob.Abstractions
{
    public class SheetData : ISheetData
    {
        public int SheetIndex { get; set; }
        public string SheetName { get; set; }

        public IEnumerable<ICellData> Cells { get; set; }
        public IEnumerable<IRowData> Rows { get; set; }

        public string WorkbookName { get; set; }

        public SheetData(string sheetName, IEnumerable<IRowData> rows, IEnumerable<ICellData> cells)
        {
            SheetName = sheetName;
            Rows = rows;
            Cells = cells;
        }

        public SheetData(int sheetIndex, string sheetName, IEnumerable<IRowData> rows, IEnumerable<ICellData> cells) : this(sheetName, rows, cells)
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
            return Cells.Where(r => r.RowIndex == rowIndex && r.ColumnIndex == columnIndex).FirstOrDefault();
        }
    }
}
