using GoodToCode.Persistence.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace GoodToCode.Persistence.Tests
{
    [TestClass]
    public class SheetDataTests
    {
        private ILogger logger;
        public SheetDataTests()
        {
            logger = LoggerFactory.CreateLogger<SheetDataTests>();
        }

        [TestMethod]
        public void SheetData_Rows_ToDictionary()
        {
            try
            {
                foreach (var sheet in WorkbookFactory.CreateWorkbookData().Sheets)
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

