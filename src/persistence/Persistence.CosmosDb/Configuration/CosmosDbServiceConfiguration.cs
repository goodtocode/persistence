using GoodToCode.Shared.Persistence;
using Microsoft.Extensions.Options;

namespace GoodToCode.Shared.Persistence.CosmosDb
{
    public class CosmosDbServiceConfiguration : ICosmosDbServiceConfiguration
    {
        public string ConnectionString { get; private set; }
        public string DatabaseName { get; private set; }
        public string ContainerName { get; private set; }
        public string PartitionKey { get; private set; }

        public CosmosDbServiceConfiguration(string connectionString, string databaseName, string containerName, string partitionKeyName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
            ContainerName = containerName;
            PartitionKey = partitionKeyName;
        }
    }

    public class CosmosDbServiceConfigurationValidation : IValidateOptions<CosmosDbServiceConfiguration>
    {
        public ValidateOptionsResult Validate(string name, CosmosDbServiceConfiguration options)
        {
            if (string.IsNullOrEmpty(options.ConnectionString))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.ConnectionString)} configuration parameter for the Azure Cosmos DB is required");
            }

            if (string.IsNullOrEmpty(options.ContainerName))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.ContainerName)} configuration parameter for the Azure Cosmos DB is required");
            }

            if (string.IsNullOrEmpty(options.DatabaseName))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.DatabaseName)} configuration parameter for the Azure Cosmos DB is required");
            }

            if (string.IsNullOrEmpty(options.PartitionKey))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.PartitionKey)} configuration parameter for the Azure Cosmos DB is required");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
