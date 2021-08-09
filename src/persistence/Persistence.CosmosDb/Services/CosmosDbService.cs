using GoodToCode.Shared.Persistence.Abstractions;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Persistence.CosmosDb
{
    /// <summary>
    /// Usage:
    ///     private readonly IDataService<IEntity> _dataService;
    ///     var myFileContent = GetFileContents(myFile);
    ///     if (myFileContent != null)
    ///     {
    ///         await _dataService.AddAsync(myFileContent);
    ///     }
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class CosmosDbService<T> : ICosmosDbService<T> where T : class, IEntity
    {
        private readonly ILogger<CosmosDbService<T>> logger;
        private readonly ICosmosDbServiceConfiguration cosmosConfig;
        private Database db;

        public CosmosDbService(CosmosDbServiceConfiguration config, IOptions<CosmosDbServiceOptions> options) =>
            cosmosConfig = options.Value;

        public CosmosDbService(ICosmosDbServiceConfiguration dataServiceConfiguration,
                                   ILogger<CosmosDbService<T>> log)
        {
            cosmosConfig = dataServiceConfiguration;            
            logger = log;
        }
        public async Task<Database> CreateDatabaseAsync()
        {
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
                return await db.CreateContainerIfNotExistsAsync(cosmosConfig.ContainerName, cosmosConfig.PartitionKeyName);
            }
            catch (CosmosException ex)
            {
                logger.LogError($"New container {cosmosConfig.ContainerName} with partition {cosmosConfig.PartitionKeyName} was not added successfully - error details: {ex.Message}");
                throw;
            }
        }

        private Container GetContainer()
        {
            var client = new CosmosClient(cosmosConfig.ConnectionString);
            Container container;

            try
            {
                var database = client.GetDatabase(cosmosConfig.DatabaseName);
                container = database.GetContainer(cosmosConfig.ContainerName);
            }
            finally
            {
                client.Dispose();
            }
            return container;
        }

        public async Task<T> AddItemAsync(T newEntity)
        {
            try
            {
                var container = GetContainer();
                ItemResponse<T> createResponse = await container.CreateItemAsync(newEntity);
                return createResponse.Resource;
            }
            catch (CosmosException ex)
            {
                logger.LogError($"New entity {newEntity.RowKey} in {newEntity.PartitionKey} was not added successfully - error details: {ex.Message}");
                throw;
            }
        }
    }
}
