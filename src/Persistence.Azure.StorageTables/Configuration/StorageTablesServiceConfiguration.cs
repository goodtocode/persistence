using Microsoft.Extensions.Options;
using System;
using System.Text.RegularExpressions;

namespace GoodToCode.Persistence.Azure.StorageTables
{
    public class StorageTablesServiceConfiguration : IStorageTablesServiceConfiguration
    {
        private string tableName;
        public string ConnectionString { get; private set; }
        public string TableName
        {
            get
            {
                var cleansedName = string.IsNullOrWhiteSpace(tableName) ? string.Empty : new string(Array.FindAll<char>(tableName.ToCharArray(), c => char.IsLetterOrDigit(c)));
                cleansedName = cleansedName.Length > 64 ? cleansedName.Substring(0, 63) : cleansedName;
                return cleansedName;
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
                return ValidateOptionsResult.Fail($"{nameof(options.ConnectionString)} ConnectionString for the Azure Storage DB is required");
            if (string.IsNullOrEmpty(options.TableName))
                return ValidateOptionsResult.Fail($"{nameof(options.TableName)} TableName for the Azure Storage DB is required");
            if(!new Regex("^[A-Za-z][A-Za-z0-9]{2,62}$").IsMatch(options.TableName))
                return ValidateOptionsResult.Fail($"{nameof(options.TableName)} TableName cannot begin with numbers or be over 63 characters");
            return ValidateOptionsResult.Success;
        }
    }
}
