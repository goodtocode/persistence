using System.Collections.Generic;

namespace GoodToCode.Shared.Blob.Abstractions
{
    public interface ISheetMetadata : IWorkbookMetadata
    {
        int SheetIndex { get; }
        string SheetName { get; }
    }
}
