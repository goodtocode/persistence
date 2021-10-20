using System.Collections.Generic;

namespace GoodToCode.Shared.Blob.Abstractions
{
    public static class RowDataExtensions
    {
        public static Dictionary<string, object> ToDictionary(this IEnumerable<IRowData> item)
        {
            var returnDict = new Dictionary<string, object>();
            foreach (var row in item)
                foreach (var cell in row.Cells)
                    if (!string.IsNullOrWhiteSpace(cell.ColumnName) && cell.CellValue != null)
                        returnDict.Add(cell.ColumnName, cell.CellValue);

            return returnDict;
        }

        public static Dictionary<string, object> ToDictionary(this IEnumerable<RowData> item)
        {
            var returnDict = new Dictionary<string, object>();
            foreach(var row in item)
                foreach (var cell in row.Cells)
                    if (!string.IsNullOrWhiteSpace(cell.ColumnName) && cell.CellValue != null)
                        returnDict.Add(cell.ColumnName, cell.CellValue);

            return returnDict;
        }
    }
}
