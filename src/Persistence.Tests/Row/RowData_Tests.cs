﻿using GoodToCode.Persistence.Blob.Csv;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GoodToCode.Persistence.Tests
{
    [TestClass]
    public class RowData_Tests
    {
        private readonly string executingPath;
        private string AssetsFolder { get { return @$"{executingPath}/Assets"; } }
        private string SutCsvFile { get { return @$"{AssetsFolder}/TestFile.csv"; } }        

        public RowData_Tests()
        {
            // Visual Studio vs. dotnet test execute different folders
            executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);                        
            executingPath = Directory.Exists(AssetsFolder) ? executingPath : Directory.GetCurrentDirectory();            
        }

        [TestMethod]
        public async Task RowData_ToDictionary()
        {
            Assert.IsTrue(File.Exists(SutCsvFile), $"{SutCsvFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
            // Input is stream
            var bytes = await FileFactoryService.GetInstance().ReadAllBytesAsync(SutCsvFile);
            var stream = new MemoryStream(bytes);
            // Service
            var csvService = new CsvService();
            var rows = csvService.GetRow(stream, 1);
            Assert.IsTrue(rows.Cells.Any());
            var rowDict = rows.ToDictionary();
            Assert.IsTrue(rowDict.Count > 0);
        }
    }
}

