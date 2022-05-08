using GoodToCode.Persistence.DurableTasks;
using GoodToCode.Persistence.Abstractions;
using GoodToCode.Persistence.Azure.StorageTables;
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
    public class RowEntity_Persist_Tests
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<RowEntity_Persist_Tests> logItem;
        private readonly StorageTablesServiceConfiguration configStorage;
        private static string SutXlsxFile { get { return @$"{PathFactory.GetProjectSubfolder("Assets")}/OpinionFile.xlsx"; } }
        public CellEntity SutRow { get; private set; }
        public IEnumerable<CellEntity> SutRows { get; private set; }
        public Dictionary<string, StringValues> SutReturn { get; private set; }

        public RowEntity_Persist_Tests()
        {
            logItem = LoggerFactory.CreateLogger<RowEntity_Persist_Tests>();
            configuration = new AppConfigurationFactory().Create();
            configStorage = new StorageTablesServiceConfiguration(
                configuration[AppConfigurationKeys.StorageTablesConnectionString],
                $"UnitTest-{DateTime.UtcNow:yyyy-MM-dd}-RowEntity");
        }

        [TestMethod]
        public async Task RowEntity_Persist_Fake()       
        {
            var row = RowFactory.CreateRowData();

            try
            {
                var entity = new RowEntity(Guid.NewGuid().ToString(), row.Cells);
                var results = await new StorageTablesService<CellEntity>(configStorage).AddItemAsync(entity.ToDictionary());
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

