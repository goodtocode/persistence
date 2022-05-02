using System;

namespace GoodToCode.Persistence.Abstractions
{
    public interface IEntity
    {
        string RowKey { get; }
        string PartitionKey { get; }
        DateTimeOffset? Timestamp { get; }
    }
}