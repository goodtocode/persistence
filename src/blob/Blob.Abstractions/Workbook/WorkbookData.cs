using System.Collections.Generic;

namespace GoodToCode.Shared.Blob.Abstractions
{
    public class WorkbookData : IWorkbookData
    {
        public string WorkbookName { get; set; }

        public IEnumerable<ISheetMetadata> SheetMetadata { get; set; }

        public WorkbookData(string workbookKey, IEnumerable<ISheetMetadata> sheetMetadatas)
        {
            WorkbookName = workbookKey;
            SheetMetadata = sheetMetadatas;
        }
    }
}
