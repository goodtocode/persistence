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
    public class StorageTables_Service_GenericTests
    {
        private IConfiguration configuration;
        private ILogger<StorageTablesService<EntityA>> logItem;
        private StorageTablesServiceOptions configPersistence;
        public StorageTablesService<EntityA> serviceEntityA { get; private set; }
        public StorageTablesService<RowEntity> serviceRowEntity { get; private set; }
        public Dictionary<string, StringValues> SutReturn { get; private set; }
        private string SutJsonFile { get { return @$"{PathFactory.GetProjectSubfolder("Assets")}/IEnumerableRowEntity_600.json"; } }

        public StorageTables_Service_GenericTests() { }

        [TestInitialize]
        public void Initialize()
        {
            logItem = LoggerFactory.CreateLogger<StorageTablesService<EntityA>>();
            configuration = new AppConfigurationFactory().Create();
            configPersistence = new StorageTablesServiceOptions(
                configuration[AppConfigurationKeys.StorageTablesConnectionString],
                $"AutoTest-{DateTime.UtcNow:yyyy-MM-dd}");
            serviceEntityA = new StorageTablesService<EntityA>(configPersistence);
            serviceRowEntity = new StorageTablesService<RowEntity>(configPersistence);
        }

        [TestMethod]
        public async Task StorageTables_Generic_GetItem()
        {
            var item = new EntityA("PartRead") { SomeString = "Some read data." };
            await serviceEntityA.AddItemAsync(item);
            var readItem = serviceEntityA.GetItem(item.RowKey);
            Assert.IsTrue(readItem.RowKey == item.RowKey);
        }

        [TestMethod]
        public async Task StorageTables_Generic_AddItemAsync()
        {
            var item = new EntityA("PartWrite") { SomeString = "Some write data." };
            await serviceEntityA.AddItemAsync(item);
            var writeItem = serviceEntityA.GetItem(item.RowKey.ToString());
            Assert.IsTrue(writeItem.RowKey == item.RowKey);
            await serviceEntityA.DeleteItemAsync(writeItem.PartitionKey, writeItem.RowKey);
            writeItem = serviceEntityA.GetItem(item.RowKey);
            Assert.IsTrue(writeItem == null);
        }

        [TestMethod]
        public async Task StorageTables_Generic_AddItemsAsync()
        {
            var items = new List<EntityA>() { 
                new EntityA("PartWrite1") { SomeString = "Some write data1." }, 
                new EntityA("PartWrite2") { SomeString = "Some write data2." },
                new EntityA("PartWrite3") { SomeString = "Some write data3." } };

            await serviceEntityA.AddItemsAsync(items);
            foreach(var item in items)
            {
                var writeItem = serviceEntityA.GetItem(item.RowKey.ToString());
                Assert.IsTrue(writeItem.RowKey == item.RowKey);
                Assert.IsTrue(writeItem["SomeString"]?.ToString() == item.SomeString);
                await serviceEntityA.DeleteItemAsync(writeItem.PartitionKey, writeItem.RowKey);
                writeItem = serviceEntityA.GetItem(item.RowKey);
                Assert.IsTrue(writeItem == null);
            }
        }

        //[TestMethod]
        public async Task StorageTables_Generic_UpsertItemsBatchAsync()
        {
            var fileContents = await File.ReadAllBytesAsync(SutJsonFile);
            var items = await JsonSerializer.DeserializeAsync<IEnumerable<RowEntity>>(new MemoryStream(fileContents));

            await serviceRowEntity.UpsertItemsAsync(items);
            foreach (var item in items)
            {
                var writeItem = serviceEntityA.GetItem(item.RowKey.ToString());
                Assert.IsTrue(writeItem.RowKey == item.RowKey);
                await serviceEntityA.DeleteItemAsync(writeItem.PartitionKey, writeItem.RowKey);
                writeItem = serviceEntityA.GetItem(item.RowKey);
                Assert.IsTrue(writeItem == null);
            }
        }

        [TestMethod]
        public async Task StorageTables_Generic_UpsertItemAsync()
        {
            var item = new EntityA("PartWrite") { SomeString = "Some write data." };
            await serviceEntityA.UpsertItemAsync(item);
            var writeItem = serviceEntityA.GetItem(item.RowKey.ToString());
            Assert.IsTrue(writeItem.RowKey == item.RowKey);
            await serviceEntityA.DeleteItemAsync(writeItem.PartitionKey, writeItem.RowKey);
            writeItem = serviceEntityA.GetItem(item.RowKey);
            Assert.IsTrue(writeItem == null);
        }

        [TestMethod]
        public async Task StorageTables_Generic_UpsertItemsAsync()
        {
            var items = new List<EntityA>() {
                new EntityA("PartWrite1") { SomeString = "Some write data1." },
                new EntityA("PartWrite2") { SomeString = "Some write data2." },
                new EntityA("PartWrite3") { SomeString = "Some write data3." } };

            await serviceEntityA.UpsertItemsAsync(items);
            foreach (var item in items)
            {
                var writeItem = serviceEntityA.GetItem(item.RowKey.ToString());
                Assert.IsTrue(writeItem.RowKey == item.RowKey);
                Assert.IsTrue(writeItem["SomeString"]?.ToString() == item.SomeString);
                await serviceEntityA.DeleteItemAsync(writeItem.PartitionKey, writeItem.RowKey);
                writeItem = serviceEntityA.GetItem(item.RowKey);
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