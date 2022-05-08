using GoodToCode.Persistence.Abstractions;
using GoodToCode.Persistence.Blob.Excel;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace GoodToCode.Persistence.Tests
{
    [TestClass]
    public class NpoiExtensions_Tests
    {
        private readonly NpoiBlobReader reader;
        private readonly string executingPath;
        private string AssetsFolder { get { return @$"{executingPath}/Assets"; } }
        private string SutXlsxFile { get { return @$"{AssetsFolder}/TestFile.xlsx"; } }        

        public ISheetData SutXls { get; private set; }
        public ISheetData SutXlsx { get; private set; }
        public Dictionary<string, StringValues> SutReturn { get; private set; }

        public NpoiExtensions_Tests()
        {
            reader = new NpoiBlobReader();
            // Visual Studio vs. dotnet test execute different folders
            executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);                        
            executingPath = Directory.Exists(AssetsFolder) ? executingPath : Directory.GetCurrentDirectory();
            
        }

        [TestMethod]
        public void ExcelFile_ToWorkbookData()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            var wb = reader.ReadFile(SutXlsxFile);
            var workbookData = wb.ToWorkbookData(Path.GetFileName(SutXlsxFile));
            Assert.IsTrue(workbookData.Sheets.Any(), $"workbookData.SheetMetadata.Any={workbookData.Sheets.Any()}");
        }

        [TestMethod]
        public void ExcelFile_ToSheetData()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            var wb = reader.ReadFile(SutXlsxFile);
            for(var count = 0; count < wb.NumberOfSheets; count++)
            {
                var sheetData = wb.GetSheetAt(count).ToSheetData(count, Path.GetFileName(SutXlsxFile));
                Assert.IsTrue(sheetData.SheetName.Length > 0 ||
                sheetData.Rows.Any(), $"sheetData.Rows.Any={sheetData.Rows.Any()}");
            }
        }
    }
}

