using GoodToCode.Persistence.Abstractions;
using GoodToCode.Persistence.Blob.Excel;
using System.Collections.Generic;
using System.IO;

namespace GoodToCode.Persistence.DurableTasks
{
    public class ExcelWorkbookLoadActivity
    {
        private readonly IExcelService service;

        public ExcelWorkbookLoadActivity(IExcelService serviceExcel)
        {
            service = serviceExcel;
        }

        public IEnumerable<ISheetData> Execute(Stream excelStream, string documentName)
        {
            var wb = service.GetWorkbook(excelStream, documentName);
            return wb.Sheets;
        }
    }
}
