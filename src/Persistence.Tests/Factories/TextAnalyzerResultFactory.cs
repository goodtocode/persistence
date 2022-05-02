using System.Collections.Generic;
using System.Linq;

namespace GoodToCode.Persistence.Tests
{
    public class TextAnalyzerResultFactory
    {
        public static IEnumerable<AnalyticsResult> CreateAnalyticsResults()
        {
            return new List<AnalyticsResult>() { new AnalyticsResult() { Category = "Event", Confidence = 1, SubCategory = "", AnalyzedText = "Admitted to hospital." } };
        }

        public static ICellData CreateCellData()
        {
            return new CellData() { CellValue = "I went to Seattle last week. While there, it was hot and I had heat exhaustion. But overall it was great!", ColumnIndex = 1, ColumnName = "Summary", RowIndex = 1, SheetIndex = 1, SheetName = "Trip Log", WorkbookName = "Trips.xlsx" };
        }

        public static IEnumerable<NamedEntity> CreateNamedEntities()
        {
            return new List<NamedEntity>() { CreateNamedEntity() };
        }

        public static NamedEntity CreateNamedEntity()
        {
            return new NamedEntity(CreateCellData(), CreateAnalyticsResults().FirstOrDefault());
        }
    }
}
