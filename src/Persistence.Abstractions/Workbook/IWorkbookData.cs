using System.Collections.Generic;

namespace GoodToCode.Shared.Blob.Abstractions
{
    public interface IWorkbookData : IWorkbookMetadata
    {
        IEnumerable<ISheetData> Sheets { get; }
        IEnumerable<IEnumerable<Dictionary<string, object>>> ToDictionary();
    }
}
