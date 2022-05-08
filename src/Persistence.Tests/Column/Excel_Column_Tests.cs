using GoodToCode.Persistence.Abstractions;
using GoodToCode.Persistence.Blob.Excel;
using Microsoft.Extensions.Logging;
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
    public class Excel_Column_Tests
    {
        private readonly ILogger<Excel_Column_Tests> logItem;
        private static string SutXlsxFile { get { return @$"{PathFactory.GetProjectSubfolder("Assets")}/OpinionFile.xlsx"; } }

        public Excel_Column_Tests()
        {
            logItem = LoggerFactory.CreateLogger<Excel_Column_Tests>();
        }

        [TestMethod]
        public async Task Excel_Column_Load_Step()       
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");

            try
            { 
                var bytes = await FileFactoryService.GetInstance().ReadAllBytesAsync(SutXlsxFile);
                Stream itemToAnalyze = new MemoryStream(bytes);
                var results = new ExcelService().GetColumn(itemToAnalyze, 0, 3);
                Assert.IsTrue(results.Any(), "No results from analytics service.");
            }
            catch (Exception ex)
            {
                logItem.LogError(ex.Message, ex);
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public async Task Excel_Column_Search_Step()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");

            try
            {
                var bytes = await FileFactoryService.GetInstance().ReadAllBytesAsync(SutXlsxFile);
                Stream itemToAnalyze = new MemoryStream(bytes);

                var returnCells = new List<ICellData>();
                var wb = new ExcelService().GetWorkbook(itemToAnalyze, "DocName");
                foreach (var sheet in wb.Sheets)
                {
                    if (!sheet.Rows.Any()) throw new ArgumentException("Passed sheet does not have any rows.");
                    var header = sheet.GetRow(1);
                    var foundCells = header.Cells.Where(c => c.ColumnName.Contains('*'));
                    foreach (var cell in foundCells)
                    {
                        var newCells = sheet.GetColumn(cell.ColumnIndex);
                        returnCells.AddRange(newCells);
                    }
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

