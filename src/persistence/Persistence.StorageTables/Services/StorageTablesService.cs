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

        public int BatchAtCount { get; private set; } = 200;
        public int BatchSize { get; private set; } = 100;

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
                if (table == null)
                    table = await serviceClient.CreateTableIfNotExistsAsync(config.TableName);
            }
            catch (RequestFailedException ex) when (ex.Status == (int)HttpStatusCode.Conflict)
            {
                // Conflict is ok, ignore
            }
            catch (NullReferenceException)
            {
                // Table already exists, ok, ignore
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

        public async Task<IEnumerable<TableEntity>> AddItemsAsync(IEnumerable<Dictionary<string, object>> items)
        {
            var returnData = new List<TableEntity>();

            await CreateOrGetTableAsync();
            foreach (var item in items)
                returnData.Add(await AddItemAsync(item));

            return returnData;
        }

        public async Task<TableEntity> AddItemAsync(Dictionary<string, object> item)
        {
            TableEntity entity = default;

            try
            {
                await CreateOrGetTableAsync();
                entity.PartitionKey = item.GetValueOrDefault("PartitionKey").ToString();
                entity.RowKey = item.GetValueOrDefault("RowKey").ToString();
                entity.Concat(item);
                await tableClient.AddEntityAsync(entity);
            }
            catch (RequestFailedException ex) when (ex.Status == (int)HttpStatusCode.Conflict)
            {
                // Conflict is ok, ignore
            }

            return entity;
        }

        public async Task<IEnumerable<TableEntity>> AddItemsAsync(IEnumerable<T> items)
        {
            var returnData = new List<TableEntity>();

            await CreateOrGetTableAsync();
            if (items.Count() > BatchAtCount)
                returnData.AddRange(await SubmitBatchTransactionAsync(items, TableTransactionActionType.Add, BatchSize));
            else
                foreach (var item in items)
                    returnData.Add(await AddItemAsync(item));

            return returnData;
        }

        public async Task<TableEntity> AddItemAsync(T item)
        {
            TableEntity entity = default;

            try
            {
                await CreateOrGetTableAsync();
                entity = item.ToTableEntity();
                await tableClient.AddEntityAsync(entity);
            }
            catch (RequestFailedException ex) when (ex.Status == (int)HttpStatusCode.Conflict)
            {
                // Conflict is ok, ignore
            }

            return entity;
        }


        public async Task<IEnumerable<TableEntity>> UpsertItemsAsync(IEnumerable<T> items)
        {
            var returnData = new List<TableEntity>();

            await CreateOrGetTableAsync();
            if (items.Count() > BatchAtCount)
                returnData.AddRange(await SubmitBatchTransactionAsync(items, TableTransactionActionType.UpsertReplace, BatchSize));
            else
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

        public async Task DeleteItemAsync(string partitionKey, string rowKey)
        {
            await CreateOrGetTableAsync();
            await tableClient.DeleteEntityAsync(partitionKey, rowKey);
        }

        public async Task<IEnumerable<TableEntity>> SubmitBatchTransactionAsync(IEnumerable<T> items, TableTransactionActionType type, int batchSize)
        {
            var returnEntities = items.ToTableList<T>();

            var partitionBatches = returnEntities.GroupBy(g => g.PartitionKey);
            foreach (var partition in partitionBatches)
            {
                var batches = partition.ToList().ToBatch(batchSize);                
                foreach (var batch in batches)
                {
                    var addEntitiesBatch = new List<TableTransactionAction>();
                    addEntitiesBatch.AddRange(batch.Select(e => new TableTransactionAction(type, e)));
                    await CreateOrGetTableAsync();
                    var response = await tableClient.SubmitTransactionAsync(addEntitiesBatch);
                }
            }

            return returnEntities;
        }
    }
}
