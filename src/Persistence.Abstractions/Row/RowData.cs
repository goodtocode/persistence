using System.Collections.Generic;

namespace GoodToCode.Persistence.Abstractions
{
    public class RowData : IRowData
    {
        public IEnumerable<ICellData> Cells { get; set; }
        public int RowIndex { get; set; }

        public RowData() { }

        public RowData(int rowIndex, IEnumerable<ICellData> cells)
        {
            RowIndex = rowIndex;
            Cells = cells;
        }

        public Dictionary<string, object> ToDictionary()
        {
            var returnDict = new Dictionary<string, object>();

            foreach (var cell in Cells)
                if(!string.IsNullOrWhiteSpace(cell.ColumnName) && cell.CellValue != null)
                    returnDict.Add(cell.ColumnName, cell.CellValue);

            return returnDict;
        }
    }
}
