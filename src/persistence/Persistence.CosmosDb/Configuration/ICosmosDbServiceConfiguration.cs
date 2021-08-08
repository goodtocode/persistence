using GoodToCode.Shared.Persistence.Abstractions;

namespace GoodToCode.Shared.Persistence.CosmosDb
{
    public interface ICosmosDbServiceConfiguration : IPersistenceServiceConfiguration
    {
        string ContainerName { get; }
        string PartitionKeyName { get; }
    }
}
