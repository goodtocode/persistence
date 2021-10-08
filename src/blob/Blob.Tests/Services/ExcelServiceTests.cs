using GoodToCode.Shared.Blob.Abstractions;
using GoodToCode.Shared.Blob.Excel;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Blob.Tests
{
    [TestClass]
    public class ExcelServiceTests
    {
        private readonly string executingPath;
        private string AssetsFolder { get { return @$"{executingPath}/Assets"; } }

        private string SutOpinionFile { get { return @$"{AssetsFolder}/OpinionFile.xlsx"; } }
        private string SutXlsxFile { get { return @$"{AssetsFolder}/TestFile.xlsx"; } }        

        public ISheetData SutXls { get; private set; }
        public ISheetData SutXlsx { get; private set; }
        public Dictionary<string, StringValues> SutReturn { get; private set; }

        public ExcelServiceTests()
        {
            // Visual Studio vs. dotnet test execute different folders
            executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);                        
            executingPath = Directory.Exists(AssetsFolder) ? executingPath : Directory.GetCurrentDirectory();
            executingPath = Directory.Exists(AssetsFolder) ? executingPath : $"{Directory.GetParent(executingPath)}/bin/Debug/net5.0";
        }

        [TestMethod]
        public async Task ExcelService_GetWorkbook()
        {
            Assert.IsTrue(File.Exists(SutOpinionFile), $"{SutOpinionFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            // Input is stream
            var bytes = await FileFactoryService.GetInstance().ReadAllBytesAsync(SutOpinionFile);
            var stream = new MemoryStream(bytes);
            // Service
            var excelService = new ExcelService();
            var wb = excelService.GetWorkbook(stream);
            Assert.IsTrue(wb.Sheets.Count() > 0);
        }

        [TestMethod]
        public async Task ExcelService_GetSheet()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            // Input is stream
            var bytes = await FileFactoryService.GetInstance().ReadAllBytesAsync(SutXlsxFile);
            var stream = new MemoryStream(bytes);
            // Service
            var excelService = new ExcelService();
            var sheet = excelService.GetSheet(stream, 0);
            Assert.IsTrue(sheet.Rows.Count() > 0);
        }

        [TestMethod]
        public async Task ExcelService_GetRow()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            // Input is stream
            var bytes = await FileFactoryService.GetInstance().ReadAllBytesAsync(SutXlsxFile);
            var stream = new MemoryStream(bytes);
            // Service
            var excelService = new ExcelService();
            var rows = excelService.GetRow(stream, 0, 1);
            Assert.IsTrue(rows.Cells.Any());
        }

        [TestMethod]
        public async Task ExcelService_GetCell()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            // Input is stream
            var bytes = await FileFactoryService.GetInstance().ReadAllBytesAsync(SutXlsxFile);
            var stream = new MemoryStream(bytes);
            // Service
            var excelService = new ExcelService();
            var cell = excelService.GetCell(stream, 0, 1, 1);
            Assert.IsTrue(cell.ToString().Length > 0);
        }

        [TestMethod]
        public async Task ExcelService_Cells()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            // Input is stream
            var bytes = await FileFactoryService.GetInstance().ReadAllBytesAsync(SutXlsxFile);
            var stream = new MemoryStream(bytes);
            // Service
            var excelService = new ExcelService();
            var sheet = excelService.GetSheet(stream, 0);
            Assert.IsTrue(sheet.Cells.Count() > 0);
            var itemWithData = sheet.Cells.Where(x => !string.IsNullOrWhiteSpace(x.CellValue));
            Assert.IsTrue(itemWithData.Any());
        }

        [TestMethod]
        public async Task ExcelService_Rows()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            // Input is stream
            var bytes = await FileFactoryService.GetInstance().ReadAllBytesAsync(SutXlsxFile);
            var stream = new MemoryStream(bytes);
            // Service
            var excelService = new ExcelService();
            var sheet = excelService.GetSheet(stream, 0);
            Assert.IsTrue(sheet.Rows.Count() > 0);
            var itemWithData = sheet.Rows.Where(x => x.RowIndex > 0);
            Assert.IsTrue(itemWithData.Any());
        }
    }
}

