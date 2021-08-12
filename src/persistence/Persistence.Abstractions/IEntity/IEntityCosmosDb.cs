using System;

namespace GoodToCode.Shared.Persistence.Abstractions
{
    public interface IEntityCosmosDb : IEntity
    {
        string id { get { return RowKey; } }
        string PartitionKeyPath { get; }
    }
}