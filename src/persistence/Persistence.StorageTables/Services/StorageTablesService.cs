using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using GoodToCode.Shared.Persistence.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Persistence.StorageTables
{
    public class StorageTablesService<T> : IStorageTablesService<T> where T : class, IEntity, new()
    {
        private readonly IStorageTablesServiceConfiguration config;
        private readonly TableServiceClient serviceClient;
        private readonly TableClient tableClient;
        private TableItem table;

        public StorageTablesService(IStorageTablesServiceConfiguration serviceConfiguration)
        {
            config = serviceConfiguration;
            serviceClient = new TableServiceClient(config.ConnectionString);
            tableClient = new TableClient(config.ConnectionString, config.TableName);
        }

        public StorageTablesService(StorageTablesServiceOptions options) : this(options.Value)
        {
        }

        public async Task<TableItem> CreateOrGetTableAsync()
        {
            try
            {
                table ??= await serviceClient.CreateTableIfNotExistsAsync(config.TableName);
            }
            catch (RequestFailedException ex) when (ex.Status == (int)HttpStatusCode.Conflict)
            {
                // Conflict is ok, ignore
            }
            catch
            {
                // Throws various exceptions on normal+successfull operations, ignore.
                // This is a bug in azure.data.tables
            }

            return table;
        }

        public async Task DeleteTableAsync()
        {
            await serviceClient.DeleteTableAsync(config.TableName);
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

        public async Task<IEnumerable<TableEntity>> AddItemsAsync(IEnumerable<T> items)
        {
            var returnData = new List<TableEntity>();

            await CreateOrGetTableAsync();
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
                await CreateOrGetTableAsync();
                entity = new TableEntity(item.PartitionKey, item.RowKey);
                foreach(var prop in item.ToDictionary())
                {
                    entity.Add(prop.Key, prop.Value);
                }
                await tableClient.AddEntityAsync(entity);
            }
            catch (RequestFailedException ex) when (ex.Status == (int)HttpStatusCode.Conflict)
            {
                // Conflict is ok, ignore
            }

            return entity;
        }

        public async Task<IEnumerable<TableEntity>> AddItemsBatchAsync(IEnumerable<T> items)
        {
            return await SubmitBatchTransactionAsync(items, TableTransactionActionType.Add, 100);
        }

        public async Task<IEnumerable<TableEntity>> UpsertItemsAsync(IEnumerable<T> items)
        {
            var returnData = new List<TableEntity>();

            await CreateOrGetTableAsync();
            foreach (var item in items)
                returnData.Add(await UpsertItemAsync(item));

            return returnData;
        }

        public async Task<TableEntity> UpsertItemAsync(T item)
        {
            TableEntity entity = default;

            try
            {
                await CreateOrGetTableAsync();
                entity = item.ToTableEntity<T>();
                await tableClient.UpsertEntityAsync(entity);
            }
            catch (RequestFailedException ex) when (ex.Status == (int)HttpStatusCode.Conflict)
            {
                // Conflict is ok, ignore
            }

            return entity;
        }

        public async Task<IEnumerable<TableEntity>> UpsertItemsBatchAsync(IEnumerable<T> items)
        {
            return await SubmitBatchTransactionAsync(items, TableTransactionActionType.UpsertReplace, 100);
        }

        public async Task DeleteItemAsync(string partitionKey, string rowKey)
        {
            await CreateOrGetTableAsync();
            await tableClient.DeleteEntityAsync(partitionKey, rowKey);
        }

        public async Task<IEnumerable<TableEntity>> SubmitBatchTransactionAsync(IEnumerable<T> items, TableTransactionActionType type, int batchSize)
        {
            await CreateOrGetTableAsync();
            var returnEntities = items.ToTableList<T>();
            var batches = returnEntities.ToBatch(batchSize);
            var addEntitiesBatch = new List<TableTransactionAction>();
            foreach (var batch in batches)
            {
                var batchTransaction = batch.Select(e => new TableTransactionAction(type, e));
                addEntitiesBatch.AddRange(batchTransaction);
            }

            var response = await tableClient.SubmitTransactionAsync(addEntitiesBatch);

            return returnEntities;
        }
    }
}
