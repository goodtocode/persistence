using GoodToCode.Persistence.Abstractions;
using GoodToCode.Persistence.Blob.Csv;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GoodToCode.Persistence.Tests
{
    [TestClass]
    public class Csv_Service_Tests
    {
        private readonly string executingPath;
        private string AssetsFolder { get { return @$"{executingPath}/Assets"; } }
        private string SutCsvFile { get { return @$"{AssetsFolder}/TestFile.csv"; } }        

        public ISheetData SutXls { get; private set; }
        public ISheetData SutXlsx { get; private set; }
        public Dictionary<string, StringValues> SutReturn { get; private set; }

        public Csv_Service_Tests()
        {
            // Visual Studio vs. dotnet test execute different folders
            executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);                        
            executingPath = Directory.Exists(AssetsFolder) ? executingPath : Directory.GetCurrentDirectory();            
        }

        [TestMethod]
        public async Task CsvService_GetSheet()
        {
            Assert.IsTrue(File.Exists(SutCsvFile), $"{SutCsvFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            // Input is stream
            var bytes = await FileFactoryService.GetInstance().ReadAllBytesAsync(SutCsvFile);
            var stream = new MemoryStream(bytes);
            // Service
            var csvService = new CsvService();
            var sheet = csvService.GetSheet(stream);
            Assert.IsTrue(sheet.Rows.Any());
        }

        [TestMethod]
        public async Task CsvService_GetRow()
        {
            Assert.IsTrue(File.Exists(SutCsvFile), $"{SutCsvFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            // Input is stream
            var bytes = await FileFactoryService.GetInstance().ReadAllBytesAsync(SutCsvFile);
            var stream = new MemoryStream(bytes);
            // Service
            var csvService = new CsvService();
            var rows = csvService.GetRow(stream, 1);
            Assert.IsTrue(rows.Cells.Any());
        }

        [TestMethod]
        public async Task CsvService_GetCell()
        {
            Assert.IsTrue(File.Exists(SutCsvFile), $"{SutCsvFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            // Input is stream
            var bytes = await FileFactoryService.GetInstance().ReadAllBytesAsync(SutCsvFile);
            var stream = new MemoryStream(bytes);
            // Service
            var csvService = new CsvService();
            var cell = csvService.GetCell(stream, 1, 1);
            Assert.IsTrue(cell.ToString().Length > 0);
        }

        [TestMethod]
        public async Task CsvService_Sheets()
        {
            Assert.IsTrue(File.Exists(SutCsvFile), $"{SutCsvFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            // Input is stream
            var bytes = await FileFactoryService.GetInstance().ReadAllBytesAsync(SutCsvFile);
            var stream = new MemoryStream(bytes);
            // Service
            var csvService = new CsvService();
            var sheet = csvService.GetSheet(stream);
            Assert.IsTrue(sheet.Rows.Any());
            var itemWithData = sheet.Rows.Where(x => x.RowIndex > 0);
            Assert.IsTrue(itemWithData.Any());
            var notInSingleSheet = sheet.Cells.Where(x => x.SheetIndex != 0);
            Assert.IsTrue(!notInSingleSheet.Any());
            // ToDictionary for unrolling into a row for persistence
            var dict = sheet.ToDictionary();
            Assert.IsTrue(dict.Any());
            Assert.IsTrue(dict.FirstOrDefault().Any());
            Assert.IsTrue(!string.IsNullOrWhiteSpace(dict.FirstOrDefault().FirstOrDefault().Key));
            Assert.IsTrue(dict.FirstOrDefault().FirstOrDefault().Value != null);
        }

        [TestMethod]
        public async Task CsvService_Rows()
        {
            Assert.IsTrue(File.Exists(SutCsvFile), $"{SutCsvFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            // Input is stream
            var bytes = await FileFactoryService.GetInstance().ReadAllBytesAsync(SutCsvFile);
            var stream = new MemoryStream(bytes);
            // Service
            var csvService = new CsvService();
            var row = csvService.GetRow(stream, 2);
            Assert.IsTrue(row.Cells.Any());
            var itemWithData = row.Cells.Where(x => !string.IsNullOrWhiteSpace(x.CellValue));
            Assert.IsTrue(itemWithData.Any());
            var notInSingleSheet = row.Cells.Where(x => x.SheetIndex != 0);
            Assert.IsTrue(!notInSingleSheet.Any());
            // ToDictionary for unrolling into a row for persistence
            var dict = row.ToDictionary();
            Assert.IsTrue(dict.Any());
            Assert.IsTrue(dict.Any());
            Assert.IsTrue(!string.IsNullOrWhiteSpace(dict.FirstOrDefault().Key));
            Assert.IsTrue(dict.FirstOrDefault().Value != null);
        }

        [TestMethod]
        public async Task CsvService_Cells()
        {
            Assert.IsTrue(File.Exists(SutCsvFile), $"{SutCsvFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            // Input is stream
            var bytes = await FileFactoryService.GetInstance().ReadAllBytesAsync(SutCsvFile);
            var stream = new MemoryStream(bytes);
            // Service
            var csvService = new CsvService();
            var cell = csvService.GetCell(stream, 2, 2);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(cell.CellValue));
        }
    }
}

