using System.Collections.Generic;

namespace GoodToCode.Persistence.Abstractions
{
    public interface IRowData : IRowMetadata
    {        
        IEnumerable<ICellData> Cells { get; }

        Dictionary<string, object> ToDictionary();
    }
}
