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
    public class ExcelFileReaderTests
    {
        private readonly NpoiBlobReader reader;
        private readonly string executingPath;
        private string AssetsFolder { get { return @$"{executingPath}/Assets"; } }

        private string SutXlsFile { get { return @$"{AssetsFolder}/TestFile.xls"; } }
        private string SutXlsxFile { get { return @$"{AssetsFolder}/TestFile.xlsx"; } }        

        public ISheetData SutXls { get; private set; }
        public ISheetData SutXlsx { get; private set; }
        public Dictionary<string, StringValues> SutReturn { get; private set; }

        public ExcelFileReaderTests()
        {
            reader = new NpoiBlobReader();
            // Visual Studio vs. dotnet test execute different folders
            executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);                        
            executingPath = Directory.Exists(AssetsFolder) ? executingPath : Directory.GetCurrentDirectory();
            executingPath = Directory.Exists(AssetsFolder) ? executingPath : $"{Directory.GetParent(executingPath)}/bin/Debug/net5.0";
        }

        public void ExcelFile_Workbook_Xlsx()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            var wb = reader.ReadFile(SutXlsxFile);
            SutXlsx = wb.GetSheetAt(0).ToSheetData(0, Path.GetFileName(SutXlsxFile));
            Assert.IsTrue(SutXlsx != null);
            Assert.IsTrue(SutXlsx.Rows.Count() > 0, $"SutXlsx.Rows.Count={SutXlsx.Rows.Count()} > 0");
        }

        [TestMethod]
        public void ExcelFile_Sheet_Xlsx()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            var sheet = reader.ReadFile(SutXlsxFile).GetSheetAt(0);
            SutXlsx = sheet.ToSheetData(0, Path.GetFileName(SutXlsxFile));
            Assert.IsTrue(SutXlsx != null);
            Assert.IsTrue(SutXlsx.Rows.Count() > 0, $"SutXlsx.Rows.Count={SutXlsx.Rows.Count()} > 0");
        }

        [TestMethod]
        public void ExcelFile_Sheet_Xls()
        {
            Assert.IsTrue(File.Exists(SutXlsFile), $"{SutXlsFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            var sheet = reader.ReadFile(SutXlsFile).GetSheetAt(0);
            SutXls = sheet.ToSheetData(0, Path.GetFileName(SutXlsFile));
            Assert.IsTrue(SutXls != null);
            Assert.IsTrue(SutXls.Rows.Any(), $"SutXls.Rows.Any()={SutXls.Rows.Any()}");
        }
    }
}

