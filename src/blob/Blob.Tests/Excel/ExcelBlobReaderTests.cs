using System.IO;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using NPOI.SS.UserModel;
using System.Reflection;

namespace GoodToCode.Shared.Blob.Excel
{
    [Binding]
    public class ExcelBlobReaderTests
    {
        private ExcelBlobReader reader;
        private readonly string executingPath;
        private string assetsFolder { get { return @$"{executingPath}/Assets"; } }
        private string sutCsvFile { get { return @$"{assetsFolder}/TestFile.csv"; } }
        private string sutXlsFile { get { return @$"{assetsFolder}/TestFile.xls"; } }
        private string sutXlsxFile { get { return @$"{assetsFolder}/TestFile.xlsx"; } }        

        public IWorkbook SutCsv { get; private set; }
        public IWorkbook SutXls { get; private set; }
        public IWorkbook SutXlsx { get; private set; }
        public Dictionary<string, StringValues> SutReturn { get; private set; }

        public ExcelBlobReaderTests()
        {
            reader = new ExcelBlobReader();
            executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);            
            executingPath = Directory.Exists(assetsFolder) ? "../.." : executingPath; // Visual Studio vs. dotnet test execute different folders            
        }

        [Given(@"I have an XLSX file")]
        public void GivenIHaveAnXLSXFile()
        {
            Assert.IsTrue(File.Exists(sutXlsxFile), $"{sutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
        }

        [When(@"read XLSX in via ExcelBlobReader")]
        public void WhenReadXLSXInViaExcelBlobReader()
        {
            SutXlsx = reader.ReadFile(sutXlsxFile);
            Assert.IsTrue(SutXlsx.GetSheetAt(0) != null);
        }

        [Then(@"all readable XLSX data is available to systems")]
        public void ThenAllReadableXLSXDataIsAvailableToSystems()
        {
            Assert.IsTrue(SutXlsx.NumberOfSheets > 0, $"SutXlsx.NumberOfSheets={SutXlsx.NumberOfSheets} > 0");
        }

        [Given(@"I have an XLS file")]
        public void GivenIHaveAnXLSFile()
        {
            Assert.IsTrue(File.Exists(sutXlsFile), $"{sutXlsFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
        }

        [When(@"read XLS in via ExcelBlobReader")]
        public void WhenReadXLSInViaExcelBlobReader()
        {
            SutXls = reader.ReadFile(sutXlsFile);
            Assert.IsTrue(SutXls.GetSheetAt(0) != null);
        }

        [Then(@"all readable XLS data is available to systems")]
        public void ThenAllReadableXLSDataIsAvailableToSystems()
        {
            Assert.IsTrue(SutXls.NumberOfSheets > 0, $"SutXls.NumberOfSheets={SutXls.NumberOfSheets} > 0");
        }
    }
}

