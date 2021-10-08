using System.Collections.Generic;

namespace GoodToCode.Shared.Blob.Abstractions
{
    public class WorkbookData : IWorkbookData
    {
        public string WorkbookName { get; set; }

        public IEnumerable<ISheetMetadata> Sheets { get; set; }

        public WorkbookData(string workbookName, IEnumerable<ISheetMetadata> sheets)
        {
            WorkbookName = workbookName;
            Sheets = sheets;
        }
    }
}
