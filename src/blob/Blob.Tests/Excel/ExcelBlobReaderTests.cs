using System.IO;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using NPOI.SS.UserModel;

namespace GoodToCode.Shared.Blob.Excel
{
    [Binding]
    public class ExcelBlobReaderTests
    {
        private readonly string sutCsvFile = @"Assets\TestFile.csv";
        private readonly string sutXlsFile = @"Assets\TestFile.xls";
        private readonly string sutXlsxFile = @"Assets\TestFile.xlsx";
        private ExcelBlobReader reader;
        public IWorkbook SutCsv { get; private set; }
        public IWorkbook SutXls { get; private set; }
        public IWorkbook SutXlsx { get; private set; }
        public Dictionary<string, StringValues> SutReturn { get; private set; }

        public ExcelBlobReaderTests()
        {
            reader = new ExcelBlobReader();
        }

        [Given(@"I have an XLSX file")]
        public void GivenIHaveAnXLSXFile()
        {
            Assert.IsTrue(File.Exists(sutXlsxFile));
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
            
        }

        [Given(@"I have an XLS file")]
        public void GivenIHaveAnXLSFile()
        {
            Assert.IsTrue(File.Exists(sutXlsFile));
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
            
        }

        [Given(@"I have an CSV file")]
        public void GivenIHaveAnCSVFile()
        {
            Assert.IsTrue(File.Exists(sutCsvFile));
        }

        [When(@"read CSV in via ExcelBlobReader")]
        public void WhenReadCSVInViaExcelBlobReader()
        {
            SutCsv = reader.ReadFile(sutCsvFile);
            Assert.IsTrue(SutCsv.GetSheetAt(0) != null);
        }

        [Then(@"all readable CSV data is available to systems")]
        public void ThenAllReadableCSVDataIsAvailableToSystems()
        {
            
        }
    }
}
