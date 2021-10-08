using GoodToCode.Shared.Blob.Abstractions;
using NPOI.SS.UserModel;
using System.Collections.Generic;

namespace GoodToCode.Shared.Blob.Excel
{
    public static class IWorkbookExtensions
    {
        public static IWorkbookData ToWorkbookData(this IWorkbook item, string workbookName)
        {
            var sheets = new List<ISheetMetadata>();
            for (int count = 0; count < item.NumberOfSheets -1; count++)
            {
                var st = item.GetSheetAt(count);
                var sheet = st.ToSheetData(count, workbookName);
                sheets.Add(sheet);
            }
            return new WorkbookData(workbookName, sheets);
        }
    }
}
