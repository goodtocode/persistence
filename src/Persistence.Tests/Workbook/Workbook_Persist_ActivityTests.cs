using Azure.Data.Tables;
using GoodToCode.Persistence.Abstractions;
using GoodToCode.Persistence.Azure.StorageTables;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodToCode.Persistence.Tests
{
    [TestClass]
    public class Workbook_Persist_Tests
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<Workbook_Persist_Tests> logItem;
        private readonly StorageTablesServiceConfiguration configStorage;
        private static string SutXlsxFile { get { return @$"{PathFactory.GetProjectSubfolder("Assets")}/OpinionFile.xlsx"; } }
        public CellEntity SutRow { get; private set; }
        public IEnumerable<CellEntity> SutRows { get; private set; }
        public Dictionary<string, StringValues> SutReturn { get; private set; }

        public Workbook_Persist_Tests()
        {
            logItem = LoggerFactory.CreateLogger<Workbook_Persist_Tests>();
            configuration = new AppConfigurationFactory().Create();
            configStorage = new StorageTablesServiceConfiguration(
                configuration[AppConfigurationKeys.StorageTablesConnectionString],
                $"UnitTest-{DateTime.UtcNow:yyyy-MM-dd}-Workbook");
        }

        [TestMethod]
        public async Task Workbook_Persist_Step()
        {

            try
            {
                var workbooks = WorkbookFactory.CreateWorkbookEntity();
                var results = new List<TableEntity>();
                foreach (var sheet in workbooks.Sheets)
                    foreach(RowEntity row in sheet.Rows)
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

