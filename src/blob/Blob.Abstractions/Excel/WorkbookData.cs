using System.Collections.Generic;

namespace GoodToCode.Shared.Blob.Abstractions
{
    public class WorkbookData : IWorkbookData
    {
        public IEnumerable<ISheetData> Sheets { get; set; }

        public string WorkbookKey { get; set; }
    }
}
