﻿using GoodToCode.Persistence.DurableTasks;
using GoodToCode.Persistence.Abstractions;
using GoodToCode.Persistence.Blob.Excel;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GoodToCode.Persistence.Tests
{
    [TestClass]
    public class Excel_Sheet_Tests
    {
        private readonly ILogger<Excel_Sheet_Tests> logItem;
        private static string SutXlsxFile { get { return @$"{PathFactory.GetProjectSubfolder("Assets")}/OpinionFile.xlsx"; } }
        public CellEntity SutRow { get; private set; }
        public IEnumerable<CellEntity> SutRows { get; private set; }
        public Dictionary<string, StringValues> SutReturn { get; private set; }


        public Excel_Sheet_Tests()
        {
            logItem = LoggerFactory.CreateLogger<Excel_Sheet_Tests>();
        }

        [TestMethod]
        public async Task Excel_Sheet_Load_Step()       
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");

            try
            { 
                var bytes = await FileFactoryService.GetInstance().ReadAllBytesAsync(SutXlsxFile);
                Stream itemToAnalyze = new MemoryStream(bytes);
                var workflow = new  ExcelSheetLoadStep(new ExcelService());
                var results = workflow.Execute(itemToAnalyze, 0);
                Assert.IsTrue(results.Rows.Any(), "No results from Excel service.");
            }
            catch (Exception ex)
            {
                logItem.LogError(ex.Message, ex);
                Assert.Fail(ex.Message);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
        }
    }
}

