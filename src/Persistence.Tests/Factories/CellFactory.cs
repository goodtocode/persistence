using GoodToCode.Persistence.Abstractions;
using System;

namespace GoodToCode.Persistence.Tests
{
    public class CellFactory
    {
        public static CellData CreateCellData()
        {
            return new CellData()
            {
                CellValue = "This is the Cell Value column.",
                ColumnIndex = 1,
                ColumnName = "Column1",
                RowIndex = 1,
                SheetIndex = 1,
                SheetName = "Sheet1",
                WorkbookName = "book.xlsx"
            };
        }
    }
}
