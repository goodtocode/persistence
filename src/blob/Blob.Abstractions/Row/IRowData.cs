using System.Collections.Generic;

namespace GoodToCode.Shared.Blob.Abstractions
{
    public interface IRowData : IRowMetadata
    {        
        IEnumerable<ICellData> Cells { get; }

        Dictionary<string, object> ToDictionary();
    }
}
