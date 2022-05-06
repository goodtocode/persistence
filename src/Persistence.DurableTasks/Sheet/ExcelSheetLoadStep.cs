using GoodToCode.Persistence.Abstractions;
using GoodToCode.Persistence.Blob.Excel;
using System.IO;

namespace GoodToCode.Persistence.DurableTasks
{
    public class ExcelSheetLoadStep
    {
        private readonly IExcelService service;

        public ExcelSheetLoadStep(IExcelService serviceExcel)
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
