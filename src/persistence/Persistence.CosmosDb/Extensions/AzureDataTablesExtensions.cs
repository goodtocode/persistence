using Azure.Data.Tables;
using GoodToCode.Shared.Persistence.Abstractions;
using GoodToCode.Shared.Persistence.CosmosDb;
using System.Collections.Generic;
using System.Linq;

namespace GoodToCode.Shared.Persistence.CosmosDb
{
    public static class AzureDataTablesExtensions
    {
        public static TableEntity ToTableEntity<T>(this T item) where T : IEntity
        {
            var entity = new TableEntity(item.PartitionKey, item.RowKey);

            foreach (var prop in item.ToDictionary())
                entity.Add(prop.Key, prop.Value);

            return entity;
        }

        public static IEnumerable<TableEntity> ToTableList<T>(this IEnumerable<T> items) where T : IEntity
        {
            var list = new List<TableEntity>();

            foreach (var item in items)
                list.Add(item.ToTableEntity<T>());

            return list;
        }
    }
}
