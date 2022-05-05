using Azure.Data.Tables;
using GoodToCode.Persistence.Abstractions;
using GoodToCode.Persistence.Azure.StorageTables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodToCode.Persistence.DurableTasks
{
    public class SheetPersistActivity
    {
        private readonly RowPersistActivity activityPersist;

        public SheetPersistActivity(IStorageTablesServiceConfiguration config)
        {
            activityPersist = new RowPersistActivity(config);
        }

        public async Task<IEnumerable<TableEntity>> ExecuteAsync(IEnumerable<ISheetData> entities, string paritionKey)
        {
            var returnData = new List<TableEntity>();
            foreach(var entity in entities)
                foreach(var row in await ExecuteAsync(entity, paritionKey))
                    returnData.Add(row);

            return returnData;
        }

        public async Task<IEnumerable<TableEntity>> ExecuteAsync(ISheetData entity, string paritionKey)
        {
            var returnData = new List<TableEntity>();
            foreach (var row in entity.Rows)
                returnData.Add(await activityPersist.ExecuteAsync(row, paritionKey));

            return returnData;
        }
    }
}
