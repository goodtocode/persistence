﻿using Azure.Data.Tables;
using GoodToCode.Persistence.Abstractions;
using GoodToCode.Persistence.Azure.StorageTables;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodToCode.Persistence.DurableTasks
{
    public class RowPersistActivity
    {
        private readonly IStorageTablesService<RowEntity> servicePersist;

        public RowPersistActivity(IStorageTablesServiceConfiguration config)
        {
            servicePersist = new StorageTablesService<RowEntity>(config);
        }

        public async Task<IEnumerable<TableEntity>> ExecuteAsync(IEnumerable<IRowData> entities, string paritionKey)
        {
            var returnData = new List<TableEntity>();
            foreach (var row in entities)
                returnData.Add(await ExecuteAsync(row, paritionKey));
            return returnData;
        }

        public async Task<TableEntity> ExecuteAsync(IRowData row, string paritionKey)
        {
            var entity = new RowEntity(paritionKey, Guid.NewGuid().ToString(), row.Cells);
            return await servicePersist.AddItemAsync(entity.ToDictionary());
        }
    }
}
