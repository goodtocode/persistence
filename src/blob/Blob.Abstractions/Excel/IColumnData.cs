using System.Collections.Generic;

namespace GoodToCode.Shared.Blob.Abstractions
{
    public interface IColumnData : IColumnMetadata
    {
        IEnumerable<ICellData> Cells { get; }
    }
}
