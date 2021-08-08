using GoodToCode.Shared.Blob.Excel;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;

namespace GoodToCode.Shared.Blob.Tests
{
    [Binding]
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

        [Given(@"I have an XLSX file and Npoi")]
        public void GivenIHaveAnXLSXFileAndNpoi()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
        }

        [When(@"read XLSX in via NpoiBlobReader")]
        public void WhenReadXLSXInViaNpoiBlobReader()
        {
            SutXlsx = reader.ReadFile(SutXlsxFile);
            Assert.IsTrue(SutXlsx.GetSheetAt(0) != null);
        }

        [Then(@"all Npoi readable XLSX data is available to systems")]
        public void ThenAllNpoiReadableXLSXDataIsAvailableToSystems()
        {
            Assert.IsTrue(SutXlsx.NumberOfSheets > 0, $"SutXlsx.NumberOfSheets={SutXlsx.NumberOfSheets} > 0");
        }

        [Given(@"I have an XLS file and Npoi")]
        public void GivenIHaveAnXLSFileAndNpoi()
        {
            Assert.IsTrue(File.Exists(SutXlsFile), $"{SutXlsFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
        }

        [When(@"read XLS in via NpoiBlobReader")]
        public void WhenReadXLSInViaNpoiBlobReader()
        {
            SutXls = reader.ReadFile(SutXlsFile);
            Assert.IsTrue(SutXls.GetSheetAt(0) != null);
        }

        [Then(@"all Npoi readable XLS data is available to systems")]
        public void ThenAllNpoiReadableXLSDataIsAvailableToSystems()
        {
            Assert.IsTrue(SutXls.NumberOfSheets > 0, $"SutXls.NumberOfSheets={SutXls.NumberOfSheets} > 0");
        }
    }
}

