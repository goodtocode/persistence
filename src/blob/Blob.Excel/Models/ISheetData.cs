using System.Collections.Generic;

namespace GoodToCode.Shared.Blob.Excel
{
    public interface ISheetData : ISheetMetadata
    {        
        IEnumerable<IRowData> Rows { get; }
    }
}
