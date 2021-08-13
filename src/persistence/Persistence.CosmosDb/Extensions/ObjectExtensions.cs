using Azure.Data.Tables;
using GoodToCode.Shared.Persistence.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GoodToCode.Shared.Persistence.CosmosDb
{
    public static class ObjectExtensions
    {
        public static Dictionary<string, string> ToDictionary<T>(this T item)
        {
            return item.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => (string)prop.GetValue(item, null));
        }

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

        public static TDestination CastOrFill<TDestination>(this object item) where TDestination : new()
        {
            var returnValue = new TDestination();

            try
            {
                returnValue = item != null ? (TDestination)item : returnValue;
            }
            catch (InvalidCastException)
            {
                returnValue.Fill(item);
            }

            return returnValue;
        }

        public static void Fill<T>(this T item, object sourceItem)
        {
            var sourceType = sourceItem.GetType();

            foreach (PropertyInfo sourceProperty in sourceType.GetRuntimeProperties())
            {
                PropertyInfo destinationProperty = typeof(T).GetRuntimeProperty(sourceProperty.Name);
                if (destinationProperty != null && destinationProperty.CanWrite)
                {
                    // Copy data only for Primitive-ish types including Value types, Guid, String, etc.
                    Type destinationPropertyType = destinationProperty.PropertyType;
                    if (destinationPropertyType.GetTypeInfo().IsPrimitive || destinationPropertyType.GetTypeInfo().IsValueType
                        || (destinationPropertyType == typeof(string)) || (destinationPropertyType == typeof(Guid)))
                    {
                        destinationProperty.SetValue(item, sourceProperty.GetValue(sourceItem, null), null);
                    }
                }
            }
        }
    }
}
