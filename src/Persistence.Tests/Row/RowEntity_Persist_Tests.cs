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
    public class RowEntity_Persist_Tests
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<RowEntity_Persist_Tests> logItem;
        private readonly StorageTablesServiceConfiguration configStorage;

        public RowEntity_Persist_Tests()
        {
            logItem = LoggerFactory.CreateLogger<RowEntity_Persist_Tests>();
            configuration = new AppConfigurationFactory().Create();
            configStorage = new StorageTablesServiceConfiguration(
                configuration[AppConfigurationKeys.StorageTablesConnectionString],
                $"UnitTest-{DateTime.UtcNow:yyyy-MM-dd}-RowEntity");
        }

        [TestMethod]
        public async Task RowEntity_Persist()       
        {
            var row = RowFactory.CreateRowData();

            try
            {
                var entity = new RowEntity(Guid.NewGuid().ToString(), row.Cells);
                var results = await new StorageTablesService<RowEntity>(configStorage).AddItemAsync(entity);
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

