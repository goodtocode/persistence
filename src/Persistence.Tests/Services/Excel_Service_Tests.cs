﻿using GoodToCode.Persistence.Blob.Excel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GoodToCode.Persistence.Tests
{
    [TestClass]
    public class Excel_Service_Tests
    {
        private readonly string executingPath;
        private string AssetsFolder { get { return @$"{executingPath}/Assets"; } }

        private string SutOpinionFile { get { return @$"{AssetsFolder}/OpinionFile.xlsx"; } }
        private string SutXlsxFile { get { return @$"{AssetsFolder}/TestFile.xlsx"; } }        

        public Excel_Service_Tests()
        {
            // Visual Studio vs. dotnet test execute different folders
            executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);                        
            executingPath = Directory.Exists(AssetsFolder) ? executingPath : Directory.GetCurrentDirectory();
            
        }

        [TestMethod]
        public async Task ExcelService_GetWorkbook()
        {
            Assert.IsTrue(File.Exists(SutOpinionFile), $"{SutOpinionFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            // Input is stream
            var bytes = await FileFactoryService.GetInstance().ReadAllBytesAsync(SutOpinionFile);
            var stream = new MemoryStream(bytes);
            // Service
            var excelService = new ExcelService();
            var wb = excelService.GetWorkbook(stream);
            Assert.IsTrue(wb.Sheets.Any());
        }

        [TestMethod]
        public async Task ExcelService_GetSheet()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            // Input is stream
            var bytes = await FileFactoryService.GetInstance().ReadAllBytesAsync(SutXlsxFile);
            var stream = new MemoryStream(bytes);
            // Service
            var excelService = new ExcelService();
            var sheet = excelService.GetSheet(stream, 0);
            Assert.IsTrue(sheet.Rows.Any());
        }

        [TestMethod]
        public async Task ExcelService_GetRow()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            // Input is stream
            var bytes = await FileFactoryService.GetInstance().ReadAllBytesAsync(SutXlsxFile);
            var stream = new MemoryStream(bytes);
            // Service
            var excelService = new ExcelService();
            var rows = excelService.GetRow(stream, 0, 1);
            Assert.IsTrue(rows.Cells.Any());
        }

        [TestMethod]
        public async Task ExcelService_GetCell()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            // Input is stream
            var bytes = await FileFactoryService.GetInstance().ReadAllBytesAsync(SutXlsxFile);
            var stream = new MemoryStream(bytes);
            // Service
            var excelService = new ExcelService();
            var cell = excelService.GetCell(stream, 0, 1, 1);
            Assert.IsTrue(cell.ToString().Length > 0);
        }
        [TestMethod]
        public async Task ExcelService_Workbook()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            // Input is stream
            var bytes = await FileFactoryService.GetInstance().ReadAllBytesAsync(SutXlsxFile);
            var stream = new MemoryStream(bytes);
            // Service
            var excelService = new ExcelService();
            var workbook = excelService.GetWorkbook(stream);
            Assert.IsTrue(workbook.Sheets.Any());
            var itemWithData = workbook.Sheets.Where(s => s.Rows.Where(x => x.RowIndex > 0).Any());
            Assert.IsTrue(itemWithData.Any());
            // ToDictionary for unrolling into a row for persistence
            var dict = workbook.ToDictionary();
            Assert.IsTrue(dict.Any());
            Assert.IsTrue(dict.FirstOrDefault().Any());
            Assert.IsTrue(dict.FirstOrDefault().FirstOrDefault().Any());
            Assert.IsTrue(!string.IsNullOrWhiteSpace(dict.FirstOrDefault().FirstOrDefault().FirstOrDefault().Key));
            Assert.IsTrue(dict.FirstOrDefault().FirstOrDefault().FirstOrDefault().Value != null);
        }

        [TestMethod]
        public async Task ExcelService_Sheets()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            // Input is stream
            var bytes = await FileFactoryService.GetInstance().ReadAllBytesAsync(SutXlsxFile);
            var stream = new MemoryStream(bytes);
            // Service
            var excelService = new ExcelService();
            var sheet = excelService.GetSheet(stream, 0);
            Assert.IsTrue(sheet.Rows.Any());
            var itemWithData = sheet.Rows.Where(x => x.RowIndex > 0);
            Assert.IsTrue(itemWithData.Any());
            // ToDictionary for unrolling into a row for persistence
            var dict = sheet.ToDictionary();
            Assert.IsTrue(dict.Any());
            Assert.IsTrue(dict.FirstOrDefault().Any());
            Assert.IsTrue(!string.IsNullOrWhiteSpace(dict.FirstOrDefault().FirstOrDefault().Key));
            Assert.IsTrue(dict.FirstOrDefault().FirstOrDefault().Value != null);
        }

        [TestMethod]
        public async Task ExcelService_Rows()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            // Input is stream
            var bytes = await FileFactoryService.GetInstance().ReadAllBytesAsync(SutXlsxFile);
            var stream = new MemoryStream(bytes);
            // Service
            var excelService = new ExcelService();
            var row = excelService.GetRow(stream, 0, 2);
            Assert.IsTrue(row.Cells.Any());
            var itemWithData = row.Cells.Where(x => !string.IsNullOrWhiteSpace(x.CellValue));
            Assert.IsTrue(itemWithData.Any());
            // ToDictionary for unrolling into a row for persistence
            var dict = row.ToDictionary();
            Assert.IsTrue(dict.Any());
            Assert.IsTrue(dict.Any());
            Assert.IsTrue(!string.IsNullOrWhiteSpace(dict.FirstOrDefault().Key));
            Assert.IsTrue(dict.FirstOrDefault().Value != null);
        }

        [TestMethod]
        public async Task ExcelService_Cells()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            // Input is stream
            var bytes = await FileFactoryService.GetInstance().ReadAllBytesAsync(SutXlsxFile);
            var stream = new MemoryStream(bytes);
            // Service
            var excelService = new ExcelService();
            var cell = excelService.GetCell(stream, 0, 2, 2);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(cell.CellValue));
        }
    }
}

