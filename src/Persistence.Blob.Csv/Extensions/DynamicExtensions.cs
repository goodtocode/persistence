using GoodToCode.Persistence.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace GoodToCode.Shared.Blob.Csv
{
    public static class DynamicExtensions
    {
        private const string defaultSheetName = "Sheet 1";
        public static ISheetData ToSheetData(this IEnumerable<dynamic> item, bool hasHeaderRow = true)
        {

            ISheetData returnSheet;
            var rowsToAdd = new List<IRowData>();
            var cellsToAdd = new List<ICellData>();
            int currRow = 0;
            Dictionary<string, object> headerDict;

            foreach (var row in item)
            {
                if (row is System.Dynamic.ExpandoObject rowExp)
                {
                    if (hasHeaderRow && currRow == 0)
                    {
                        headerDict = rowExp.ToDictionary(x => x.Key, y => y.Value);
                    }                        
                    else
                    {
                        var rowDict = rowExp.ToDictionary(x => x.Key, y => y.Value);
                        var rowCells = rowDict.Select((c, i) => new CellData() {RowIndex = currRow, ColumnIndex = i, ColumnName = c.Key, CellValue = c.Value.ToString() });
                        var rowToAdd = new RowData(currRow, rowCells);
                        rowsToAdd.Add(rowToAdd);
                        cellsToAdd.AddRange(rowCells);
                    }
                    currRow++;
                }
            }
            returnSheet = new SheetData(0, defaultSheetName, rowsToAdd, cellsToAdd);

            return returnSheet;
        }
    }
}
