using GoodToCode.Persistence.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;

namespace GoodToCode.Shared.Blob.Csv
{
    public class CsvService : ICsvService
    {
        public ISheetData GetSheet(Stream fileStream)
        {
            var sheet = new CsvBlobReader().ReadFile(fileStream);
            return sheet;
        }

        public IEnumerable<ICellData> GetColumn(Stream fileStream, int column)
        {
            return GetSheet(fileStream).GetColumn(column);
        }

        public IRowData GetRow(Stream fileStream, int row)
        {
            ISheetData currSheet = GetSheet(fileStream);
            if (currSheet == null)
                throw new ArgumentOutOfRangeException("Sheet not found.");
            return currSheet.GetRow(row);
        }

        public ICellData GetCell(Stream fileStream, int row, int cell)
        {
            ISheetData currSheet = GetSheet(fileStream);
            if (currSheet == null)
                throw new ArgumentOutOfRangeException("Sheet not found.");
            return currSheet.GetCell(cell, row);
        }
    }
}
