using Azure.Data.Tables;
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
    public class Sheet_Persist_Tests
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<Sheet_Persist_Tests> logItem;
        private readonly StorageTablesServiceConfiguration configStorage;

        public Sheet_Persist_Tests()
        {
            logItem = LoggerFactory.CreateLogger<Sheet_Persist_Tests>();
            configuration = new AppConfigurationFactory().Create();
            configStorage = new StorageTablesServiceConfiguration(
                configuration[AppConfigurationKeys.StorageTablesConnectionString],
                $"UnitTest-{DateTime.UtcNow:yyyy-MM-dd}-Sheet");
        }

        [TestMethod]
        public async Task Sheet_Persist()
        {
            try
            {
                var sheet = SheetFactory.CreateSheetData();
                var results = new List<TableEntity>();
                foreach (RowEntity row in sheet.Rows)
                    results.Add(await new StorageTablesService<RowEntity>(configStorage).AddItemAsync(row));
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

