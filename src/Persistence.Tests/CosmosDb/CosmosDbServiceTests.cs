﻿using GoodToCode.Persistence.Azure.CosmosDb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodToCode.Persistence.Tests
{

    public class CosmosDbServiceTests
    {
        private IConfiguration configuration;
        private ILogger<CosmosDbServiceTests> log;
        private CosmosDbServiceOptions configCosmos;
        public CosmosDbItemService<EntityA> SutItem { get; private set; }
        public Dictionary<string, StringValues> SutReturn { get; private set; }

        public CosmosDbServiceTests()
        {
        }
        
        public void Initialize()
        {
            log = LoggerFactory.CreateLogger<CosmosDbServiceTests>();
            configuration = new AppConfigurationFactory().Create();
            configCosmos = new CosmosDbServiceOptions(
                configuration[AppConfigurationKeys.CosmosDbConnectionString],
                $"UnitTest-{DateTime.UtcNow:yyyy-MM-dd}"); 
            SutItem = new CosmosDbItemService<EntityA>(configCosmos);
        }

        
        public async Task CosmosDb_Create()
        {
            var table = await SutItem.CreateOrGetTableAsync();
            Assert.IsTrue(table != null);
        }

        
        public async Task CosmosDb_Read()
        {
            var item = new EntityA("PartRead") { SomeString = "Some read data." };
            await SutItem.AddItemAsync(item);
            var readItem = SutItem.GetItem(item.RowKey);
            Assert.IsTrue(readItem.RowKey == item.RowKey);
        }

        
        public async Task CosmosDb_Write()
        {
            var item = new EntityA("PartWrite") { SomeString = "Some write data." };
            await SutItem.AddItemAsync(item);
            var writeItem = SutItem.GetItem(item.RowKey.ToString());
            Assert.IsTrue(writeItem.RowKey == item.RowKey);
            await SutItem.DeleteItemAsync(writeItem.PartitionKey, writeItem.RowKey);
            writeItem = SutItem.GetItem(item.RowKey);
            Assert.IsTrue(writeItem == null);
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            await SutItem.DeleteTableAsync();
        }
    }
}