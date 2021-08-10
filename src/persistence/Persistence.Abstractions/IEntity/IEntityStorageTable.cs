using System;

namespace GoodToCode.Shared.Persistence.Abstractions
{
    public interface IEntityStorageTable
    {
        string id { get; }
        string PartitionKey { get; }
    }
}