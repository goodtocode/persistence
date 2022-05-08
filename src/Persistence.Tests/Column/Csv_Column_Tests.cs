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
                var results = new CsvService().GetColumn(itemToAnalyze, 1);
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
                var returnCells = new List<ICellData>();
                var sheet = new CsvService().GetSheet(itemToAnalyze);
                if (!sheet.Rows.Any()) throw new ArgumentException("Passed sheet does not have any rows.");
                var header = sheet.GetRow(1);
                var foundCells = header.Cells.Where(c => c.ColumnName.Contains('*'));
                foreach (var cell in foundCells)
                {
                    var newCells = sheet.GetColumn(cell.ColumnIndex);
                    returnCells.AddRange(newCells);
                }
                Assert.IsTrue(returnCells.Any(), "No results from analytics service.");
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

