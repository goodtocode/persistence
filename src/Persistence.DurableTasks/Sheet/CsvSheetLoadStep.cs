using GoodToCode.Persistence.Abstractions;
using GoodToCode.Persistence.Blob.Csv;
using System.IO;

namespace GoodToCode.Persistence.DurableTasks
{
    public class CsvSheetLoadStep
    {
        private readonly ICsvService service;

        public CsvSheetLoadStep(ICsvService serviceCsv)
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
