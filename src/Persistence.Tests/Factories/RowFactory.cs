using GoodToCode.Persistence.Abstractions;
using System;
using System.Collections.Generic;

namespace GoodToCode.Persistence.Tests
{
    public class RowFactory
    {
        public static RowData CreateRowData()
        {
            var cells = new List<CellData>() { CellFactory.CreateCellData() };
            return new RowData(0, cells);
        }

        public static RowEntity CreateRowEntity()
        {
            var cells = new List<CellEntity>() { CellFactory.CreateCellEntity() };
            return new RowEntity(Guid.NewGuid().ToString(), cells);
        }
    }
}
