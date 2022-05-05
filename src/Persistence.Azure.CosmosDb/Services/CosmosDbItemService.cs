using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using GoodToCode.Persistence.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace GoodToCode.Persistence.Azure.CosmosDb
{
    public class CosmosDbItemService<T> : ICosmosDbItemService<T> where T : class, IEntity, new()
    {
        private readonly ICosmosDbServiceConfiguration config;
        private readonly TableServiceClient serviceClient;
        private readonly TableClient tableClient;
        private TableItem table;

        public CosmosDbItemService(ICosmosDbServiceConfiguration serviceConfiguration)
        {
            config = serviceConfiguration;
            serviceClient = new TableServiceClient(config.ConnectionString);
            tableClient = new TableClient(config.ConnectionString, config.TableName);
        }

        public CosmosDbItemService(CosmosDbServiceOptions options) : this(options.Value)
        { }

        public async Task<TableItem> CreateOrGetTableAsync()
        {
            try
            {
                table ??= await serviceClient.CreateTableIfNotExistsAsync(config.TableName);
            }
            catch
            {
                // Remove: -beta dependency throws unhandled errors when working correctly.
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
            queryResultsFilter = tableClient.Query<TableEntity>(filter: $"PartitionKey eq '{partitionKey}'");

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
                // Already exists
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
