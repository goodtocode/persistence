using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using GoodToCode.Shared.Persistence.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Persistence.CosmosDb
{
    public class CosmosDbItemService<T> : ICosmosDbItemService<T> where T : class, IEntity, new()
    {
        private readonly ILogger<CosmosDbItemService<T>> logger;
        private readonly ICosmosDbServiceConfiguration config;
        private readonly TableServiceClient serviceClient;
        private readonly TableClient tableClient;
        private TableItem table;

        private CosmosDbItemService(ILogger<CosmosDbItemService<T>> log)
        {
            logger = log;
        }

        public CosmosDbItemService(CosmosDbServiceOptions options,
                           ILogger<CosmosDbItemService<T>> log) : this(log)
        {

            config = options.Value;
            serviceClient = new TableServiceClient(config.ConnectionString);
            tableClient = new TableClient(config.ConnectionString, config.TableName);

        }

        public CosmosDbItemService(ICosmosDbServiceConfiguration serviceConfiguration,
                           ILogger<CosmosDbItemService<T>> log) : this(log)
        {
            config = serviceConfiguration;
            serviceClient = new TableServiceClient(config.ConnectionString);
            tableClient = new TableClient(config.ConnectionString, config.TableName);
        }

        public async Task<TableItem> CreateOrGetTableAsync()
        {
            try
            {
                table ??= await serviceClient.CreateTableIfNotExistsAsync(config.TableName);
            }
            catch (RequestFailedException ex) when (ex.Status == (int)HttpStatusCode.Conflict)
            {
                if (table == null) logger.LogError($"New table {config.TableName} was not added successfully - error details: {ex.Message}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                if (table == null) logger.LogError($"New table {config.TableName} was not added successfully - error details: {ex.Message}");
            }

            return table;
        }

        public TableEntity GetItem(string rowKey)
        {
            return tableClient.Query<TableEntity>(ent => ent.RowKey == rowKey).FirstOrDefault();
        }

        public Pageable<TableEntity> GetItems(Expression<Func<TableEntity, bool>> filter)
        {
            return tableClient.Query<TableEntity>(filter);
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

        public async Task DeleteTableAsync()
        {
            await serviceClient.DeleteTableAsync(config.TableName);
        }

        public async Task<TableEntity> AddItemAsync(T item)
        {
            TableEntity entity = default;
            try
            {
                await CreateOrGetTableAsync();
                entity = new TableEntity(item.PartitionKey, item.RowKey);
                foreach (var prop in item.ToDictionary())
                {
                    entity.Add(prop.Key, prop.Value);
                }
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

        public async Task<IEnumerable<TableEntity>> AddItemsAsync(IEnumerable<T> items)
        {
            await CreateOrGetTableAsync();

            var entityList = items.ToTableList<T>();
            var addEntitiesBatch = new List<TableTransactionAction>();
            addEntitiesBatch.AddRange(entityList.Select(e => new TableTransactionAction(TableTransactionActionType.Add, e)));
            var response = await tableClient.SubmitTransactionAsync(addEntitiesBatch).ConfigureAwait(false);

            return entityList;
        }

        public async Task DeleteItemAsync(string partitionKey, string rowKey)
        {
            await CreateOrGetTableAsync();
            await tableClient.DeleteEntityAsync(partitionKey, rowKey);
        }
    }
}
