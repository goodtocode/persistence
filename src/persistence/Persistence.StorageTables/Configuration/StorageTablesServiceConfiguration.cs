using Microsoft.Extensions.Options;
using System;

namespace GoodToCode.Shared.Persistence.StorageTables
{
    public class StorageTablesServiceConfiguration : IStorageTablesServiceConfiguration
    {
        private string tableName;
        public string ConnectionString { get; private set; }
        public string TableName
        {
            get
            {
                return string.IsNullOrWhiteSpace(tableName) ? string.Empty : new string(Array.FindAll<char>(tableName.ToCharArray(), c => char.IsLetterOrDigit(c)));
            }
            private set { tableName = value; }
        }

        public StorageTablesServiceConfiguration(string connectionString, string tableName)
        {
            ConnectionString = connectionString;
            TableName = tableName;
        }
    }

    public class StorageTablesServiceConfigurationValidation : IValidateOptions<StorageTablesServiceConfiguration>
    {
        public ValidateOptionsResult Validate(string name, StorageTablesServiceConfiguration options)
        {
            if (string.IsNullOrEmpty(options.ConnectionString))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.ConnectionString)} configuration parameter for the Azure Storage DB is required");
            }

            if (string.IsNullOrEmpty(options.TableName))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.TableName)} configuration parameter for the Azure Storage DB is required");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
