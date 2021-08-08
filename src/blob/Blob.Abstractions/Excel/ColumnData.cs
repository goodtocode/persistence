using System.Collections.Generic;

namespace GoodToCode.Shared.Blob.Abstractions
{
    public class ColumnData : IColumnData
    {
        public IEnumerable<ICellData> Cells { get; set; }

        public int ColumnIndex { get; set; }

        public int SheetIndex { get; set; }

        public string SheetKey { get; set; }

        public string WorkbookKey { get; set; }


        public ColumnData()
        {

        }

        public ColumnData(int columnIndex, ICollection<ICellData> cells)
        {
            ColumnIndex = columnIndex;
            Cells = cells;
        }

        public ColumnData(int columnIndex, IEnumerable<ICellData> cells) : this(columnIndex, (ICollection<ICellData>)cells)
        {
        }
    }
}
