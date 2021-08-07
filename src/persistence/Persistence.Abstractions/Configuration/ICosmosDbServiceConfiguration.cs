namespace GoodToCode.Shared.Persistence.Abstractions
{
    public interface ICosmosDbServiceConfiguration : IPersistenceServiceConfiguration
    {
        string ContainerName { get; set; }
        string PartitionKeyPath { get; set; }
    }
}
