using GoodToCode.Persistence.Abstractions;
using System;
using System.Collections.Generic;

namespace GoodToCode.Persistence.Tests
{
    public class RowDataFactory
    {
        public static RowData CreateRowData()
        {
            var cells = new List<CellData>() { CellDataFactory.CreateCellData() };
            return new RowData(0, cells);
        }
    }
}
