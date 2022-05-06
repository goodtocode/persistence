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
    public class Column_Persist_StepTests
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<Column_Persist_StepTests> logItem;
        private readonly StorageTablesServiceConfiguration configStorage;
        private static string SutXlsxFile { get { return @$"{PathFactory.GetProjectSubfolder("Assets")}/OpinionFile.xlsx"; } }
        public CellEntity SutRow { get; private set; }
        public IEnumerable<CellEntity> SutRows { get; private set; }
        public Dictionary<string, StringValues> SutReturn { get; private set; }

        public Column_Persist_StepTests()
        {
            logItem = LoggerFactory.CreateLogger<Column_Persist_StepTests>();
            configuration = new AppConfigurationFactory().Create();
            configStorage = new StorageTablesServiceConfiguration(
                configuration[AppConfigurationKeys.StorageTablesConnectionString],
                $"UnitTest-{DateTime.UtcNow:yyyy-MM-dd}-Column");
        }

        [TestMethod]
        public async Task Column_Persist_Step()       
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");

            try
            { 
                var bytes = await FileFactoryService.GetInstance().ReadAllBytesAsync(SutXlsxFile);
                Stream itemToAnalyze = new MemoryStream(bytes);
                var workflow = new ColumnPersistStep(configStorage);
                var results = await workflow.ExecuteAsync(CellFactory.CreateCellEntity());
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

