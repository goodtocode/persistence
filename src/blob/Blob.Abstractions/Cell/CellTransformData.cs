using System.Collections.Generic;
using System.Linq;

namespace GoodToCode.Shared.Blob.Abstractions
{
    public class CellTransformData : ICellTransformData
    {
        public string TransformName { get ;set; }

        public string TransformValue { get ;set; }

        public string CellValue { get ;set; }

        public int SheetIndex { get ;set; }

        public string SheetName { get ;set; }

        public string WorkbookName { get ;set; }

        public int RowIndex { get ;set; }

        public int ColumnIndex { get; set; }

        public string ColumnName { get; set; }

        public CellTransformData()
        {
        }

        public CellTransformData(ICellData cell, string transformName, string transformValue)
        {
            TransformName = transformName;
            TransformValue = transformValue;
            ColumnIndex = cell.ColumnIndex;
            ColumnName = cell.ColumnName;
            RowIndex = cell.RowIndex;
            SheetIndex = cell.SheetIndex;
            SheetName = cell.SheetName;
            WorkbookName = cell.WorkbookName;
        }
    }
}
