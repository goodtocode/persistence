using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using GoodToCode.Shared.Persistence.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Persistence.StorageTables
{
    public interface IStorageTablesService<T> where T : class, IEntity, new()
    {
        Task<TableEntity> AddItemAsync(T item);
        Task<IEnumerable<TableEntity>> AddItemsAsync(IEnumerable<T> items);
        Task<TableEntity> AddItemAsync(Dictionary<string, object> item);
        Task<IEnumerable<TableEntity>> AddItemsAsync(IEnumerable<Dictionary<string, object>> items);
        Task<TableItem> CreateOrGetTableAsync();
        Task DeletePartitionAsync(string partitionKey);
        Task DeletePartitionsAsync(IEnumerable<string> partitionKeys);
        Task DeleteItemAsync(string partitionKey, string rowKey);
        Task DeleteTableAsync();
        Pageable<TableEntity> GetAllItems(string partitionKey);
        TableEntity GetItem(string rowKey);
        T GetAndCastItem(string rowKey);        
        Pageable<TableEntity> GetItems(Expression<Func<TableEntity, bool>> filter);
        IEnumerable<T> GetAndCastItems(Expression<Func<TableEntity, bool>> filter);
    }
}