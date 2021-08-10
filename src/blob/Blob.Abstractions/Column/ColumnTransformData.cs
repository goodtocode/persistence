using System.Collections.Generic;
using System.Linq;

namespace GoodToCode.Shared.Blob.Abstractions
{
    public class ColumnTransformData : IColumnTransformData
    {
        public string ColumnValue { get; set; }

        public string TransformedColumnName { get; set; }

        public string TransformedColumnValue { get; set; }

        public int ColumnIndex { get; set; }

        public string ColumnName { get; set; }

        public int SheetIndex { get; set; }

        public string SheetName { get; set; }

        public string WorkbookName { get; set; }

        public ColumnTransformData()
        {
        }

        public ColumnTransformData(int columnIndex, IEnumerable<ICellData> cells)
        {
            ColumnIndex = columnIndex;
            var cell = cells.FirstOrDefault();
            ColumnName = cell.ColumnName;
            SheetIndex = cell.SheetIndex;
            SheetName = cell.SheetName;
            WorkbookName = cell.WorkbookName;
        }
    }
}
