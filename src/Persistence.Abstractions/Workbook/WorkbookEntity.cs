using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace GoodToCode.Persistence.Abstractions
{
    public class WorkbookEntity : IWorkbookEntity
    {
        [JsonInclude]
        public string PartitionKey { get; private set; }
        [JsonInclude]
        public string RowKey { get; private set; }
        [JsonInclude]
        public DateTimeOffset? Timestamp { get; set; } = DateTime.UtcNow;

        public string WorkbookName { get; set; }

        public IEnumerable<ISheetData> Sheets { get; set; }

        public WorkbookEntity(string workbookName, IEnumerable<ISheetData> sheets)
        {
            WorkbookName = workbookName;
            Sheets = sheets;
        }

        public ISheetEntity GetSheet(int sheetIndex)
        {            
            return Sheets.FirstOrDefault(r => r.SheetIndex == sheetIndex);
        }

        public IEnumerable<IEnumerable<Dictionary<string, object>>> ToDictionary()
        {
            throw new NotImplementedException();
        }
    }
}
