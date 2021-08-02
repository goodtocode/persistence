namespace GoodToCode.Shared.Persistence
{
    public interface ICosmosDbServiceConfiguration : IPersistenceServiceConfiguration
    {
        string ContainerName { get; set; }
        string PartitionKeyPath { get; set; }
    }
}
