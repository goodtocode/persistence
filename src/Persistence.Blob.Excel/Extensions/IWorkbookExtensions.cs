using GoodToCode.Persistence.Abstractions;
using NPOI.SS.UserModel;
using System.Collections.Generic;

namespace GoodToCode.Persistence.Blob.Excel
{
    public static class IWorkbookExtensions
    {
        public static IWorkbookData ToWorkbookData(this IWorkbook item, string workbookName)
        {
            var sheets = new List<ISheetData>();
            for (int count = 0; count < item.NumberOfSheets; count++)
            {
                var st = item.GetSheetAt(count);
                var sheet = st.ToSheetData(count, workbookName);
                sheets.Add(sheet);
            }
            return new WorkbookData(workbookName, sheets);
        }
    }
}
