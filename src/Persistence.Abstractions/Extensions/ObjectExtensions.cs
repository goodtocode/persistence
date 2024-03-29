﻿using GoodToCode.Persistence.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GoodToCode.Persistence.Abstractions
{
    public static class ObjectExtensions
    {
        public static Dictionary<string, object> ToDictionary<T>(this T item)
        {
            Dictionary<string, object> returnData = null;
            returnData = item.GetType()
                            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                .Where(x => x.PropertyType.IsPrimitive || x.PropertyType.IsValueType || x.PropertyType == typeof(Guid) || x.PropertyType == typeof(string))
                            .ToDictionary(prop => prop.Name, prop => (object)prop.GetValue(item, null));
            return returnData; 
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

        public static void Fill<T>(this T item, IEnumerable<KeyValuePair<string, object>> sourceList)
        {
            foreach(var sourceItem in sourceList)
            {
                PropertyInfo destinationProperty = typeof(T).GetRuntimeProperty(sourceItem.Key);
                if (destinationProperty != null && destinationProperty.CanWrite)
                {
                    // Copy data only for Primitive-ish types including Value types, Guid, String, etc.
                    Type destinationPropertyType = destinationProperty.PropertyType;
                    if (destinationPropertyType.GetTypeInfo().IsPrimitive || destinationPropertyType.GetTypeInfo().IsValueType
                        || (destinationPropertyType == typeof(string)) || (destinationPropertyType == typeof(Guid)))
                    {
                        destinationProperty.SetValue(item, sourceItem.Value, null);
                    }
                }
            }
        }
    }
}
