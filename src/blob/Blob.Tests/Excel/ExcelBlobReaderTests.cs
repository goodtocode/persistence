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
        private readonly string sutCsvFile = @"Assets\TestFile.csv";
        private readonly string sutXlsFile = @"Assets\TestFile.xls";
        private readonly string sutXlsxFile = @"Assets\TestFile.xlsx";
        private readonly string sutCsvFullPath;
        private readonly string sutXlsFullPath;
        private readonly string sutXlsxFullPath;
        private ExcelBlobReader reader;
        public IWorkbook SutCsv { get; private set; }
        public IWorkbook SutXls { get; private set; }
        public IWorkbook SutXlsx { get; private set; }
        public Dictionary<string, StringValues> SutReturn { get; private set; }

        public ExcelBlobReaderTests()
        {
            reader = new ExcelBlobReader();
            var cwd = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            sutCsvFullPath = $@"{cwd}\{sutCsvFile}";
            sutXlsFullPath = $@"{cwd}\{sutXlsFile}";
            sutXlsxFullPath = $@"{cwd}\{sutXlsxFile}";
        }

        [Given(@"I have an XLSX file")]
        public void GivenIHaveAnXLSXFile()
        {
            Assert.IsTrue(File.Exists(sutXlsxFullPath));
        }

        [When(@"read XLSX in via ExcelBlobReader")]
        public void WhenReadXLSXInViaExcelBlobReader()
        {
            SutXlsx = reader.ReadFile(sutXlsxFullPath);
            Assert.IsTrue(SutXlsx.GetSheetAt(0) != null);
        }

        [Then(@"all readable XLSX data is available to systems")]
        public void ThenAllReadableXLSXDataIsAvailableToSystems()
        {
            
        }

        [Given(@"I have an XLS file")]
        public void GivenIHaveAnXLSFile()
        {
            Assert.IsTrue(File.Exists(sutXlsFullPath));
        }

        [When(@"read XLS in via ExcelBlobReader")]
        public void WhenReadXLSInViaExcelBlobReader()
        {
            SutXls = reader.ReadFile(sutXlsFullPath);
            Assert.IsTrue(SutXls.GetSheetAt(0) != null);
        }

        [Then(@"all readable XLS data is available to systems")]
        public void ThenAllReadableXLSDataIsAvailableToSystems()
        {
            
        }
    }
}

