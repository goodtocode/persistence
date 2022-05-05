using GoodToCode.Persistence.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GoodToCode.Persistence.Azure.StorageTables
{
    public static class StorageTablesServiceExtensions
    {
            public static IServiceCollection AddStorageTablesService<T>(this IServiceCollection collection, IConfiguration config) where T : class, IEntity, new()
            {
                if (collection == null) throw new ArgumentNullException(nameof(collection));
                if (config == null) throw new ArgumentNullException(nameof(config));

                collection.Configure<StorageTablesServiceOptions>(config);
                return collection.AddTransient<IStorageTablesService<T>, StorageTablesService<T>>();
        }
    }
}
