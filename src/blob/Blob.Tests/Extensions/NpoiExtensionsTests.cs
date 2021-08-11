using GoodToCode.Shared.Blob.Abstractions;
using GoodToCode.Shared.Blob.Excel;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace GoodToCode.Shared.Blob.Tests
{
    [TestClass]
    public class NpoiExtensionsTests
    {
        private readonly NpoiBlobReader reader;
        private readonly string executingPath;
        private string AssetsFolder { get { return @$"{executingPath}/Assets"; } }

        private string SutXlsFile { get { return @$"{AssetsFolder}/TestFile.xls"; } }
        private string SutXlsxFile { get { return @$"{AssetsFolder}/TestFile.xlsx"; } }        

        public ISheetData SutXls { get; private set; }
        public ISheetData SutXlsx { get; private set; }
        public Dictionary<string, StringValues> SutReturn { get; private set; }

        public NpoiExtensionsTests()
        {
            reader = new NpoiBlobReader();
            // Visual Studio vs. dotnet test execute different folders
            executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);                        
            executingPath = Directory.Exists(AssetsFolder) ? executingPath : Directory.GetCurrentDirectory();
            executingPath = Directory.Exists(AssetsFolder) ? executingPath : $"{Directory.GetParent(executingPath)}/bin/Debug/net5.0";
        }

        [TestMethod]
        public void ExcelFile_ToWorkbookData()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            var wb = reader.ReadFile(SutXlsxFile);
            var workbookData = wb.ToWorkbookData(Path.GetFileName(SutXlsxFile));
            Assert.IsTrue(workbookData.SheetMetadata.Any(), $"workbookData.SheetMetadata.Any={workbookData.SheetMetadata.Any()}");
        }

        [TestMethod]
        public void ExcelFile_ToSheetData()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            var wb = reader.ReadFile(SutXlsxFile);
            var sheetData = wb.GetSheetAt(0).ToSheetData(Path.GetFileName(SutXlsxFile));
            Assert.IsTrue(sheetData.Rows.Any(), $"sheetData.Rows.Any={sheetData.Rows.Any()}");
        }
    }
}

