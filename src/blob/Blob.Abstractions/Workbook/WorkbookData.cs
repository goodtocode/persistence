using System.Collections.Generic;

namespace GoodToCode.Shared.Blob.Abstractions
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
    }
}
