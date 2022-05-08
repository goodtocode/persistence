using GoodToCode.Persistence.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace GoodToCode.Persistence.Tests
{
    [TestClass]
    public class SheetData_Tests
    {
        private ILogger logger;
        public SheetData_Tests()
        {
            logger = LoggerFactory.CreateLogger<SheetData_Tests>();
        }

        [TestMethod]
        public void SheetData_Rows_ToDictionary()
        {
            try
            {
                foreach (var sheet in WorkbookDataFactory.CreateWorkbookData().Sheets)
                {
                    var rows = sheet.Rows.ToDictionary();
                    Assert.IsTrue(rows.Any());
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                Assert.Fail(ex.Message);
            }
        }
    }
}

