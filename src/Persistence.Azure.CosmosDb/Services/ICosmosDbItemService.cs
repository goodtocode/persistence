﻿using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using GoodToCode.Persistence.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GoodToCode.Persistence.Azure.CosmosDb
{
    public interface ICosmosDbItemService<T> where T : class, IEntity, new()
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