using GoodToCode.Persistence.Azure.StorageTables;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodToCode.Persistence.Tests
{
    [TestClass]
    public class StorageTables_Service_DictionaryTests
    {
        private IConfiguration configuration;
        private ILogger<StorageTablesService<EntityA>> logItem;
        private StorageTablesServiceOptions configPersistence;
        public StorageTablesService<EntityA> serviceEntityA { get; private set; }
        public StorageTablesService<RowEntity> serviceRowEntity { get; private set; }

        public StorageTables_Service_DictionaryTests() { }

        [TestInitialize]
        public void Initialize()
        {
            logItem = LoggerFactory.CreateLogger<StorageTablesService<EntityA>>();
            configuration = new AppConfigurationFactory().Create();
            configPersistence = new StorageTablesServiceOptions(
                configuration[AppConfigurationKeys.StorageTablesConnectionString],
                $"UnitTest-{DateTime.UtcNow:yyyy-MM-dd}");
            serviceEntityA = new StorageTablesService<EntityA>(configPersistence);
            serviceRowEntity = new StorageTablesService<RowEntity>(configPersistence);
        }

        [TestMethod]
        public async Task StorageTables_Dictionary_AddItemAndKeys()
        {
            var item = new Dictionary<string, object>();
            item.Add("RowIndex", "1");
            item.Add("Column1", "This is the cell value.");
            var returnTable = await serviceEntityA.AddItemAsync(item, "Partition1", Guid.NewGuid().ToString());
            Assert.IsTrue(returnTable.Count > 0);
            await serviceEntityA.DeleteItemAsync(returnTable.PartitionKey, returnTable.RowKey);
        }

        [TestMethod]
        public async Task StorageTables_Dictionary_AddItemAsync()
        {
            var item = new Dictionary<string, object>();
            item.Add("PartitionKey", "Partition1");
            item.Add("RowKey", Guid.NewGuid().ToString());
            item.Add("RowIndex", "1");
            item.Add("Column1", "This is the cell value.");
            var returnTable = await serviceEntityA.AddItemAsync(item);            
            Assert.IsTrue(returnTable.Count > 0);
            await serviceEntityA.DeleteItemAsync(returnTable.PartitionKey, returnTable.RowKey);
        }

        [TestMethod]
        public async Task StorageTables_Dictionary_AddItemsAsync()
        {
            var items = new List<Dictionary<string, object>>();

            var itemToAdd1 = new Dictionary<string, object>();
            itemToAdd1.Add("PartitionKey", "Partition1");
            itemToAdd1.Add("RowKey", Guid.NewGuid().ToString());
            itemToAdd1.Add("RowIndex", "1");
            itemToAdd1.Add("Column1", "This is the cell value #1.");
            items.Add(itemToAdd1);
            var itemToAdd2 = new Dictionary<string, object>();
            itemToAdd2.Add("PartitionKey", "Partition1");
            itemToAdd2.Add("RowKey", Guid.NewGuid().ToString());
            itemToAdd2.Add("RowIndex", "2");
            itemToAdd2.Add("Column1", "This is the cell value #2.");
            items.Add(itemToAdd2);
            var returnTable = await serviceEntityA.AddItemsAsync(items);
            Assert.IsTrue(returnTable.Count() > 0);
            foreach(var item in returnTable)
            {
                var writeItem = serviceEntityA.GetItem(item.RowKey.ToString());
                Assert.IsTrue(writeItem.RowKey == item.RowKey);
                await serviceEntityA.DeleteItemAsync(writeItem.PartitionKey, writeItem.RowKey);
                writeItem = serviceEntityA.GetItem(item.RowKey);
                Assert.IsTrue(writeItem == null);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
        }
    }
}