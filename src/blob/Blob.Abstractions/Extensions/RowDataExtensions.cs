using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GoodToCode.Shared.Blob.Abstractions
{
    public static class RowDataExtensions
    {
        public static Dictionary<string, object> ToDictionary(this IEnumerable<IRowData> item)
        {
            var returnDict = new Dictionary<string, object>();
            foreach (var row in item)
            {
                var props = row.GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(x => x.PropertyType.IsPrimitive)
                    .ToDictionary(prop => prop.Name, prop => (object)prop.GetValue(row, null));
                foreach (var prop in props)
                    returnDict.Add(prop.Key, prop.Value);
                foreach (var cell in row.Cells)
                    if (!string.IsNullOrWhiteSpace(cell.ColumnName))
                        returnDict.Add(cell.ColumnName, (cell.CellValue ?? "").ToString());
            }

            return returnDict;
        }
    }
}
