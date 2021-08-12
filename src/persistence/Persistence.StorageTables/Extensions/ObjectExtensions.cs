using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GoodToCode.Shared.Persistence.StorageTables
{
    public static class ObjectExtensions
    {
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
