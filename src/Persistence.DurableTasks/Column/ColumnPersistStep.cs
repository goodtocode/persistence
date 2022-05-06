using Azure.Data.Tables;
using GoodToCode.Persistence.Abstractions;
using GoodToCode.Persistence.Azure.StorageTables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodToCode.Persistence.DurableTasks
{
    public class ColumnPersistStep
    {
        private readonly IStorageTablesService<CellEntity> servicePersist;

        public ColumnPersistStep(IStorageTablesServiceConfiguration config)
        {
            servicePersist = new StorageTablesService<CellEntity>(config);
        }

        public async Task<IEnumerable<TableEntity>> ExecuteAsync(IEnumerable<CellEntity> entities)
        {
            return await servicePersist.AddItemsAsync(entities);
        }

        public async Task<TableEntity> ExecuteAsync(CellEntity entity)
        {
            return await servicePersist.AddItemAsync(entity);
        }
    }
}
