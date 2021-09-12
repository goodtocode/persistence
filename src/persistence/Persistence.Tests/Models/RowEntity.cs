using GoodToCode.Shared.Persistence.Abstractions;
using System.Text.Json.Serialization;

namespace GoodToCode.Shared.Persistence.Tests
{
    public class RowEntity : IEntity
    {
        [JsonInclude]
        public string PartitionKey { get; private set; }
        [JsonInclude]
        public string RowKey { get; private set; }
        [JsonInclude]
        public string SheetName { get; private set; }
        [JsonInclude]
        public string ColumnName { get; private set; }
        [JsonInclude]
        public int RowIndex { get; private set; }
        [JsonInclude]
        public string CellValue { get; private set; }
        [JsonInclude]
        public string KeyPhrase { get; private set; }

        public RowEntity() { }
    }
}


