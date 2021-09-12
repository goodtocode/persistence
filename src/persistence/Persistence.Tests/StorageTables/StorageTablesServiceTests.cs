using GoodToCode.Shared.Persistence.StorageTables;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Persistence.Tests
{
    [TestClass]
    public class StorageTablesServiceTests
    {
        private IConfiguration configuration;
        private ILogger<StorageTablesService<EntityA>> logItem;
        private StorageTablesServiceOptions configCosmos;
        public StorageTablesService<EntityA> SutItem { get; private set; }
        public Dictionary<string, StringValues> SutReturn { get; private set; }

        public StorageTablesServiceTests() { }

        [TestInitialize]
        public void Initialize()
        {
            logItem = LoggerFactory.CreateLogger<StorageTablesService<EntityA>>();
            configuration = new AppConfigurationFactory().Create();
            configCosmos = new StorageTablesServiceOptions(
                configuration[AppConfigurationKeys.StorageTablesConnectionString],
                $"AutoTest-{DateTime.UtcNow:yyyy-MM-dd}");
            SutItem = new StorageTablesService<EntityA>(configCosmos);
        }

        [TestMethod]
        public async Task StorageTables_GetItem()
        {
            var item = new EntityA("PartRead") { SomeString = "Some read data." };
            await SutItem.AddItemAsync(item);
            var readItem = SutItem.GetItem(item.RowKey);
            Assert.IsTrue(readItem.RowKey == item.RowKey);
        }

        [TestMethod]
        public async Task StorageTables_AddItemAsync()
        {
            var item = new EntityA("PartWrite") { SomeString = "Some write data." };
            await SutItem.AddItemAsync(item);
            var writeItem = SutItem.GetItem(item.RowKey.ToString());
            Assert.IsTrue(writeItem.RowKey == item.RowKey);
            await SutItem.DeleteItemAsync(writeItem.PartitionKey, writeItem.RowKey);
            writeItem = SutItem.GetItem(item.RowKey);
            Assert.IsTrue(writeItem == null);
        }

        [TestMethod]
        public async Task StorageTables_AddItemsAsync()
        {
            var items = new List<EntityA>() { 
                new EntityA("PartWrite1") { SomeString = "Some write data1." }, 
                new EntityA("PartWrite2") { SomeString = "Some write data2." },
                new EntityA("PartWrite3") { SomeString = "Some write data3." } };

            await SutItem.AddItemsAsync(items);
            foreach(var item in items)
            {
                var writeItem = SutItem.GetItem(item.RowKey.ToString());
                Assert.IsTrue(writeItem.RowKey == item.RowKey);
                Assert.IsTrue(writeItem["SomeString"]?.ToString() == item.SomeString);
                await SutItem.DeleteItemAsync(writeItem.PartitionKey, writeItem.RowKey);
                writeItem = SutItem.GetItem(item.RowKey);
                Assert.IsTrue(writeItem == null);
            }
        }

        [TestMethod]
        public async Task StorageTables_AddItemsBatchAsync()
        {
            var items = new List<EntityA>() {
                new EntityA("PartWrite1") { SomeString = "Some write data1." },
                new EntityA("PartWrite2") { SomeString = "Some write data2." },
                new EntityA("PartWrite1") { SomeString = "Some write data1." },
                new EntityA("PartWrite2") { SomeString = "Some write data2." },
                new EntityA("PartWrite1") { SomeString = "Some write data1." },
                new EntityA("PartWrite2") { SomeString = "Some write data2." },
                new EntityA("PartWrite1") { SomeString = "Some write data1." },
                new EntityA("PartWrite2") { SomeString = "Some write data2." },
                new EntityA("PartWrite1") { SomeString = "Some write data1." },
                new EntityA("PartWrite2") { SomeString = "Some write data2." },
                new EntityA("PartWrite1") { SomeString = "Some write data1." },
                new EntityA("PartWrite2") { SomeString = "Some write data2." },
                new EntityA("PartWrite1") { SomeString = "Some write data1." },
                new EntityA("PartWrite2") { SomeString = "Some write data2." },
                new EntityA("PartWrite1") { SomeString = "Some write data1." },
                new EntityA("PartWrite2") { SomeString = "Some write data2." },
                new EntityA("PartWrite1") { SomeString = "Some write data1." },
                new EntityA("PartWrite2") { SomeString = "Some write data2." },
                new EntityA("PartWrite3") { SomeString = "Some write data3." } };

            await SutItem.AddItemsAsync(items);
            foreach (var item in items)
            {
                var writeItem = SutItem.GetItem(item.RowKey.ToString());
                Assert.IsTrue(writeItem.RowKey == item.RowKey);
                Assert.IsTrue(writeItem["SomeString"]?.ToString() == item.SomeString);
                await SutItem.DeleteItemAsync(writeItem.PartitionKey, writeItem.RowKey);
                writeItem = SutItem.GetItem(item.RowKey);
                Assert.IsTrue(writeItem == null);
            }
        }

        [TestMethod]
        public async Task StorageTables_UpsertItemAsync()
        {
            var item = new EntityA("PartWrite") { SomeString = "Some write data." };
            await SutItem.UpsertItemAsync(item);
            var writeItem = SutItem.GetItem(item.RowKey.ToString());
            Assert.IsTrue(writeItem.RowKey == item.RowKey);
            await SutItem.DeleteItemAsync(writeItem.PartitionKey, writeItem.RowKey);
            writeItem = SutItem.GetItem(item.RowKey);
            Assert.IsTrue(writeItem == null);
        }

        [TestMethod]
        public async Task StorageTables_UpsertItemsAsync()
        {
            var items = new List<EntityA>() {
                new EntityA("PartWrite1") { SomeString = "Some write data1." },
                new EntityA("PartWrite2") { SomeString = "Some write data2." },
                new EntityA("PartWrite3") { SomeString = "Some write data3." } };

            await SutItem.UpsertItemsAsync(items);
            foreach (var item in items)
            {
                var writeItem = SutItem.GetItem(item.RowKey.ToString());
                Assert.IsTrue(writeItem.RowKey == item.RowKey);
                Assert.IsTrue(writeItem["SomeString"]?.ToString() == item.SomeString);
                await SutItem.DeleteItemAsync(writeItem.PartitionKey, writeItem.RowKey);
                writeItem = SutItem.GetItem(item.RowKey);
                Assert.IsTrue(writeItem == null);
            }
        }

        [TestMethod]
        public async Task StorageTables_UpsertItemsBatchAsync()
        {
            var items = new List<EntityA>() {
                new EntityA("PartWrite1") { SomeString = "Some write data1." },
                new EntityA("PartWrite2") { SomeString = "Some write data2." },
                new EntityA("PartWrite3") { SomeString = "Some write data3." } };

            await SutItem.UpsertItemsAsync(items);
            foreach (var item in items)
            {
                var writeItem = SutItem.GetItem(item.RowKey.ToString());
                Assert.IsTrue(writeItem.RowKey == item.RowKey);
                Assert.IsTrue(writeItem["SomeString"]?.ToString() == item.SomeString);
                await SutItem.DeleteItemAsync(writeItem.PartitionKey, writeItem.RowKey);
                writeItem = SutItem.GetItem(item.RowKey);
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