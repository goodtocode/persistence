using GoodToCode.Persistence.Abstractions;
using System.Collections.Generic;

namespace GoodToCode.Persistence.Tests
{
    public class SheetDataFactory
    {
        public static SheetData CreateSheetData()
        {
            var cells = new List<CellData>() { CellDataFactory.CreateCellData() };
            var row = new List<RowData>() { new RowData(1, cells) };
            return new SheetData(0, "Sheet1", row);
        }
    }
}
