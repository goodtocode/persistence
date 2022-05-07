using Azure.Data.Tables;
using GoodToCode.Persistence.Abstractions;
using GoodToCode.Persistence.Azure.StorageTables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodToCode.Persistence.DurableTasks
{
    public class WorkbookPersistStep
    {
        private readonly SheetPersistStep activityPersist;

        public WorkbookPersistStep(IStorageTablesServiceConfiguration config)
        {
            activityPersist = new SheetPersistStep(config);
        }

        public async Task<IEnumerable<TableEntity>> ExecuteAsync(IWorkbookData entity, string paritionKey)
        {
            return await activityPersist.ExecuteAsync(entity.Sheets, paritionKey);
        }
    }
}