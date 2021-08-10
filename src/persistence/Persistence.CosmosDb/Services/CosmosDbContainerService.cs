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
        private Database database;
        private Container container;

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
            try
            {
                database = await client.CreateDatabaseIfNotExistsAsync(cosmosConfig.DatabaseName);                
            }
            catch (CosmosException ex)
            {
                logger.LogError($"New database {cosmosConfig.DatabaseName} was not added successfully - error details: {ex.Message}");
                throw;
            }
 
            return database;
        }

        public async Task<Container> CreateContainerAsync()
        {            
            try
            {
                var database = await CreateDatabaseAsync();
                container = await database.CreateContainerIfNotExistsAsync(cosmosConfig.ContainerName, cosmosConfig.PartitionKeyPath);
                return container; 
            }
            catch (CosmosException ex)
            {
                logger.LogError($"New container {cosmosConfig.ContainerName} with partition {cosmosConfig.PartitionKeyPath} was not added successfully - error details: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteDatabaseAsync()
        {
            try
            {
                await database.DeleteAsync();
            }
            catch (CosmosException ex)
            {
                logger.LogError($"Database {cosmosConfig.DatabaseName} was not deleted successfully - error details: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteContainerAsync()
        {
            try
            {
                container = await database.CreateContainerIfNotExistsAsync(cosmosConfig.ContainerName, cosmosConfig.PartitionKeyPath);
                await container.DeleteContainerAsync();
            }
            catch (CosmosException ex)
            {
                logger.LogError($"Container {cosmosConfig.ContainerName} was not deleted successfully - error details: {ex.Message}");
                throw;
            }
        }
    }
}
