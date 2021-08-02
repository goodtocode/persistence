using System;

namespace GoodToCode.Shared.Persistence
{
    public interface IEntity
    {
        Guid Id { get; }
        string PartitionKey { get; }
    }
}