using System.Collections.Generic;

namespace GoodToCode.Shared.Blob.Excel
{
    public interface IRowData : IRowMetadata
    {        
        IEnumerable<ICellData> Cells { get; }
    }
}
