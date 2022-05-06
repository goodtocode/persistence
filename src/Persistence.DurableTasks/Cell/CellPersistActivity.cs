using Azure.Data.Tables;
using GoodToCode.Persistence.Abstractions;
using GoodToCode.Persistence.Azure.StorageTables;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodToCode.Persistence.DurableTasks
{
    public class CellPersistActivity
    {
        private readonly IStorageTablesService<CellEntity> servicePersist;

        public CellPersistActivity(IStorageTablesServiceConfiguration config)
        {
            servicePersist = new StorageTablesService<CellEntity>(config);
        }

        public async Task<IEnumerable<TableEntity>> ExecuteAsync(IEnumerable<CellEntity> entities)
        {
            return await servicePersist.AddItemsAsync(entities);
        }

        public async Task<TableEntity> ExecuteAsync(CellEntity entity)
        {
            if (string.IsNullOrWhiteSpace(entity.PartitionKey) || string.IsNullOrWhiteSpace(entity.RowKey))
                throw new ArgumentException("PartitionKey and RowKey are required.", entity.GetType().Name);
            return await servicePersist.AddItemAsync(entity);
        }
    }
}
