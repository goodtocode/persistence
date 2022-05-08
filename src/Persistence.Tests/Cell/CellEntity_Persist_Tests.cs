using GoodToCode.Persistence.Abstractions;
using GoodToCode.Persistence.Azure.StorageTables;
using GoodToCode.Persistence.DurableTasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GoodToCode.Persistence.Tests
{
    [TestClass]
    public class CellEntity_Persist_Tests
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<CellEntity_Persist_Tests> logItem;
        private readonly StorageTablesServiceConfiguration configStorage;
        private static string SutXlsxFile { get { return @$"{PathFactory.GetProjectSubfolder("Assets")}/OpinionFile.xlsx"; } }
        public CellEntity SutRow { get; private set; }
        public IEnumerable<CellEntity> SutRows { get; private set; }
        public Dictionary<string, StringValues> SutReturn { get; private set; }

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

