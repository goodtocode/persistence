using GoodToCode.Persistence.Abstractions;
using GoodToCode.Persistence.Blob.Csv;
using System.IO;

namespace GoodToCode.Persistence.DurableTasks
{
    public class CsvSheetLoadActivity
    {
        private readonly ICsvService service;

        public CsvSheetLoadActivity(ICsvService serviceCsv)
        {
            service = serviceCsv;
        }

        public ISheetData Execute(Stream CsvStream)
        {
            var sheet = service.GetSheet(CsvStream);
            return sheet;
        }
    }
}
