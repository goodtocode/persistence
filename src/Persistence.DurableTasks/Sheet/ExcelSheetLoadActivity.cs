using GoodToCode.Persistence.Abstractions;
using GoodToCode.Persistence.Blob.Excel;
using System.IO;

namespace GoodToCode.Persistence.DurableTasks
{
    public class ExcelSheetLoadActivity
    {
        private readonly IExcelService service;

        public ExcelSheetLoadActivity(IExcelService serviceExcel)
        {
            service = serviceExcel;
        }

        public ISheetData Execute(Stream excelStream, int sheetToAnalyze)
        {
            var sheet = service.GetSheet(excelStream, sheetToAnalyze);
            return sheet;
        }
    }
}
