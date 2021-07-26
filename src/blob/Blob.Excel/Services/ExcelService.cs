using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace GoodToCode.Shared.Blob.Excel
{
    public class ExcelService : IExcelService
    {
        public IWorkbook GetWorkbook(Stream fileStream)
        {
            return WorkbookFactory.Create(fileStream);
        }

        public ISheet GetSheet(Stream fileStream, int sheet)
        {
            IWorkbook currWorkbook = WorkbookFactory.Create(fileStream);
            return currWorkbook.GetSheetAt(sheet);
        }
        public IEnumerable<ICell> GetColumn(Stream fileStream, int sheet, int column)
        {
            ISheet currSheet = GetSheet(fileStream, sheet);

            foreach (IRow rowItem in currSheet)
                yield return rowItem.GetCell(column);
        }

        public IRow GetRow(Stream fileStream, int sheet, int row)
        {
            ISheet currSheet = GetSheet(fileStream, sheet);
            if (currSheet == null)
                throw new ArgumentOutOfRangeException("Sheet not found.");
            return currSheet.GetRow(row);
        }

        public ICell GetCell(Stream fileStream, int sheet, int row, int cell)
        {
            ISheet currSheet = GetSheet(fileStream, sheet);
            if (currSheet == null)
                throw new ArgumentOutOfRangeException("Sheet not found.");
            return currSheet.GetRow(row).GetCell(cell);
        }
    }
}
