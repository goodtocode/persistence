using Microsoft.Extensions.Options;
using System;

namespace GoodToCode.Persistence.Azure.CosmosDb
{
    public class CosmosDbServiceConfiguration : ICosmosDbServiceConfiguration
    {
        private string tableName;
        public string DatabaseName { get; private set; }
        public string ConnectionString { get; private set; }
        public string PartitionKeyPath { get; private set; }
        public string TableName
        {
            get
            {
                return new string(Array.FindAll<char>(tableName.ToCharArray(), (c => (char.IsLetterOrDigit(c)))));
            }
            private set { tableName = value; }
        }

        public CosmosDbServiceConfiguration(string connectionString, string tableName)
        {
            PartitionKeyPath = "/PartitionKey";
            ConnectionString = connectionString;
            TableName = tableName;
        }
    }

    public class CosmosDbServiceConfigurationValidation : IValidateOptions<CosmosDbServiceConfiguration>
    {
        public ValidateOptionsResult Validate(string name, CosmosDbServiceConfiguration options)
        {
            if (string.IsNullOrEmpty(options.ConnectionString))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.ConnectionString)} configuration parameter for the Azure CosmosDb is required");
            }

            if (string.IsNullOrEmpty(options.TableName))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.TableName)} configuration parameter for the Azure CosmosDb is required");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
