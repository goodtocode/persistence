using Microsoft.Extensions.Options;

namespace GoodToCode.Shared.Persistence.CosmosDb
{
    public class CosmosDbServiceOptions : IOptions<ICosmosDbServiceConfiguration>
    {
        public ICosmosDbServiceConfiguration Value { get; }

        public CosmosDbServiceOptions(string connectionString, string databaseName, string containerName, string partitionKeyName)
        {
            Value = new CosmosDbServiceConfiguration(connectionString, databaseName, containerName, partitionKeyName);
        }
    }
}
