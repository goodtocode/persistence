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

        [TestMethod]
        public void ExcelBlob_Xlsx()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
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
            Assert.IsTrue(SutXlsx.Rows.Count() > 0, $"SutXlsx.Rows.Count={SutXlsx.Rows.Count()} > 0");
        }

        [TestMethod]
        public void ExcelBlob_Xls()
        {
            Assert.IsTrue(File.Exists(SutXlsFile), $"{SutXlsFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
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
            Assert.IsTrue(SutXls.Rows.Count() > 0, $"SutXlsx.Rows.Count={SutXls.Rows.Count()} > 0");
        }
    }
}

