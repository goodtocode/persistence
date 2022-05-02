using System.Collections.Generic;

namespace GoodToCode.Persistence.Abstractions
{
    public interface ISheetMetadata : IWorkbookMetadata
    {
        int SheetIndex { get; }
        string SheetName { get; }
    }
}
