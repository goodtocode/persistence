﻿using Microsoft.Extensions.Options;

namespace GoodToCode.Persistence.Azure.StorageTables
{
    public class StorageTablesServiceOptions : IOptions<IStorageTablesServiceConfiguration>
    {
        public IStorageTablesServiceConfiguration Value { get; }

        public StorageTablesServiceOptions(string connectionString, string tableName)
        {
            Value = new StorageTablesServiceConfiguration(connectionString, tableName);
        }
    }
}
