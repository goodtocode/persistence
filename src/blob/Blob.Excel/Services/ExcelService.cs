using GoodToCode.Shared.Blob.Abstractions;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace GoodToCode.Shared.Blob.Excel
{
    public class ExcelService : IExcelService
    {
        public string WorkbookName { get; }

        public ExcelService(string workbookName)
        {
            WorkbookName = workbookName;
        }

        public IWorkbookData GetWorkbook(Stream fileStream)
        {
            var wb = WorkbookFactory.Create(fileStream);
            if (wb == null)
                throw new ArgumentOutOfRangeException("Sheet not found.");
            return wb.ToWorkbookData(WorkbookName); 
        }

        public ISheetData GetSheet(Stream fileStream, int sheetIndex)
        {
            var currSheet = WorkbookFactory.Create(fileStream).GetSheetAt(sheetIndex);
            if (currSheet == null)
                throw new ArgumentOutOfRangeException("Sheet not found.");
            return currSheet.ToSheetData(WorkbookName, sheetIndex);
        }

        public IEnumerable<ICellData> GetColumn(Stream fileStream, int sheet, int column)
        {
            ISheetData currSheet = GetSheet(fileStream, sheet);
            if (currSheet == null)
                throw new ArgumentOutOfRangeException("Sheet not found.");
            return currSheet.GetColumn(column);
        }

        public IRowData GetRow(Stream fileStream, int sheet, int row)
        {            
            ISheetData currSheet = GetSheet(fileStream, sheet);
            if (currSheet == null)
                throw new ArgumentOutOfRangeException("Sheet not found.");

            return currSheet.GetRow(row);
        }

        public ICellData GetCell(Stream fileStream, int sheet, int column, int row)
        {
            ISheetData currSheet = GetSheet(fileStream, sheet);
            if (currSheet == null)
                throw new ArgumentOutOfRangeException("Sheet not found.");
            
            return currSheet.GetCell(column, row);
        }
    }
}
