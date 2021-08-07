using System;

namespace GoodToCode.Shared.Persistence.Abstractions
{
    public interface IEntity
    {
        Guid Id { get; }
        string PartitionKey { get; }
    }
}