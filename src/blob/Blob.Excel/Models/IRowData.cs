using System.Collections.Generic;

namespace GoodToCode.Shared.Blob.Excel
{
    public interface IRowData : IRowMetadata
    {        
        ICollection<ICellData> Cells { get; }
    }
}
