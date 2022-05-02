using GoodToCode.Persistence.Abstractions;
using System.Collections.Generic;

namespace GoodToCode.Persistence.Tests
{
    public class SheetFactory
    {
        public static SheetData CreateSheetData()
        {
            var cells = new List<CellData>() { CellFactory.CreateCellData() };
            var row = new List<RowData>() { new RowData(1, cells) };
            return new SheetData(0, "Sheet1", row, cells);
        }
    }
}
