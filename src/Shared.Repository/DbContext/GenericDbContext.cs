using GoodToCode.Shared.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace MySundial.Reflections.Infrastructure
{
    public class GenericDbContext<T> : DbContext, IGenericDbContext<T> where T : class
    {
        public GenericDbContext(DbContextOptions<GenericDbContext<T>> options)
            : base(options)
        { }

        public virtual DbSet<T> Items { get; set; }

        public string GetConnectionFromAzureSettings(string configKey)
        {
            var builder = new ConfigurationBuilder();
            builder.AddAzureAppConfigurationWithSentinel(Environment.GetEnvironmentVariable("AppSettingsConnection"), "Reflections:Shared:Sentinel");
            var config = builder.Build();

            return config[configKey];
        }
    }
}
