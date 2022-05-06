using GoodToCode.Persistence.Azure.StorageTables;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace GoodToCode.Persistence.Tests
{
    [TestClass]
    public class StorageTables_Service_GenericTests
    {
        private IConfiguration configuration;
        private StorageTablesServiceOptions configPersistence;
        public StorageTablesService<EntityA> ServiceEntityA { get; private set; }
        public StorageTablesService<RowEntity> ServiceRowEntity { get; private set; }
        public Dictionary<string, StringValues> SutReturn { get; private set; }
        private static string SutJsonFile { get { return @$"{PathFactory.GetProjectSubfolder("Assets")}/IEnumerableRowEntity_600.json"; } }

        public StorageTables_Service_GenericTests() { }

        [TestInitialize]
        public void Initialize()
        {
            configuration = new AppConfigurationFactory().Create();
            configPersistence = new StorageTablesServiceOptions(
                configuration[AppConfigurationKeys.StorageTablesConnectionString],
                $"UnitTest-{DateTime.UtcNow:yyyy-MM-dd}");
            ServiceEntityA = new StorageTablesService<EntityA>(configPersistence);
            ServiceRowEntity = new StorageTablesService<RowEntity>(configPersistence);
        }

        [TestMethod]
        public async Task StorageTables_Generic_GetItem()
        {
            var item = new EntityA("PartRead") { SomeData = "Some read data." };
            await ServiceEntityA.AddItemAsync(item);
            var readItem = ServiceEntityA.GetItem(item.RowKey);
            Assert.IsTrue(readItem.RowKey == item.RowKey);
        }

        [TestMethod]
        public async Task StorageTables_Generic_GetAndCastItem()
        {
            var item = new EntityA("PartRead") { SomeData = "Some read data." };
            await ServiceEntityA.AddItemAsync(item);
            var readItem = ServiceEntityA.GetAndCastItem(item.RowKey);
            Assert.IsTrue(readItem.RowKey == item.RowKey);
        }

        [TestMethod]
        public async Task StorageTables_Generic_GetItems()
        {
            var item = new EntityA("PartRead") { SomeData = "Some read data." };
            await ServiceEntityA.AddItemAsync(item);
            var readItem = ServiceEntityA.GetItems(x => x.RowKey == item.RowKey);
            Assert.IsTrue(readItem.FirstOrDefault().RowKey == item.RowKey);
        }

        [TestMethod]
        public async Task StorageTables_Generic_GetAndCastItems()
        {
            var item = new EntityA("PartRead") { SomeData = "Some read data." };
            await ServiceEntityA.AddItemAsync(item);
            var readItem = ServiceEntityA.GetAndCastItems(x => x.RowKey == item.RowKey);
            Assert.IsTrue(readItem.FirstOrDefault().RowKey == item.RowKey);
        }

        [TestMethod]
        public async Task StorageTables_Generic_GetAllItems()
        {
            var item = new EntityA("PartRead") { SomeData = "Some read data." };
            await ServiceEntityA.AddItemAsync(item);
            var readItem = ServiceEntityA.GetItems(x => x.RowKey == item.RowKey);
            Assert.IsTrue(readItem.FirstOrDefault().RowKey == item.RowKey);
        }

        [TestMethod]
        public async Task StorageTables_Generic_AddItemAsync()
        {
            var item = new EntityA("PartWrite") { SomeData = "Some write data." };
            await ServiceEntityA.AddItemAsync(item);
            var writeItem = ServiceEntityA.GetItem(item.RowKey.ToString());
            Assert.IsTrue(writeItem.RowKey == item.RowKey);
            await ServiceEntityA.DeleteItemAsync(writeItem.PartitionKey, writeItem.RowKey);
            writeItem = ServiceEntityA.GetItem(item.RowKey);
            Assert.IsTrue(writeItem == null);
        }

        [TestMethod]
        public async Task StorageTables_Generic_AddItemsAsync()
        {
            var items = new List<EntityA>() { 
                new EntityA("PartWrite1") { SomeData = "Some write data1." }, 
                new EntityA("PartWrite2") { SomeData = "Some write data2." },
                new EntityA("PartWrite3") { SomeData = "Some write data3." } };

            await ServiceEntityA.AddItemsAsync(items);
            foreach(var item in items)
            {
                var writeItem = ServiceEntityA.GetItem(item.RowKey.ToString());
                Assert.IsTrue(writeItem.RowKey == item.RowKey);
                Assert.IsTrue(writeItem["SomeData"]?.ToString() == item.SomeData);
                await ServiceEntityA.DeleteItemAsync(writeItem.PartitionKey, writeItem.RowKey);
                writeItem = ServiceEntityA.GetItem(item.RowKey);
                Assert.IsTrue(writeItem == null);
            }
        }

        //[TestMethod]
        public async Task StorageTables_Generic_UpsertItemsBatchAsync()
        {
            var fileContents = await File.ReadAllBytesAsync(SutJsonFile);
            var items = await JsonSerializer.DeserializeAsync<IEnumerable<RowEntity>>(new MemoryStream(fileContents));

            await ServiceRowEntity.UpsertItemsAsync(items);
            foreach (var item in items)
            {
                var writeItem = ServiceEntityA.GetItem(item.RowKey.ToString());
                Assert.IsTrue(writeItem.RowKey == item.RowKey);
                await ServiceEntityA.DeleteItemAsync(writeItem.PartitionKey, writeItem.RowKey);
                writeItem = ServiceEntityA.GetItem(item.RowKey);
                Assert.IsTrue(writeItem == null);
            }
        }

        [TestMethod]
        public async Task StorageTables_Generic_UpsertItemAsync()
        {
            var item = new EntityA("PartWrite") { SomeData = "Some write data." };
            await ServiceEntityA.UpsertItemAsync(item);
            var writeItem = ServiceEntityA.GetItem(item.RowKey.ToString());
            Assert.IsTrue(writeItem.RowKey == item.RowKey);
            await ServiceEntityA.DeleteItemAsync(writeItem.PartitionKey, writeItem.RowKey);
            writeItem = ServiceEntityA.GetItem(item.RowKey);
            Assert.IsTrue(writeItem == null);
        }

        [TestMethod]
        public async Task StorageTables_Generic_UpsertItemsAsync()
        {
            var items = new List<EntityA>() {
                new EntityA("PartWrite1") { SomeData = "Some write data1." },
                new EntityA("PartWrite2") { SomeData = "Some write data2." },
                new EntityA("PartWrite3") { SomeData = "Some write data3." } };

            await ServiceEntityA.UpsertItemsAsync(items);
            foreach (var item in items)
            {
                var writeItem = ServiceEntityA.GetItem(item.RowKey.ToString());
                Assert.IsTrue(writeItem.RowKey == item.RowKey);
                Assert.IsTrue(writeItem["SomeData"]?.ToString() == item.SomeData);
                await ServiceEntityA.DeleteItemAsync(writeItem.PartitionKey, writeItem.RowKey);
                writeItem = ServiceEntityA.GetItem(item.RowKey);
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