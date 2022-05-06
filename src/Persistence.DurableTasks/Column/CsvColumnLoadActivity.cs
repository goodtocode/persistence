using GoodToCode.Persistence.Abstractions;
using GoodToCode.Persistence.Blob.Csv;
using System.Collections.Generic;
using System.IO;

namespace GoodToCode.Persistence.DurableTasks
{
    public class CsvColumnLoadActivity
    {
        private readonly ICsvService service;

        public CsvColumnLoadActivity(ICsvService serviceCsv)
        {
            service = serviceCsv;
        }

        public  IEnumerable<ICellData> Execute(Stream CsvStream, int columnToAnalyze)
        {
            return service.GetColumn(CsvStream, columnToAnalyze);
        }
    }
}
