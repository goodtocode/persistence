using GoodToCode.Shared.Blob.Abstractions;
using NPOI.SS.UserModel;
using System.Collections.Generic;

namespace GoodToCode.Shared.Blob.Excel
{
    public static class IWorkbookExtensions
    {
        public static WorkbookData ToWorkbookData(this IWorkbook item, string workbookName)
        {
            var sheetMeta = new List<ISheetMetadata>();
            for (int count = 0; count < item.NumberOfSheets -1; count++)
            {
                var st = item.GetSheetAt(count);
                var sheet = st.ToSheetData(workbookName);
                sheetMeta.Add(sheet);
            }
            return new WorkbookData(workbookName, sheetMeta);
        }
    }
}
