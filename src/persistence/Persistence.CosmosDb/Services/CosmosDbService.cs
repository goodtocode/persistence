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
        private readonly ICosmosDbServiceConfiguration _dataServiceConfiguration;
        private readonly CosmosClient _client;
        private readonly ILogger<CosmosDbService<T>> _logger;

        public CosmosDbService(IOptions<CosmosDbServiceOptions> options) =>
            _dataServiceConfiguration = options.Value;

        public CosmosDbService(ICosmosDbServiceConfiguration dataServiceConfiguration,
                                   CosmosClient client,
                                   ILogger<CosmosDbService<T>> logger)
        {
            _dataServiceConfiguration = dataServiceConfiguration;
            _client = client;
            _logger = logger;
        }

        public async Task<T> AddAsync(T newEntity)
        {
            try
            {
                var container = GetContainer();
                ItemResponse<T> createResponse = await container.CreateItemAsync(newEntity);
                return createResponse.Resource;
            }
            catch (CosmosException ex)
            {
                _logger.LogError($"New entity {newEntity.RowKey} in {newEntity.PartitionKey} was not added successfully - error details: {ex.Message}");
                throw;
            }
        }

        private Container GetContainer()
        {
            var database = _client.GetDatabase(_dataServiceConfiguration.DatabaseName);
            var container = database.GetContainer(_dataServiceConfiguration.ContainerName);
            return container;
        }
    }
}
