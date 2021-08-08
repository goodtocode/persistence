using System;

namespace GoodToCode.Shared.Persistence.Abstractions
{
    public interface IEntity
    {
        Guid RowKey { get; }
        string PartitionKey { get; }
    }
}