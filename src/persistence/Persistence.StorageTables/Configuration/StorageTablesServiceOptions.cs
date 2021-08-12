using Microsoft.Extensions.Options;

namespace GoodToCode.Shared.Persistence.StorageTables
{
    public class StorageTablesServiceOptions : IOptions<IStorageTablesServiceConfiguration>
    {
        public IStorageTablesServiceConfiguration Value { get; }

        public StorageTablesServiceOptions(string connectionString, string databaseName, string containerName, string partitionKeyName)
        {
            Value = new StorageTablesServiceConfiguration(connectionString, databaseName, containerName, partitionKeyName);
        }
    }
}
