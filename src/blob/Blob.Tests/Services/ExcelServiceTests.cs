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
        private readonly NpoiBlobReader reader;
        private readonly string executingPath;
        private string AssetsFolder { get { return @$"{executingPath}/Assets"; } }

        private string SutXlsFile { get { return @$"{AssetsFolder}/TestFile.xls"; } }
        private string SutXlsxFile { get { return @$"{AssetsFolder}/TestFile.xlsx"; } }        

        public ISheetData SutXls { get; private set; }
        public ISheetData SutXlsx { get; private set; }
        public Dictionary<string, StringValues> SutReturn { get; private set; }

        public ExcelServiceTests()
        {
            reader = new NpoiBlobReader();
            // Visual Studio vs. dotnet test execute different folders
            executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);                        
            executingPath = Directory.Exists(AssetsFolder) ? executingPath : Directory.GetCurrentDirectory();
            executingPath = Directory.Exists(AssetsFolder) ? executingPath : $"{Directory.GetParent(executingPath)}/bin/Debug/net5.0";
        }

        [TestMethod]
        public async Task ExcelService_GetSheet()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            // Input is stream
            var bytes = await File.ReadAllBytesAsync(SutXlsxFile);
            var stream = new MemoryStream(bytes);
            // Service
            var excelService = new ExcelService(Path.GetFileName(SutXlsxFile));
            var sheet = excelService.GetSheet(stream, 0);
            Assert.IsTrue(sheet.Rows.Any());
        }

        [TestMethod]
        public async Task ExcelService_GetRow()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            // Input is stream
            var bytes = await File.ReadAllBytesAsync(SutXlsxFile);
            var stream = new MemoryStream(bytes);
            // Service
            var excelService = new ExcelService(Path.GetFileName(SutXlsxFile));
            var rows = excelService.GetRow(stream, 0, 1);
            Assert.IsTrue(rows.Cells.Any());
            var first = rows.Cells.FirstOrDefault();
            Assert.IsNotNull(first);
            Assert.IsNotNull(first.CellValue);
            Assert.IsTrue(first.ColumnIndex > -1);
            Assert.IsNotNull(first.ColumnName);
            Assert.IsTrue(first.RowIndex > -1);
            Assert.IsTrue(first.SheetIndex > -1);
            Assert.IsNotNull(first.SheetName);
            Assert.IsNotNull(first.WorkbookName);
        }

        [TestMethod]
        public async Task ExcelService_GetColumn()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            // Input is stream
            var bytes = await File.ReadAllBytesAsync(SutXlsxFile);
            var stream = new MemoryStream(bytes);
            // Service
            var excelService = new ExcelService(Path.GetFileName(SutXlsxFile));
            var columns = excelService.GetColumn(stream, 0, 1);            
            Assert.IsTrue(columns.Any());
            var first = columns.FirstOrDefault();
            Assert.IsNotNull(first);
            Assert.IsNotNull(first.CellValue);
            Assert.IsTrue(first.ColumnIndex > -1);
            Assert.IsNotNull(first.ColumnName);
            Assert.IsTrue(first.RowIndex > -1);
            Assert.IsTrue(first.SheetIndex > -1);
            Assert.IsNotNull(first.SheetName);
            Assert.IsNotNull(first.WorkbookName);
        }

        [TestMethod]
        public async Task ExcelService_GetCell()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            // Input is stream
            var bytes = await File.ReadAllBytesAsync(SutXlsxFile);
            var stream = new MemoryStream(bytes);
            // Service
            var excelService = new ExcelService(Path.GetFileName(SutXlsxFile));
            var cell = excelService.GetCell(stream, 0, 1, 1);
            Assert.IsTrue(cell.CellValue.Length > 0);
            Assert.IsNotNull(cell.CellValue);
            Assert.IsTrue(cell.ColumnIndex > -1);
            Assert.IsNotNull(cell.ColumnName);
            Assert.IsTrue(cell.RowIndex > -1);
            Assert.IsTrue(cell.SheetIndex > -1);
            Assert.IsNotNull(cell.SheetName);
            Assert.IsNotNull(cell.WorkbookName);
        }
    }
}

