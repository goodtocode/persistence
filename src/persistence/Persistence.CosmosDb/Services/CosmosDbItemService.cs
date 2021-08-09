using GoodToCode.Shared.Persistence.Abstractions;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Persistence.CosmosDb
{
    public class CosmosDbItemService<T> : ICosmosDbItemService<T> where T : IEntity, new()
    {
        private readonly ILogger<CosmosDbItemService<T>> logger;
        private readonly ICosmosDbServiceConfiguration config;
        private readonly CosmosClient client;
        private Database database;
        private Container container;

        public CosmosDbItemService(CosmosDbServiceOptions options)
        {
            config = options.Value;
        }
            
        public CosmosDbItemService(ICosmosDbServiceConfiguration dataServiceConfiguration,
                           ILogger<CosmosDbItemService<T>> log)
        {
            logger = log;
            config = dataServiceConfiguration;
            client = new CosmosClient(config.ConnectionString);           
        }

        private async void CreateContainerAsync()
        {
            database = await client.CreateDatabaseIfNotExistsAsync(config.DatabaseName);
            container = await database.CreateContainerIfNotExistsAsync(config.ContainerName, config.PartitionKeyName);
        }

        public async Task AddItemAsync(T item)
        {
            await container.CreateItemAsync<T>(item, new PartitionKey(item.PartitionKey));
        }

        public async Task DeleteItemAsync(string id)
        {
            await container.DeleteItemAsync<T>(id, new PartitionKey(id));
        }

        public async Task<T> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<T> response = await this.container.ReadItemAsync<T>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new T();
            }
        }

        public async Task<IEnumerable<T>> GetItemsAsync(string queryString)
        {
            var query = this.container.GetItemQueryIterator<T>(new QueryDefinition(queryString));
            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(string id, T item)
        {
            await this.container.UpsertItemAsync<T>(item, new PartitionKey(id));
        }
    }
}
