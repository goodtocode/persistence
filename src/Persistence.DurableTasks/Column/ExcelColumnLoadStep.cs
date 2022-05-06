using GoodToCode.Persistence.Abstractions;
using GoodToCode.Persistence.Blob.Excel;
using System.Collections.Generic;
using System.IO;

namespace GoodToCode.Persistence.DurableTasks
{
    public class ExcelColumnLoadStep
    {
        private readonly IExcelService service;

        public ExcelColumnLoadStep(IExcelService serviceExcel)
        {
            service = serviceExcel;
        }

        public  IEnumerable<ICellData> Execute(Stream excelStream, int sheetToAnalyze, int columnToAnalyze)
        {
            return service.GetColumn(excelStream, sheetToAnalyze, columnToAnalyze);
        }
    }
}
