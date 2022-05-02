using GoodToCode.Persistence.Abstractions;
using System.Collections.Generic;

namespace GoodToCode.Persistence.Tests
{
    public class WorkbookFactory
    {
        public static WorkbookData CreateWorkbookData()
        {
            var sheets = new List<SheetData>() { SheetFactory.CreateSheetData() };
            return new WorkbookData("book1.xls", sheets);
        }
    }
}
