using GoodToCode.Shared.Persistence;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Persistence
{
    //private readonly IDataService<myFileRawDataModel> _dataService;
    //var myFileContent = GetFileContents(myFile);
    //if (myFileContent != null)
    //{
    //    await _dataService.AddAsync(myFileContent);
    //}

    public sealed class CosmosDbService<T> : IPersistenceService<T> where T : class, IEntity
    {
        private readonly ICosmosDbServiceConfiguration _dataServiceConfiguration;
        private readonly CosmosClient _client;
        private readonly ILogger<CosmosDbService<T>> _logger;

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
                _logger.LogError($"New entity with ID: {newEntity.Id} was not added successfully - error details: {ex.Message}");
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
