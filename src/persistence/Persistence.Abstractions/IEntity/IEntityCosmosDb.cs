using System;

namespace GoodToCode.Shared.Persistence.Abstractions
{
    public interface IEntityCosmosDb
    {
        string id { get; }
        string PartitionKey { get; }
        string PartitionKeyPath { get; }
    }
}