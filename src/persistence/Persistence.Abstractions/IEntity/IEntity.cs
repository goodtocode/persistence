using System;

namespace GoodToCode.Shared.Persistence.Abstractions
{
    public interface IEntity
    {
        string RowKey { get; }
        string PartitionKey { get; }
    }
}