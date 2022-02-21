using GoodToCode.Shared.Persistence.Abstractions;

namespace GoodToCode.Shared.Persistence.CosmosDb
{
    public interface ICosmosDbServiceConfiguration : IPersistenceServiceConfiguration
    {
        string DatabaseName { get; }
        string TableName { get; }
        string PartitionKeyPath { get; }
    }
}
