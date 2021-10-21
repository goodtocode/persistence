using GoodToCode.Shared.Persistence.StorageTables;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Persistence.Tests
{
    [TestClass]
    public class StorageTables_Service_EntityTests
    {
        private IConfiguration configuration;
        private ILogger<StorageTablesService<NamedEntity>> logItem;
        private StorageTablesServiceOptions configPersistence;
        public StorageTablesService<NamedEntity> serviceNamedEntity { get; private set; }        
        public Dictionary<string, StringValues> SutReturn { get; private set; }
        private static string SutFile { get { return @$"{PathFactory.GetProjectSubfolder("Assets")}/OpinionFile.xlsx"; } }

        public StorageTables_Service_EntityTests() { }

        [TestInitialize]
        public void Initialize()
        {
            logItem = LoggerFactory.CreateLogger<StorageTablesService<NamedEntity>>();
            configuration = new AppConfigurationFactory().Create();
            configPersistence = new StorageTablesServiceOptions(
                configuration[AppConfigurationKeys.StorageTablesConnectionString],
                $"AutoTest-{DateTime.UtcNow:yyyy-MM-dd}");
            serviceNamedEntity = new StorageTablesService<NamedEntity>(configPersistence);
        }

        [TestMethod]
        public async Task StorageTables_Generic_GetItem()
        {
            var item = TextAnalyzerResultFactory.CreateNamedEntity();
            await serviceNamedEntity.AddItemAsync(item);
            var readItem = serviceNamedEntity.GetItem(item.RowKey);
            Assert.IsTrue(readItem.RowKey == item.RowKey);
        }

        [TestMethod]
        public async Task StorageTables_Generic_AddItemAsync()
        {
            var item = TextAnalyzerResultFactory.CreateNamedEntity();
            await serviceNamedEntity.AddItemAsync(item);
            var writeItem = serviceNamedEntity.GetItem(item.RowKey.ToString());
            Assert.IsTrue(writeItem.RowKey == item.RowKey);
            await serviceNamedEntity.DeleteItemAsync(writeItem.PartitionKey, writeItem.RowKey);
            writeItem = serviceNamedEntity.GetItem(item.RowKey);
            Assert.IsTrue(writeItem == null);
        }

        [TestMethod]
        public async Task StorageTables_Generic_AddItemsAsync()
        {
            var items = TextAnalyzerResultFactory.CreateNamedEntities(); ;

            await serviceNamedEntity.AddItemsAsync(items);
            foreach(var item in items)
            {
                var writeItem = serviceNamedEntity.GetItem(item.RowKey.ToString());
                Assert.IsTrue(writeItem.RowKey == item.RowKey);
                await serviceNamedEntity.DeleteItemAsync(writeItem.PartitionKey, writeItem.RowKey);
                writeItem = serviceNamedEntity.GetItem(item.RowKey);
                Assert.IsTrue(writeItem == null);
            }
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            // Fails: await SutItem.DeleteTableAsync();
        }
    }
}