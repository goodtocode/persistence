using System.Collections.Generic;

namespace GoodToCode.Persistence.Abstractions
{
    public interface IWorkbookData : IWorkbookMetadata
    {
        IEnumerable<ISheetData> Sheets { get; }
        IEnumerable<IEnumerable<Dictionary<string, object>>> ToDictionary();
    }
}
