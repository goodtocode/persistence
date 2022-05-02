using GoodToCode.Persistence.Abstractions;

namespace GoodToCode.Persistence.Azure.CosmosDb
{
    public interface ICosmosDbServiceConfiguration : IPersistenceServiceConfiguration
    {
        string DatabaseName { get; }
        string TableName { get; }
        string PartitionKeyPath { get; }
    }
}
