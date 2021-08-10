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
    public class NpoiServiceTests
    {
        private readonly string executingPath;
        private string AssetsFolder { get { return @$"{executingPath}/Assets"; } }

        private string SutXlsFile { get { return @$"{AssetsFolder}/TestFile.xls"; } }
        private string SutXlsxFile { get { return @$"{AssetsFolder}/TestFile.xlsx"; } }        

        public ISheetData SutXls { get; private set; }
        public ISheetData SutXlsx { get; private set; }
        public Dictionary<string, StringValues> SutReturn { get; private set; }

        public NpoiServiceTests()
        {
            // Visual Studio vs. dotnet test execute different folders
            executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);                        
            executingPath = Directory.Exists(AssetsFolder) ? executingPath : Directory.GetCurrentDirectory();
            executingPath = Directory.Exists(AssetsFolder) ? executingPath : $"{Directory.GetParent(executingPath)}/bin/Debug/net5.0";
        }

        [TestMethod]
        public async Task NpoiService_GetSheet()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            // Input is stream
            var bytes = await File.ReadAllBytesAsync(SutXlsxFile);
            var stream = new MemoryStream(bytes);
            // Service
            var npoiService = new NpoiService();
            var sheet = npoiService.GetSheet(stream, 0);
            Assert.IsTrue(sheet.LastRowNum > 0);
        }

        [TestMethod]
        public async Task NpoiService_GetRow()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            // Input is stream
            var bytes = await File.ReadAllBytesAsync(SutXlsxFile);
            var stream = new MemoryStream(bytes);
            // Service
            var NpoiService = new NpoiService();
            var rows = NpoiService.GetRow(stream, 0, 1);
            Assert.IsTrue(rows.Cells.Any());
        }

        [TestMethod]
        public async Task NpoiService_GetCell()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            // Input is stream
            var bytes = await File.ReadAllBytesAsync(SutXlsxFile);
            var stream = new MemoryStream(bytes);
            // Service
            var NpoiService = new NpoiService();
            var cell = NpoiService.GetCell(stream, 0, 1, 1);
            Assert.IsTrue(cell.ToString().Length > 0);
        }
    }
}

