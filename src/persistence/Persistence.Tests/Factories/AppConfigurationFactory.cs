﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using System;

namespace GoodToCode.Shared.Unit
{
    public class AppConfigurationFactory
    {
        public IConfiguration Configuration { get; private set; }
        public IConfiguration Create()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
            var builder = new ConfigurationBuilder();
            builder.AddAzureAppConfiguration(options =>
                    options
                        .Connect(Environment.GetEnvironmentVariable("AppSettingsConnection"))
                        .ConfigureRefresh(refresh =>
                        {
                            refresh.Register("Reflections:Shared:Sentinel", refreshAll: true)
                                    .SetCacheExpiration(new TimeSpan(0, 60, 0));
                        })
                        .Select(KeyFilter.Any, LabelFilter.Null)
                        .Select(KeyFilter.Any, environment)
                    );
            Configuration = builder.Build();
            return Configuration;
        }
    }
}
