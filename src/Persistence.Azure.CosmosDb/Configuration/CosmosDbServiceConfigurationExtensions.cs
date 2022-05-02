using GoodToCode.Persistence.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GoodToCode.Shared.Persistence.CosmosDb
{
    public static class CosmosDbServiceConfigurationExtensions
    {
            public static IServiceCollection AddCosmosDbService<T>(this IServiceCollection collection, IConfiguration config) where T : class, IEntity, new()
            {
                if (collection == null) throw new ArgumentNullException(nameof(collection));
                if (config == null) throw new ArgumentNullException(nameof(config));

                collection.Configure<CosmosDbServiceOptions>(config);
                return collection.AddTransient<ICosmosDbItemService<T>, CosmosDbItemService<T>>();
        }
    }
}
