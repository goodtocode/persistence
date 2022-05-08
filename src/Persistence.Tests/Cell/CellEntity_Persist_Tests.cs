﻿using GoodToCode.Persistence.Abstractions;
using GoodToCode.Persistence.Azure.StorageTables;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GoodToCode.Persistence.Tests
{
    [TestClass]
    public class CellEntity_Persist_Tests
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<CellEntity_Persist_Tests> logItem;
        private readonly StorageTablesServiceConfiguration configStorage;

        public CellEntity_Persist_Tests()
        {
            logItem = LoggerFactory.CreateLogger<CellEntity_Persist_Tests>();
            configuration = new AppConfigurationFactory().Create();
            configStorage = new StorageTablesServiceConfiguration(
                configuration[AppConfigurationKeys.StorageTablesConnectionString],
                $"UnitTest-{DateTime.UtcNow:yyyy-MM-dd}-Cell");
        }

        [TestMethod]
        public async Task CellEntity_Persist_Step()       
        {
            var entity = CellFactory.CreateCellEntity();
            Assert.IsTrue(string.IsNullOrWhiteSpace(entity.PartitionKey) || string.IsNullOrWhiteSpace(entity.RowKey), $"PartitionKey and RowKey are required. {entity.GetType().Name}");

            try
            {
                var results = await new StorageTablesService<CellEntity>(configStorage).AddItemAsync(entity);
                Assert.IsTrue(results.Any(), "Failed to persist.");
            }
            catch (Exception ex)
            {
                logItem.LogError(ex.Message, ex);
                Assert.Fail(ex.Message);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
        }
    }
}

