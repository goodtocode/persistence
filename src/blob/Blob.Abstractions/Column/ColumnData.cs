﻿using System.Collections.Generic;
using System.Linq;

namespace GoodToCode.Shared.Blob.Abstractions
{
    public class ColumnData : IColumnData
    {
        public IEnumerable<ICellData> Cells { get; set; }

        public int ColumnIndex { get; set; }

        public string ColumnName { get; set; }

        public int SheetIndex { get; set; }

        public string SheetName { get; set; }

        public string WorkbookName { get; set; }

        public ColumnData()
        {

        }

        public ColumnData(int columnIndex, IEnumerable<ICellData> cells)
        {
            ColumnIndex = columnIndex;
            Cells = cells;
            var cell = cells.FirstOrDefault();
            ColumnName = cell.ColumnName;
            SheetIndex = cell.SheetIndex;
            SheetName = cell.SheetName;
            WorkbookName = cell.WorkbookName;
        }
    }
}
