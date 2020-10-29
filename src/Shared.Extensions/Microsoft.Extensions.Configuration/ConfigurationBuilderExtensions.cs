using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using System;

namespace GoodToCode.Shared.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfiguration AddAzureAppConfigurationWithSentinel(this ConfigurationBuilder item, string appConfigurationConnection, string sentinelAppConfigKey, string environment)
        {
            item.AddAzureAppConfiguration(options =>
                            options
                                .Connect(appConfigurationConnection)
                                .ConfigureRefresh(refresh =>
                                {
                                    refresh.Register(sentinelAppConfigKey, refreshAll: true)
                                           .SetCacheExpiration(new TimeSpan(0, 60, 0));
                                })
                                .Select(KeyFilter.Any, LabelFilter.Null)
                                .Select(KeyFilter.Any, environment)
                        );
            return item.Build();
        }

        public static IConfiguration AddAzureAppConfigurationWithSentinel(this ConfigurationBuilder item, string appConfigurationConnection, string sentinelAppConfigKey)
        {
            return AddAzureAppConfigurationWithSentinel(item, appConfigurationConnection, sentinelAppConfigKey, Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production");
        }
    }
}
