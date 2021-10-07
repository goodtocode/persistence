using NPOI.SS.UserModel;
using GoodToCode.Shared.Blob.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;

namespace GoodToCode.Shared.Blob.Excel
{
    public class ExcelService : IExcelService
    {
        private string defaultWorkbook = "book1.xlsx";

        public IWorkbookData GetWorkbook(Stream fileStream)
        {
            return GetWorkbook(fileStream, defaultWorkbook);
        }

        public IWorkbookData GetWorkbook(Stream fileStream, string name)
        {
            return WorkbookFactory.Create(fileStream).ToWorkbookData(name);
        }

        public ISheetData GetSheet(Stream fileStream, int sheet)
        {
            IWorkbook currWorkbook = WorkbookFactory.Create(fileStream);
            return currWorkbook.GetSheetAt(sheet).ToSheetData();
        }

        public IEnumerable<ICellData> GetColumn(Stream fileStream, int sheet, int column)
        {
            return GetSheet(fileStream, sheet).GetColumn(column);
        }

        public IRowData GetRow(Stream fileStream, int sheet, int row)
        {
            ISheetData currSheet = GetSheet(fileStream, sheet);
            if (currSheet == null)
                throw new ArgumentOutOfRangeException("Sheet not found.");
            return currSheet.GetRow(row);
        }

        public ICellData GetCell(Stream fileStream, int sheet, int row, int cell)
        {
            ISheetData currSheet = GetSheet(fileStream, sheet);
            if (currSheet == null)
                throw new ArgumentOutOfRangeException("Sheet not found.");
            return currSheet.GetCell(cell, row);
        }
    }
}
