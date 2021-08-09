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

        public CosmosDbContainerService(CosmosDbServiceConfiguration config, IOptions<CosmosDbServiceOptions> options) =>
            cosmosConfig = options.Value;

        public CosmosDbContainerService(ICosmosDbServiceConfiguration dataServiceConfiguration,
                                   ILogger<CosmosDbContainerService<T>> log)
        {
            cosmosConfig = dataServiceConfiguration;            
            logger = log;
        }

        public async Task<Database> CreateDatabaseAsync()
        {
            Database db;
            var client = new CosmosClient(cosmosConfig.ConnectionString);
            try
            {                
                db = await client.CreateDatabaseIfNotExistsAsync(cosmosConfig.DatabaseName);                
            }
            catch (CosmosException ex)
            {
                logger.LogError($"New database {cosmosConfig.DatabaseName} was not added successfully - error details: {ex.Message}");
                throw;
            }
            finally
            {
                client.Dispose();
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
