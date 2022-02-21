using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using System;

namespace GoodToCode.Shared.Patterns.Tests
{
    public class AppConfigurationFactory
    {
        public IConfiguration Configuration { get; private set; }
        public IConfiguration Create()
        {
            var connection = Environment.GetEnvironmentVariable(EnvironmentVariableKeys.AppConfigurationConnection);
            var environment = Environment.GetEnvironmentVariable(EnvironmentVariableKeys.EnvironmentAspNetCore) ?? EnvironmentVariableDefaults.Environment;
            var builder = new ConfigurationBuilder();

            if (connection?.Length > 0)
            {
                builder.AddAzureAppConfiguration(options =>
                    options
                        .Connect(connection)
                        .ConfigureRefresh(refresh =>
                        {
                            refresh.Register(AppConfigurationKeys.SentinelKey, refreshAll: true)
                                    .SetCacheExpiration(new TimeSpan(0, 60, 0));
                        })
                        .Select(KeyFilter.Any, LabelFilter.Null)
                        .Select(KeyFilter.Any, environment));
            }
            builder
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json");
            Configuration = builder.Build();
            return Configuration;
        }
    }
}
