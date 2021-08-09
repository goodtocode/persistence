using GoodToCode.Shared.Persistence.Abstractions;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Persistence.CosmosDb
{
    public sealed class CosmosDbContainerService<T> : ICosmosDbContainerService<T> where T : class, IEntity
    {
        private readonly ILogger<CosmosDbContainerService<T>> logger;
        private readonly ICosmosDbServiceConfiguration cosmosConfig;
        private readonly CosmosClient client;

        public CosmosDbContainerService(CosmosDbServiceOptions options) =>
            cosmosConfig = options.Value;

        public CosmosDbContainerService(CosmosDbServiceOptions dataServiceConfiguration,
                                   ILogger<CosmosDbContainerService<T>> log)
        {
            cosmosConfig = dataServiceConfiguration.Value;
            client = new CosmosClient(cosmosConfig.ConnectionString);
            logger = log;
        }

        public async Task<Database> CreateDatabaseAsync()
        {
            Database db;
            try
            {                
                db = await client.CreateDatabaseIfNotExistsAsync(cosmosConfig.DatabaseName);                
            }
            catch (CosmosException ex)
            {
                logger.LogError($"New database {cosmosConfig.DatabaseName} was not added successfully - error details: {ex.Message}");
                throw;
            }
 
            return db;
        }

        public async Task<Container> CreateContainerAsync()
        {            
            try
            {
                var db = await CreateDatabaseAsync();
                return await db.CreateContainerIfNotExistsAsync(cosmosConfig.ContainerName, cosmosConfig.PartitionKeyName);
            }
            catch (CosmosException ex)
            {
                logger.LogError($"New container {cosmosConfig.ContainerName} with partition {cosmosConfig.PartitionKeyName} was not added successfully - error details: {ex.Message}");
                throw;
            }
        }
    }
}
