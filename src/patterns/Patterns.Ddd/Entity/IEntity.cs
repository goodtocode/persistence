using System;

namespace GoodToCode.Shared.Domain
{
    public interface IEntity
    {
        Guid RowKey { get; }
        string PartitionKey { get; }
    }
}