using System.Collections.Generic;

namespace GoodToCode.Persistence.Abstractions
{
    public class WorkbookData : IWorkbookData
    {
        public string WorkbookName { get; set; }

        public IEnumerable<ISheetData> Sheets { get; set; }

        public WorkbookData(string workbookName, IEnumerable<ISheetData> sheets)
        {
            WorkbookName = workbookName;
            Sheets = sheets;
        }

        public IEnumerable<IEnumerable<Dictionary<string, object>>> ToDictionary()
        {
            var returnDict = new List<IEnumerable<Dictionary<string, object>>>();

            foreach (var sheet in Sheets)
                returnDict.Add(sheet.ToDictionary());

            return returnDict;
        }
    }
}
