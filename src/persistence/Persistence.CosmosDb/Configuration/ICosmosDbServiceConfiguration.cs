using GoodToCode.Shared.Persistence.Abstractions;

namespace GoodToCode.Shared.Persistence.CosmosDb
{
    public interface ICosmosDbServiceConfiguration : IPersistenceServiceConfiguration
    {
        string DatabaseName { get; }
        string ContainerName { get; }
        string PartitionKeyPath { get; }
    }
}
