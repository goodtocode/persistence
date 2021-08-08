using GoodToCode.Shared.Blob.Excel;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace GoodToCode.Shared.Blob.Tests
{
    [TestClass]
    public class NpoiBlobReaderTests
    {
        private readonly NpoiBlobReader reader;
        private readonly string executingPath;
        private string AssetsFolder { get { return @$"{executingPath}/Assets"; } }

        private string SutXlsFile { get { return @$"{AssetsFolder}/TestFile.xls"; } }
        private string SutXlsxFile { get { return @$"{AssetsFolder}/TestFile.xlsx"; } }        

        public IWorkbook SutCsv { get; private set; }
        public IWorkbook SutXls { get; private set; }
        public IWorkbook SutXlsx { get; private set; }
        public Dictionary<string, StringValues> SutReturn { get; private set; }

        public NpoiBlobReaderTests()
        {
            reader = new NpoiBlobReader();
            // Visual Studio vs. dotnet test execute different folders
            executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);                        
            executingPath = Directory.Exists(AssetsFolder) ? executingPath : Directory.GetCurrentDirectory();
            executingPath = Directory.Exists(AssetsFolder) ? executingPath : $"{Directory.GetParent(executingPath)}/bin/Debug/net5.0";
        }

        [TestMethod]
        public void NpoiBlob_Xlsx()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            SutXlsx = reader.ReadFile(SutXlsxFile);
            Assert.IsTrue(SutXlsx.GetSheetAt(0) != null);
            Assert.IsTrue(SutXlsx.NumberOfSheets > 0, $"SutXlsx.NumberOfSheets={SutXlsx.NumberOfSheets} > 0");
        }

        [TestMethod]
        public void NpoiBlob_Xls()
        {
            Assert.IsTrue(File.Exists(SutXlsFile), $"{SutXlsFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            SutXls = reader.ReadFile(SutXlsFile);
            Assert.IsTrue(SutXls.GetSheetAt(0) != null);
            Assert.IsTrue(SutXls.NumberOfSheets > 0, $"SutXls.NumberOfSheets={SutXls.NumberOfSheets} > 0");
        }
    }
}

