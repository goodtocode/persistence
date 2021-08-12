using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using GoodToCode.Shared.Persistence.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Persistence.StorageTables
{
    public class StorageTablesItemService<T> : IStorageTablesItemService<T> where T : class, IEntity, new()
    {
        private readonly ILogger<StorageTablesItemService<T>> logger;
        private readonly IStorageTablesServiceConfiguration config;
        private readonly TableServiceClient serviceClient;
        private readonly TableClient tableClient;
        private TableItem table;

        private StorageTablesItemService(ILogger<StorageTablesItemService<T>> log)
        {
            logger = log;
            serviceClient = new TableServiceClient(config.ConnectionString);
            tableClient = new TableClient(config.ConnectionString, config.TableName);

        }

        public StorageTablesItemService(StorageTablesServiceOptions options,
                           ILogger<StorageTablesItemService<T>> log) : this(log)
        {

            config = options.Value;
        }

        public StorageTablesItemService(IStorageTablesServiceConfiguration dataServiceConfiguration,
                           ILogger<StorageTablesItemService<T>> log) : this(log)
        {
            config = dataServiceConfiguration;
        }

        public async Task<TableItem> CreateOrGetTableAsync()
        {
            try
            {
                table = await serviceClient.CreateTableIfNotExistsAsync(config.TableName);
            }
            catch (RequestFailedException ex) when (ex.Status == (int)HttpStatusCode.Conflict)
            {
                if (table == null) logger.LogError($"New table {config.TableName} was not added successfully - error details: {ex.Message}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                if (table == null) logger.LogError($"New table {config.TableName} was not added successfully - error details: {ex.Message}");
                throw;
            }

            return table;
        }

        public TableEntity GetItem(string rowKey)
        {
            return tableClient.Query<TableEntity>(ent => ent.RowKey == rowKey).FirstOrDefault();
        }

        public Pageable<TableEntity> GetAllItems(string partitionKey)
        {
            Pageable<TableEntity> queryResultsFilter;
            try
            {
                queryResultsFilter = tableClient.Query<TableEntity>(filter: $"PartitionKey eq '{partitionKey}'");
            }
            catch (Exception ex)
            {
                logger.LogError($"Items {partitionKey} was not queried successfully - error details: {ex.Message}");
                throw;
            }

            return queryResultsFilter;
        }

        public async Task DeleteTableAsync() =>
            await serviceClient.DeleteTableAsync(config.TableName);


        public async Task<IEnumerable<TableEntity>> AddItemsAsync(IEnumerable<T> items)
        {
            var returnData = new List<TableEntity>();

            foreach (var item in items)
            {
                returnData.Add(await AddItemAsync(item));
            }

            return returnData;
        }

        public async Task<TableEntity> AddItemAsync(T item)
        {
            TableEntity entity = default;
            try
            {
                entity = new TableEntity(item.PartitionKey, item.RowKey)
                    {
                        { "Product", "Marker Set" },
                        { "Price", 5.00 },
                        { "Quantity", 21 }
                    };
                await tableClient.AddEntityAsync(entity);
            }
            catch (RequestFailedException ex) when (ex.Status == (int)HttpStatusCode.Conflict)
            {
                if (entity == null) logger.LogError($"New entity {item.RowKey} was not added successfully - error details: {ex.Message}");
            }
            catch (Exception ex)
            {
                logger.LogError($"Item {item.PartitionKey}-{item.RowKey} was not added successfully - error details: {ex.Message}");
                throw;
            }
            return entity;
        }

        public async Task DeleteItemAsync(string partitionKey, string rowKey) =>
            await tableClient.DeleteEntityAsync(partitionKey, rowKey);

        public Pageable<TableEntity> GetItems(string partitionKey)
        {
            Pageable<TableEntity> results;
            try
            {
                results = tableClient.Query<TableEntity>(filter: $"PartitionKey eq '{partitionKey}'");
            }
            catch (Exception ex)
            {
                logger.LogError($"Item {partitionKey} was not queried successfully - error details: {ex.Message}");
                throw;
            }

            return results;
        }
    }
}
