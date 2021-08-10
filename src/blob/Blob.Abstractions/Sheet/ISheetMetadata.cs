using System.Collections.Generic;

namespace GoodToCode.Shared.Blob.Abstractions
{
    public interface ISheetMetadata : IWorkbookMetadata
    {
        string SheetKey { get; }
    }
}
