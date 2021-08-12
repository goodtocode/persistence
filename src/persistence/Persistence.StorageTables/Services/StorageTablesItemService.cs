using GoodToCode.Shared.Persistence.Abstractions;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Persistence.StorageTables
{
    public class StorageTablesItemService<T> : IStorageTablesItemService<T> where T : IEntity, new()
    {
        private readonly ILogger<StorageTablesItemService<T>> logger;
        private readonly IStorageTablesServiceConfiguration config;
        private CosmosClient client;
        private Database database;
        private Container container;

        public StorageTablesItemService(StorageTablesServiceOptions options,
                           ILogger<StorageTablesItemService<T>> log)
        {
            logger = log;
            config = options.Value;
        }

        public StorageTablesItemService(IStorageTablesServiceConfiguration dataServiceConfiguration,
                           ILogger<StorageTablesItemService<T>> log)
        {
            logger = log;
            config = dataServiceConfiguration;
        }

        private async Task CreateAsync()
        {
            try
            {
                client ??= new CosmosClient(config.ConnectionString);
                database ??= await client.CreateDatabaseIfNotExistsAsync(config.DatabaseName);
                container ??= await database.CreateContainerIfNotExistsAsync(config.ContainerName, config.PartitionKeyPath);
            }
            catch (CosmosException ex)
            {
                logger.LogError(ex, ex.Message);
                if (database == null) logger.LogError($"New database {config.DatabaseName} was not added successfully - error details: {ex.Message}");
                if (container == null) logger.LogError($"New database {config.DatabaseName} was not added successfully - error details: {ex.Message}");
                throw;
            }
        }

        public async Task AddItemAsync(T item)
        {
            try
            {
                await CreateAsync();
                await container.CreateItemAsync<T>(item);
            }
            catch (CosmosException ex)
            {
                logger.LogError($"Item {item.PartitionKey}-{item.id} was not added successfully - error details: {ex.Message}");
                throw;
            }

        }

        public async Task DeleteItemAsync(string id)
        {
            await DeleteItemAsync(id, id);
        }

        public async Task<T> GetItemAsync(Guid id, string partitionKey)
        {
            return await GetItemAsync(id.ToString(), partitionKey);
        }

        public async Task<T> GetItemAsync(string id, string partitionKey)
        {
            try
            {
                await CreateAsync();
                ItemResponse<T> response = await container.ReadItemAsync<T>(id, new PartitionKey(partitionKey));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new T();
            }
            catch (CosmosException ex)
            {
                logger.LogError($"Item {id} was not queried successfully - error details: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetItemsAsync(string queryString)
        {
            var results = new List<T>();
            try
            {
                await CreateAsync();
                var query = container.GetItemQueryIterator<T>(new QueryDefinition(queryString));
                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    results.AddRange(response.ToList());
                }
            }
            catch (CosmosException ex)
            {
                logger.LogError($"Item {queryString} was not queried successfully - error details: {ex.Message}");
                throw;
            }

            return results;
        }

        public async Task UpdateItemAsync(string id, T item)
        {
            try
            {
                await CreateAsync();
                await container.UpsertItemAsync<T>(item);
            }
            catch (CosmosException ex)
            {
                logger.LogError($"Item {item.PartitionKey}-{item.id} was not updated successfully - error details: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteItemAsync(string id, string partitionKey)
        {
            try
            {
                await CreateAsync();
                await container.DeleteItemAsync<T>(id, new PartitionKey(partitionKey));
            }
            catch (CosmosException ex)
            {
                logger.LogError($"Item {partitionKey}-{id} was not deleted successfully - error details: {ex.Message}");
                throw;
            }
        }
    }
}
