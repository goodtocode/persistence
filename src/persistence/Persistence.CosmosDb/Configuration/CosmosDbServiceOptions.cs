namespace GoodToCode.Shared.Persistence.CosmosDb
{
    public class CosmosDbServiceOptions : ICosmosDbServiceConfiguration
    {
        public string ContainerName { get; set; }

        public string PartitionKeyName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public CosmosDbServiceOptions(string connectionString, string databaseName, string containerName, string partitionKeyName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
            ContainerName = containerName;
            PartitionKeyName = partitionKeyName;
        }
    }
}
