using System;

namespace GoodToCode.Shared.Persistence.CosmosDb
{
    public interface IEntity
    {
        Guid Id { get; }
        string PartitionKey { get; }
    }
}