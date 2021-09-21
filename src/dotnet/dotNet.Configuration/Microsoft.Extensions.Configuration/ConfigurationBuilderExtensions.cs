using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using System;

namespace GoodToCode.Shared.dotNet.Configuration
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfiguration AddAzureAppConfigurationWithSentinel(this ConfigurationBuilder item, string appConfigurationConnection, string sentinelAppConfigKey, string environment, TimeSpan timeout)
        {
            item.AddAzureAppConfiguration(options =>
                            options
                                .Connect(appConfigurationConnection)
                                .ConfigureRefresh(refresh =>
                                {
                                    refresh.Register(sentinelAppConfigKey, refreshAll: true)
                                           .SetCacheExpiration(timeout);
                                })
                                .Select(KeyFilter.Any, LabelFilter.Null)
                                .Select(KeyFilter.Any, environment)
                        );
            return item.Build();
        }

        public static IConfiguration AddAzureAppConfigurationWithSentinel(this ConfigurationBuilder item, string appConfigurationConnection, string sentinelAppConfigKey)
        {
            return AddAzureAppConfigurationWithSentinel(item, appConfigurationConnection, sentinelAppConfigKey, Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production", new TimeSpan(0, 60, 0));
        }
    }
}
