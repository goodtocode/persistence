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

namespace GoodToCode.Shared.Persistence.StorageTables
{
    public class StorageTablesItemService<T> : IStorageTablesItemService<T> where T : class, IEntity, new()
    {
        private readonly IStorageTablesServiceConfiguration config;
        private readonly TableServiceClient serviceClient;
        private readonly TableClient tableClient;
        private TableItem table;

        public StorageTablesItemService(IStorageTablesServiceConfiguration serviceConfiguration)
        {
            config = serviceConfiguration;
            serviceClient = new TableServiceClient(config.ConnectionString);
            tableClient = new TableClient(config.ConnectionString, config.TableName);
        }

        public StorageTablesItemService(StorageTablesServiceOptions options) : this(options.Value)
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
                // Conflict
            }
            catch (Exception ex)
            {
                ex = ex;
                // Throws exceptions on normal operations
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
                // Conflict
            }

            return entity;
        }

        public async Task DeleteItemAsync(string partitionKey, string rowKey)
        {
            await CreateOrGetTableAsync();
            await tableClient.DeleteEntityAsync(partitionKey, rowKey);
        }
    }
}
