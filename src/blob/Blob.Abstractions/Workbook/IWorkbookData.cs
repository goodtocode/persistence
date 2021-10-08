using System.Collections.Generic;

namespace GoodToCode.Shared.Blob.Abstractions
{
    public interface IWorkbookData : IWorkbookMetadata
    {
        IEnumerable<ISheetMetadata> Sheets { get; }
    }
}
