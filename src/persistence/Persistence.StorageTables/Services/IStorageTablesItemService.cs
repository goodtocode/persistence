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
    public interface IStorageTablesItemService<T> where T : class, IEntity, new()
    {
        Task<TableEntity> AddItemAsync(T item);
        Task<IEnumerable<TableEntity>> AddItemsAsync(IEnumerable<T> items);
        Task<TableItem> CreateOrGetTableAsync();
        Task DeleteItemAsync(string partitionKey, string rowKey);
        Task DeleteTableAsync();
        Pageable<TableEntity> GetAllItems(string partitionKey);
        TableEntity GetItem(string rowKey);
        Pageable<TableEntity> GetItems(Expression<Func<TableEntity, bool>> filter);
    }
}