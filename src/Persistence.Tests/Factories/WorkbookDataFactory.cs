using GoodToCode.Persistence.Abstractions;
using System.Collections.Generic;

namespace GoodToCode.Persistence.Tests
{
    public class WorkbookDataFactory
    {
        public static WorkbookData CreateWorkbookData()
        {
            var sheets = new List<SheetData>() { SheetDataFactory.CreateSheetData() };
            return new WorkbookData("book1.xls", sheets);
        }
    }
}
