﻿using System.Collections.Generic;
using System.Linq;

namespace GoodToCode.Persistence.Abstractions
{
    public class SheetData : ISheetData
    {
        public string WorkbookName { get; set; }
        public int SheetIndex { get; set; }
        public string SheetName { get; set; }
        public IEnumerable<IRowData> Rows { get; set; }

        public SheetData(int sheetIndex, string sheetName, IEnumerable<IRowData> rows)
        {
            SheetIndex = sheetIndex;
            SheetName = sheetName;
            Rows = rows;
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
            return GetRow(rowIndex).Cells.Where(r => r.ColumnIndex == columnIndex).FirstOrDefault();
        }

        public IEnumerable<Dictionary<string, object>> ToDictionary()
        {
            var returnDict = new List<Dictionary<string, object>>();

            foreach (var row in Rows)
                returnDict.Add(row.ToDictionary());

            return returnDict;
        }
    }
}
