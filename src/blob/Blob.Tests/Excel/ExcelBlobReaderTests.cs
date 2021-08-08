using GoodToCode.Shared.Blob.Abstractions;
using GoodToCode.Shared.Blob.Excel;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TechTalk.SpecFlow;

namespace GoodToCode.Shared.Blob.Tests
{
    [Binding]
    public class ExcelBlobReaderTests
    {
        private readonly NpoiBlobReader reader;
        private readonly string executingPath;
        private string AssetsFolder { get { return @$"{executingPath}/Assets"; } }

        private string SutXlsFile { get { return @$"{AssetsFolder}/TestFile.xls"; } }
        private string SutXlsxFile { get { return @$"{AssetsFolder}/TestFile.xlsx"; } }        

        public ISheetData SutXls { get; private set; }
        public ISheetData SutXlsx { get; private set; }
        public Dictionary<string, StringValues> SutReturn { get; private set; }

        public ExcelBlobReaderTests()
        {
            reader = new NpoiBlobReader();
            // Visual Studio vs. dotnet test execute different folders
            executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);                        
            executingPath = Directory.Exists(AssetsFolder) ? executingPath : Directory.GetCurrentDirectory();
            executingPath = Directory.Exists(AssetsFolder) ? executingPath : $"{Directory.GetParent(executingPath)}/bin/Debug/net5.0";
        }

        [Given(@"I have an XLSX file")]
        public void GivenIHaveAnXLSXFile()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
        }

        [When(@"read XLSX in via ExcelBlobReader")]
        public void WhenReadXLSXInViaExcelBlobReader()
        {
            var sheet = reader.ReadFile(SutXlsxFile).GetSheetAt(0);
            var rows = new List<IRowData>();
            for (int count = sheet.FirstRowNum; count < sheet.LastRowNum; count++)
            {
                var row = sheet.GetRow(count);
                var cells = row.Cells.GetRange(0, row.Cells.Count - 1).Select(c => new CellData() { CellValue = c.StringCellValue, ColumnIndex = c.ColumnIndex, RowIndex = count, SheetKey = sheet.SheetName });
                rows.Add(new RowData(count, cells));
            }
            SutXlsx = new SheetData(sheet.SheetName, rows);
            Assert.IsTrue(SutXlsx != null);
        }

        [Then(@"all readable XLSX data is available to systems")]
        public void ThenAllReadableXLSXDataIsAvailableToSystems()
        {
            Assert.IsTrue(SutXlsx.Rows.Count() > 0, $"SutXlsx.Rows.Count={SutXlsx.Rows.Count()} > 0");
        }

        [Given(@"I have an XLS file")]
        public void GivenIHaveAnXLSFile()
        {
            Assert.IsTrue(File.Exists(SutXlsFile), $"{SutXlsFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
        }

        [When(@"read XLS in via ExcelBlobReader")]
        public void WhenReadXLSInViaExcelBlobReader()
        {
            var sheet = reader.ReadFile(SutXlsFile).GetSheetAt(0);
            var rows = new List<IRowData>();
            for (int count = sheet.FirstRowNum; count < sheet.LastRowNum; count++)
            {
                var row = sheet.GetRow(count);
                var cells = row.Cells.GetRange(0, row.Cells.Count - 1).Select(c => new CellData() { CellValue = c.StringCellValue, ColumnIndex = c.ColumnIndex, RowIndex = count, SheetKey = sheet.SheetName });
                rows.Add(new RowData(count, cells));
            }
            SutXls = new SheetData(sheet.SheetName, rows);
            Assert.IsTrue(SutXls != null);
        }

        [Then(@"all readable XLS data is available to systems")]
        public void ThenAllReadableXLSDataIsAvailableToSystems()
        {
            Assert.IsTrue(SutXls.Rows.Count() > 0, $"SutXlsx.Rows.Count={SutXls.Rows.Count()} > 0");
        }
    }
}

