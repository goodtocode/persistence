using GoodToCode.Persistence.Abstractions;
using GoodToCode.Persistence.Blob.Csv;
using GoodToCode.Persistence.DurableTasks;
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
    public class Csv_Column_Tests
    {
        private readonly ILogger<Csv_Column_Tests> logItem;
        private static string SutCsvFile { get { return @$"{PathFactory.GetProjectSubfolder("Assets")}/OpinionFile.csv"; } }
        public CellEntity SutRow { get; private set; }
        public IEnumerable<CellEntity> SutRows { get; private set; }
        public Dictionary<string, StringValues> SutReturn { get; private set; }


        public Csv_Column_Tests()
        {
            logItem = LoggerFactory.CreateLogger<Csv_Column_Tests>();
        }

        [TestMethod]
        public async Task Csv_Column_Load_Step()
        {
            Assert.IsTrue(File.Exists(SutCsvFile), $"{SutCsvFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");

            try
            {
                var bytes = await FileFactoryService.GetInstance().ReadAllBytesAsync(SutCsvFile);
                Stream itemToAnalyze = new MemoryStream(bytes);




                var workflow = new CsvColumnLoadStep(new CsvService());
                var results = workflow.Execute(itemToAnalyze, 3);

                Assert.IsTrue(results.Any(), "No results from analytics service.");
            }
            catch (Exception ex)
            {
                logItem.LogError(ex.Message, ex);
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public async Task Csv_Column_Search_Step()
        {
            Assert.IsTrue(File.Exists(SutCsvFile), $"{SutCsvFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");

            try
            {
                var bytes = await FileFactoryService.GetInstance().ReadAllBytesAsync(SutCsvFile);
                Stream itemToAnalyze = new MemoryStream(bytes);
                var workflow = new CsvColumnSearchStep(new CsvService());
                var results = workflow.Execute(itemToAnalyze, "DocName", "*");
                Assert.IsTrue(results.Any(), "No results from analytics service.");
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

