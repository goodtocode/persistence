using System.Collections.Generic;

namespace GoodToCode.Shared.Blob.Abstractions
{
    public class RowData : IRowData
    {
        public IEnumerable<ICellData> Cells { get; set; }
        public int RowIndex { get; set; }

        public RowData()
        {

        }

        public RowData(int rowIndex, IEnumerable<ICellData> cells)
        {
            RowIndex = rowIndex;
            Cells = cells;
        }
    }
}
